using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sweeps.DataTypes;

namespace Sweeps.UI
{
    class CellControl : Label
    {
        private Color _currentColor;
        private readonly Color _originalColor;

        public CellControl(int x, int y)
        {
            X = x;
            Y = y;
            _currentColor = base.BackColor;
            _originalColor = base.BackColor;
        }

        public int X { get; private set; }

        public int Y { get; private set; }

        public void Assign(ICell cell)
        {
            if (cell == null)
            {
                throw new Exception("cell cannot be null");
            }

            if (Cell != null)
            {
                throw new Exception("cell already assigned");
            }

            Cell = cell;
            cell.Revealed += cell_Revealed;
            cell.Exploded += cell_Exploded;
            cell.CellFlagged += cell_CellFlagged;
            cell.CellUnflagged += cell_CellUnflagged;
            cell.Click += cell_Click;
        }

        void cell_Click(object sender, MouseEventArgs e)
        {
            OnMouseClick(e);
        }

        ICell Cell { get; set; }

        #region Cell Event Handers
        void cell_CellUnflagged(object sender, FlaggedEventArgs e)
        {
            base.BackColor = _originalColor;
            _currentColor = _originalColor;
        }

        void cell_CellFlagged(object sender, FlaggedEventArgs e)
        {
            base.BackColor = CellColours.FlaggedColour;
            _currentColor = CellColours.FlaggedColour;
        }

        void cell_Revealed(object sender, EventArgs e)
        {
            _currentColor = CellColours.RevealedColour;
            base.BackColor = CellColours.RevealedColour;
            if (Cell.NearByBombCount > 0)
            {
                this.Text = Cell.NearByBombCount.ToString();
            }
            else
            {
                this.Text = string.Empty;
            }
        }

        void cell_Exploded(object sender, EventArgs e)
        {
            _currentColor = CellColours.ExplodedColour;
            base.BackColor = CellColours.ExplodedColour;
            this.Text = "X";
        }
        #endregion

        #region Mouse Event Handlers
        protected override void OnMouseEnter(EventArgs e)
        {
            if (Cell != null && Cell.State == CellState.New)
            {
                base.BackColor = CellColours.HoverColour;
            }
            base.OnMouseHover(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (Cell != null && Cell.State == CellState.New)
            {
                base.BackColor = _currentColor;
            }
            base.OnMouseLeave(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (Cell == null)
            {
                OnStartGame();
            }

            if (Cell == null)
            {
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
                Cell.TryReveal();
            }
            else if (e.Button == MouseButtons.Right)
            {
                Cell.ToggleFlag();
            }

            base.OnMouseClick(e);
        }

        public event EventHandler<CellEventArgs> StartGame;

        public void OnStartGame()
        {
            EventHandler<CellEventArgs> startGame = StartGame;
            if (startGame != null)
            {
                startGame(this, new CellEventArgs(X, Y));
            }
        }
        #endregion
    }
}
