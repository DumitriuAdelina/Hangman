using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace WindowsFormsApplication6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private string word = string.Empty;
        List<Label> litere = new List<Label>();
        private int nr;
        enum BodyParts
        {
            head,
            left_eye,
            right_eye,
            mouth,
            body,
            right_arm,
            left_arm,
            right_leg,
            left_leg
        }
        void DeseneazaCorp(BodyParts bp)
        {
            Graphics g = panel1.CreateGraphics();
            Pen p = new Pen(Color.Black, 2);
            if(bp==BodyParts.head)
            {
                g.DrawEllipse(p, 40, 50, 40, 40);
            }
            else if(bp==BodyParts.left_eye)
            {
                SolidBrush s = new SolidBrush(Color.Black);
                g.FillEllipse(s, 50, 60, 5, 5);
            }
            else if (bp == BodyParts.right_eye)
            {
                SolidBrush s = new SolidBrush(Color.Black);
                g.FillEllipse(s, 63, 60, 5, 5);
            }
            else if (bp == BodyParts.mouth) g.DrawArc(p, 50, 60, 20, 20, 45, 90);
            else if (bp == BodyParts.body) g.DrawLine(p, new Point(60, 90), new Point(60, 170));
            else if (bp == BodyParts.left_arm) g.DrawLine(p, new Point(60, 100), new Point(30, 85));
            else if (bp == BodyParts.right_arm) g.DrawLine(p, new Point(60, 100), new Point(90, 85));
            else if (bp == BodyParts.left_leg) g.DrawLine(p, new Point(60, 170), new Point(30, 190));
            else if (bp == BodyParts.right_leg) g.DrawLine(p, new Point(60, 170), new Point(90, 190));

        }
        void DrawHangPost()
        {
            Graphics g = panel1.CreateGraphics();
            Pen p = new Pen(Color.Brown, 10);
            g.DrawLine(p, new Point(130, 218), new Point(130, 5));
            g.DrawLine(p, new Point(135, 5), new Point(65, 5));
            g.DrawLine(p, new Point(60, 0), new Point(60, 50));
            
        }

        void ResetGame()
        {
            Graphics g = panel1.CreateGraphics();
            g.Clear(panel1.BackColor);
            GetRandomWord();
            MakeLabels();
            DrawHangPost();
            label2.Text = "Litere ratate : ";
            textBox1.Text = "";
        }
        void MakeLabels()
        {
            
            word = GetRandomWord();
            char[] chars = word.ToCharArray();
            int between = 330 / chars.Length - 1;
            for(int i=0;i<chars.Length-1;i++)
            {
                litere.Add(new Label());
                litere[i].Location=new Point((i*between)+10,80);
                litere[i].Text = "_";
                litere[i].Parent = groupBox1;
                litere[i].BringToFront();
                litere[i].CreateControl();
            }
            label1.Text = "Lung cuv : " + (chars.Length - 1).ToString();

        }
        string GetRandomWord()
        {
            WebClient wc = new WebClient();
            string wordList = wc.DownloadString("http://www.puzzlers.org/pub/wordlists/ospd.txt");
            string[] words = wordList.Split('\n');
            Random ran = new Random();
            return words[ran.Next(0, words.Length - 1)];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            char letter = textBox1.Text.ToLower().ToCharArray()[0];
            if(!char.IsLetter(letter))
            {
                MessageBox.Show("Introduceți doar litere!!");
                return;
            }
            if(word.Contains(letter))
            {
                char[] letters = word.ToCharArray();
                for(int i=0;i<letters.Length;i++)
                {
                    if (letters[i] == letter) litere[i].Text = letter.ToString();
                    textBox1.Text = "";
                }
                foreach(Label l in litere)
                    if (l.Text == "_") return;
                MessageBox.Show("Bravo! Ai câștigat!");
                ResetGame();
            
            }
            else
            {
                label2.Text += " " + letter.ToString() + ", ";
                DeseneazaCorp((BodyParts)nr);
                nr++;
                if(nr==9)
                {
                    MessageBox.Show("Ai pierdut. Cuvântul era : " + word);
                    ResetGame();
                }
            }
            textBox1.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox2.Text==word)
            {
                MessageBox.Show("Bravo! Ai câștigat!");
                ResetGame();
            }
            else
            {
                DeseneazaCorp((BodyParts)nr);
                nr++;
                if(nr==9)
                {
                    MessageBox.Show("Ai pierdut. Cuvântul era : " + word);
                    ResetGame();
                }
            }
            textBox2.Text = "";
         }

        private void Form1_Shown(object sender, EventArgs e)
        {
            DrawHangPost();
            MakeLabels();
        }
        
    }
}
