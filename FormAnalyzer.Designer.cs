namespace App
{
    partial class FormAnalyzer
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
            this.gbInputStr = new System.Windows.Forms.GroupBox();
            this.tbInputStr = new System.Windows.Forms.TextBox();
            this.btnAnalyze = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.labelOutputMessage = new System.Windows.Forms.Label();
            this.tbOutputMessage = new System.Windows.Forms.TextBox();
            this.labelOutputIdentsAndConsts = new System.Windows.Forms.Label();
            this.tbOutputIdentsAndConsts = new System.Windows.Forms.TextBox();
            this.labelAuthor = new System.Windows.Forms.Label();
            this.btnAbout = new System.Windows.Forms.Button();
            this.gbInputStr.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbInputStr
            // 
            this.gbInputStr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbInputStr.Controls.Add(this.tbInputStr);
            this.gbInputStr.Controls.Add(this.btnAnalyze);
            this.gbInputStr.Controls.Add(this.btnClear);
            this.gbInputStr.Location = new System.Drawing.Point(12, 12);
            this.gbInputStr.Name = "gbInputStr";
            this.gbInputStr.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.gbInputStr.Size = new System.Drawing.Size(460, 115);
            this.gbInputStr.TabIndex = 0;
            this.gbInputStr.TabStop = false;
            this.gbInputStr.Text = "Ввод строки:";
            // 
            // tbInputStr
            // 
            this.tbInputStr.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbInputStr.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbInputStr.Location = new System.Drawing.Point(6, 23);
            this.tbInputStr.Name = "tbInputStr";
            this.tbInputStr.Size = new System.Drawing.Size(448, 29);
            this.tbInputStr.TabIndex = 1;
            this.tbInputStr.Tag = "";
            this.tbInputStr.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbInputStr_KeyPress);
            // 
            // btnAnalyze
            // 
            this.btnAnalyze.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAnalyze.Location = new System.Drawing.Point(6, 58);
            this.btnAnalyze.Name = "btnAnalyze";
            this.btnAnalyze.Size = new System.Drawing.Size(448, 23);
            this.btnAnalyze.TabIndex = 2;
            this.btnAnalyze.Text = "Анализировать";
            this.btnAnalyze.UseVisualStyleBackColor = true;
            this.btnAnalyze.Click += new System.EventHandler(this.btnAnalyze_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(6, 86);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(448, 23);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "Очистить";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // labelOutputMessage
            // 
            this.labelOutputMessage.AutoSize = true;
            this.labelOutputMessage.Location = new System.Drawing.Point(9, 130);
            this.labelOutputMessage.Name = "labelOutputMessage";
            this.labelOutputMessage.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.labelOutputMessage.Size = new System.Drawing.Size(103, 23);
            this.labelOutputMessage.TabIndex = 5;
            this.labelOutputMessage.Text = "Вывод сообщения:";
            // 
            // tbOutputMessage
            // 
            this.tbOutputMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOutputMessage.BackColor = System.Drawing.SystemColors.Window;
            this.tbOutputMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbOutputMessage.ForeColor = System.Drawing.Color.Red;
            this.tbOutputMessage.Location = new System.Drawing.Point(12, 156);
            this.tbOutputMessage.MinimumSize = new System.Drawing.Size(260, 150);
            this.tbOutputMessage.Multiline = true;
            this.tbOutputMessage.Name = "tbOutputMessage";
            this.tbOutputMessage.ReadOnly = true;
            this.tbOutputMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbOutputMessage.Size = new System.Drawing.Size(460, 150);
            this.tbOutputMessage.TabIndex = 6;
            // 
            // labelOutputIdentsAndConsts
            // 
            this.labelOutputIdentsAndConsts.AutoSize = true;
            this.labelOutputIdentsAndConsts.Location = new System.Drawing.Point(9, 309);
            this.labelOutputIdentsAndConsts.Name = "labelOutputIdentsAndConsts";
            this.labelOutputIdentsAndConsts.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.labelOutputIdentsAndConsts.Size = new System.Drawing.Size(204, 18);
            this.labelOutputIdentsAndConsts.TabIndex = 7;
            this.labelOutputIdentsAndConsts.Text = "Таблица идентификаторов и констант:";
            // 
            // tbOutputIdentsAndConsts
            // 
            this.tbOutputIdentsAndConsts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOutputIdentsAndConsts.BackColor = System.Drawing.SystemColors.Window;
            this.tbOutputIdentsAndConsts.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbOutputIdentsAndConsts.Location = new System.Drawing.Point(12, 330);
            this.tbOutputIdentsAndConsts.MinimumSize = new System.Drawing.Size(260, 150);
            this.tbOutputIdentsAndConsts.Multiline = true;
            this.tbOutputIdentsAndConsts.Name = "tbOutputIdentsAndConsts";
            this.tbOutputIdentsAndConsts.ReadOnly = true;
            this.tbOutputIdentsAndConsts.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbOutputIdentsAndConsts.Size = new System.Drawing.Size(460, 150);
            this.tbOutputIdentsAndConsts.TabIndex = 8;
            // 
            // labelAuthor
            // 
            this.labelAuthor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelAuthor.AutoSize = true;
            this.labelAuthor.Location = new System.Drawing.Point(9, 489);
            this.labelAuthor.Name = "labelAuthor";
            this.labelAuthor.Size = new System.Drawing.Size(155, 13);
            this.labelAuthor.TabIndex = 9;
            this.labelAuthor.Text = "Кузнецов, группа 6213 (2018)";
            // 
            // btnAbout
            // 
            this.btnAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAbout.Location = new System.Drawing.Point(372, 485);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(100, 20);
            this.btnAbout.TabIndex = 10;
            this.btnAbout.Text = "О программе";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAboutProgram_Click);
            // 
            // FormAnalyzer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 511);
            this.Controls.Add(this.gbInputStr);
            this.Controls.Add(this.labelOutputMessage);
            this.Controls.Add(this.tbOutputMessage);
            this.Controls.Add(this.labelOutputIdentsAndConsts);
            this.Controls.Add(this.tbOutputIdentsAndConsts);
            this.Controls.Add(this.labelAuthor);
            this.Controls.Add(this.btnAbout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(500, 550);
            this.Name = "FormAnalyzer";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Анализатор автоматного языка операторов присоединения языка Turbo Pascal ";
            this.gbInputStr.ResumeLayout(false);
            this.gbInputStr.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbInputStr;
        private System.Windows.Forms.TextBox tbInputStr;
        private System.Windows.Forms.TextBox tbOutputMessage;
        private System.Windows.Forms.TextBox tbOutputIdentsAndConsts;
        private System.Windows.Forms.Button btnAnalyze;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label labelOutputMessage;
        private System.Windows.Forms.Label labelOutputIdentsAndConsts;
        private System.Windows.Forms.Label labelAuthor;
        private System.Windows.Forms.Button btnAbout;
    }
}
