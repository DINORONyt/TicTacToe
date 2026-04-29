using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class Form1 : Form
    {
        private Game game = new Game();

        // isGameActive: Игра началась (сделан хотя бы один ход)
        private bool isGameActive = false;

        // isGameOver: Игра закончена (победа или ничья) — блокирует клики
        private bool isGameOver = false;

        private Button[,] buttons = new Button[3, 3];

        public Form1()
        {
            InitializeComponent();

            textBox1.ReadOnly = true;

            // Назначение координат
            button1.Tag = (0, 0);
            button2.Tag = (0, 1);
            button3.Tag = (0, 2);
            button4.Tag = (1, 0);
            button5.Tag = (1, 1);
            button6.Tag = (1, 2);
            button7.Tag = (2, 0);
            button8.Tag = (2, 1);
            button9.Tag = (2, 2);

            // Сохраняем ссылки на кнопки
            buttons[0, 0] = button1; buttons[0, 1] = button2; buttons[0, 2] = button3;
            buttons[1, 0] = button4; buttons[1, 1] = button5; buttons[1, 2] = button6;
            buttons[2, 0] = button7; buttons[2, 1] = button8; buttons[2, 2] = button9;

            game.OnWin += HandleWin;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (isGameActive) return;

            game.CurrentPlayer = Game.Player.X;
            textBox1.Text = "Ходит X";
            textBox1.SelectionLength = 0; // Снимаем выделение
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (isGameActive) return;

            game.CurrentPlayer = Game.Player.O;
            textBox1.Text = "Ходит O";
            textBox1.SelectionLength = 0; // Снимаем выделение
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
            textBox1.SelectionLength = 0; // Снимаем выделение

            isGameActive = false;
            isGameOver = false; // Сбрасываем флаг конца игры
            button11.Enabled = true;
            button12.Enabled = true;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            // Если игра закончена, игнорируем клик
            if (isGameOver) return;

            Button btn = sender as Button;

            // Игнорируем клик, если клетка занята
            if (btn.Text != "") return;

            // Игнорируем клик, если не выбран игрок (X или O)
            if (game.CurrentPlayer == Game.Player.None) return;

            var (row, col) = ((int, int))btn.Tag;

            if (!game.MakeMove(row, col, game.CurrentPlayer)) return;

            btn.Text = game.CurrentPlayer.ToString();
            btn.ForeColor = game.CurrentPlayer == Game.Player.X ? Color.Blue : Color.Red;

            // Активируем игру после первого хода (блокируем выбор игрока)
            if (!isGameActive)
            {
                isGameActive = true;
                button11.Enabled = false;
                button12.Enabled = false;
            }

            var winLine = game.CheckWinnerAndRaise();
            if (winLine != null)
            {
                // Победа обработана в HandleWin
                return;
            }

            if (game.IsDraw())
            {
                textBox1.Text = "Ничья!";
                textBox1.SelectionLength = 0;
                isGameOver = true; // Блокируем дальнейшие ходы
                return;
            }

            game.SwitchPlayer();
            textBox1.Text = $"Ходит {game.CurrentPlayer}";
            textBox1.SelectionLength = 0; // Снимаем выделение
        }

        private void HandleWin(Game.Player winner, List<(int, int)> winLine)
        {
            textBox1.Text = $"Победа: {winner}";
            textBox1.SelectionLength = 0; // Снимаем выделение

            isGameOver = true; // Устанавливаем флаг конца игры

            

            foreach (var (r, c) in winLine)
            {
                if (r >= 0 && r < 3 && c >= 0 && c < 3)
                {
                    var btn = buttons[r, c];
                    if (btn != null)
                    {
                        btn.ForeColor = Color.Green; // Фигуры становятся зелёными
                    }
                }
            }
        }
    }
}