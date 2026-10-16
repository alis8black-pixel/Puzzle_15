using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Пятнашки
{
    public partial class FormRecords : Form
    {
        private RecordManager recordManager;

        public FormRecords()
        {
            InitializeComponent();
            recordManager = new RecordManager();
            LoadRecords();
        }

        private void LoadRecords()
        {
            try
            {
                List<RecordEntry> allRecords = recordManager.GetAllRecords();

                if (allRecords.Count == 0)
                {
                    dataGridViewRecords.DataSource = null;
                    Label emptyLabel = new Label();
                    emptyLabel.Text = "Пока нет ни одного рекорда.\nСыграйте в игру и установите рекорд!";
                    emptyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    emptyLabel.Dock = DockStyle.Fill;
                    emptyLabel.Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
                    emptyLabel.ForeColor = System.Drawing.Color.Gray;
                    this.Controls.Add(emptyLabel);
                    emptyLabel.BringToFront();
                    return;
                }

                var displayList = new List<object>();
                foreach (var record in allRecords)
                {
                    displayList.Add(new
                    {
                        Сложность = $"{record.GridSize}x{record.GridSize}",
                        Ходы = record.Moves,
                        Дата = record.Date.ToString("dd.MM.yyyy HH:mm:ss")
                    });
                }

                dataGridViewRecords.DataSource = displayList;

                dataGridViewRecords.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridViewRecords.AutoResizeColumns();
                dataGridViewRecords.ReadOnly = true;
                dataGridViewRecords.AllowUserToAddRows = false;
                dataGridViewRecords.RowHeadersVisible = false;

                if (dataGridViewRecords.Columns["Сложность"] != null)
                    dataGridViewRecords.Columns["Сложность"].HeaderText = "Сложность";
                if (dataGridViewRecords.Columns["Ходы"] != null)
                    dataGridViewRecords.Columns["Ходы"].HeaderText = "Количество ходов";
                if (dataGridViewRecords.Columns["Дата"] != null)
                    dataGridViewRecords.Columns["Дата"].HeaderText = "Дата установки";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки рекордов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void RefreshRecords()
        {
            LoadRecords();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            LoadRecords();
        }

        private void dataGridViewRecords_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}