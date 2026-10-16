using System;
using System.Windows.Forms;

namespace Пятнашки
{
    public partial class Form4 : Form
    {
        private int categoryId;
        private int imageIndex;
        private Form3 previousForm;

        public Form4(int selectedCategoryId, int selectedImageIndex, Form3 prevForm)
        {
            InitializeComponent();
            categoryId = selectedCategoryId;
            imageIndex = selectedImageIndex;
            previousForm = prevForm;
        }

        private void StartGame(int gridSize)
        {
            Form5 gameForm = new Form5(categoryId, imageIndex, gridSize, this);
            gameForm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e) => StartGame(2);
        private void button2_Click(object sender, EventArgs e) => StartGame(4);
        private void button3_Click(object sender, EventArgs e) => StartGame(5);
        private void button4_Click(object sender, EventArgs e) => StartGame(6);

        private void button5_Click(object sender, EventArgs e)
        {
            if (previousForm != null)
            {
                previousForm.WindowState = FormWindowState.Normal;
                previousForm.Show();
                this.Hide();
            }
        }

        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}