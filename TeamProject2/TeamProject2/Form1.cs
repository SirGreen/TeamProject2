namespace TeamProject2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void GetNumOfPlayers(int players)
        {
            Form2 f2 = new Form2();
            f2.NumOfPlayers = players;
        }

        private void ButtonSolve()
        {
            this.Hide();
            
            Form2 f2 = new Form2();
            f2.ShowDialog();

            this.Show();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            GetNumOfPlayers(2);
            ButtonSolve();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GetNumOfPlayers(3);
            ButtonSolve();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GetNumOfPlayers(4);
            ButtonSolve();
        }
    }
}