using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeamProject2
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private struct Card
        {
            public string gen;
            public int point, den, w, r, g, b;
            public bool picked;
        }

        static int numCard;
        static string[] s = new string[3];

        public int NumReCard
        {
            get { return numCard; }
            set { numCard = value; }
        }

        public string Text1
        {
            get { return s[0]; }
            set { s[0] = value; }
        }

        public string Text2
        {
            get { return s[1]; }
            set { s[1] = value; }
        }

        public string Text3
        {
            get { return s[2]; }
            set { s[2] = value; }
        }

        Form2 f2 = new Form2();


        private void Form4_Load(object sender, EventArgs e)
        {
            for(int i=0;i<numCard;i++)
            {
                Button btn = new Button()
                {
                    Name = i.ToString(),
                    Size = new Size(141, 250),
                    Text = s[i]
                };
                btn.Click += Btn_Click;

                flowLayoutPanel1.Controls.Add(btn);
            }
            flowLayoutPanel2.Location = new Point(flowLayoutPanel1.Location.X,
                flowLayoutPanel1.Location.Y + flowLayoutPanel1.Height + 25);
        }

        private void Btn_Click(object? sender, EventArgs e)
        {
            Button btn = sender as Button;
            f2.ReseverCardNum = Convert.ToInt32(btn.Name);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ///Vo endturn
            f2.ReserveCardAction = true;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            f2.ReserveCardAction = false;
        }
    }
}
