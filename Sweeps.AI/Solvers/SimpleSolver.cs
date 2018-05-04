using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sweeps.DataTypes;

namespace Sweeps.AI.Solvers
{
    class SimpleSolver : SolverBase
    {
        private readonly Random _random;
        private bool _isFreshBoard;

        public SimpleSolver(List<List<Cell>> cells)
            : base(cells)
        {
            _random = new Random(Environment.TickCount);
            _isFreshBoard = true;
        }

        protected override async Task SolveInternal()
        {
            while (!IsCancelled)
            {
                if (_isFreshBoard && IsFreshBoard())
                {
                    RevealRandom();
                }
                else
                {
                    await SweepBoard();
                }
            }
        }

        void RevealRandom()
        {
            Reveal(GetRandomNewCell());
        }

        Cell GetRandomNewCell()
        {
            int x = _random.Next(0, Cells.Count - 1);
            int y = _random.Next(0, Cells[0].Count - 1);
            Cell cell = Cells[x][y];
            if (cell.State != CellState.New)
            {
                return GetRandomNewCell();
            }
            return cell;
        }

        bool IsFreshBoard()
        {
            _isFreshBoard = Cells
                .SelectMany(c => c)
                .All(c => c.State == CellState.New);

            return _isFreshBoard;
        }

        async Task SweepBoard()
        {
            foreach (Cell cell in Cells.SelectMany(c => c))
            {
                if (IsCancelled)
                {
                    return;
                }

                if (cell.State == CellState.New)
                {
                    Reveal(cell);
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
