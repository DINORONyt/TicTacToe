using System;
using System.Collections.Generic;

namespace TicTacToe
{
    /// <summary>
    /// Класс, реализующий алгоритм принятия решений для бота
    /// </summary>
    public class BotPlayer
    {
        private readonly Random _random = new Random();

        /// <summary>
        /// Возвращает координаты лучшего хода для бота.
        /// </summary>
        /// <param name="board">Текущее состояние поля 3x3</param>
        /// <param name="botSymbol">Символ бота ("X" или "O")</param>
        /// <param name="playerSymbol">Символ игрока</param>
        public (int row, int col) GetBestMove(string[,] board, string botSymbol, string playerSymbol)
        {
            // 1. Алгоритм поиска выигрышного хода
            var winMove = FindStrategicMove(board, botSymbol);
            if (winMove != (-1, -1)) return winMove;

            // 2. Алгоритм блокировки хода противника
            var blockMove = FindStrategicMove(board, playerSymbol);
            if (blockMove != (-1, -1)) return blockMove;

            // 3. Стратегия занятия центра
            if (board[1, 1] == "") return (1, 1);

            // 4. Стратегия занятия углов
            var corners = new (int r, int c)[] { (0, 0), (0, 2), (2, 0), (2, 2) };
            foreach (var corner in corners)
            {
                if (board[corner.r, corner.c] == "") return corner;
            }

            // 5. Запасной алгоритм: случайный ход
            return GetRandomAvailableMove(board);
        }

        /// <summary>
        /// Ищет линию, где 2 клетки заняты указанным символом, а третья свободна.
        /// </summary>
        private (int row, int col) FindStrategicMove(string[,] board, string symbol)
        {
            var lines = new List<(int r, int c)[]>
            {
                new[] { (0,0), (0,1), (0,2) }, new[] { (1,0), (1,1), (1,2) }, new[] { (2,0), (2,1), (2,2) },
                new[] { (0,0), (1,0), (2,0) }, new[] { (0,1), (1,1), (2,1) }, new[] { (0,2), (1,2), (2,2) },
                new[] { (0,0), (1,1), (2,2) }, new[] { (0,2), (1,1), (2,0) }
            };

            foreach (var line in lines)
            {
                int matchCount = 0;
                int emptyCount = 0;
                (int r, int c) emptyPos = (-1, -1);

                foreach (var (r, c) in line)
                {
                    if (board[r, c] == symbol) matchCount++;
                    else if (board[r, c] == "")
                    {
                        emptyCount++;
                        emptyPos = (r, c);
                    }
                }

                if (matchCount == 2 && emptyCount == 1) return emptyPos;
            }
            return (-1, -1);
        }

        /// <summary>
        /// Выбирает случайную свободную клетку.
        /// </summary>
        private (int row, int col) GetRandomAvailableMove(string[,] board)
        {
            var available = new List<(int, int)>();
            for (int r = 0; r < 3; r++)
                for (int c = 0; c < 3; c++)
                    if (board[r, c] == "") available.Add((r, c));

            return available.Count > 0 ? available[_random.Next(available.Count)] : (-1, -1);
        }
    }
}