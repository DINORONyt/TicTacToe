namespace TicTacToe
{
    partial class MenuForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            // Цветовая схема (как в основной игре)
            System.Drawing.Color bgColor = System.Drawing.Color.FromArgb(240, 240, 240);
            System.Drawing.Color btnBackColor = System.Drawing.Color.White;
            System.Drawing.Color btnForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            System.Drawing.Color borderColor = System.Drawing.Color.FromArgb(200, 200, 200);
            System.Drawing.Color titleColor = System.Drawing.Color.FromArgb(0, 120, 212); // Синий акцент
            System.Drawing.Color hoverColor = System.Drawing.Color.FromArgb(230, 245, 255);

            // НАСТРОЙКА ФОРМЫ
            this.Text = "Крестики-Нолики";
            this.ClientSize = new System.Drawing.Size(400, 500);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = bgColor;
            this.Name = "MenuForm";

            // ЗАГОЛОВОК
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblTitle.Text = "Крестики-Нолики";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTitle.ForeColor = titleColor;
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Height = 120;
            this.lblTitle.Margin = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.lblTitle.Name = "lblTitle";

            // ПАНЕЛЬ ДЛЯ КНОПОК (чтобы было легко центрировать)
            this.panelButtons = new System.Windows.Forms.Panel();
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelButtons.BackColor = System.Drawing.Color.Transparent;
            this.panelButtons.Name = "panelButtons";

            // КНОПКА 1: ДВА ИГРОКА
            this.btnTwoPlayers = new System.Windows.Forms.Button();
            this.btnTwoPlayers.Text = "👥 Два игрока";
            this.btnTwoPlayers.Size = new System.Drawing.Size(300, 65);
            // Центрирование кнопки по горизонтали вручную (400 ширина формы - 300 ширина кнопки) / 2 = 50
            this.btnTwoPlayers.Location = new System.Drawing.Point(50, 50);
            this.btnTwoPlayers.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnTwoPlayers.ForeColor = btnForeColor;
            this.btnTwoPlayers.BackColor = btnBackColor;
            this.btnTwoPlayers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTwoPlayers.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTwoPlayers.FlatAppearance.BorderSize = 1;
            this.btnTwoPlayers.FlatAppearance.BorderColor = borderColor;
            this.btnTwoPlayers.FlatAppearance.MouseOverBackColor = hoverColor;
            this.btnTwoPlayers.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(200, 230, 255);
            this.btnTwoPlayers.Name = "btnTwoPlayers";
            this.btnTwoPlayers.Click += new System.EventHandler(this.btnTwoPlayers_Click);

            // КНОПКА 2: ИГРА С БОТОМ
            this.btnWithBot = new System.Windows.Forms.Button();
            this.btnWithBot.Text = "🤖 Играть с ботом";
            this.btnWithBot.Size = new System.Drawing.Size(300, 65);
            this.btnWithBot.Location = new System.Drawing.Point(50, 140); // Отступ 25 (предыдущая) + 65 (высота) + 15 (зазор) = 105? Нет, просто зададим фикс.
            // Пересчитаем: 50 (начало) + 65 (кнопка) + 25 (отступ) = 140
            this.btnWithBot.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnWithBot.ForeColor = btnForeColor;
            this.btnWithBot.BackColor = btnBackColor;
            this.btnWithBot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWithBot.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnWithBot.FlatAppearance.BorderSize = 1;
            this.btnWithBot.FlatAppearance.BorderColor = borderColor;
            this.btnWithBot.FlatAppearance.MouseOverBackColor = hoverColor;
            this.btnWithBot.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(200, 230, 255);
            this.btnWithBot.Name = "btnWithBot";
            this.btnWithBot.Click += new System.EventHandler(this.btnWithBot_Click);

            // КНОПКА 3: ВЫХОД
            this.btnExit = new System.Windows.Forms.Button();
            this.btnExit.Text = "❌ Выход";
            this.btnExit.Size = new System.Drawing.Size(300, 65);
            this.btnExit.Location = new System.Drawing.Point(50, 230); // 140 + 65 + 25 = 230
            this.btnExit.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnExit.ForeColor = btnForeColor;
            this.btnExit.BackColor = btnBackColor;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.FlatAppearance.BorderSize = 1;
            this.btnExit.FlatAppearance.BorderColor = borderColor;
            this.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(255, 230, 230); // Красноватый при наведении
            this.btnExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(255, 200, 200);
            this.btnExit.Name = "btnExit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);

            // ДОБАВЛЕНИЕ КНОПОК НА ПАНЕЛЬ
            this.panelButtons.Controls.Add(this.btnExit);
            this.panelButtons.Controls.Add(this.btnWithBot);
            this.panelButtons.Controls.Add(this.btnTwoPlayers);

            // ДОБАВЛЕНИЕ ЭЛЕМЕНТОВ НА ФОРМУ
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.lblTitle);

            // АВТОМАТИЧЕСКОЕ МАСШТАБИРОВАНИЕ
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnTwoPlayers;
        private System.Windows.Forms.Button btnWithBot;
        private System.Windows.Forms.Button btnExit;
    }
}