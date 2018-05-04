using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sweeps.DataTypes;

namespace Sweeps.AI.Solvers
{
    class RandomSolver : SolverBase
    {
        private readonly Random _random; 

        public RandomSolver()
        {
            _random = new Random(Environment.TickCount);
        }

        protected override async Task SolveInternal()
        {
            while (!IsCancelled)
            {
                int x = _random.Next(0, Cells.Count - 1);
                int y = _random.Next(0, Cells[0].Count - 1);
                ICell cell = Cells[x][y];

                if (cell.State == CellState.New)
                {
                    await Reveal(cell);
                }
            }
            foreach (ICell cell in Cells.SelectMany(c => c))
            {
                if (IsCancelled)
                {
                    return;
                }

                if (cell.State == CellState.New)
                {
                    await Reveal(cell);
                }
            }
        }
    }
}
