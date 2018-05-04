using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sweeps.DataTypes;

namespace Sweeps.AI
{
    abstract class SolverBase
    {
        protected bool IsCancelled { get; private set; }

        public List<List<Cell>> Cells { get; private set; }

        public SolverBase(List<List<Cell>> cells)
        {
            IsCancelled = false;
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

            Cells = cells;
        }

        public async Task Solve()
        {
            if (IsCancelled)
            {
                return;
            }
            await SolveInternal();
        }

        protected abstract Task SolveInternal();

        public void ForceStop()
        {
            IsCancelled = true;
        }

        protected void Reveal(Cell cell)
        {
            if (IsCancelled)
            {
                return;
            }

            cell.OnClick(MouseButtons.Left);
        }

        protected void ToggleFlag(Cell cell)
        {
            if (IsCancelled)
            {
                return;
            }

            cell.OnClick(MouseButtons.Right);
        }
    }
}
