using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sweeps.DataTypes;

namespace Sweeps.BusinessLogic
{
    class Board
    {
        public Board()
        {
            Cells = new List<List<ICell>>();
        }

        public void AddRow(List<ICell> row)
        {
            Cells.Add(row);
            row.ForEach(cell => cell.Exploded += cell_Exploded);
            row.ForEach(cell => cell.Revealed += cell_Revealed);
            row.ForEach(cell => cell.CellFlagged += cell_CellFlagged);
            row.ForEach(cell => cell.CellUnflagged += cell_CellUnflagged);
        }

        public List<List<ICell>> Cells { get; private set; }

        void cell_Exploded(object sender, EventArgs e)
        {
            OnExplosion();
        }

        void cell_Revealed(object sender, EventArgs e)
        {
            OnRevealed();
        }

        void cell_CellFlagged(object sender, FlaggedEventArgs e)
        {
            OnCellFlagged(e);
        }

        void cell_CellUnflagged(object sender, FlaggedEventArgs e)
        {
            OnCellUnflagged(e);
        }

        public event EventHandler Explosion;

        protected virtual void OnExplosion()
        {
            EventHandler explosion = Explosion;
            if (explosion != null)
            {
                explosion(this, new EventArgs());
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

        protected virtual void OnCellFlagged(FlaggedEventArgs e)
        {
            EventHandler<FlaggedEventArgs> cellFlagged = CellFlagged;
            if (cellFlagged != null)
            {
                cellFlagged(this, e);
            }
        }

        public event EventHandler<FlaggedEventArgs> CellUnflagged;

        protected virtual void OnCellUnflagged(FlaggedEventArgs e)
        {
            EventHandler<FlaggedEventArgs> cellUnflagged = CellUnflagged;
            if (cellUnflagged != null)
            {
                cellUnflagged(this, e);
            }
        }
    }
}
