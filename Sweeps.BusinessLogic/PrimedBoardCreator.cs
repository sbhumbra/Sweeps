using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sweeps.BusinessLogic
{
    class PrimedBoardCreator
    {
        private readonly Random _random;

        public PrimedBoardCreator()
        {
            _random = new Random(Environment.TickCount);
        }

        public Board CreateBoard(int x, int y, int bombCount)
        {
            Board board = new EmptyBoardCreator().CreateBoard(x, y);
            
            int bombsPlaced = 0;
            while(bombsPlaced < bombCount)
            {
                if (board.Cells[_random.Next(0, x)][_random.Next(0, y)].TryBombify())
                {
                    bombsPlaced++;
                }
            }

            return board;
        }
    }
}
