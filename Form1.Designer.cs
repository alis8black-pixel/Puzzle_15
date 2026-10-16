namespace Пятнашки
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>  
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            button1 = new Button();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            buttonRecords = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)axWindowsMediaPlayer1).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = Color.Transparent;
            button1.BackgroundImage = Properties.Resources.button_1_копия1;
            button1.BackgroundImageLayout = ImageLayout.Stretch;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button1.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Location = new Point(158, 383);
            button1.Name = "button1";
            button1.Size = new Size(196, 71);
            button1.TabIndex = 2;
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = Properties.Resources.Copilot_20251107_1302063;
            pictureBox1.Location = new Point(-2, 120);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(525, 109);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.Image = Properties.Resources._6SSp1;
            pictureBox2.Location = new Point(-35, 0);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(568, 685);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 5;
            pictureBox2.TabStop = false;
            // 
            // axWindowsMediaPlayer1
            // 
            axWindowsMediaPlayer1.Enabled = true;
            axWindowsMediaPlayer1.Location = new Point(62, 701);
            axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            axWindowsMediaPlayer1.OcxState = (AxHost.State)resources.GetObject("axWindowsMediaPlayer1.OcxState");
            axWindowsMediaPlayer1.Size = new Size(225, 47);
            axWindowsMediaPlayer1.TabIndex = 6;
            axWindowsMediaPlayer1.Visible = false;
            // 
            // buttonRecords
            // 
            buttonRecords.BackColor = Color.Transparent;
            buttonRecords.BackgroundImage = Properties.Resources.button_4;
            buttonRecords.BackgroundImageLayout = ImageLayout.Stretch;
            buttonRecords.FlatAppearance.BorderSize = 0;
            buttonRecords.FlatAppearance.MouseDownBackColor = Color.Transparent;
            buttonRecords.FlatAppearance.MouseOverBackColor = Color.Transparent;
            buttonRecords.FlatStyle = FlatStyle.Flat;
            buttonRecords.Location = new Point(158, 497);
            buttonRecords.Name = "buttonRecords";
            buttonRecords.Size = new Size(196, 68);
            buttonRecords.TabIndex = 8;
            buttonRecords.UseVisualStyleBackColor = false;
            buttonRecords.Click += buttonRecords_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.photo_2025_11_06_16_03_56_копия_2;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(521, 760);
            Controls.Add(buttonRecords);
            Controls.Add(axWindowsMediaPlayer1);
            Controls.Add(button1);
            Controls.Add(pictureBox1);
            Controls.Add(pictureBox2);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Главное меню";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)axWindowsMediaPlayer1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Button button1;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
        private Button buttonRecords;
    }
}
