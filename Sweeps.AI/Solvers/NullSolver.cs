using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sweeps.DataTypes;

namespace Sweeps.AI.Solvers
{
    class NullSolver : SolverBase
    {
        public NullSolver(List<List<Cell>> cells)
            : base(cells)
        {
        }

        protected override async Task SolveInternal()
        {
            await AIHelper.Pause();
        }
    }
}
