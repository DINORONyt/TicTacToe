using System;
using System.Collections.Generic;
using System.Drawing;
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

        // Динамические символы
        private string playerSymbol = "X";
        private string botSymbol = "O";

        private Button[,] buttons = new Button[3, 3];

        public Form1(bool botMode)
        {
            InitializeComponent();
            isBotMode = botMode;
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            textBox1.ReadOnly = true;

            button1.Tag = (0, 0); button2.Tag = (0, 1); button3.Tag = (0, 2);
            button4.Tag = (1, 0); button5.Tag = (1, 1); button6.Tag = (1, 2);
            button7.Tag = (2, 0); button8.Tag = (2, 1); button9.Tag = (2, 2);

            buttons[0, 0] = button1; buttons[0, 1] = button2; buttons[0, 2] = button3;
            buttons[1, 0] = button4; buttons[1, 1] = button5; buttons[1, 2] = button6;
            buttons[2, 0] = button7; buttons[2, 1] = button8; buttons[2, 2] = button9;

            game.OnWin += HandleWin;

            if (isBotMode)
                textBox1.Text = "Режим: Игрок vs Бот. Выберите сторону";
            else
                textBox1.Text = "Режим: 2 Игрока";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (isGameActive) return;

            playerSymbol = "X";
            botSymbol = "O";
            game.CurrentPlayer = Game.Player.X;
            isGameActive = true;

            button11.Enabled = false;
            button12.Enabled = false;
            textBox1.Text = "Ваш ход (X)";
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (isGameActive) return;

            playerSymbol = "O";
            botSymbol = "X";
            game.CurrentPlayer = Game.Player.X;

            button11.Enabled = false;
            button12.Enabled = false;
            isGameActive = true;
            textBox1.Text = "Бот думает...";

            // Запуск первого хода бота с небольшой задержкой
            System.Threading.Thread.Sleep(500);
            MakeBotMove();
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
            textBox1.Text = isBotMode ? "Режим: Игрок vs Бот. Выберите сторону" : "Поле очищено!";
            isGameActive = false;
            isGameOver = false;
            button11.Enabled = true;
            button12.Enabled = true;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (isGameOver) return;

            Button btn = sender as Button;
            if (btn.Text != "") return;
            if (game.CurrentPlayer == Game.Player.None) return;

            var (row, col) = ((int, int))btn.Tag;

            // Выполняем ход в логике игры
            if (!game.MakeMove(row, col, game.CurrentPlayer)) return;

            btn.Text = game.CurrentPlayer.ToString();
            btn.ForeColor = game.CurrentPlayer.ToString() == "X" ? Color.Blue : Color.Red;

            // Проверяем победу или ничью
            var winLine = game.CheckWinnerAndRaise();
            if (winLine != null) return;

            if (game.IsDraw())
            {
                textBox1.Text = "Ничья!";
                isGameOver = true;
                DisableGameButtons();
                return;
            }

            // Передаём ход
            game.SwitchPlayer();

            //  Если режим с ботом и сейчас ход бота
            if (isBotMode && game.CurrentPlayer.ToString() == botSymbol && !isGameOver)
            {
                textBox1.Text = "Бот думает...";
                // Небольшая пауза для реалистичности
                System.Threading.Thread.Sleep(500);
                MakeBotMove();
            }
            else
            {
                textBox1.Text = $"Ходит {game.CurrentPlayer}";
            }
        }

        // МЕТОД ХОДА БОТА
        private void MakeBotMove()
        {
            // 1. Собираем текущее состояние поля
            string[,] boardState = new string[3, 3];
            for (int r = 0; r < 3; r++)
                for (int c = 0; c < 3; c++)
                    boardState[r, c] = buttons[r, c].Text;

            // 2. Спрашиваем у бота лучший ход
            var (bestRow, bestCol) = _bot.GetBestMove(boardState, botSymbol, playerSymbol);

            // 3. Эмулируем клик по кнопке
            if (bestRow != -1 && bestCol != -1)
            {
                var targetBtn = buttons[bestRow, bestCol];

                // ВАЖНО: Проверяем, не занята ли клетка (на всякий случай)
                if (targetBtn.Text == "")
                {
                    // Вызываем обработчик клика напрямую
                    Button_Click(targetBtn, EventArgs.Empty);
                }
            }
        }

        private void HandleWin(Game.Player winner, List<(int, int)> winLine)
        {
            textBox1.Text = $"Победа: {winner}";
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