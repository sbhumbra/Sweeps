using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sweeps.DataTypes;

namespace Sweeps.AI
{
    public abstract class SolverBase
    {
        protected bool IsCancelled { get; private set; }

        public List<List<Cell>> Cells { get; private set; }

        public SolverBase()
        {
            IsCancelled = false;
        }

        public async Task Solve(List<List<Cell>> cells, int x = 0, int y = 0)
        {
            IsCancelled = false;

            Cells = cells;
            if (cells == null)
            {
                throw new Exception("cells cannot be null");
            }

            if (cells.Any(r => r == null))
            {
                throw new Exception("rows cannot be null");
            }

            if (cells.SelectMany(c => c).Any(c => c == null))
            {
                throw new Exception("no cell can be null");
            }

            if (IsFreshBoard())
            {
                await Reveal(Cells[x][y]);
            }

            await SolveInternal();
        }

        protected abstract Task SolveInternal();

        public void ForceStop()
        {
            IsCancelled = true;
        }

        protected async Task Reveal(Cell cell)
        {
            if (IsCancelled)
            {
                return;
            }

            cell.OnClick(MouseButtons.Left);
            await Pause();
        }

        protected async Task ToggleFlag(Cell cell)
        {
            if (IsCancelled)
            {
                return;
            }

            cell.OnClick(MouseButtons.Right);
            await Pause();
        }

        bool IsFreshBoard()
        {
            return Cells
                .SelectMany(c => c)
                .All(c => c.State == CellState.New);
        }

        const int Speed = 1000;

        protected async Task Pause()
        {
            await Task.Delay(Speed);
        }
    }
}
