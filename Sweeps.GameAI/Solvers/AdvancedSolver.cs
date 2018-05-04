using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sweeps.DataTypes;

namespace Sweeps.AI.Solvers
{
    // TODO: implement look ahead techniques
    class AdvancedSolver : SimpleSolver
    {
        protected override async Task SolveInternal()
        {
            while (!IsCancelled)
            {
                await SweepBoard();
            }
        }

        async Task SweepBoard()
        {
            bool informationGained = await TrySimpleSweep();
            if (informationGained)
            {
                return;
            }

            informationGained = await TryLookAheadCascade();
            if (informationGained)
            {
                return;
            }


        }

        async Task<bool> TryLookAheadCascade()
        {
            bool informationGained = false;


            return informationGained;
        }
    }
}
