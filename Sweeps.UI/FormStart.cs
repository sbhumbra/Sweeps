using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sweeps.DataTypes;

namespace Sweeps.UI
{
    public partial class FormStart : Form
    {
        private const double EASY = 0.05;
        private const double MEDIUM = 0.15;
        private const double HARD = 0.20;
        private const double VERY_HARD = 0.25;
        private const double EXTREME = 0.30;
        private const double IMPOSSIBLE = 0.35;

        public int X { get; private set; }

        public int Y { get; private set; }

        public int BombCount { get; private set; }

        public FormStart()
        {
            InitializeComponent();
            CalculateDifficulty();
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            if (CanRunGame())
            {
                X = (int)this.numeric_Width.Value;
                Y = (int)this.numeric_Height.Value;
                BombCount = (int)this.numeric_Mines.Value;
                this.Close();
            }
        }

        bool CanRunGame()
        {
            var x = (int)this.numeric_Width.Value;
            var y = (int)this.numeric_Height.Value;
            var bombCount = (int)this.numeric_Mines.Value;
            if (x < 0 || y < 0)
            {
                MessageBox.Show("board must have positive dimensions");
                return false;
            }

            if (x > 100 || y > 100)
            {
                MessageBox.Show("board side length cannot exceed 100");
                return false;
            }

            if ((x * y) <= bombCount)
            {
                MessageBox.Show("board must have more cells than bombs");
                return false;
            }

            if ((x * y * .85) < bombCount)
            {
                MessageBox.Show("board contains too many bombs");
                return false;
            }
            return true;
        }

        void numeric_Width_ValueChanged(object sender, EventArgs e)
        {
            CalculateDifficulty();
        }

        void numeric_Height_ValueChanged(object sender, EventArgs e)
        {
            CalculateDifficulty();
        }

        void numeric_Mines_ValueChanged(object sender, EventArgs e)
        {
            CalculateDifficulty();
        }

        void CalculateDifficulty()
        {
            int size = (int)numeric_Height.Value * (int)numeric_Width.Value;
            double ratio = (double)numeric_Mines.Value / (double)size;
            if (ratio < EASY)
            {
                lbl_Difficulty.Text = "Trivial";
                lbl_Difficulty.ForeColor = Color.LightGreen;
            }
            else if (ratio < MEDIUM)
            {
                lbl_Difficulty.Text = "Easy";
                lbl_Difficulty.ForeColor = Color.Green;
            }
            else if (ratio < HARD)
            {
                lbl_Difficulty.Text = "Medium";
                lbl_Difficulty.ForeColor = Color.Orange;
            }
            else if (ratio < VERY_HARD)
            {
                lbl_Difficulty.Text = "Hard";
                lbl_Difficulty.ForeColor = Color.OrangeRed;
            }
            else if (ratio < EXTREME)
            {
                lbl_Difficulty.Text = "Very Hard";
                lbl_Difficulty.ForeColor = Color.Red;
            }
            else if (ratio < IMPOSSIBLE)
            {
                lbl_Difficulty.Text = "Extreme";
                lbl_Difficulty.ForeColor = Color.DarkRed;
            }
            else
            {
                lbl_Difficulty.Text = "Near Impossible";
                lbl_Difficulty.ForeColor = Color.Purple;
            }
        }

        protected override void OnShown(EventArgs e)
        {
            X = 0;
            Y = 0;
            BombCount = 0;
            base.OnShown(e);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
        }
    }
}
