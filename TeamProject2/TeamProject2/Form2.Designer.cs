namespace TeamProject2
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.GBOPlayers = new System.Windows.Forms.GroupBox();
            this.FPOP = new System.Windows.Forms.FlowLayoutPanel();
            this.CardGame = new System.Windows.Forms.GroupBox();
            this.TokenGame = new System.Windows.Forms.GroupBox();
            this.lbTurn = new System.Windows.Forms.Label();
            this.EndTurn = new System.Windows.Forms.Button();
            this.GBOPlayers.SuspendLayout();
            this.SuspendLayout();
            // 
            // GBOPlayers
            // 
            this.GBOPlayers.AutoSize = true;
            this.GBOPlayers.Controls.Add(this.FPOP);
            this.GBOPlayers.Location = new System.Drawing.Point(66, 54);
            this.GBOPlayers.Name = "GBOPlayers";
            this.GBOPlayers.Size = new System.Drawing.Size(233, 204);
            this.GBOPlayers.TabIndex = 0;
            this.GBOPlayers.TabStop = false;
            this.GBOPlayers.Text = "Players";
            // 
            // FPOP
            // 
            this.FPOP.AutoSize = true;
            this.FPOP.Location = new System.Drawing.Point(11, 33);
            this.FPOP.Name = "FPOP";
            this.FPOP.Size = new System.Drawing.Size(200, 141);
            this.FPOP.TabIndex = 0;
            // 
            // CardGame
            // 
            this.CardGame.AutoSize = true;
            this.CardGame.Location = new System.Drawing.Point(421, 143);
            this.CardGame.Name = "CardGame";
            this.CardGame.Size = new System.Drawing.Size(300, 174);
            this.CardGame.TabIndex = 1;
            this.CardGame.TabStop = false;
            this.CardGame.Text = "Card";
            // 
            // TokenGame
            // 
            this.TokenGame.Location = new System.Drawing.Point(231, 296);
            this.TokenGame.Name = "TokenGame";
            this.TokenGame.Size = new System.Drawing.Size(300, 150);
            this.TokenGame.TabIndex = 2;
            this.TokenGame.TabStop = false;
            this.TokenGame.Text = "Token";
            // 
            // lbTurn
            // 
            this.lbTurn.AutoSize = true;
            this.lbTurn.Location = new System.Drawing.Point(432, 55);
            this.lbTurn.Name = "lbTurn";
            this.lbTurn.Size = new System.Drawing.Size(59, 25);
            this.lbTurn.TabIndex = 3;
            this.lbTurn.Text = "label1";
            // 
            // EndTurn
            // 
            this.EndTurn.Location = new System.Drawing.Point(320, 224);
            this.EndTurn.Name = "EndTurn";
            this.EndTurn.Size = new System.Drawing.Size(112, 34);
            this.EndTurn.TabIndex = 4;
            this.EndTurn.Text = "End Turn";
            this.EndTurn.UseVisualStyleBackColor = true;
            this.EndTurn.Click += new System.EventHandler(this.EndTurn_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.EndTurn);
            this.Controls.Add(this.lbTurn);
            this.Controls.Add(this.TokenGame);
            this.Controls.Add(this.CardGame);
            this.Controls.Add(this.GBOPlayers);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.GBOPlayers.ResumeLayout(false);
            this.GBOPlayers.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private GroupBox GBOPlayers;
        private FlowLayoutPanel FPOP;
        private GroupBox CardGame;
        private GroupBox TokenGame;
        private Label lbTurn;
        private Button EndTurn;
    }
}