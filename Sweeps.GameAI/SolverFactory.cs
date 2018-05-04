using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sweeps.AI.Solvers;
using Sweeps.DataTypes;

namespace Sweeps.AI
{
    public static class SolverFactory
    {
        public static SolverBase CreateSolver(AIType type)
        {
            switch (type)
            {
                case AIType.Random:
                    return new RandomSolver();
                case AIType.Wimpy:
                    return new WimpySolver();
                case AIType.Simple:
                    return new SimpleSolver();
                case AIType.Advanced:
                    return new AdvancedSolver();
                default:
                    return new NullSolver();
            }
        }
    }
}
