using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sweeps.DataTypes;

namespace Sweeps.AI.Solvers
{
    class WimpySolver : SolverBase
    {
        protected override async Task SolveInternal()
        {
            foreach (Cell cell in Cells.SelectMany(c => c))
            {
                if (IsCancelled)
                {
                    return;
                }
                if (cell.State == CellState.New)
                {
                    await ToggleFlag(cell);
                }
            }
        }
    }
}
