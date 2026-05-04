using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class Form1 : Form
    {
        private Game game = new Game();
        private bool isGameActive = false;
        private bool isGameOver = false;
        private bool isBotMode = false;
        private readonly BotPlayer _bot = new BotPlayer();

        private Button[,] buttons = new Button[3, 3];

        public Form1(bool botMode)
        {
            InitializeComponent();
            isBotMode = botMode;
            InitializeBoard();
        }

        // ✅ МЕТОД ИНИЦИАЛИЗАЦИИ ДОСКИ
        private void InitializeBoard()
        {
            textBox1.ReadOnly = true;

            // Назначение координат кнопкам
            button1.Tag = (0, 0);
            button2.Tag = (0, 1);
            button3.Tag = (0, 2);
            button4.Tag = (1, 0);
            button5.Tag = (1, 1);
            button6.Tag = (1, 2);
            button7.Tag = (2, 0);
            button8.Tag = (2, 1);
            button9.Tag = (2, 2);

            // Сохраняем ссылки на кнопки в массив
            buttons[0, 0] = button1;
            buttons[0, 1] = button2;
            buttons[0, 2] = button3;
            buttons[1, 0] = button4;
            buttons[1, 1] = button5;
            buttons[1, 2] = button6;
            buttons[2, 0] = button7;
            buttons[2, 1] = button8;
            buttons[2, 2] = button9;

            // Подписка на событие победы
            game.OnWin += HandleWin;

            // Установка начального текста
            if (isBotMode)
                textBox1.Text = "Режим: Игрок (X) vs Бот (O)";
            else
                textBox1.Text = "Режим: 2 Игрока";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (isGameActive) return;

            game.CurrentPlayer = Game.Player.X;
            textBox1.Text = isBotMode ? "Режим: Игрок (X) vs Бот (O)" : "Ходит X";
            textBox1.SelectionLength = 0;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (isGameActive) return;

            game.CurrentPlayer = Game.Player.O;
            textBox1.Text = isBotMode ? "Режим: Бот (X) vs Игрок (O)" : "Ходит O";
            textBox1.SelectionLength = 0;
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            foreach (Control control in this.Controls)
            {
                if (control is Button btn && btn.Tag is ValueTuple<int, int>)
                {
                    btn.Text = "";
                    btn.ForeColor = Color.Black;
                    btn.Enabled = true;
                }
            }

            game.Reset();
            textBox1.Text = "Поле очищено!";
            textBox1.SelectionLength = 0;

            isGameActive = false;
            isGameOver = false;
            button11.Enabled = true;
            button12.Enabled = true;

            textBox1.Text = isBotMode ? "Режим: Игрок (X) vs Бот (O)" : "Поле очищено!";
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (isGameOver) return;

            Button btn = sender as Button;
            if (btn.Text != "") return;
            if (game.CurrentPlayer == Game.Player.None) return;

            var (row, col) = ((int, int))btn.Tag;

            if (!game.MakeMove(row, col, game.CurrentPlayer)) return;

            btn.Text = game.CurrentPlayer.ToString();
            btn.ForeColor = game.CurrentPlayer == Game.Player.X ? Color.Blue : Color.Red;

            if (!isGameActive)
            {
                isGameActive = true;
                button11.Enabled = false;
                button12.Enabled = false;
            }

            var winLine = game.CheckWinnerAndRaise();
            if (winLine != null) return;

            if (game.IsDraw())
            {
                textBox1.Text = "Ничья!";
                textBox1.SelectionLength = 0;
                isGameOver = true;
                DisableGameButtons();
                return;
            }

            game.SwitchPlayer();
            textBox1.Text = $"Ходит {game.CurrentPlayer}";
            textBox1.SelectionLength = 0;

            // Если включен режим бота и сейчас ход Бота (O)
            if (isBotMode && game.CurrentPlayer == Game.Player.O && !isGameOver)
            {
                System.Threading.Thread.Sleep(400);
                MakeBotMove();
            }
        }

        private void MakeBotMove()
        {
            string[,] boardState = new string[3, 3];
            for (int r = 0; r < 3; r++)
                for (int c = 0; c < 3; c++)
                    boardState[r, c] = buttons[r, c].Text;

            var (bestRow, bestCol) = _bot.GetBestMove(boardState, "O", "X");

            if (bestRow != -1 && bestCol != -1)
            {
                Button_Click(buttons[bestRow, bestCol], EventArgs.Empty);
            }
        }

        private void HandleWin(Game.Player winner, List<(int, int)> winLine)
        {
            textBox1.Text = $"Победа: {winner}";
            textBox1.SelectionLength = 0;
            isGameOver = true;
            DisableGameButtons();

            foreach (var (r, c) in winLine)
            {
                if (r >= 0 && r < 3 && c >= 0 && c < 3)
                {
                    var btn = buttons[r, c];
                    if (btn != null) btn.ForeColor = Color.Green;
                }
            }
        }

        private void DisableGameButtons()
        {
            foreach (Control control in this.Controls)
            {
                if (control is Button btn && btn.Tag is ValueTuple<int, int>)
                {
                    btn.Enabled = false;
                }
            }
        }
    }
}