using System;
using System.Drawing;
using System.Windows.Forms;

namespace Пятнашки
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void OpenImageSelection(int categoryId)
        {
            Form3 form = new Form3(categoryId, this);
            form.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e) => OpenImageSelection(1);
        private void pictureBox2_Click(object sender, EventArgs e) => OpenImageSelection(2);
        private void pictureBox3_Click(object sender, EventArgs e) => OpenImageSelection(3);
        private void pictureBox4_Click(object sender, EventArgs e) => OpenImageSelection(4);
        private void pictureBox5_Click(object sender, EventArgs e) => OpenImageSelection(5);
        private void pictureBox6_Click(object sender, EventArgs e) => OpenImageSelection(6);

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form is Form1)
                {
                    form.WindowState = FormWindowState.Normal;
                    form.Show();
                    this.Hide();
                    break;
                }
            }
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.Size = new Size(170, 170);
            pictureBox1.Location = new Point(58, 47);
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Size = new Size(151, 151);
            pictureBox1.Location = new Point(64, 53);
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.Size = new Size(170, 170);
            pictureBox2.Location = new Point(294, 46);
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Size = new Size(151, 151);
            pictureBox2.Location = new Point(300, 53);
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            pictureBox3.Size = new Size(170, 170);
            pictureBox3.Location = new Point(58, 293);
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.Size = new Size(151, 151);
            pictureBox3.Location = new Point(64, 299);
        }

        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            pictureBox4.Size = new Size(170, 170);
            pictureBox4.Location = new Point(294, 293);
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            pictureBox4.Size = new Size(151, 151);
            pictureBox4.Location = new Point(300, 299);
        }

        private void pictureBox5_MouseEnter(object sender, EventArgs e)
        {
            pictureBox5.Size = new Size(170, 170);
            pictureBox5.Location = new Point(58, 535);
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            pictureBox5.Size = new Size(151, 151);
            pictureBox5.Location = new Point(64, 541);
        }

        private void pictureBox6_MouseEnter(object sender, EventArgs e)
        {
            pictureBox6.Size = new Size(170, 170);
            pictureBox6.Location = new Point(294, 535);
        }

        private void pictureBox6_MouseLeave(object sender, EventArgs e)
        {
            pictureBox6.Size = new Size(151, 151);
            pictureBox6.Location = new Point(300, 541);
        }
    }
}