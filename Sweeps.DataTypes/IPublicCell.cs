using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sweeps.DataTypes
{
    public interface IPublicCell
    {
        CellState State { get; }
        
        List<IPublicCell> NearbyCells { get; }

        int ApparentNumber { get; }

        int NearByBombCount { get; }

        void OnClick(MouseButtons button);
    }
}
