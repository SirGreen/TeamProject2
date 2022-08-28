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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        static int currentturn;

        static int l = 0;
        public int LeftOver
        {
            get { return l; }
            set { l = value; }
        }

        public int Turn
        {
            get { return currentturn; }
            set { currentturn = value; }
        }

        static int[] Token = new int[6];

        public int blackToken
        {
            get { return Token[0]; }
            set { Token[0] = value; }
        }

        public int whiteToken
        {
            get { return Token[1]; }
            set { Token[1] = value; }
        }

        public int redToken
        {
            get { return Token[2]; }
            set { Token[2] = value; }
        }

        public int blueToken
        {
            get { return Token[3]; }
            set { Token[3] = value; }
        }

        public int greenToken
        {
            get { return Token[4]; }
            set { Token[4] = value; }
        }

        public int GoldToken
        {
            get { return Token[5]; }
            set { Token[5] = value; }
        }

        private string ShowTokenLeft(int x)
        {
            switch(x)
            {
                case 0: return "Black: " + Token[x];
                case 1: return "White: " + Token[x];
                case 2: return "Red: " + Token[x];
                case 3: return "Blue: " + Token[x];
                case 4: return "Green: " + Token[x];
                default: return "Gold: " + Token[x];
            }
        }

        NumericUpDown[] nm = new NumericUpDown[6];
        Label[] lb = new Label[6];
        static int[] left = new int[6]; 

        private void Form3_Load(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            string s;
            if (l > 1) s = " tokens!";
            else s = " token!";
            label7.Text = "Please take out " + l + s;

            lb[0] = label1;
            lb[1] = label2;
            lb[2] = label3;
            lb[3] = label4;
            lb[4] = label5;
            lb[5] = label6;

            
            nm[0] = numericUpDown2;
            nm[1] = numericUpDown3;
            nm[2] = numericUpDown4;
            nm[3] = numericUpDown5;
            nm[4] = numericUpDown6;
            nm[5] = numericUpDown7;

            for (int i=0;i<6;i++)
            {
                lb[i].Text = ShowTokenLeft(i);
                if (Token[i] == 0) nm[i].Enabled = false;
                left[i]=0;
                int k = Math.Min(Token[i], l);
                nm[i].Maximum = k;
            }


        }

        private void ChangeMax(int x)
        {
            for (int i = 0; i < 6; i++)
            {
                int v = Convert.ToInt32(nm[i].Value);
                int k = Math.Min(Token[i] + v, l + v);
                nm[i].Maximum = k;
            }
        }

        private void Solve(int i)
        {
            int v = Convert.ToInt32(nm[i].Value);

            if (v > left[i])
            {
                Token[i] -= v - left[i];
                l -= v - left[i];
            }
            else
            {
                Token[i] += left[i] - v;
                l += left[i] - v;
            }
            lb[i].Text = ShowTokenLeft(i);
            left[i] = v;
            ChangeMax(i);

            string s;
            if (l > 1) s = " tokens!";
            else s = " token!";
            label7.Text = "Please take out " + l + s;

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            Solve(0);
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            Solve(1);
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            Solve(2);
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            Solve(3);
        }

        private void numericUpDown6_ValueChanged(object sender, EventArgs e)
        {
            Solve(4);
        }

        private void numericUpDown7_ValueChanged(object sender, EventArgs e)
        {
            Solve(5);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();

            if (l > 0)
            {
                MessageBox.Show(label7.Text);
                return;
            }

            f2.blackTokenBonus = Convert.ToInt32(nm[0].Value);
            f2.whiteTokenBonus = Convert.ToInt32(nm[1].Value);
            f2.redTokenBonus = Convert.ToInt32(nm[2].Value);
            f2.blueTokenBonus = Convert.ToInt32(nm[3].Value);
            f2.greenTokenBonus = Convert.ToInt32(nm[4].Value);
            f2.GoldTokenBonus = Convert.ToInt32(nm[5].Value);

            f2.blackTokenleft = Token[0];
            f2.whiteTokenleft = Token[1];
            f2.redTokenleft = Token[2];
            f2.blueTokenleft = Token[3];
            f2.greenTokenleft = Token[4];
            f2.GoldTokenleft = Token[5];

            this.Close();
        }
    }
}
