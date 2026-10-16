using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WMPLib;

namespace Пятнашки
{
    public partial class Form1 : Form
    {
        private Image hoverImage;
        private Image hoverImage2;
        private WindowsMediaPlayer player;
        private List<string> playlist;
        private Random random;

        public Form1()
        {
            InitializeComponent();
            InitializeMusic();

            LoadHoverImages();
            AttachHoverEvents();
        }

        private void LoadHoverImages()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string resourcesPath = Path.Combine(basePath, "Resources");

            string img1Path = Path.Combine(resourcesPath, "button 1 click копия.png");
            string img2Path = Path.Combine(resourcesPath, "button 4 click.png");

            if (File.Exists(img1Path))
                hoverImage = Image.FromFile(img1Path);
            else
                Console.WriteLine("Не найден файл: " + img1Path);

            if (File.Exists(img2Path))
                hoverImage2 = Image.FromFile(img2Path);
            else
                Console.WriteLine("Не найден файл: " + img2Path);
        }

        private void AttachHoverEvents()
        {
            if (button1 != null)
            {
                button1.MouseEnter += (s, e) =>
                {
                    if (hoverImage != null)
                    {
                        button1.Tag = button1.BackgroundImage;
                        button1.BackgroundImage = hoverImage;
                    }
                };
                button1.MouseLeave += (s, e) =>
                {
                    if (button1.Tag != null && button1.Tag is Image oldImg)
                        button1.BackgroundImage = oldImg;
                };
            }

            if (buttonRecords != null)
            {
                buttonRecords.MouseEnter += (s, e) =>
                {
                    if (hoverImage2 != null)
                    {
                        buttonRecords.Tag = buttonRecords.BackgroundImage;
                        buttonRecords.BackgroundImage = hoverImage2;
                    }
                };
                buttonRecords.MouseLeave += (s, e) =>
                {
                    if (buttonRecords.Tag != null && buttonRecords.Tag is Image oldImg)
                        buttonRecords.BackgroundImage = oldImg;
                };
            }
        }

        private void InitializeMusic()
        {
            try
            {
                player = new WindowsMediaPlayer();
                player.settings.volume = 50;
                player.PlayStateChange += Player_PlayStateChange;

                playlist = new List<string>();
               
                string musicDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Музыка");
                if (Directory.Exists(musicDir))
                {
                    var mp3Files = Directory.GetFiles(musicDir, "*.mp3");
                    foreach (var file in mp3Files)
                        playlist.Add(file);
                }
                else
                {
                    Console.WriteLine("Папка Music не найдена, музыка отключена.");
                    return;
                }

                if (playlist.Count == 0)
                {
                    Console.WriteLine("Нет MP3-файлов в папке Music");
                    return;
                }

                random = new Random();
                PlayRandomTrack();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка инициализации музыки: " + ex.Message);
            }
        }

        private void PlayRandomTrack()
        {
            if (playlist == null || playlist.Count == 0) return;
            try
            {
                string trackPath = playlist[random.Next(playlist.Count)];
                player.controls.stop();
                player.URL = trackPath;
                player.controls.play();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка воспроизведения: " + ex.Message);
            }
        }

        private void Player_PlayStateChange(int NewState)
        {
            WMPPlayState state = (WMPPlayState)NewState;
            if (state == WMPPlayState.wmppsMediaEnded || state == WMPPlayState.wmppsStopped)
            {
                
                System.Threading.Timer timer = null;
                timer = new System.Threading.Timer(_ =>
                {
                    PlayRandomTrack();
                    timer?.Dispose();
                }, null, 500, System.Threading.Timeout.Infinite);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 secondForm = new Form2();
            secondForm.Show();
            this.WindowState = FormWindowState.Minimized;
        }

        private void buttonRecords_Click(object sender, EventArgs e)
        {
            FormRecords recordsForm = new FormRecords();
            recordsForm.ShowDialog();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            player?.controls.stop();
            player?.close();
            base.OnFormClosed(e);
        }
    }
}