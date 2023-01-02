using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pathfinder_ROHEE_Guillaume_Version_Graph
{
    public partial class Grille : Form
    {
        int SizeGrid = 15;
        Button[,] Grid = null;
        bool DepSet = false;
        bool ArrSet = false;

        public Grille()
        {
            InitializeComponent();
        }

        private void Generate_Weight()
        {
            Random rand = new Random();

            for (int i = 0; i < SizeGrid; i++)
            {
                for (int j = 0; j < SizeGrid; j++)
                {
                    Grid[i, j].Text = rand.Next(10).ToString();
                }
            }
        }

        private void Initialisation()
        {
            int grilleX = 0;
            int grilleY = 40;
            this.AutoSize = true;
            this.Width = SizeGrid * 40;

            Grid = new Button[SizeGrid, SizeGrid];

            for (int i = 0; i < SizeGrid; i++)
            {
                for (int j = 0; j < SizeGrid; j++)
                {
                    Grid[i, j] = new Button();
                    Grid[i, j].Size = new Size(35, 35);
                    Grid[i, j].Text = "1";
                    Grid[i, j].Location = new Point(grilleX, grilleY);
                    Grid[i, j].Click += new EventHandler(ClickCase);

                    Controls.Add(Grid[i, j]);
                    grilleX = grilleX + 40;
                }
                grilleY = grilleY + 40;
                grilleX = 0;
            }

            CheckBox ActiveWeight = new CheckBox();

            Button ExecAlgobtn = new Button();
            ExecAlgobtn.Width = 250;
            ExecAlgobtn.Height = 40;
            ExecAlgobtn.Text = "Démarrer le pathfinder";
            ExecAlgobtn.Click += new EventHandler((sender, e) => ExecAlgo(sender, e, ActiveWeight));
            ExecAlgobtn.Location = new Point((Width / 2 - ExecAlgobtn.Width / 2), Height - (ExecAlgobtn.Height - 10));
            this.Controls.Add(ExecAlgobtn);

            
            ActiveWeight.Width = 150;
            ActiveWeight.Height = 40;
            ActiveWeight.Text = "Activer le calcul du poids";
            ActiveWeight.Location = new Point((Width / 2 - ActiveWeight.Width / 2), Height - (ActiveWeight.Height - 10));
            this.Controls.Add(ActiveWeight);

            Button ResetFormbtn = new Button();
            ResetFormbtn.Width = 150;
            ResetFormbtn.Height = 40;
            ResetFormbtn.Text = "Réinitialiser";
            ResetFormbtn.Click += new EventHandler(ResetForm);
            ResetFormbtn.Location = new Point((Width / 2 - ResetFormbtn.Width / 2), Height - (ResetFormbtn.Height - 10));
            this.Controls.Add(ResetFormbtn);

        }

        private void Grille_Load(object sender, EventArgs e)
        {
            Initialisation();
        }

        private void ExecAlgo(object sender, EventArgs e, CheckBox ActiveWeight)
        {
            int[] originXY = new int[2];
            int[] destXY = new int[2];

            for (int i = 0; i < SizeGrid; i++)
            {
                for (int j = 0; j < SizeGrid; j++)
                {
                    if (Grid[i, j].Text == "D")
                    {
                        originXY[0] = i;
                        originXY[1] = j;
                    }

                    if (Grid[i, j].Text == "A")
                    {
                        destXY[0] = i;
                        destXY[1] = j;
                    }
                }
            }

            if (ActiveWeight.Checked)
            {
                Generate_Weight();
            }

            Pathfinder.MainLoop(Grid, SizeGrid, originXY, destXY);
        }

        private void ClickCase(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            if (btn.Text == "1")
            {
                if (DepSet == false)
                {
                    btn.Text = "D";
                    btn.BackColor = Color.Blue;
                    DepSet = true;
                }
                else if (DepSet == true && ArrSet == false)
                {
                    btn.Text = "A";
                    btn.BackColor = Color.Red;
                    ArrSet = true;
                }
                else if (DepSet == true && ArrSet == true)
                {

                    if (btn.BackColor != Color.Gray)
                    {
                        btn.BackColor = Color.Gray;
                    }
                    else
                    {
                        btn.BackColor = Control.DefaultBackColor;
                    }
                }
            }
            else if (btn.Text == "D")
            {
                btn.Text = "1";
                DepSet = false;
                btn.BackColor = Control.DefaultBackColor;
            }

            
            else if (btn.Text == "A")
            {
                btn.Text = "1";
                ArrSet = false;
                btn.BackColor = Control.DefaultBackColor;

            }          
        }

        private void ResetForm(object sender, EventArgs e)
        {
            this.Controls.Clear();
            this.InitializeComponent();
            Initialisation();
            DepSet = false;
            ArrSet = false;
        }
    }
}
