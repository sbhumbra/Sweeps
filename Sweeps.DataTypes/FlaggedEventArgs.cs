using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sweeps.DataTypes
{
    public class FlaggedEventArgs : EventArgs
    {
        public FlaggedEventArgs(bool isBomb)
        {
            IsBomb = isBomb;
        }

        public bool IsBomb { get; private set; }
    }
}
