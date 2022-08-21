﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace TeamProject2
{
    public partial class Form2 : Form
    {
        // Variable Declaration

        #region var
        private struct Card
        {
            public string gen;
            public int point, den, w, r, g, b;
            public bool picked;
        }
        Card[] D1 = new Card[40];
        Card[] D2 = new Card[30];
        Card[] D3 = new Card[20];
        public struct Nobel
        {
            public int den, w, r, g, b;
            public bool picked;
        }
        Nobel[] nobles = new Nobel[10];
        static int Players, currentturn;
        static bool firstturn;
        static int[] TokenG = new int[6];

        struct PlayerInfo
        {
            public int id = 0, point = 0;

            public int blackToken = 0, whiteToken = 0, redToken = 0, blueToken = 0, greenToken = 0, GoldToken = 0;
            public int blackCard = 0, whiteCard = 0, redCard = 0, blueCard = 0, greenCard = 0, GoldCard = 0;
            /* 
             * 0: black
             * 1: white
             * 2: red
             * 3: blue
             * 4: green
             * 5: Gold
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

        Label[] PointOfPlayer = new Label[4];
        public int NumOfPlayers
            {
                get { return Players; }
                set { Players = value; }
            }

        Card[] ShowingCards = new Card[12];
        Random rand = new();
        //Cards left in deck
        int[] DL = { 40, 30, 20 };
        //biến tạm
        bool muadc;
        Card GodCard;//ko mua đc :>

        //chỗ chứa nobles
        Label[] NoblesShowing = new Label[5];
        #endregion

        //private

        #region Player's Board
        private string ShowCard(int x, int y)
        {
            switch (x)
            {
                case 0: return "Black: " + info[y].blackCard; // để v cũng đc nè :v
                case 1: return "White: " + info[y].whiteCard.ToString();
                case 2: return "Red: " + info[y].redCard.ToString();
                case 3: return "Blue: " + info[y].blueCard.ToString();
                case 4: return "Green: " + info[y].greenCard.ToString();
                default: return "Gold: " + info[y].GoldCard.ToString();
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
                default: return "Gold: " + info[y].GoldToken.ToString();
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
            firstturn = true;
            int x = rand.Next(10);

            for (int i = 0; i < Players; i++) 
            {
                NoblesShowing[i] = new();
                NoblesShowing[i].AutoSize = true;
                while (nobles[x].picked) x = rand.Next(10);
                nobles[x].picked = true;
                NoblesShowing[i].Text = NoblesToText(nobles[x]);
                NoblesfP.Controls.Add(NoblesShowing[i]);

                ////Players////////////////////////////////////////
                
                info[i].id = i + 1;

                gbOfPlayer[i] = new GroupBox()
                {
                    AutoSize = true,
                    Text = "Player " + info[i].id.ToString() 
                };

                fpOfPlayer[i] = new FlowLayoutPanel()
                {
                    Location = new Point(gbOfPlayer[i].Location.X+10, gbOfPlayer[i].Location.Y + 50),
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

                /////////Point/////////////
                PointOfPlayer[i] = new Label
                {
                    Text = "Point: " + info[i].point,
                    Location = new Point(gbOfPlayer[i].Location.X + 10, gbOfPlayer[i].Location.Y + 25)
                };
                gbOfPlayer[i].Controls.Add(PointOfPlayer[i]);

                //////////////Add to Player///////////////
                fpOfPlayer[i].Controls.Add(gbOfCard[i]);
                fpOfPlayer[i].Controls.Add(gbOfToken[i]);

                ////////Add to form/////
                FPOP.Controls.Add(gbOfPlayer[i]);   
            }

            GBOPlayers.Location = new Point(10, 0);

            //////////Card Game////////////
            CardGame.Location = new Point(10, GBOPlayers.Height + 25);

            //////////TokenGame///////////////
            TokenGame.Location = new Point(CardGame.Width+25,GBOPlayers.Height + 25);

            //Nobles' panel
            NoblesBox.Location = new Point(CardGame.Width + 25, GBOPlayers.Height + TokenGame.Height + 30);

            //////////////Turn Status/////////////
            lbTurn.Location = new Point(TokenGame.Location.X+TokenGame.Width+25, GBOPlayers.Height + 25);
            EndTurn.Location = new Point(TokenGame.Location.X+TokenGame.Width+25, lbTurn.Location.Y + lbTurn.Height + 25);

            //last noble
            x = rand.Next(10);
            NoblesShowing[Players] = new();
            NoblesShowing[Players].AutoSize = true;
            while (nobles[x].picked) x = rand.Next(10);
            NoblesShowing[Players].Text = NoblesToText(nobles[x]);
            NoblesfP.Controls.Add(NoblesShowing[Players]);
        }

        private void ShowTurnStatus(int k)
        {
            lbTurn.Text = "Player " + info[k].id.ToString() + "'s Turn";
        }

        private void NextTurn()
        {
            if(firstturn)
            {
                Random rand = new Random();
                currentturn = rand.Next(0, Players);
                ShowTurnStatus(currentturn);

                firstturn = false;
            }
            else
            {
                if (currentturn < Players - 1) currentturn++;
                else currentturn = 0;

                ShowTurnStatus(currentturn);
            }
        }
        #endregion

        public Form2()
        {
            InitializeComponent();
            InitShowCards();
        }

        #region NoblesInit
        //Translate nobles to text
        private string NoblesToText(Nobel n)
        {
            return $"3 points \r\n Black: {n.den}\r\n White: {n.w}\r\n Red: {n.r}\r\n Blue: {n.b}\r\n Green: {n.g}";
        }

        //Choose init Nobles
        private void ChooseNobles()
        {
            //for (int i=0; i<Players)
        }
        #endregion

        #region Showing Card on Button
        //Use decknumber -1 for DeckNum
        private Card TakeCardFromDeck(Card[] Deck, int DeckNum)
        {          
            DL[DeckNum]--; 
            int i = rand.Next(Deck.Length);
            while (i < Deck.Length && Deck[i].picked) i++;
            if (i == Deck.Length) i = 0;
            while(Deck[i].picked) i++;
            Deck[i].picked = true;
            return Deck[i];
        }

        private string ButtonShowString(Card c)
        {
            return "Generate: " + c.gen + "\n Point: " + c.point + "\n Black: " + c.den + "\n White: " + c.w + "\n Red: "
                + c.r + "\n Blue: " + c.b + "\n Green: " + c.g;
        }
        #endregion

        private void InitShowCards()
        {
            GodCard.g = 1000;
            ReadNoblesFile();
            ReadDeck(D1, "Tier1Deck.txt");
            ReadDeck(D2, "Tier2Deck.txt");
            ReadDeck(D3, "Tier3Deck.txt");
            Card card = TakeCardFromDeck(D1, 0); //input number of deck -1
            T1C1.Text = ButtonShowString(card);
            ShowingCards[0]=card;
            card = TakeCardFromDeck(D1, 0);
            T1C2.Text = ButtonShowString(card);
            ShowingCards[0] = card;
            card = TakeCardFromDeck(D1, 0);
            T1C3.Text = ButtonShowString(card);
            ShowingCards[0] = card;
            card = TakeCardFromDeck(D1, 0);
            T1C4.Text = ButtonShowString(card);
            ShowingCards[0] = card;
            card = TakeCardFromDeck(D2, 1);
            T2C1.Text = ButtonShowString(card);
            ShowingCards[0] = card;
            card = TakeCardFromDeck(D2, 1);
            T2C2.Text = ButtonShowString(card);
            ShowingCards[0] = card;
            card = TakeCardFromDeck(D2, 1);
            T2C3.Text = ButtonShowString(card);
            ShowingCards[0] = card;
            card = TakeCardFromDeck(D2, 1);
            T2C4.Text = ButtonShowString(card);
            ShowingCards[0] = card;
            card = TakeCardFromDeck(D3, 2);
            T3C1.Text = ButtonShowString(card);
            ShowingCards[0] = card;
            card = TakeCardFromDeck(D3, 2);
            T3C2.Text = ButtonShowString(card);
            ShowingCards[0] = card;
            card = TakeCardFromDeck(D3, 2);
            T3C3.Text = ButtonShowString(card);
            ShowingCards[0] = card;
            card = TakeCardFromDeck(D3, 2);
            T3C4.Text = ButtonShowString(card);
            ShowingCards[0] = card;
        }

        #region ReadFiles
        private void ReadDeck(Card[] Deck, string fName)
        {

            string[] s2 = File.ReadAllLines(fName);
            int i = 0, l = 0;
            foreach (string s in s2)
                if (s != "")
                {
                    switch (l)
                    {
                        case 0:
                            Deck[i].gen = s;
                            l++;
                            break;
                        case 1:
                            Deck[i].point = int.Parse(s);
                            l++;
                            break;
                        case 2:
                            Deck[i].den = int.Parse(s);
                            l++;
                            break;
                        case 3:
                            Deck[i].w = int.Parse(s);
                            l++;
                            break;
                        case 4:
                            Deck[i].r = int.Parse(s);
                            l++;
                            break;
                        case 5:
                            Deck[i].b = int.Parse(s);
                            l++;
                            break;
                        case 6:
                            Deck[i].g = int.Parse(s);
                            l++;
                            break;
                    }
                    if (l == 7) { Deck[i].picked = false; i++; l = 0; };
                }
        }

        private void ReadNoblesFile()
        {
            string[] s2 = File.ReadAllLines("Nobles.txt");
            int i = 0,l=0;
            foreach (string s in s2)
                if (s != "")
            {
                    switch (l)
                    {
                        case 0:
                            nobles[i].den = int.Parse(s);
                            l++;
                            break;
                        case 1:
                            nobles[i].w = int.Parse(s);
                            l++;
                            break;
                        case 2:
                            nobles[i].r = int.Parse(s);
                            l++;
                            break;
                        case 3:
                            nobles[i].b = int.Parse(s);
                            l++;
                            break;
                        case 4:
                            nobles[i].g = int.Parse(s);
                            l++;
                            break;
                    }
                    if (l == 5) { nobles[i].picked = false; i++; l = 0; };
            }
        }
        #endregion
        
        private void EndTurn_Click(object sender, EventArgs e)
        {
            NextTurn();
        }

        void InitTokenStatus()
        {
            int k = 0;
            switch(Players)
            {
                case 2: k = 4; break;
                case 3: k = 5; break;
                case 4: k = 7; break;
            }

            checkBox1.Text = "Black: " + k.ToString();
            checkBox7.Text = checkBox1.Text;
            TokenG[0] = k;

            checkBox2.Text = "White: " + k.ToString();
            checkBox8.Text = checkBox2.Text;
            TokenG[1] = k;

            checkBox3.Text = "Red: " + k.ToString();
            checkBox9.Text = checkBox3.Text;
            TokenG[2] = k;

            checkBox4.Text = "Blue: " + k.ToString();
            checkBox10.Text = checkBox4.Text;
            TokenG[3] = k;

            checkBox5.Text = "Green: " + k.ToString();
            checkBox11.Text = checkBox5.Text;
            TokenG[4] = k;

            checkBox6.Text = "Gold: " + 5.ToString();
            TokenG[5] = 5;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            PlayersSetting();
            InitTokenStatus();
            NextTurn();
        }

        #region Board's Button
        //giá trị card của mấy cái này nằm trong mảng ShowingCards
        private void T1C1_Click(object sender, EventArgs e)
        {
            muadc = true;
            if (muadc && DL[0] > 0) 
            {
                Card card = TakeCardFromDeck(D1, 0);
                T1C1.Text = ButtonShowString(card);
                ShowingCards[0] = card;
            } else
                if (muadc)
            {
                T1C1.Text = "Deck out of cards :<";
                ShowingCards[0] = GodCard;
            }
        }

        private void T1C2_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            if (muadc && DL[0] > 0)
            {
                Card card = TakeCardFromDeck(D1, 0);
                b.Text = ButtonShowString(card);
                ShowingCards[1] = card;
            }
            else
                if (muadc)
            {
                b.Text = "Deck out of cards :<";
                ShowingCards[1] = GodCard;
            }
        }

        private void T1C3_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            if (muadc && DL[0] > 0)
            {
                Card card = TakeCardFromDeck(D1, 0);
                b.Text = ButtonShowString(card);
                ShowingCards[2] = card;
            }
            else
                if (muadc)
            {
                b.Text = "Deck out of cards :<";
                ShowingCards[2] = GodCard;
            }
        }

        private void T1C4_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            if (muadc && DL[0] > 0)
            {
                Card card = TakeCardFromDeck(D1, 0);
                b.Text = ButtonShowString(card);
                ShowingCards[3] = card;
            }
            else
                if (muadc)
            {
                b.Text = "Deck out of cards :<";
                ShowingCards[3] = GodCard;
            }
        }

        private void T2C1_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            if (muadc && DL[1] > 0)
            {
                Card card = TakeCardFromDeck(D2, 1);
                b.Text = ButtonShowString(card);
                ShowingCards[4] = card;
            }
            else
                if (muadc)
            {
                b.Text = "Deck out of cards :<";
                ShowingCards[4] = GodCard;
            }
        }

        private void T2C2_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            if (muadc && DL[1] > 0)
            {
                Card card = TakeCardFromDeck(D2, 1);
                b.Text = ButtonShowString(card);
                ShowingCards[5] = card;
            }
            else
                if (muadc)
            {
                b.Text = "Deck out of cards :<";
                ShowingCards[5] = GodCard;
            }
        }

        private void T2C3_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            if (muadc && DL[1] > 0)
            {
                Card card = TakeCardFromDeck(D2, 1);
                b.Text = ButtonShowString(card);
                ShowingCards[6] = card;
            }
            else
                if (muadc)
            {
                b.Text = "Deck out of cards :<";
                ShowingCards[6] = GodCard;
            }
        }

        private void T2C4_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            if (muadc && DL[1] > 0)
            {
                Card card = TakeCardFromDeck(D2, 1);
                b.Text = ButtonShowString(card);
                ShowingCards[7] = card;
            }
            else
                if (muadc)
            {
                b.Text = "Deck out of cards :<";
                ShowingCards[7] = GodCard;
            }
        }

        private void T3C1_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            if (muadc && DL[2] > 0)
            {
                Card card = TakeCardFromDeck(D3, 2);
                b.Text = ButtonShowString(card);
                ShowingCards[8] = card;
            }
            else
                if (muadc)
            {
                b.Text = "Deck out of cards :<";
                ShowingCards[8] = GodCard;
            }
        }

        private void T3C2_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            if (muadc && DL[2] > 0)
            {
                Card card = TakeCardFromDeck(D3, 2);
                b.Text = ButtonShowString(card);
                ShowingCards[9] = card;
            }
            else
                if (muadc)
            {
                b.Text = "Deck out of cards :<";
                ShowingCards[9] = GodCard;
            }
        }

        private void T3C3_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            if (muadc && DL[2] > 0)
            {
                Card card = TakeCardFromDeck(D3, 2);
                b.Text = ButtonShowString(card);
                ShowingCards[10] = card;
            }
            else
                if (muadc)
            {
                b.Text = "Deck out of cards :<";
                ShowingCards[10] = GodCard;
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void T3C4_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            if (muadc && DL[2] > 0)
            {
                Card card = TakeCardFromDeck(D3, 2);
                b.Text = ButtonShowString(card);
                ShowingCards[11] = card;
            }
            else
                if (muadc)
            {
                b.Text = "Deck out of cards :<";
                ShowingCards[11] = GodCard;
            }
        }
    #endregion
    }
}
