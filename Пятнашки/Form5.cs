using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Пятнашки
{
    public partial class Form5 : Form
    {
        private int categoryId;
        private int imageIndex;
        private int gridSize;
        private Form4 previousForm;

        private Image originalImage;
        private List<PictureBox> tiles = new List<PictureBox>();
        private PictureBox emptyTile;
        private int movesCount = 0;
        private Random random = new Random();
        private bool isShuffling = false;

        private Label movesLabel;
        private Panel gamePanel;
        private Panel controlPanel;
        private Panel tilesContainer;

        private Dictionary<int, string> categoryNames = new Dictionary<int, string>
        {
            {1, "Животные"},
            {2, "Аниме"},
            {3, "Пейзажи"},
            {4, "Фрески"},
            {5, "Мультики"},
            {6, "Люди"}
        };

        private RecordManager recordManager;

        public Form5(int selectedCategoryId, int selectedImageIndex, int selectedGridSize, Form4 prevForm)
        {
            InitializeComponent();

            categoryId = selectedCategoryId;
            imageIndex = selectedImageIndex;
            gridSize = selectedGridSize;
            previousForm = prevForm;

            recordManager = new RecordManager();

            InitializeGame();
        }

        private void InitializeGame()
        {
            originalImage = LoadSelectedImage();
            if (originalImage == null)
            {
                MessageBox.Show("Не удалось загрузить изображение. Проверьте ресурсы.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            string categoryName = categoryNames.ContainsKey(categoryId) ? categoryNames[categoryId] : "Неизвестно";
            this.Text = $"Пятнашки - {categoryName} - {gridSize}x{gridSize}";

            CreateGameBoard();
            ShuffleTilesSync();
            UpdateMovesCounter();
        }

        private Image LoadSelectedImage()
        {
            try
            {
                var resourceManager = Properties.Resources.ResourceManager;
                string categoryName = categoryNames[categoryId];
                string resourceName = $"{categoryName}_{imageIndex + 1}";
                object resource = resourceManager.GetObject(resourceName);
                return resource as Image;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка загрузки изображения: " + ex.Message);
                return null;
            }
        }

        private void CreateGameBoard()
        {
            this.Controls.Clear();

            Panel mainPanel = new Panel();
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.BackColor = Color.Transparent;
            this.Controls.Add(mainPanel);

            CreateGameField(mainPanel);
            CreateControlPanel(mainPanel);
        }

        private void CreateGameField(Panel mainPanel)
        {
            int boardSize = 400;

            gamePanel = new Panel();
            gamePanel.Size = new Size(boardSize, boardSize);
            gamePanel.Location = new Point((mainPanel.Width - boardSize) / 2, 130);
            gamePanel.BackColor = Color.FromArgb(150, 0, 0, 0);
            gamePanel.BorderStyle = BorderStyle.FixedSingle;
            mainPanel.Controls.Add(gamePanel);

            tilesContainer = new Panel();
            tilesContainer.Size = new Size(boardSize, boardSize);
            tilesContainer.Location = new Point(0, 0);
            tilesContainer.BackColor = Color.Transparent;
            gamePanel.Controls.Add(tilesContainer);

            int tileSize = boardSize / gridSize;
            Bitmap resizedImage = new Bitmap(originalImage, new Size(boardSize, boardSize));

            for (int row = 0; row < gridSize; row++)
            {
                for (int col = 0; col < gridSize; col++)
                {
                    if (row == gridSize - 1 && col == gridSize - 1)
                    {
                        CreateEmptyTile(col, row, tileSize);
                        continue;
                    }
                    CreateImageTile(col, row, tileSize, resizedImage);
                }
            }
        }

        private void CreateEmptyTile(int col, int row, int tileSize)
        {
            emptyTile = new PictureBox();
            emptyTile.Size = new Size(tileSize, tileSize);
            emptyTile.Location = new Point(col * tileSize, row * tileSize);
            emptyTile.BackColor = Color.FromArgb(230, 250, 250, 250);
            emptyTile.BorderStyle = BorderStyle.FixedSingle;
            emptyTile.Tag = new Point(col, row);
            tilesContainer.Controls.Add(emptyTile);
        }

        private void CreateImageTile(int col, int row, int tileSize, Bitmap resizedImage)
        {
            Rectangle sourceRect = new Rectangle(col * tileSize, row * tileSize, tileSize, tileSize);
            Bitmap tileImage = new Bitmap(tileSize, tileSize);
            using (Graphics g = Graphics.FromImage(tileImage))
            {
                g.DrawImage(resizedImage, new Rectangle(0, 0, tileSize, tileSize), sourceRect, GraphicsUnit.Pixel);
            }

            PictureBox tile = new PictureBox();
            tile.Size = new Size(tileSize, tileSize);
            tile.Location = new Point(col * tileSize, row * tileSize);
            tile.Image = tileImage;
            tile.SizeMode = PictureBoxSizeMode.StretchImage;
            tile.BorderStyle = BorderStyle.FixedSingle;
            tile.Cursor = Cursors.Hand;
            tile.Tag = new Point(col, row);
            tile.Click += Tile_Click;

            tiles.Add(tile);
            tilesContainer.Controls.Add(tile);
        }

        private void CreateControlPanel(Panel mainPanel)
        {
            controlPanel = new Panel();
            controlPanel.Size = new Size(540, 110);
            controlPanel.Location = new Point((mainPanel.Width - controlPanel.Width) / 2, gamePanel.Bottom + 40);
            controlPanel.BackColor = Color.Transparent;
            mainPanel.Controls.Add(controlPanel);

            movesLabel = new Label();
            movesLabel.Text = "Ходы: 0";
            movesLabel.Size = new Size(150, 40);
            movesLabel.ForeColor = Color.White;
            movesLabel.Location = new Point(40, 2);
            movesLabel.Font = new Font("Arial", 14, FontStyle.Bold);
            movesLabel.TextAlign = ContentAlignment.MiddleLeft;
            controlPanel.Controls.Add(movesLabel);

            Button shuffleButton = CreateControlButton("Перемешать", 40);
            shuffleButton.Size = new Size(140, 60);
            shuffleButton.Click += async (s, e) => await ShuffleTilesAsync();
            controlPanel.Controls.Add(shuffleButton);

            Button showImageButton = CreateControlButton("Показать изображение", 200);
            showImageButton.Click += (s, e) => ShowOriginalImage();
            showImageButton.Size = new Size(140, 60);
            controlPanel.Controls.Add(showImageButton);

            Button exitButton = CreateControlButton("Выход в меню", 350);
            exitButton.Size = new Size(140, 60);
            exitButton.Click += (s, e) =>
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
            };
            controlPanel.Controls.Add(exitButton);
        }

        private Button CreateControlButton(string text, int x)
        {
            Button button = new Button();
            button.Text = text;
            button.Size = new Size(text == "Выход в меню" ? 120 : 150, 30);
            button.Location = new Point(x, 50);
            button.Font = new Font("Arial", 9, FontStyle.Bold);
            button.ForeColor = Color.White;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button.FlatAppearance.BorderSize = 0;

            string bgPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "game_button.png");
            if (File.Exists(bgPath))
                button.BackgroundImage = Image.FromFile(bgPath);
            button.BackgroundImageLayout = ImageLayout.Stretch;
            return button;
        }

        private void Tile_Click(object sender, EventArgs e)
        {
            if (isShuffling) return;

            PictureBox clickedTile = (PictureBox)sender;
            Point tilePos = (Point)clickedTile.Tag;
            Point emptyPos = (Point)emptyTile.Tag;

            if (CanMove(tilePos, emptyPos))
            {
                SwapTiles(clickedTile, emptyTile);
                clickedTile.Tag = emptyPos;
                emptyTile.Tag = tilePos;
                movesCount++;
                UpdateMovesCounter();

                if (CheckWin())
                {
                    CheckAndSaveRecord();
                }
            }
        }

        private bool CanMove(Point tilePos, Point emptyPos)
        {
            return (Math.Abs(tilePos.X - emptyPos.X) == 1 && tilePos.Y == emptyPos.Y) ||
                   (Math.Abs(tilePos.Y - emptyPos.Y) == 1 && tilePos.X == emptyPos.X);
        }

        private void SwapTiles(PictureBox tile1, PictureBox tile2)
        {
            Point tempLocation = tile1.Location;
            tile1.Location = tile2.Location;
            tile2.Location = tempLocation;
        }

        private bool CheckWin()
        {
            foreach (PictureBox tile in tiles)
            {
                Point currentPos = (Point)tile.Tag;
                Point correctPos = GetCorrectPosition(tile);
                if (currentPos != correctPos)
                    return false;
            }
            return true;
        }

        private Point GetCorrectPosition(PictureBox tile)
        {
            int index = tiles.IndexOf(tile);
            return new Point(index % gridSize, index / gridSize);
        }

        private void ShuffleTilesSync()
        {
            for (int i = 0; i < 200; i++)
            {
                List<PictureBox> movableTiles = tiles.Where(t => CanMove((Point)t.Tag, (Point)emptyTile.Tag)).ToList();
                if (movableTiles.Count > 0)
                {
                    PictureBox randomTile = movableTiles[random.Next(movableTiles.Count)];
                    Point tilePos = (Point)randomTile.Tag;
                    Point emptyPos = (Point)emptyTile.Tag;
                    SwapTiles(randomTile, emptyTile);
                    randomTile.Tag = emptyPos;
                    emptyTile.Tag = tilePos;
                }
            }

            if (CheckWin())
            {
                for (int i = 0; i < 23; i++)
                {
                    var movableTiles = tiles.Where(t => CanMove((Point)t.Tag, (Point)emptyTile.Tag)).ToList();
                    if (movableTiles.Count > 0)
                    {
                        PictureBox randomTile = movableTiles[random.Next(movableTiles.Count)];
                        Point tilePos = (Point)randomTile.Tag;
                        Point emptyPos = (Point)emptyTile.Tag;
                        SwapTiles(randomTile, emptyTile);
                        randomTile.Tag = emptyPos;
                        emptyTile.Tag = tilePos;
                    }
                }
            }

            movesCount = 0;
            UpdateMovesCounter();
        }

        private async Task ShuffleTilesAsync()
        {
            if (isShuffling) return;
            isShuffling = true;

            Button shuffleButton = controlPanel.Controls.OfType<Button>().First(b => b.Text == "Перемешать");
            shuffleButton.Enabled = false;
            shuffleButton.Text = "Перемешивание...";

            try
            {
                for (int i = 0; i < 103; i++)
                {
                    List<PictureBox> movableTiles = tiles.Where(t => CanMove((Point)t.Tag, (Point)emptyTile.Tag)).ToList();
                    if (movableTiles.Count > 0)
                    {
                        PictureBox randomTile = movableTiles[random.Next(movableTiles.Count)];
                        Point tilePos = (Point)randomTile.Tag;
                        Point emptyPos = (Point)emptyTile.Tag;
                        SwapTiles(randomTile, emptyTile);
                        randomTile.Tag = emptyPos;
                        emptyTile.Tag = tilePos;
                    }
                    await Task.Delay(5);
                    Application.DoEvents();
                }

                if (CheckWin())
                {
                    for (int i = 0; i < 23; i++)
                    {
                        var movableTiles = tiles.Where(t => CanMove((Point)t.Tag, (Point)emptyTile.Tag)).ToList();
                        if (movableTiles.Count > 0)
                        {
                            PictureBox randomTile = movableTiles[random.Next(movableTiles.Count)];
                            Point tilePos = (Point)randomTile.Tag;
                            Point emptyPos = (Point)emptyTile.Tag;
                            SwapTiles(randomTile, emptyTile);
                            randomTile.Tag = emptyPos;
                            emptyTile.Tag = tilePos;
                        }
                        await Task.Delay(5);
                        Application.DoEvents();
                    }
                }

                movesCount = 0;
                UpdateMovesCounter();
            }
            finally
            {
                shuffleButton.Enabled = true;
                shuffleButton.Text = "Перемешать";
                isShuffling = false;
            }
        }

        private void UpdateMovesCounter()
        {
            if (movesLabel != null)
                movesLabel.Text = $"Ходы: {movesCount}";
        }

        private void ShowOriginalImage()
        {
            Form previewForm = new Form();
            previewForm.Text = "Оригинальное изображение";
            previewForm.Size = new Size(500, 500);
            previewForm.StartPosition = FormStartPosition.CenterParent;
            previewForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            previewForm.MaximizeBox = false;

            PictureBox previewBox = new PictureBox();
            previewBox.Dock = DockStyle.Fill;
            previewBox.SizeMode = PictureBoxSizeMode.Zoom;
            previewBox.Image = originalImage;

            previewForm.Controls.Add(previewBox);
            previewForm.ShowDialog();
        }

        private void CheckAndSaveRecord()
        {
            bool isNewRecord = recordManager.TryAddRecord(gridSize, movesCount);
            int? best = recordManager.GetBestMoves(gridSize);

            string message = $"Вы собрали пазл за {movesCount} ходов!";
            if (isNewRecord)
            {
                if (best.HasValue && best.Value == movesCount)
                    message += "\n\n✨ НОВЫЙ РЕКОРД! ✨";
                else
                    message += "\n\n🎉 Это ваш первый рекорд для данной сложности! 🎉";
            }
            else if (best.HasValue)
            {
                message += $"\n\nЛучший результат: {best.Value} ходов.";
            }

            MessageBox.Show(message, "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (originalImage != null && WindowState != FormWindowState.Minimized)
            {
                CreateGameBoard();
            }
        }

        private void Form5_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}