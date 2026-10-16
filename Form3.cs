using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Пятнашки
{
    public partial class Form3 : Form
    {
        private int categoryId;
        private Form2 previousForm;

        private Dictionary<int, string> categoryNames = new Dictionary<int, string>
        {
            {1, "Животные"},
            {2, "Аниме"},
            {3, "Пейзажи"},
            {4, "Фрески"},
            {5, "Мультики"},
            {6, "Люди"}
        };

        public Form3(int selectedCategoryId, Form2 prevForm)
        {
            InitializeComponent();

            categoryId = selectedCategoryId;
            previousForm = prevForm;

            this.Text = $"Выбор изображения - {categoryNames[categoryId]}";
            this.DoubleBuffered = true;
            InitializeImages();
        }

        private void InitializeImages()
        {
            Image[] categoryImages = GetImagesForCategory(categoryId);

            if (categoryImages.Length == 0)
            {
                Label noImagesLabel = new Label();
                noImagesLabel.Text = "Изображения не найдены";
                noImagesLabel.Dock = DockStyle.Fill;
                noImagesLabel.TextAlign = ContentAlignment.MiddleCenter;
                noImagesLabel.Font = new Font("Arial", 14, FontStyle.Bold);
                this.Controls.Add(noImagesLabel);
                return;
            }

            TableLayoutPanel tablePanel = new DoubleBufferedTableLayoutPanel();
            tablePanel.RowCount = 2;
            tablePanel.ColumnCount = 2;
            tablePanel.BackColor = Color.Transparent;
            tablePanel.Size = new Size(539, 750);
            tablePanel.Padding = new Padding(40, 180, 0, 0);

            for (int i = 0; i < 2; i++)
            {
                tablePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                tablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            }

            for (int i = 0; i < categoryImages.Length; i++)
            {
                PictureBox pb = CreatePictureBox(categoryImages[i], i);
                tablePanel.Controls.Add(pb, i % 2, i / 2);
            }

            this.Controls.Add(tablePanel);
        }

        private PictureBox CreatePictureBox(Image image, int index)
        {
            DoubleBufferedPictureBox pb = new DoubleBufferedPictureBox();
            pb.Dock = DockStyle.None;
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
            pb.Image = image;
            pb.Cursor = Cursors.Hand;
            pb.Size = new Size(180, 180);
            pb.BorderStyle = BorderStyle.Fixed3D;
            pb.BackColor = Color.LightGray;

            pb.OriginalImage = image;
            pb.OriginalBackColor = pb.BackColor;

            pb.MouseEnter += (s, e) =>
            {
                if (s is DoubleBufferedPictureBox pictureBox && pictureBox.Image != null)
                {
                    Bitmap darkened = new Bitmap(pictureBox.Image);
                    using (Graphics g = Graphics.FromImage(darkened))
                    {
                        using (Brush brush = new SolidBrush(Color.FromArgb(100, Color.Black)))
                        {
                            g.FillRectangle(brush, 0, 0, darkened.Width, darkened.Height);
                        }
                    }
                    pictureBox.Image = darkened;
                    pictureBox.BackColor = Color.Gold;
                }
            };

            pb.MouseLeave += (s, e) =>
            {
                if (s is DoubleBufferedPictureBox pictureBox && pictureBox.OriginalImage != null)
                {
                    pictureBox.Image = pictureBox.OriginalImage;
                    pictureBox.BackColor = pictureBox.OriginalBackColor;
                }
            };

            int imageIndex = index;
            pb.Click += (s, e) => SelectImage(imageIndex);

            return pb;
        }

        private Image[] GetImagesForCategory(int categoryId)
        {
            if (!categoryNames.ContainsKey(categoryId))
            {
                MessageBox.Show($"Категория с ID {categoryId} не найдена!");
                return Array.Empty<Image>();
            }

            string categoryName = categoryNames[categoryId];
            List<Image> images = new List<Image>();

            try
            {
                var resourceManager = Properties.Resources.ResourceManager;

                for (int i = 1; i <= 4; i++)
                {
                    string resourceName = $"{categoryName}_{i}";
                    object resourceObj = resourceManager.GetObject(resourceName);
                    if (resourceObj is Image img)
                        images.Add(img);
                    else
                        Console.WriteLine($"Ресурс {resourceName} не найден или не является изображением");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки изображений: {ex.Message}");
            }

            return images.ToArray();
        }

        private void SelectImage(int imageIndex)
        {
            Form4 form4 = new Form4(categoryId, imageIndex, this);
            form4.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (previousForm != null)
            {
                previousForm.WindowState = FormWindowState.Normal;
                previousForm.Show();
                this.Hide();
            }
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }

    public class DoubleBufferedPictureBox : PictureBox
    {
        public Image? OriginalImage { get; set; }
        public Color OriginalBackColor { get; set; }

        public DoubleBufferedPictureBox()
        {
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                         ControlStyles.UserPaint |
                         ControlStyles.ResizeRedraw |
                         ControlStyles.OptimizedDoubleBuffer |
                         ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();
        }
    }

    public class DoubleBufferedTableLayoutPanel : TableLayoutPanel
    {
        public DoubleBufferedTableLayoutPanel()
        {
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                         ControlStyles.UserPaint |
                         ControlStyles.ResizeRedraw |
                         ControlStyles.OptimizedDoubleBuffer |
                         ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();
        }
    }
}