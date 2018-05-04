using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sweeps.DataTypes;
using Sweeps.BusinessLogic;
using Sweeps.AI;

namespace Sweeps.UI
{
    public partial class FormGame : Form
    {
        private const int X_OFFSET = 50;
        private const int Y_OFFSET = 80;
        private const int X_SIZE = 30;
        private const int Y_SIZE = 30;

        private readonly PrimedBoardCreator _boardCreator;
        private readonly SolverBase _solver;
        private readonly object _timerLock;

        private FormStart _start;
        private System.Threading.Timer _timer;
        private List<List<CellControl>> _cells;
        private int _flagCount;
        private int _bombCount;
        private int _nonBombCount;
        private int _x;
        private int _y;
        private int _secondsPassed;
        private int _revealedCount;
        private bool _bombHit;
        private Board _board;

        public FormGame()
        {
            _boardCreator = new PrimedBoardCreator();
            _cells = new List<List<CellControl>>();
            _timerLock = new object();
            _solver = SolverFactory.CreateSolver(AIType.Simple);
            InitializeComponent();
        }

        void StartNewGame(int x, int y, int bombCount)
        {
            foreach (CellControl cell in _cells.SelectMany(c => c))
            {
                this.Controls.Remove(cell);
                cell.Enabled = false;
                cell.Visible = false;
                cell.StartGame -= cellControl_StartGame;
                cell.Dispose();
                if (_board != null)
                {
                    _board.Explosion -= _board_Explosion;
                    _board.CellFlagged -= _board_CellFlagged;
                    _board.CellUnflagged -= _board_CellUnflagged;
                    _board.Revealed -= _board_Revealed;
                }
            }

            _x = x;
            _y = y;
            _revealedCount = 0;
            _secondsPassed = 0;
            _flagCount = 0;
            _bombCount = bombCount;
            _bombHit = false;
            _nonBombCount = (_x * _y) - bombCount;
            _cells = new List<List<CellControl>>();

            for (int i = 0; i < _x; i++)
            {
                _cells.Add(new List<CellControl>());
                for (int j = 0; j < _y; j++)
                {
                    var cellControl = new CellControl(i, j);
                    cellControl.StartGame += cellControl_StartGame;
                    cellControl.Location = new Point(X_OFFSET + (i * X_SIZE), Y_OFFSET + (j * Y_SIZE));
                    cellControl.Name = string.Format("{0} {1}", i, j);
                    cellControl.Size = new System.Drawing.Size(X_SIZE, Y_SIZE);
                    cellControl.BorderStyle = BorderStyle.FixedSingle;
                    _cells.Last().Add(cellControl);
                    this.Controls.Add(cellControl);
                }
            }

            this.ClientSize = new Size((_x * X_SIZE) + (2 * X_OFFSET), (_y * Y_SIZE) + (2 * Y_OFFSET));
            this.MinimumSize = new Size(this.ClientSize.Width, this.ClientSize.Height);
            this.MaximumSize = new Size(this.ClientSize.Width + X_OFFSET, this.ClientSize.Height + Y_OFFSET);
            UpdateMinesText();
        }

        void cellControl_StartGame(object sender, CellEventArgs e)
        {
            CreateAndPopulateBoard(e);
        }

        void EnsureGameBegun()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() => EnsureGameBegun()));
                return;
            }

            UpdateMinesText();
            lock (_timerLock)
            {
                if (_timer == null)
                {
                    _secondsPassed = 0;
                    TimerCallback callBack = new TimerCallback(x => Tick());
                    _timer = new System.Threading.Timer(callBack, null, 1000, 1000);
                    UpdateClock();
                }
            }
        }

        void CreateAndPopulateBoard(CellEventArgs firstCellClicked)
        {
            CreateBoard(firstCellClicked);
            PopulateCells();
        }

        void CreateBoard(CellEventArgs firstCellClicked)
        {
            const int maxAttempts = 10000;
            int attempts = 0;
            _board = CreateBoard();
            while (_board.Cells[firstCellClicked.X][firstCellClicked.Y].IsBomb)
            {
                _board = CreateBoard();
                if (attempts++ > maxAttempts)
                {
                    throw new Exception(string.Format("unable to create board after {0} attempts", maxAttempts));
                }
            }

            _board.Explosion += _board_Explosion;
            _board.CellFlagged += _board_CellFlagged;
            _board.CellUnflagged += _board_CellUnflagged;
            _board.Revealed += _board_Revealed;
        }

        Board CreateBoard()
        {
            try
            {
                return _boardCreator.CreateBoard(_x, _y, _bombCount);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
                return null;
            }
        }

        void PopulateCells()
        {
            for (int i = 0; i < _x; i++)
            {
                for (int j = 0; j < _y; j++)
                {
                    _cells[i][j].Assign(_board.Cells[i][j]);
                }
            }
        }

        void Tick()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() => Tick()));
                return;
            }
            Interlocked.Increment(ref _secondsPassed);
            UpdateClock();
        }

        private void UpdateClock()
        {
            this.lbl_Time.Text = _secondsPassed.ToString() + " seconds";
        }

        void EndGame()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() => EndGame()));
                return;
            }

            _solver.ForceStop();
            _board = null;
            lock (_timerLock)
            {
                if (_timer == null)
                {
                    return;
                }
                _timer.Dispose();
                _timer = null;
                this.lbl_Time.Text = "Game ended in " + this.lbl_Time.Text;
            }
        }

        void _board_Revealed(object sender, EventArgs e)
        {
            Interlocked.Increment(ref _revealedCount);
            EnsureGameBegun();
            DetectWin();
        }

        void _board_Explosion(object sender, EventArgs e)
        {
            _bombHit = true;
            EnsureGameBegun();
            EndGame();
            MessageBox.Show("You hit a bomb. Game over!");
            Controls.OfType<CellControl>().ToList().ForEach(c => c.Enabled = false);
        }

        void _board_CellFlagged(object sender, FlaggedEventArgs e)
        {
            Interlocked.Increment(ref _flagCount);
            EnsureGameBegun();
            DetectWin();
        }

        void _board_CellUnflagged(object sender, FlaggedEventArgs e)
        {
            Interlocked.Decrement(ref _flagCount);
            EnsureGameBegun();
            DetectWin();
        }

        void UpdateMinesText()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() => UpdateMinesText()));
                return;
            }

            lbl_Mines.Text = string.Format("{0} mines left...", _bombCount - _flagCount);
        }

        void DetectWin()
        {
            if (WinCriteria())
            {
                Win();
            }
        }

        void Win()
        {
            EndGame();
            MessageBox.Show("All bombs found. You win!");
            Controls.OfType<CellControl>().ToList().ForEach(c => c.Enabled = false);
        }

        bool WinCriteria()
        {
            return !_bombHit && _revealedCount.Equals(_nonBombCount);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnStartNewGame(10, 10, 5);
        }

        private void easyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnStartNewGame(10, 10, 10);
        }

        private void mediumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnStartNewGame(10, 10, 15);
        }

        private void hardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnStartNewGame(10, 10, 20);
        }

        private void veryHardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnStartNewGame(10, 10, 25);
        }

        private void extremeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnStartNewGame(10, 10, 30);
        }

        private void nearImpossibleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnStartNewGame(10, 10, 35);
        }

        private void customToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _start = new FormStart();
            _start.FormClosed += _start_FormClosed;
            _start.Show();
        }

        void _start_FormClosed(object sender, FormClosedEventArgs e)
        {
            _start.FormClosed -= _start_FormClosed;
            var start = sender as FormStart;
            if (start.BombCount > 0)
            {
                OnStartNewGame(start.X, start.Y, start.BombCount);
            }
        }

        void OnStartNewGame(int x, int y, int bombCount)
        {
            try
            {
                lbl_Time.Text = "Ready...";
                StartNewGame(x, y, bombCount);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void menu_AI_Click(object sender, EventArgs e)
        {
            try
            {
                var random = new Random(Environment.TickCount);
                int x = random.Next(0, _cells.Count - 1);
                int y = random.Next(0, _cells[0].Count - 1);
                if (_board == null)
                {
                    _cells[x][y].OnStartGame();
                }

                await _solver.Solve(_board.Cells, x, y);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(string.Format("the AI made a fatal error... {0}", ex.Message));
            }
        }
    }
}
