using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sweeps.DataTypes
{
    interface ICell : IPublicCell
    {
        bool IsBomb { get; }
        
        bool TryBombify();

        void AssignNearBy(ICell nearbyCell);

        void TryReveal();

        void ToggleFlag();

        event EventHandler Exploded;
        
        event EventHandler Revealed;

        event EventHandler Bombified;

        event EventHandler<FlaggedEventArgs> CellFlagged;
        
        event EventHandler<FlaggedEventArgs> CellUnflagged;
        
        event EventHandler<MouseEventArgs> Click;
    }
}
