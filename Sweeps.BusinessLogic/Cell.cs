﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sweeps.DataTypes
{
    class Cell : ICell
    {
        public Cell()
        {
            NearbyCells = new List<IPublicCell>();
            State = CellState.New;
        }

        public CellState State { get; private set; }

        public bool IsBomb { get; private set; }

        public int NearByBombCount { get; private set; }

        public List<IPublicCell> NearbyCells { get; private set; }

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

        public bool TryBombify()
        {
            if (IsBomb)
            {
                return false;
            }

            IsBomb = true;
            OnBombified();
            return true;
        }

        public void AssignNearBy(ICell nearbyCell)
        {
            nearbyCell.Bombified += (o, e) => NearByBombCount++;
            NearbyCells.Add(nearbyCell);
        }

        public void TryReveal()
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
                    NearbyCells
                        .OfType<ICell>()
                        .ToList()
                        .ForEach(c => c.TryReveal());
                }
            }
        }

        public void ToggleFlag()
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

        public event EventHandler Bombified;

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
