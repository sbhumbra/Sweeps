using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sweeps.DataTypes;

namespace Sweeps.BusinessLogic
{
    class EmptyBoardCreator
    {
        public Board CreateBoard(int x, int y)
        {
            if (x < 5 || y < 5)
            {
                throw new Exception("minimum board size is 5x5");
            }

            Board board = CreateDudBoard(x, y);
            SubscribeCellsToNeighbors(x, y, board);
            return board;
        }

        Board CreateDudBoard(int x, int y)
        {
            var board = new Board();

            for (int i = 0; i < x; i++)
            {
                var row = new List<ICell>();
                for (int j = 0; j < y; j++)
                {
                    row.Add(new Cell());
                }

                board.AddRow(row);
            }

            return board;
        }

        void SubscribeCellsToNeighbors(int x, int y, Board board)
        {
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    ICell cell = board.Cells[i][j];
                    if (i != 0)
                    {
                        if (j != 0)
                        {
                            cell.AssignNearBy(board.Cells[i - 1][j - 1]);
                        }

                        cell.AssignNearBy(board.Cells[i - 1][j]);
                        if (j < y - 1)
                        {
                            cell.AssignNearBy(board.Cells[i - 1][j + 1]);
                        }
                    }

                    if (j != 0)
                    {
                        cell.AssignNearBy(board.Cells[i][j - 1]);
                    }

                    if (j < y - 1)
                    {
                        cell.AssignNearBy(board.Cells[i][j + 1]);
                    }

                    if (i < x - 1)
                    {
                        if (j != 0)
                        {
                            cell.AssignNearBy(board.Cells[i + 1][j - 1]);
                        }

                        cell.AssignNearBy(board.Cells[i + 1][j]);
                        if (j < y - 1)
                        {
                            cell.AssignNearBy(board.Cells[i + 1][j + 1]);
                        }
                    }
                }
            }
        }
    }
}
