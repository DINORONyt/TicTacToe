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
        /// Ожидание: Все ячейки пусты (содержат пустую строку)
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
    }
}