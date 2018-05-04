using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sweeps.AI.Solvers;
using Sweeps.DataTypes;

namespace Sweeps.AI
{
    static class SolverFactory
    {
        public static SolverBase CreateSolver(AIType type, List<List<Cell>> cells)
        {
            switch (type)
            {
                case AIType.Sequential:
                    return new SequentialSolver(cells);
                case AIType.Random:
                    return new RandomSolver(cells);
                case AIType.Wimpy:
                    return new WimpySolver(cells);
                case AIType.Simple:
                    return new SimpleSolver(cells);
                default:
                    return new NullSolver(cells);
            }
        }
    }
}
