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

        public SimpleSolver()
        {
            _random = new Random(Environment.TickCount);
        }

        protected override async Task SolveInternal()
        {
            while (!IsCancelled)
            {
                await SweepBoard();
            }
        }

        protected async Task DumbGuess()
        {
            await Reveal(GetRandomNewCell());
        }

        protected IPublicCell GetRandomNewCell()
        {
            List<IPublicCell> newCells = Cells
                .SelectMany(c => c)
                .Where(c => c.State == CellState.New)
                .ToList();

            if (!newCells.Any())
            {
                return Cells[0][0];
            }
            else
            {
                return newCells[_random.Next(0, newCells.Count - 1)];
            }
        }

        async Task SweepBoard()
        {
            bool informationGained = await TrySimpleSweep();
            if (!informationGained)
            {
                await DumbGuess();
            }
        }

        protected async Task<bool> TrySimpleSweep()
        {
            bool informationGained = false;
            foreach (IPublicCell cell in Cells.SelectMany(c => c))
            {
                if (IsCancelled)
                {
                    return false;
                }

                if (cell.State == CellState.Flagged)
                {
                    continue;
                }

                if (cell.State == CellState.New)
                {
                    continue;
                }

                if (cell.ApparentNumber == 8)
                {
                    foreach (IPublicCell nearByCell in cell.NearbyCells)
                    {
                        await Reveal(nearByCell);
                        informationGained = true;
                    }
                }

                int unknowns = 0;
                int knownBombs = 0;
                foreach (IPublicCell nearByCell in cell.NearbyCells)
                {
                    if (nearByCell.State == CellState.New)
                    {
                        unknowns++;
                    }

                    if (nearByCell.State == CellState.Flagged)
                    {
                        knownBombs++;
                    }
                }

                int bombsLeft = cell.ApparentNumber - knownBombs;

                if (bombsLeft >= unknowns)
                {
                    foreach (IPublicCell nearByCell in cell.NearbyCells)
                    {
                        if (nearByCell.State == CellState.New)
                        {
                            await ToggleFlag(nearByCell);
                            informationGained = true;
                        }
                    }
                }

                if (bombsLeft <= 0)
                {
                    foreach (IPublicCell nearByCell in cell.NearbyCells)
                    {
                        if (nearByCell.State == CellState.New)
                        {
                            await Reveal(nearByCell);
                            informationGained = true;
                        }
                    }
                }
            }

            return informationGained;
        }
    }
}
