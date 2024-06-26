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
using System.Runtime.InteropServices;

namespace TeamProject2
{
    public partial class Form2 : Form
    {
        // Variable Declaration

        #region var
        int NobleNum = Players + 1;
        public struct Card
        {
            public string gen;
            public int point, den, w, r, g, b;
            public bool picked;
        }
        Card[] D1 = new Card[40];
        Card[] D2 = new Card[30];
        Card[] D3 = new Card[20];
        public struct Noble
        {
            public int den, w, r, g, b;
            public bool picked;
        }
        Noble[] nobles = new Noble[10];
        Noble[] nb = new Noble[5];
        Button[] CButtonShowing = new Button[15];
        int theChoosenOne = -1;
        public static int Players, currentturn, maxpick = 5, firstplayer;
        static bool firstturn, changeturn = false, isWin = false;
        static int[] TokenG = new int[6];
        static int pick3token = 0;

        public struct PlayerInfo
        {
            public int id = 0, point = 0, NumReserving = 0;

            public int blackToken = 0, whiteToken = 0, redToken = 0, blueToken = 0, greenToken = 0, GoldToken = 0, GoldTemp = 0;
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
        public bool[,] XaiHet = new bool[4, 5];
        public PlayerInfo[] info = new PlayerInfo[4];

        GroupBox[] gbOfPlayer = new GroupBox[4];
        GroupBox[] gbOfToken = new GroupBox[4];
        GroupBox[] gbOfCard = new GroupBox[4];

        FlowLayoutPanel[] fpOfPlayer = new FlowLayoutPanel[4];
        FlowLayoutPanel[] fpOfToken = new FlowLayoutPanel[4];
        FlowLayoutPanel[] fpOfCard = new FlowLayoutPanel[4];

        Label[] PointOfPlayer = new Label[4];

        #region hmm
        public int NumOfPlayers
        {
            get { return Players; }
            set { Players = value; }
        }

        static int[] TokenL = new int[6];

        public int blackTokenleft
        {
            get { return TokenL[0]; }
            set { TokenL[0] = value; }
        }

        public int whiteTokenleft
        {
            get { return TokenL[1]; }
            set { TokenL[1] = value; }
        }

        public int redTokenleft
        {
            get { return TokenL[2]; }
            set { TokenL[2] = value; }
        }

        public int blueTokenleft
        {
            get { return TokenL[3]; }
            set { TokenL[3] = value; }
        }

        public int greenTokenleft
        {
            get { return TokenL[4]; }
            set { TokenL[4] = value; }
        }

        public int GoldTokenleft
        {
            get { return TokenL[5]; }
            set { TokenL[5] = value; }
        }

        static int[] TokenB = new int[6];

        public int blackTokenBonus
        {
            get { return TokenB[0]; }
            set { TokenB[0] = value; }
        }

        public int whiteTokenBonus
        {
            get { return TokenB[1]; }
            set { TokenB[1] = value; }
        }

        public int redTokenBonus
        {
            get { return TokenB[2]; }
            set { TokenB[2] = value; }
        }

        public int blueTokenBonus
        {
            get { return TokenB[3]; }
            set { TokenB[3] = value; }
        }

        public int greenTokenBonus
        {
            get { return TokenB[4]; }
            set { TokenB[4] = value; }
        }

        public int GoldTokenBonus
        {
            get { return TokenB[5]; }
            set { TokenB[5] = value; }
        }

        static int theReserveCard;
        public int ReseverCardNum
        {
            get { return theReserveCard; }
            set { theReserveCard = value; }
        }

        static bool isBuyingReserveCard;
        public bool ReserveCardAction
        {
            get { return isBuyingReserveCard; }
            set { isBuyingReserveCard = value; }
        }

        #endregion
        Card[] ShowingCards = new Card[12];
        Random rand = new();
        //Cards left in deck
        int[] DL = { 40, 30, 20 };
        bool muadc;
        Card GodCard;//ko mua đc :>

        //chỗ chứa nobles
        Label[] NoblesShowing = new Label[5];

        public static Card[,] ReservedCards = new Card[4, 3];
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
                    Text = ShowCard(i, x),
                    ForeColor = SystemColors.ControlText,
                    AutoSize= true,
                };
                fP.Controls.Add(lb);
            }
        }

        public string ShowToken(int x, int y)
        {
            switch (x)
            {
                case 0: return "Black: " + info[y].blackToken.ToString();
                case 1: return "White: " + info[y].whiteToken.ToString();
                case 2: return "Red: " + info[y].redToken.ToString();
                case 3: return "Blue: " + info[y].blueToken.ToString();
                case 4: return "Green: " + info[y].greenToken.ToString();
                default: return "Gold: " + info[y].GoldToken.ToString();
            }
        }

        private void AddToken(FlowLayoutPanel fP, int x)
        {
            for (int i = 0; i < 6; i++)
            {
                Label lb = new Label()
                {
                    Text = ShowToken(i, x),
                    ForeColor = SystemColors.ControlText,
                    AutoSize = true
                };
                fP.Controls.Add(lb);
            }
        }

        private int PlayersTokensSum(int id)
        {
            return info[id].blackToken + info[id].whiteToken + info[id].redToken +
                info[id].blueToken + info[id].greenToken + info[id].GoldToken;
        }

        private void PlayersSetting()
        {
            firstturn = true;
            int x = rand.Next(10);
            int br = 0;
            for (int i = 0; i < Players; i++)
            {
                NoblesShowing[i] = new();
                NoblesShowing[i].AutoSize = true;
                while (nobles[x].picked) x = rand.Next(10);
                nobles[x].picked = true;
                nb[br] = nobles[x];
                nb[br].picked = false;
                br++;
                NoblesShowing[i].Text = NoblesToText(nobles[x]);
                NoblesfP.Controls.Add(NoblesShowing[i]);

                ////Players////////////////////////////////////////

                info[i].id = i + 1;

                gbOfPlayer[i] = new GroupBox()
                {
                    AutoSize = true,
                    Text = "Player " + info[i].id.ToString(),
                    ForeColor = Color.MidnightBlue,
                };

                fpOfPlayer[i] = new FlowLayoutPanel()
                {
                    Location = new Point(gbOfPlayer[i].Location.X + 10, gbOfPlayer[i].Location.Y + 50),
                    AutoSize = true
                };
                gbOfPlayer[i].Controls.Add(fpOfPlayer[i]);


                /////Token////////////////////////////////////////

                gbOfToken[i] = new GroupBox()
                {
                    Text = "Token",
                    ForeColor = Color.Indigo,
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
                    ForeColor = Color.DarkGreen,
                    Width = 150,
                    Height = 200
                };
                fpOfCard[i] = new FlowLayoutPanel()
                {
                    Location = new Point(gbOfCard[i].Location.X + 10, gbOfCard[i].Location.Y + 25),
                    Width = 120,
                    Height = 150,
                    AutoScroll = true
                };

                AddCard(fpOfCard[i], i);
                gbOfCard[i].Controls.Add(fpOfCard[i]);

                /////////Point/////////////
                PointOfPlayer[i] = new Label
                {
                    Text = "Point: " + info[i].point,
                    ForeColor = Color.Firebrick,
                    Location = new Point(gbOfPlayer[i].Location.X + 10, gbOfPlayer[i].Location.Y + 25),
                    AutoSize = true
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
            TokenGame.Location = new Point(CardGame.Width + 25, GBOPlayers.Height + 25);

            //Nobles' panel
            NoblesBox.Location = new Point(CardGame.Width + 25, GBOPlayers.Height + TokenGame.Height + 30);

            //////////////Turn Status/////////////
            lbTurn.Location = new Point(TokenGame.Location.X + TokenGame.Width + 25, GBOPlayers.Height + 25);
            EndTurn.Location = new Point(TokenGame.Location.X + TokenGame.Width + 25, lbTurn.Location.Y + lbTurn.Height + 70);

            ///////////////////Reserve Card//////////
            ReserveCardbtn.Location = new Point(TokenGame.Location.X + TokenGame.Width + 25, lbTurn.Location.Y + lbTurn.Height + 25);

            //last noble
            x = rand.Next(10);
            NoblesShowing[Players] = new();
            NoblesShowing[Players].AutoSize = true;
            while (nobles[x].picked) x = rand.Next(10);
            NoblesShowing[Players].Text = NoblesToText(nobles[x]);
            NoblesfP.Controls.Add(NoblesShowing[Players]);
            nb[br] = nobles[x];
            nb[br].picked = false;
            br++;
            isWin = false;
        }

        private void ShowTurnStatus(int k)
        {
            lbTurn.Text = "Player " + info[k].id.ToString() + "'s Turn";
        }

        private void NextTurn()
        {
            if (firstturn)
            {
                Random rand = new Random();
                currentturn = rand.Next(0, Players);
                firstplayer = currentturn-1;
                if (firstplayer < 0) firstplayer = Players - 1;

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

        #region Form2
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            InitShowCards();
            PlayersSetting();
            InitTokenStatus();
            InitCkb();
            NextTurn();
        }
        #endregion

        #region NoblesInit
        //Translate nobles to text
        private string NoblesToText(Noble n)
        {
            return $"3 points \r\nBlack: {n.den}\r\nWhite: {n.w}\r\nRed: {n.r}\r\nBlue: {n.b}\r\nGreen: {n.g}";
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
            while (Deck[i].picked) i++;
            Deck[i].picked = true;
            return Deck[i];
        }

        private string ButtonShowString(Card c)
        {
            return "Generate: " + c.gen + "\n Point: " + c.point + "\n Black: " + c.den + "\n White: " + c.w + "\n Red: "
                + c.r + "\n Blue: " + c.b + "\n Green: " + c.g;
        }

        private void TakeNewCard(Card[] Deck, int DeckNum, int x)
        {
            if (DL[DeckNum] <= 0)
            {
                CButtonShowing[x].Text = "Deck out of card :<";
                CButtonShowing[x].BackColor = SystemColors.ButtonHighlight;
                ShowingCards[x] = GodCard;
                return;
            }
            Card card = TakeCardFromDeck(Deck, DeckNum);
            CButtonShowing[x].Text = ButtonShowString(card);
            ShowingCards[x] = card;

            BtnCardColor(CButtonShowing[x], card);
        }
        private static int CardGenToInt(Card c)
        {
            switch (c.gen)
            {
                case "Black": return 1;
                case "White": return 2;
                case "Red": return 3;
                case "Green": return 4;
                case "Blue": return 5;
            }
            return 0;
        }

        
        public static void BtnCardColor(Button btn, Card c)
        {
            int i = CardGenToInt(c);
            switch (i)
            {
                case 1:
                    btn.BackColor = Color.LightGray;
                    break;
                case 2:
                    btn.BackColor = Color.White;
                    break;
                case 3:
                    btn.BackColor = Color.FromArgb(255, 192, 192);      //Red
                    break;
                case 4:
                    btn.BackColor = Color.FromArgb(192, 255, 192);      //Green
                    break;
                case 5:
                    btn.BackColor = Color.FromArgb(190, 231, 254);      //Blue
                    break;
            }
        }
        #endregion

        #region Inits
        private void InitShowCards()
        {
            GodCard.g = 1000;
            CButtonShowing[0] = T1C1;
            CButtonShowing[1] = T1C2;
            CButtonShowing[2] = T1C3;
            CButtonShowing[3] = T1C4;
            CButtonShowing[4] = T2C1;
            CButtonShowing[5] = T2C2;
            CButtonShowing[6] = T2C3;
            CButtonShowing[7] = T2C4;
            CButtonShowing[8] = T3C1;
            CButtonShowing[9] = T3C2;
            CButtonShowing[10] = T3C3;
            CButtonShowing[11] = T3C4;
            ReadNoblesFile();
            ReadDeck(D1, "Tier1Deck.txt");
            ReadDeck(D2, "Tier2Deck.txt");
            ReadDeck(D3, "Tier3Deck.txt");
            TakeNewCard(D1, 0, 0);
            TakeNewCard(D1, 0, 1);
            TakeNewCard(D1, 0, 2);
            TakeNewCard(D1, 0, 3);
            TakeNewCard(D2, 1, 4);
            TakeNewCard(D2, 1, 5);
            TakeNewCard(D2, 1, 6);
            TakeNewCard(D2, 1, 7);
            TakeNewCard(D3, 2, 8);
            TakeNewCard(D3, 2, 9);
            TakeNewCard(D3, 2, 10);
            TakeNewCard(D3, 2, 11);
        }

        void InitTokenStatus()
        {
            int k = 0;
            switch (Players)
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

        void ShowGameToken()
        {
            checkBox1.Text = "Black: " + TokenG[0];
            checkBox7.Text = checkBox1.Text;

            checkBox2.Text = "White: " + TokenG[1];
            checkBox8.Text = checkBox2.Text;

            checkBox3.Text = "Red: " + TokenG[2];
            checkBox9.Text = checkBox3.Text;

            checkBox4.Text = "Blue: " + TokenG[3];
            checkBox10.Text = checkBox4.Text;

            checkBox5.Text = "Green: " + TokenG[4];
            checkBox11.Text = checkBox5.Text;

            checkBox6.Text = "Gold: " + TokenG[5];
        }
        #endregion

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
            int i = 0, l = 0;
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

        #region En/Unable || Show
        private void EnableControls()
        {
            fp3picktoken.Enabled = true;
            foreach (CheckBox item in fp3picktoken.Controls)
            {
                item.Enabled = true;
                if (item.Checked) item.Checked = false;
            }

            fP2picktoken.Enabled = true;
            foreach (CheckBox item in fP2picktoken.Controls)
            {
                item.Enabled = true;
                if (item.Checked) item.Checked = false;
            }

            CardGame.Enabled = true;
            checkBox6.Checked = false;
            checkBox6.Enabled = true;
        }

        CheckBox[] ckb = new CheckBox[10];

        private void InitCkb()
        {
            ckb[0] = checkBox7;
            ckb[1] = checkBox8;
            ckb[2] = checkBox9;
            ckb[3] = checkBox10;
            ckb[4] = checkBox11;
            ckb[5] = checkBox1;
            ckb[6] = checkBox2;
            ckb[7] = checkBox3;
            ckb[8] = checkBox4;
            ckb[9] = checkBox5;
        }

        private void UnableControlsToken()
        {
            for (int i = 0; i < 5; i++)
            {
                if (TokenG[i] < 4)
                {
                    if (!ckb[i].Checked && !ckb[i + 5].Checked)
                    {
                        ckb[i].Enabled = false;
                        if (TokenG[i] <= 0)
                        {
                            ckb[i + 5].Enabled = false;
                        }
                    }
                }
            }

        }

        private void UnableOtherAction()
        {
            fP2picktoken.Enabled = false;
            CardGame.Enabled = false;
            checkBox6.Enabled = false;
        }

        private void ShowAgainToken()
        {
            int i = 0;
            foreach (Label lb in fpOfToken[currentturn].Controls)
            {
                lb.Text = ShowToken(i, currentturn);
                i++;
            }
        }
        #endregion

        private void TraLai(ref int token, ref int pay, bool XaiHet, ref int GameToken, int dis)
        {
            if (XaiHet)
            {
                GameToken += token;
                token = 0;
            }
            else
            {
                int x = 0;
                if (pay - dis > 0) x = pay - dis;
                GameToken += x;
                token -= x;
            }
        }

        private void HandleMuaBai(Card c)
        {
            int x = CardGenToInt(c);
            switch (x)
            {
                case 1:
                    info[currentturn].blackCard++;
                    break;
                case 2:
                    info[currentturn].whiteCard++;
                    break;
                case 3:
                    info[currentturn].redCard++;
                    break;
                case 4:
                    info[currentturn].greenCard++;
                    break;
                default:
                    info[currentturn].blueCard++;
                    break;
            }
            info[currentturn].point += c.point;
            int p = 0;
            foreach (Label lb in fpOfCard[currentturn].Controls)
            {
                lb.Text = ShowCard(p, currentturn);
                p++;
            }
            PointOfPlayer[currentturn].Text = "Point: " + info[currentturn].point;
            ref PlayerInfo pl = ref info[currentturn];
            x = currentturn;
            TokenG[5] += pl.GoldToken - pl.GoldTemp;
            pl.GoldToken = pl.GoldTemp;
            TraLai(ref pl.blackToken, ref c.den, XaiHet[x, 0], ref TokenG[0], pl.blackCard);
            TraLai(ref pl.whiteToken, ref c.w, XaiHet[x, 1], ref TokenG[1], pl.whiteCard);
            TraLai(ref pl.redToken, ref c.r, XaiHet[x, 2], ref TokenG[2], pl.redCard);
            TraLai(ref pl.blueToken, ref c.b, XaiHet[x, 3], ref TokenG[3], pl.blueCard);
            TraLai(ref pl.greenToken, ref c.g, XaiHet[x, 4], ref TokenG[4], pl.greenCard);
            ShowAgainToken();
            ShowGameToken();
        }

        private void EndTurn_Click(object sender, EventArgs e)
        {
            #region Reserve Card Check
            if (checkBox6.Checked && theChoosenOne != -1 && info[currentturn].NumReserving < 3)
            {
                //add card vô biến
                if (TokenG[5] > 0)
                {
                    info[currentturn].GoldToken++;
                    TokenG[5]--;
                }
                ShowAgainToken();
                if (theChoosenOne < 12)
                {
                    ReservedCards[currentturn, info[currentturn].NumReserving] = ShowingCards[theChoosenOne];
                    info[currentturn].NumReserving++;
                }
                if (theChoosenOne < 4)
                {
                    UpdateNewCard(D1, 0, CButtonShowing[theChoosenOne], theChoosenOne);
                }
                else
                if (theChoosenOne > 7 && theChoosenOne < 12)
                {
                    UpdateNewCard(D3, 2, CButtonShowing[theChoosenOne], theChoosenOne);
                }
                else
                if (theChoosenOne > 3 && theChoosenOne < 8)
                {
                    UpdateNewCard(D2, 1, CButtonShowing[theChoosenOne], theChoosenOne);
                }
                else
                if (theChoosenOne == 12)
                {
                    Card card = TakeCardFromDeck(D1, 0);
                    DL[0]--;
                    ReservedCards[currentturn, info[currentturn].NumReserving] = card;
                    info[currentturn].NumReserving++;
                }
                else
                if (theChoosenOne == 13)
                {
                    Card card = TakeCardFromDeck(D2, 1);
                    DL[1]--;
                    ReservedCards[currentturn, info[currentturn].NumReserving] = card;
                    info[currentturn].NumReserving++;
                }
                if (theChoosenOne == 14)
                {
                    Card card = TakeCardFromDeck(D3, 2);
                    DL[2]--;
                    ReservedCards[currentturn, info[currentturn].NumReserving] = card;
                    info[currentturn].NumReserving++;
                }
            }
            else
                if (checkBox6.Checked && info[currentturn].NumReserving < 3)
            {
                MessageBox.Show("Please choose a card to reserve! >:(");
                return;
            }
            else
                if (checkBox6.Checked)
            {
                MessageBox.Show("Quá 3 lá rồi :<");
                return;
            }
            #endregion

            #region CheckMuaBai
            if (muadc)
            {
                Card c = ShowingCards[theChoosenOne];
                HandleMuaBai(c);
                if (theChoosenOne < 4) TakeNewCard(D1, 0, theChoosenOne);
                else
                    if (theChoosenOne > 7) TakeNewCard(D3, 2, theChoosenOne);
                else
                    TakeNewCard(D2, 1, theChoosenOne);
            }
            #endregion

            if (theChoosenOne != -1 && theChoosenOne < 12) BtnCardColor(CButtonShowing[theChoosenOne], ShowingCards[theChoosenOne]); 
            else if (theChoosenOne != -1) CButtonShowing[theChoosenOne].BackColor = Color.GhostWhite;
            muadc = false;
            theChoosenOne = -1;

            #region Check Token
            maxpick = 5;
            int i = 0;
            foreach (CheckBox item in fp3picktoken.Controls)
            {
                if (TokenG[i] == 0 && !ckb[i].Checked && !ckb[i + 5].Checked) maxpick--;
                i++;
            }

            maxpick = Math.Min(3, maxpick);

            if (pick3token < maxpick && fp3picktoken.Enabled == true)
            {
                MessageBox.Show("Pick thêm đi :v");
                return;
            }

            changeturn = true;
            pick3token = 0;


            ///check token>10

            int sum = PlayersTokensSum(currentturn);
            if (sum > 10)
            {
                TakeoutToken(sum);
            }

            EnableControls();
            UnableControlsToken();
            #endregion

            #region CheckNoble
            for (int q = 0; q <=Players; q++)
                if (!nb[q].picked)
            {
                    int c = 0;
                    ref PlayerInfo p = ref info[currentturn];
                    if (p.whiteCard >= nb[q].w) c++;
                    if (p.blackCard >= nb[q].den) c++;
                    if (p.redCard >= nb[q].r) c++;
                    if (p.greenCard >= nb[q].g) c++;
                    if (p.blueCard >= nb[q].b) c++;
                    if (c==5)
                    {
                        nb[q].picked = true;
                        NoblesfP.Controls.Remove(NoblesShowing[q]);
                        p.point += 3;
                        PointOfPlayer[currentturn].Text = "Point: " + p.point;
                        MessageBox.Show("You've just earned a noble's trust :O");
                        break;
                    }
            }
            #endregion

            #region Check Win

            if (info[currentturn].point >= 15 && !isWin)
            {
                MessageBox.Show("Player " + info[currentturn].id + " win!!!");

                if(currentturn!=firstplayer)
                {
                    MessageBox.Show("Please finish remaining turn!");
                    Label lb = new Label()
                    {
                        AutoSize = true,
                        Text = "Please finish remaining turns!",
                        Font = new Font("Consolas", 10F, FontStyle.Bold, GraphicsUnit.Point),
                        Location = new Point(EndTurn.Location.X, EndTurn.Location.Y + EndTurn.Height + 25),
                    };
                    this.Controls.Add(lb);
                }
                isWin = true;
            }

            if (isWin && currentturn == firstplayer)
            {
                DialogResult result = MessageBox.Show("Game End!", "Notification", MessageBoxButtons.OK);
                if (result == DialogResult.OK) this.Close();
            }

            #endregion

            NextTurn();
            changeturn = false;
        }

        private void TakeoutToken(int sum)
        {
            Form3 f3 = new Form3();
            f3.Turn = currentturn;
            f3.LeftOver = sum - 10;

            f3.blackToken = info[currentturn].blackToken;
            f3.whiteToken = info[currentturn].whiteToken;
            f3.redToken = info[currentturn].redToken;
            f3.blueToken = info[currentturn].blueToken;
            f3.greenToken = info[currentturn].greenToken;
            f3.GoldToken = info[currentturn].GoldToken;

            f3.ShowDialog();

            info[currentturn].blackToken = TokenL[0];
            info[currentturn].whiteToken = TokenL[1];
            info[currentturn].redToken = TokenL[2];
            info[currentturn].blueToken = TokenL[3];
            info[currentturn].greenToken = TokenL[4];
            info[currentturn].GoldToken = TokenL[5];

            int i = 0;
            foreach (Label lb in fpOfToken[currentturn].Controls)
            {
                lb.Text = ShowToken(i, currentturn);
                TokenG[i] += TokenB[i];
                i++;
            }

            ckb[0].Text = "Black: " + TokenG[0];
            ckb[5].Text = ckb[0].Text;
            ckb[1].Text = "White: " + TokenG[1];
            ckb[6].Text = ckb[1].Text;
            ckb[2].Text = "Red: " + TokenG[2];
            ckb[7].Text = ckb[2].Text;
            ckb[3].Text = "Blue: " + TokenG[3];
            ckb[8].Text = ckb[3].Text;
            ckb[4].Text = "Green: " + TokenG[4];
            ckb[9].Text = ckb[4].Text;

            checkBox6.Text = "Gold: " + TokenG[5];
        }

        /*private void GodBut_Click(object sender, EventArgs e)
        {
            info[currentturn].GoldToken = 1000;
            ShowAgainToken();
        }*/

        private void ReserveCardbtn_Click(object sender, EventArgs e)
        {
            if (info[currentturn].NumReserving == 0)
            {
                MessageBox.Show("Không có reserve card để coi :vv");
                return;
            }

            Form4 f4 = new Form4();

            f4.NumReCard = info[currentturn].NumReserving;

            if (info[currentturn].NumReserving > 0) f4.Text1 = ButtonShowString(ReservedCards[currentturn, 0]);
            if (info[currentturn].NumReserving > 1) f4.Text2 = ButtonShowString(ReservedCards[currentturn, 1]);
            if (info[currentturn].NumReserving > 2) f4.Text3 = ButtonShowString(ReservedCards[currentturn, 2]);

            f4.ShowDialog();
            if (isBuyingReserveCard)
            {
                CheckMuaDc(ReservedCards[currentturn, ReseverCardNum]);
                if (muadc && fp3picktoken.Enabled && CardGame.Enabled)
                {
                    Card c = ReservedCards[currentturn, ReseverCardNum];
                    HandleMuaBai(c);
                    Card[] card = new Card[3];
                    int x = 0;
                    for (int i = 0; i < info[currentturn].NumReserving; i++)
                        if (i != ReseverCardNum)
                        {
                            card[x] = ReservedCards[currentturn, i];
                            x++;
                        }
                    for (int i = 0; i < x; i++)
                        ReservedCards[currentturn, i] = card[i];
                    info[currentturn].NumReserving--;
                    muadc = false;
                    fp3picktoken.Enabled = false;
                    EndTurn_Click(sender, e);
                    MessageBox.Show("Reserved card purchased successfully");
                }
                else
                {
                    MessageBox.Show("Can't buy card");
                }
            }
        }

        //giá trị card của mấy cái này nằm trong mảng ShowingCards
        #region Cards' Button
        private void UpdateNewCard(Card[] D, int DeckNum, Button b, int bNum)
        {
            if (DL[DeckNum] == 0)
            {
                b.Text = "Deck out of cards :<";
                b.BackColor = SystemColors.ButtonHighlight;
                ShowingCards[bNum] = GodCard;
                return;
            }
            DL[DeckNum]--;
            Card card = TakeCardFromDeck(D, DeckNum);
            b.Text = ButtonShowString(card);
            ShowingCards[bNum] = card;

        }

        private void CheckReserve(int x)
        {
            if (ShowingCards[x].g == 1000) return;
            if (theChoosenOne != -1 && theChoosenOne < 12) BtnCardColor(CButtonShowing[theChoosenOne], ShowingCards[theChoosenOne]);
            else if (theChoosenOne != -1) CButtonShowing[theChoosenOne].BackColor = Color.GhostWhite;
            CButtonShowing[x].BackColor = Color.Yellow;
            checkBox6.Enabled = true;
            theChoosenOne = x;
        }

        private bool CheckHelper(int token, ref int vang, int canmua, int x, int dis)
        {
            int z = currentturn;
            XaiHet[z, x] = false;
            if (token < canmua - dis)
            {
                if (token + vang < canmua - dis) return true;
                vang -= (canmua - dis) - token ;
                XaiHet[z, x] = true;
            }
            return false;
        }

        public void CheckMuaDc(Card c)
        {
            muadc = false;
            info[currentturn].GoldTemp = info[currentturn].GoldToken;
            ref PlayerInfo pl = ref info[currentturn];
            if (CheckHelper(pl.blackToken, ref pl.GoldTemp, c.den, 0, pl.blackCard)) return;
            if (CheckHelper(pl.whiteToken, ref pl.GoldTemp, c.w, 1, pl.whiteCard)) return;
            if (CheckHelper(pl.redToken, ref pl.GoldTemp, c.r, 2, pl.redCard)) return;
            if (CheckHelper(pl.blueToken, ref pl.GoldTemp, c.b, 3, pl.blueCard)) return;
            if (CheckHelper(pl.greenToken, ref pl.GoldTemp, c.g, 4, pl.greenCard)) return;
            muadc = true;
        }

        private void XuLiMuaDc(int x)
        {
            if (muadc && theChoosenOne == x)
            {
                muadc = false;
                if (theChoosenOne != -1 && theChoosenOne < 12) BtnCardColor(CButtonShowing[theChoosenOne], ShowingCards[theChoosenOne]);
                else if (theChoosenOne != -1) CButtonShowing[theChoosenOne].BackColor = Color.GhostWhite;
                fp3picktoken.Enabled = true;
                fP2picktoken.Enabled = true;
                checkBox6.Enabled = true;
                theChoosenOne = -1;
                return;
            }
            CheckMuaDc(ShowingCards[x]);
            if (muadc)
            {
                if (theChoosenOne != -1 && theChoosenOne < 12) BtnCardColor(CButtonShowing[theChoosenOne], ShowingCards[theChoosenOne]);
                else if (theChoosenOne != -1) CButtonShowing[theChoosenOne].BackColor = Color.GhostWhite;
                theChoosenOne = x;
                CButtonShowing[x].BackColor = Color.LightSeaGreen;
                fp3picktoken.Enabled = false;
                fP2picktoken.Enabled = false;
                checkBox6.Enabled = false;
            }
            else
            {
                checkBox6.CheckState = CheckState.Checked;
                CheckReserve(x);
            }
        }

        private void T1C1_Click(object sender, EventArgs e)
        {
            int code = 0;
            if (checkBox6.Checked)
            {
                CheckReserve(code);
            }
            else
            {
                XuLiMuaDc(code);
            }
        }

        private void T1C2_Click(object sender, EventArgs e)
        {
            int code = 1;
            if (checkBox6.Checked)
            {
                CheckReserve(code);
            }
            else
            {
                XuLiMuaDc(code);
            }
        }

        private void T1C3_Click(object sender, EventArgs e)
        {
            int code = 2;
            if (checkBox6.Checked)
            {
                CheckReserve(code);
            }
            else
            {
                XuLiMuaDc(code);
            }
        }

        private void T1C4_Click(object sender, EventArgs e)
        {
            int code = 3;
            if (checkBox6.Checked)
            {
                CheckReserve(code);
            }
            else
            {
                XuLiMuaDc(code);
            }
        }

        private void T2C1_Click(object sender, EventArgs e)
        {
            int code = 4;
            if (checkBox6.Checked)
            {
                CheckReserve(code);
            }
            else
            {
                XuLiMuaDc(code);
            }
        }

        private void T2C2_Click(object sender, EventArgs e)
        {
            int code = 5;
            if (checkBox6.Checked)
            {
                CheckReserve(code);
            }
            else
            {
                XuLiMuaDc(code);
            }
        }

        private void T2C3_Click(object sender, EventArgs e)
        {
            int code = 6;
            if (checkBox6.Checked)
            {
                CheckReserve(code);
            }
            else
            {
                XuLiMuaDc(code);
            }
        }

        private void T2C4_Click(object sender, EventArgs e)
        {
            int code = 7;
            if (checkBox6.Checked)
            {
                CheckReserve(code);
            }
            else
            {
                XuLiMuaDc(code);
            }
        }

        private void T3C1_Click(object sender, EventArgs e)
        {
            int code = 8;
            if (checkBox6.Checked)
            {
                CheckReserve(code);
            }
            else
            {
                XuLiMuaDc(code);
            }
        }

        private void T3C2_Click(object sender, EventArgs e)
        {
            int code = 9;
            if (checkBox6.Checked)
            {
                CheckReserve(code);
            }
            else
            {
                XuLiMuaDc(code);
            }
        }

        private void T3C3_Click(object sender, EventArgs e)
        {
            int code = 10;
            if (checkBox6.Checked)
            {
                CheckReserve(code);
            }
            else
            {
                XuLiMuaDc(code);
            }
        }
        private void T3C4_Click(object sender, EventArgs e)
        {
            int code = 11;
            if (checkBox6.Checked)
            {
                CheckReserve(code);
            }
            else
            {
                XuLiMuaDc(code);
            }
        }

        private void T1D_Click(object sender, EventArgs e)
        {
            int code = 12, dn = 0 ;
            CButtonShowing[code] = sender as Button;
            checkBox6.Checked = true;
            if (TokenG[5] > 0) checkBox6.Text = "Gold: " + (TokenG[5] - 1);
            else checkBox6.Text = "Gold: 0";
            if (theChoosenOne != -1 && theChoosenOne < 12) BtnCardColor(CButtonShowing[theChoosenOne], ShowingCards[theChoosenOne]);
            else if (theChoosenOne != -1) CButtonShowing[theChoosenOne].BackColor = Color.GhostWhite;
            if (DL[dn] <= 0)
            {
                MessageBox.Show("Deck empty :<");
                T1D.Text = "Deck 1 empty";
                return;
            }
            CButtonShowing[code].BackColor = Color.Yellow;
            checkBox6.Enabled = true;
            theChoosenOne = code;
        }

        private void T2D_Click(object sender, EventArgs e)
        {
            int code = 13, dn = 1;
            CButtonShowing[code] = sender as Button;
            checkBox6.Checked = true;
            if (TokenG[5] > 0) checkBox6.Text = "Gold: " + (TokenG[5] - 1);
            else checkBox6.Text = "Gold: 0";
            if (theChoosenOne != -1 && theChoosenOne < 12) BtnCardColor(CButtonShowing[theChoosenOne], ShowingCards[theChoosenOne]);
            else if (theChoosenOne != -1) CButtonShowing[theChoosenOne].BackColor = Color.GhostWhite;
            if (DL[dn] <= 0)
            {
                MessageBox.Show("Deck empty :<");
                T1D.Text = "Deck 2 empty";
                return;
            }
            CButtonShowing[code].BackColor = Color.Yellow;
            checkBox6.Enabled = true;
            theChoosenOne = code;
        }

        private void T3D_Click(object sender, EventArgs e)
        {
            int code = 14, dn = 2;
            CButtonShowing[code] = sender as Button;
            checkBox6.Checked = true;
            if (TokenG[5] > 0) checkBox6.Text = "Gold: " + (TokenG[5] - 1);
            else checkBox6.Text = "Gold: 0";
            if (theChoosenOne != -1 && theChoosenOne < 12) BtnCardColor(CButtonShowing[theChoosenOne], ShowingCards[theChoosenOne]);
            else if (theChoosenOne != -1) CButtonShowing[theChoosenOne].BackColor = Color.GhostWhite;
            if (DL[dn] <= 0)
            {
                MessageBox.Show("Deck empty :<");
                T1D.Text = "Deck 3 empty";
                return;
            }
            CButtonShowing[code].BackColor = Color.Yellow;
            checkBox6.Enabled = true;
            theChoosenOne = code;
        }
        #endregion

        #region Pick/Unpick 3
        private void Pick3()
        {
            pick3token++;
            if (pick3token == 3)
            {
                foreach (CheckBox item in fp3picktoken.Controls)
                {
                    if (!item.Checked)
                    {
                        item.Enabled = false;
                    }
                }
            }
        }

        private void Unpick3()
        {
            pick3token--;
            foreach (CheckBox item in fp3picktoken.Controls)
            {
                if (!item.Checked)
                {
                    item.Enabled = true;
                }
            }
            if (pick3token == 0)
            {
                fP2picktoken.Enabled = true;
                CardGame.Enabled = true;
                checkBox6.Enabled = true;
            }
            UnableControlsToken();
        }
        #endregion

        #region CheckBoxes
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ckb = sender as CheckBox;

            if (changeturn) return;

            if (ckb.Checked)
            {
                Pick3();
                TokenG[0]--;
                UnableOtherAction();
                ckb.Text = "Black: " + TokenG[0].ToString();
                info[currentturn].blackToken++;
                ShowAgainToken();
            }
            else
            {
                TokenG[0]++;
                ckb.Text = "Black: " + TokenG[0].ToString();
                info[currentturn].blackToken--;
                ShowAgainToken();
                Unpick3();
            }
            checkBox7.Text = ckb.Text;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ckb = sender as CheckBox;

            if (changeturn) return;

            if (ckb.Checked)
            {
                Pick3();
                TokenG[1]--;
                UnableOtherAction();
                ckb.Text = "White: " + TokenG[1].ToString();
                info[currentturn].whiteToken++;
                ShowAgainToken();
            }
            else
            {
                TokenG[1]++;
                ckb.Text = "White: " + TokenG[1].ToString();
                info[currentturn].whiteToken--;
                ShowAgainToken();
                Unpick3();
            }
            checkBox8.Text = ckb.Text;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ckb = sender as CheckBox;

            if (changeturn) return;

            if (ckb.Checked)
            {
                Pick3();
                TokenG[2]--;
                UnableOtherAction();
                ckb.Text = "Red: " + TokenG[2].ToString();
                info[currentturn].redToken++;
                ShowAgainToken();
            }
            else
            {
                TokenG[2]++;
                ckb.Text = "Red: " + TokenG[2].ToString();
                info[currentturn].redToken--;
                ShowAgainToken();
                Unpick3();
            }
            checkBox9.Text = ckb.Text;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ckb = sender as CheckBox;

            if (changeturn) return;

            if (ckb.Checked)
            {
                Pick3();
                TokenG[3]--;
                UnableOtherAction();
                ckb.Text = "Blue: " + TokenG[3].ToString();
                info[currentturn].blueToken++;
                ShowAgainToken();
            }
            else
            {
                TokenG[3]++;
                ckb.Text = "Blue: " + TokenG[3].ToString();
                info[currentturn].blueToken--;
                ShowAgainToken();
                Unpick3();
            }
            checkBox10.Text = ckb.Text;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ckb = sender as CheckBox;

            if (changeturn) return;

            if (ckb.Checked)
            {
                Pick3();
                TokenG[4]--;
                UnableOtherAction();
                ckb.Text = "Green: " + TokenG[4].ToString();
                info[currentturn].greenToken++;
                ShowAgainToken();
            }
            else
            {
                TokenG[4]++;
                ckb.Text = "Green: " + TokenG[4].ToString();
                info[currentturn].greenToken--;
                ShowAgainToken();
                Unpick3();
            }
            checkBox11.Text = ckb.Text;
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked)
            {
                if (TokenG[5] > 0) checkBox6.Text = "Gold: " + (TokenG[5] - 1);
                else checkBox6.Text = "Gold: " + TokenG[5];
                fp3picktoken.Enabled = false;
                fP2picktoken.Enabled = false;
            }
            else
            if (!changeturn)
            {
                if (theChoosenOne != -1 && theChoosenOne < 12) BtnCardColor(CButtonShowing[theChoosenOne], ShowingCards[theChoosenOne]);
                else if (theChoosenOne != -1) CButtonShowing[theChoosenOne].BackColor = Color.GhostWhite;
                theChoosenOne = -1;
                checkBox6.Text = "Gold: " + TokenG[5];
                fp3picktoken.Enabled = true;
                fP2picktoken.Enabled = true;
            }
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ckb = sender as CheckBox;

            if (changeturn) return;

            if (ckb.Checked)
            {
                TokenG[0] -= 2;

                ///Unable 
                fp3picktoken.Enabled = false;
                CardGame.Enabled = false;
                foreach (CheckBox item in fP2picktoken.Controls)
                    if (!item.Checked) item.Enabled = false;
                checkBox6.Enabled = false;

                ckb.Text = "Black: " + TokenG[0].ToString();
                info[currentturn].blackToken += 2;
                ShowAgainToken();
            }
            else
            {
                TokenG[0] += 2;

                ///Enable 
                fp3picktoken.Enabled = true;
                CardGame.Enabled = true;
                foreach (CheckBox item in fP2picktoken.Controls) item.Enabled = true;
                checkBox6.Enabled = true;
                UnableControlsToken();

                ckb.Text = "Black: " + TokenG[0].ToString();
                info[currentturn].blackToken -= 2;
                ShowAgainToken();
            }
            checkBox1.Text = ckb.Text;
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ckb = sender as CheckBox;

            if (changeturn) return;

            if (ckb.Checked)
            {
                TokenG[1] -= 2;

                ///Unable 
                fp3picktoken.Enabled = false;
                CardGame.Enabled = false;
                foreach (CheckBox item in fP2picktoken.Controls)
                    if (!item.Checked) item.Enabled = false;
                checkBox6.Enabled = false;

                ckb.Text = "White: " + TokenG[1].ToString();
                info[currentturn].whiteToken += 2;
                ShowAgainToken();
            }
            else
            {
                TokenG[1] += 2;

                ///Enable 
                fp3picktoken.Enabled = true;
                CardGame.Enabled = true;
                foreach (CheckBox item in fP2picktoken.Controls) item.Enabled = true;
                checkBox6.Enabled = true;
                UnableControlsToken();

                ckb.Text = "White: " + TokenG[1].ToString();
                info[currentturn].whiteToken -= 2;
                ShowAgainToken();
            }
            checkBox2.Text = ckb.Text;
        }


        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ckb = sender as CheckBox;

            if (changeturn) return;

            if (ckb.Checked)
            {
                TokenG[2] -= 2;

                ///Unable 
                fp3picktoken.Enabled = false;
                CardGame.Enabled = false;
                foreach (CheckBox item in fP2picktoken.Controls)
                    if (!item.Checked) item.Enabled = false;
                checkBox6.Enabled = false;

                ckb.Text = "Red: " + TokenG[2].ToString();
                info[currentturn].redToken += 2;
                ShowAgainToken();
            }
            else
            {
                TokenG[2] += 2;

                ///Enable 
                fp3picktoken.Enabled = true;
                CardGame.Enabled = true;
                foreach (CheckBox item in fP2picktoken.Controls) item.Enabled = true;
                checkBox6.Enabled = true;
                UnableControlsToken();

                ckb.Text = "Red: " + TokenG[2].ToString();
                info[currentturn].redToken -= 2;
                ShowAgainToken();
            }
            checkBox3.Text = ckb.Text;
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ckb = sender as CheckBox;

            if (changeturn) return;

            if (ckb.Checked)
            {
                TokenG[3] -= 2;

                ///Unable 
                fp3picktoken.Enabled = false;
                CardGame.Enabled = false;
                foreach (CheckBox item in fP2picktoken.Controls)
                    if (!item.Checked) item.Enabled = false;
                checkBox6.Enabled = false;

                ckb.Text = "Blue: " + TokenG[3].ToString();
                info[currentturn].blueToken += 2;
                ShowAgainToken();
            }
            else
            {
                TokenG[3] += 2;

                ///Enable 
                fp3picktoken.Enabled = true;
                CardGame.Enabled = true;
                foreach (CheckBox item in fP2picktoken.Controls) item.Enabled = true;
                checkBox6.Enabled = true;
                UnableControlsToken();

                ckb.Text = "Blue: " + TokenG[3].ToString();
                info[currentturn].blueToken -= 2;
                ShowAgainToken();
            }
            checkBox4.Text = ckb.Text;
        }

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ckb = sender as CheckBox;

            if (changeturn) return;

            if (ckb.Checked)
            {
                TokenG[4] -= 2;

                ///Unable 
                fp3picktoken.Enabled = false;
                CardGame.Enabled = false;
                foreach (CheckBox item in fP2picktoken.Controls)
                    if (!item.Checked) item.Enabled = false;
                checkBox6.Enabled = false;

                ckb.Text = "Green: " + TokenG[4].ToString();
                info[currentturn].greenToken += 2;
                ShowAgainToken();
            }
            else
            {
                TokenG[4] += 2;

                ///Enable 
                fp3picktoken.Enabled = true;
                CardGame.Enabled = true;
                foreach (CheckBox item in fP2picktoken.Controls) item.Enabled = true;
                checkBox6.Enabled = true;
                UnableControlsToken();

                ckb.Text = "Green: " + TokenG[4].ToString();
                info[currentturn].greenToken -= 2;
                ShowAgainToken();
            }
            checkBox5.Text = ckb.Text;
        }
        #endregion
    }
}
