using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sweeps.DataTypes
{
    public class Cell
    {
        public Cell()
        {
            NearbyCells = new List<Cell>();
            State = CellState.New;
        }

        public CellState State { get; private set; }

        internal bool IsBomb { get; private set; }

        internal int NearByBombCount { get; private set; }

        public List<Cell> NearbyCells { get; private set; }

        public int ApparentNumber
        {
            get
            {
                if (State == CellState.Revealed)
                {
                    return NearByBombCount;
                }
                else
                {
                    return int.MinValue;
                }
            }
        }

        internal bool TryBombify()
        {
            if (IsBomb)
            {
                return false;
            }
            IsBomb = true;
            OnBombified();
            return true;
        }

        internal void AssignNearBy(Cell nearbyCell)
        {
            nearbyCell.Bombified += (o, e) => NearByBombCount++;
            NearbyCells.Add(nearbyCell);
        }

        internal void TryReveal()
        {
            if (State == CellState.Flagged || State == CellState.Revealed)
            {
                return;
            }

            State = CellState.Revealed;
            if (IsBomb)
            {
                OnExploded();
            }
            else
            {
                OnRevealed();
                if (NearByBombCount <= 0)
                {
                    NearbyCells.ForEach(c => c.TryReveal());
                }
            }
        }

        internal void ToggleFlag()
        {
            if (State == CellState.Flagged)
            {
                State = CellState.New;
                OnCellUnflagged();
            }
            else if (State == CellState.New)
            {
                State = CellState.Flagged;
                OnCellFlagged();
            }
        }

        event EventHandler Bombified;

        protected virtual void OnBombified()
        {
            EventHandler bombified = Bombified;
            if (bombified != null)
            {
                bombified(this, new EventArgs());
            }
        }

        public event EventHandler Exploded;

        protected virtual void OnExploded()
        {
            EventHandler exploded = Exploded;
            if (exploded != null)
            {
                exploded(this, new EventArgs());
            }
        }

        public event EventHandler Revealed;

        protected virtual void OnRevealed()
        {
            EventHandler revealed = Revealed;
            if (revealed != null)
            {
                revealed(this, new EventArgs());
            }
        }

        public event EventHandler<FlaggedEventArgs> CellFlagged;

        protected virtual void OnCellFlagged()
        {
            EventHandler<FlaggedEventArgs> cellFlagged = CellFlagged;
            if (cellFlagged != null)
            {
                cellFlagged(this, new FlaggedEventArgs(IsBomb));
            }
        }

        public event EventHandler<FlaggedEventArgs> CellUnflagged;

        protected virtual void OnCellUnflagged()
        {
            EventHandler<FlaggedEventArgs> cellUnflagged = CellUnflagged;
            if (cellUnflagged != null)
            {
                cellUnflagged(this, new FlaggedEventArgs(IsBomb));
            }
        }

        public event EventHandler<MouseEventArgs> Click;

        public void OnClick(MouseButtons button)
        {
            EventHandler<MouseEventArgs> click = Click;
            if (click != null)
            {
                click(this, new MouseEventArgs(button, 1, 0, 0, 0));
            }
        }
    }
}
