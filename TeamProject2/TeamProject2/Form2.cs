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
    public partial class Form2 : Form
    {
        // Variable Declaration
        static int Players;
        struct PlayerInfo
        {
            public int id = 0;

            public int blackToken = 0, whiteToken = 0, redToken = 0, blueToken = 0, greenToken = 0, wildToken = 0;
            public int blackCard = 0, whiteCard = 0, redCard = 0, blueCard = 0, greenCard = 0, wildCard = 0;
            /* 
             * 0: black
             * 1: white
             * 2: red
             * 3: blue
             * 4: green
             * 5: wild
             */

            public PlayerInfo(int id)
            {
                this.id = id;
            }
        }

        PlayerInfo[] info = new PlayerInfo[4];
        
        GroupBox[] gbOfPlayer = new GroupBox[4];
        GroupBox[] gbOfToken = new GroupBox[4];
        GroupBox[] gbOfCard = new GroupBox[4];

        FlowLayoutPanel[] fpOfPlayer = new FlowLayoutPanel[4];
        FlowLayoutPanel[] fpOfToken = new FlowLayoutPanel[4];
        FlowLayoutPanel[] fpOfCard = new FlowLayoutPanel[4];

        public int NumOfPlayers
            {
                get { return Players; }
                set { Players = value; }
            }

        private string ShowCard(int x, int y)
        {
            switch (x)
            {
                case 0: return "Black: " + info[y].blackCard.ToString();
                case 1: return "White: " + info[y].whiteCard.ToString();
                case 2: return "Red: " + info[y].redCard.ToString();
                case 3: return "Blue: " + info[y].blueCard.ToString();
                case 4: return "Green: " + info[y].greenCard.ToString();
                default: return "Wild: " + info[y].wildCard.ToString();
            }
        }

        private void AddCard(FlowLayoutPanel fP, int x)
        {
            for (int i = 0; i < 6; i++)
            {
                Label lb = new Label()
                {
                    Text = ShowCard(i, x)
                };
                fP.Controls.Add(lb);
            }
        }

        private string ShowToken(int x, int y)
        {
            switch(x)
            {
                case 0: return "Black: " + info[y].blackToken.ToString();
                case 1: return "White: " + info[y].whiteToken.ToString();
                case 2: return "Red: " + info[y].redToken.ToString();
                case 3: return "Blue: " + info[y].blueToken.ToString();
                case 4: return "Green: " + info[y].greenToken.ToString();
                default: return "Wild: " + info[y].wildToken.ToString();
            }
        }

        private void AddToken(FlowLayoutPanel fP,int x)
        {
            for (int i = 0; i < 6; i++)
            {
                Label lb = new Label()
                {
                    Text = ShowToken(i,x)
                };
                fP.Controls.Add(lb);
            }
        }

        private void PlayersSetting()
        {

            for (int i = 0; i < Players; i++) 
            {
                ////Players////////////////////////////////////////
                
                info[i].id = i + 1;

                gbOfPlayer[i] = new GroupBox()
                {
                    AutoSize = true,
                    Text = "Player " + info[i].id.ToString() 
                };

                fpOfPlayer[i] = new FlowLayoutPanel()
                {
                    Location = new Point(gbOfPlayer[i].Location.X+10, gbOfPlayer[i].Location.Y + 25),
                    AutoSize = true
                };
                gbOfPlayer[i].Controls.Add(fpOfPlayer[i]);


                /////Token////////////////////////////////////////

                gbOfToken[i] = new GroupBox()
                {
                    Text = "Token",
                    Width = 150,
                    Height = 200
                };
                fpOfToken[i] = new FlowLayoutPanel()
                {
                    Location = new Point(gbOfToken[i].Location.X + 10, gbOfToken[i].Location.Y + 25),
                    Width = 120,
                    Height = 150,
                    AutoScroll = true
                };
                AddToken(fpOfToken[i], i);
                gbOfToken[i].Controls.Add(fpOfToken[i]);


                ///////Card///////////////////////////////////////

                gbOfCard[i] = new GroupBox()
                {
                    Text = "Card",
                    Width = 150,
                    Height = 200
                };
                fpOfCard[i] = new FlowLayoutPanel()
                {
                    Location = new Point(gbOfCard[i].Location.X + 10, gbOfCard[i].Location.Y + 25),
                    Width = 120,
                    Height = 150,
                    AutoScroll =true
                };

                AddCard(fpOfCard[i], i);
                gbOfCard[i].Controls.Add(fpOfCard[i]);


                //////////////Add to Player///////////////
                fpOfPlayer[i].Controls.Add(gbOfCard[i]);
                fpOfPlayer[i].Controls.Add(gbOfToken[i]);

                ////////Add to form/////
                FPOP.Controls.Add(gbOfPlayer[i]);   
            }

            GBOPlayers.Location = new Point(0, 0);

            //////////Card Game////////////
            CardGame.Location = new Point(0, GBOPlayers.Height + 25);

            //////////TokenGame///////////////
            TokenGame.Location = new Point(CardGame.Width+25,GBOPlayers.Height + 25);
        }

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            PlayersSetting();
        }
    }
}
