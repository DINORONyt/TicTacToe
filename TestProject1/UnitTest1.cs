using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace TicTacToe.Tests
{
    public class GameTests
    {
        // ==========================================
        // 1. Инициализация и сброс состояния
        // ==========================================

        /// <summary>
        /// Тест: Проверка начального состояния игрового поля.
        /// Ожидание: Все ячейки пусты (содержат пустую строку).
        /// </summary>
        [Fact]
        public void NewGame_BoardIsEmpty()
        {
            // Arrange
            var game = new Game();

            // Act & Assert
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Assert.Equal("", game.Board[i][j]);
                }
            }
        }

        /// <summary>
        /// Тест: Проверка текущего игрока при старте.
        /// Ожидание: CurrentPlayer равен Player.None.
        /// </summary>
        [Fact]
        public void NewGame_CurrentPlayerIsNone()
        {
            var game = new Game();
            Assert.Equal(Game.Player.None, game.CurrentPlayer);
        }

        /// <summary>
        /// Тест: Проверка метода Reset (сброс игры).
        /// Ожидание: Поле очищается, игрок сбрасывается на None.
        /// </summary>
        [Fact]
        public void Reset_ClearsBoardAndResetsPlayer()
        {
            var game = new Game();
            game.CurrentPlayer = Game.Player.X;
            game.MakeMove(0, 0, Game.Player.X);  

            game.Reset();

            Assert.Equal(Game.Player.None, game.CurrentPlayer);
            // Проверяем, что все ячейки снова пусты
            Assert.All(game.Board.SelectMany(row => row), cell => Assert.Equal("", cell));
        }

        // ==========================================
        // 2. Выполнение ходов и смена игрока
        // ==========================================

        /// <summary>
        /// Тест: Успешное выполнение хода в пустую клетку.
        /// Ожидание: Метод возвращает true, в ячейке появляется символ игрока.
        /// </summary>
        [Fact]
        public void MakeMove_ValidCell_ReturnsTrueAndPlacesMark()
        {
            var game = new Game();
            game.CurrentPlayer = Game.Player.X;

            var result = game.MakeMove(1, 1, Game.Player.X);  

            Assert.True(result);
            Assert.Equal("X", game.Board[1][1]);
        }

        /// <summary>
        /// Тест: Попытка хода в занятую клетку.
        /// Ожидание: Метод возвращает false, состояние не меняется.
        /// </summary>
        [Fact]
        public void MakeMove_OccupiedCell_ReturnsFalse()
        {
            var game = new Game();
            game.CurrentPlayer = Game.Player.X;
            game.MakeMove(0, 0, Game.Player.X);  

            var result = game.MakeMove(0, 0, Game.Player.X);  

            Assert.False(result);
            Assert.Equal("X", game.Board[0][0]); // Значение не изменилось
        }

        /// <summary>
        /// Тест: Корректная смена игрока (X -> O -> X).
        /// </summary>
        [Fact]
        public void SwitchPlayer_XtoO_and_OtoX()
        {
            var game = new Game();

            game.CurrentPlayer = Game.Player.X;
            game.SwitchPlayer();
            Assert.Equal(Game.Player.O, game.CurrentPlayer);

            game.SwitchPlayer();
            Assert.Equal(Game.Player.X, game.CurrentPlayer);
        }

        // ==========================================
        // 3. Проверка победы (Все 8 комбинаций)
        // ==========================================

        /// <summary>
        /// Параметризованный тест для проверки всех выигрышных линий.
        /// Проверяет строки, столбцы и диагонали.
        /// </summary>
        [Theory]
        // Строки
        [InlineData(0, 0, 0, 1, 0, 2)]
        [InlineData(1, 0, 1, 1, 1, 2)]
        [InlineData(2, 0, 2, 1, 2, 2)]
        // Столбцы
        [InlineData(0, 0, 1, 0, 2, 0)]
        [InlineData(0, 1, 1, 1, 2, 1)]
        [InlineData(0, 2, 1, 2, 2, 2)]
        // Диагонали
        [InlineData(0, 0, 1, 1, 2, 2)]
        [InlineData(0, 2, 1, 1, 2, 0)]
        public void CheckWinner_AnyWinLine_ReturnsCoordinates(
            int r1, int c1, int r2, int c2, int r3, int c3)
        {
            var game = new Game();
            game.CurrentPlayer = Game.Player.O;

            // Совершаем 3 хода для создания линии
            game.MakeMove(r1, c1, Game.Player.O);  
            game.MakeMove(r2, c2, Game.Player.O);  
            game.MakeMove(r3, c3, Game.Player.O);  

            var winLine = game.CheckWinner();

            Assert.NotNull(winLine);
            Assert.Equal(3, winLine.Count);
            // Убеждаемся, что координаты линии совпадают с ходами
            Assert.Contains((r1, c1), winLine);
            Assert.Contains((r2, c2), winLine);
            Assert.Contains((r3, c3), winLine);
        }

        /// <summary>
        /// Тест: Отсутствие победы при разрозненных ходах.
        /// Ожидание: Метод возвращает null.
        /// </summary>
        [Fact]
        public void CheckWinner_NoWinner_ReturnsNull()
        {
            var game = new Game();
            game.CurrentPlayer = Game.Player.X;
            game.MakeMove(0, 0, Game.Player.X);  
            game.CurrentPlayer = Game.Player.O;
            game.MakeMove(0, 1, Game.Player.O);  
            game.CurrentPlayer = Game.Player.X;
            game.MakeMove(1, 1, Game.Player.X);  

            var result = game.CheckWinner();
            Assert.Null(result);
        }

        // ==========================================
        // 4. Событие победы (OnWin)
        // ==========================================

        /// <summary>
        /// Тест: Событие OnWin срабатывает при победе.
        /// </summary>
        [Fact]
        public void CheckWinnerAndRaise_Winner_InvokesOnWinEvent()
        {
            var game = new Game();
            game.CurrentPlayer = Game.Player.X;

            // Создаем победную диагональ
            game.MakeMove(0, 0, Game.Player.X);  
            game.MakeMove(1, 1, Game.Player.X);  
            game.MakeMove(2, 2, Game.Player.X);  

            Game.Player eventWinner = Game.Player.None;
            game.OnWin += (winner, line) => eventWinner = winner;

            game.CheckWinnerAndRaise();

            Assert.Equal(Game.Player.X, eventWinner);
        }

        // ==========================================
        // 5. Ничья (Draw)
        // ==========================================

        /// <summary>
        /// Тест: Определение ничьей при полном заполнении поля.
        /// </summary>
        [Fact]
        public void IsDraw_FullBoardNoWinner_ReturnsTrue()
        {
            var game = new Game();
            // Заполняем поле "ничейной" комбинацией вручную
            string[,] drawBoard = {
                { "X", "O", "X" },
                { "X", "O", "O" },
                { "O", "X", "X" }
            };

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    game.Board[i][j] = drawBoard[i, j];

            Assert.True(game.IsDraw());
        }

        /// <summary>
        /// Тест: Если есть победитель, ничьей быть не может.
        /// </summary>
        [Fact]
        public void IsDraw_WinnerExists_ReturnsFalse()
        {
            var game = new Game();
            game.CurrentPlayer = Game.Player.O;
            game.MakeMove(0, 0, Game.Player.O);  
            game.MakeMove(0, 1, Game.Player.O);  
            game.MakeMove(0, 2, Game.Player.O);  

            Assert.False(game.IsDraw());
        }
    }
}