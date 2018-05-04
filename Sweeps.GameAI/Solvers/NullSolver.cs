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
        protected override async Task SolveInternal()
        {
            await base.Pause();
        }
    }
}
