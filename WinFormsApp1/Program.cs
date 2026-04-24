using System;
using System.Windows.Forms;
using TicTacToe;
using WinFormsApp1;  // ← Добавьте эту строку (или ваше пространство имён)

namespace WinFormsApp1
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}