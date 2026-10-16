namespace Пятнашки
{
    partial class FormRecords
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRecords));
            dataGridViewRecords = new DataGridView();
            buttonRefresh = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewRecords).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewRecords
            // 
            dataGridViewRecords.BackgroundColor = Color.White;
            dataGridViewRecords.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewRecords.Location = new Point(12, 40);
            dataGridViewRecords.Name = "dataGridViewRecords";
            dataGridViewRecords.RowHeadersWidth = 51;
            dataGridViewRecords.Size = new Size(583, 202);
            dataGridViewRecords.TabIndex = 0;
            // 
            // buttonRefresh
            // 
            buttonRefresh.BackColor = Color.Snow;
            buttonRefresh.Location = new Point(233, 348);
            buttonRefresh.Name = "buttonRefresh";
            buttonRefresh.Size = new Size(153, 45);
            buttonRefresh.TabIndex = 1;
            buttonRefresh.Text = "Обновить";
            buttonRefresh.UseVisualStyleBackColor = false;
            buttonRefresh.Click += buttonRefresh_Click;
            // 
            // FormRecords
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkSlateBlue;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(607, 455);
            Controls.Add(buttonRefresh);
            Controls.Add(dataGridViewRecords);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FormRecords";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Рекорды";
            ((System.ComponentModel.ISupportInitialize)dataGridViewRecords).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridViewRecords;
        private Button buttonRefresh;
    }
}