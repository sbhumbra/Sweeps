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
        public WimpySolver(List<List<Cell>> cells)
            : base(cells)
        {
        }

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
                    ToggleFlag(cell);
                }
                else
                {
                    continue;
                }

                await AIHelper.Pause();
            }
        }
    }
}
