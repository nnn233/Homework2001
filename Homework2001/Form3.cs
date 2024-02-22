using System.Drawing;
using System;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace Homework2001
{
    public partial class Form3 : Form
    {
        private string text = String.Empty;
        public Form3()
        {
            InitializeComponent();
            Load += Form3_Load;

        }

        private Bitmap CreateImage(int Width, int Height)
        {
            Random random = new Random();
            Bitmap result = new Bitmap(Width, Height);

            int[] fontSizes = new int[] { 15, 20, 25, 30, 35 };
            Brush[] colors = { Brushes.Black,
                     Brushes.Red,
                     Brushes.RoyalBlue,
                     Brushes.Green };

            string[] fontNames = new string[]
            {
                "Comic Sans MS",
                "Arial",
                "Times New Roman",
                "Georgia",
                "Verdana",
                "Geneva"
            };

            FontStyle[] fontStyles = new FontStyle[]
            {
                FontStyle.Bold,
                FontStyle.Italic,
                FontStyle.Regular,
                FontStyle.Strikeout,
                FontStyle.Underline
            };

            HatchStyle[] hatchStyles = new HatchStyle[]
            {
                HatchStyle.BackwardDiagonal, HatchStyle.Cross,
                HatchStyle.DashedDownwardDiagonal, HatchStyle.DashedHorizontal,
                HatchStyle.DashedUpwardDiagonal, HatchStyle.DashedVertical,
                HatchStyle.DiagonalBrick, HatchStyle.DiagonalCross,
                HatchStyle.Divot, HatchStyle.DottedDiamond, HatchStyle.DottedGrid,
                HatchStyle.ForwardDiagonal, HatchStyle.Horizontal,
                HatchStyle.HorizontalBrick, HatchStyle.LargeCheckerBoard,
                HatchStyle.LargeConfetti, HatchStyle.LargeGrid,
                HatchStyle.LightDownwardDiagonal, HatchStyle.LightHorizontal,
                HatchStyle.LightUpwardDiagonal, HatchStyle.LightVertical,
                HatchStyle.Max, HatchStyle.Min, HatchStyle.NarrowHorizontal,
                HatchStyle.NarrowVertical, HatchStyle.OutlinedDiamond,
                HatchStyle.Plaid, HatchStyle.Shingle, HatchStyle.SmallCheckerBoard,
                HatchStyle.SmallConfetti, HatchStyle.SmallGrid,
                HatchStyle.SolidDiamond, HatchStyle.Sphere, HatchStyle.Trellis,
                HatchStyle.Vertical, HatchStyle.Wave, HatchStyle.Weave,
                HatchStyle.WideDownwardDiagonal, HatchStyle.WideUpwardDiagonal, HatchStyle.ZigZag
            };


            Graphics g = Graphics.FromImage((Image)result);
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            text = String.Empty;
            string ALF = "1234567890abcdefghjklmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ";
            for (int i = 0; i < 5; ++i)
                text += ALF[random.Next(ALF.Length)];

            RectangleF rectangle = new RectangleF(0, 0, Width, Height);

            var brush = new HatchBrush(hatchStyles[random.Next
              (hatchStyles.Length - 1)], Color.FromArgb((random.Next(100, 255)),
              (random.Next(100, 255)), (random.Next(100, 255))), Color.White);
            g.FillRectangle(brush, rectangle);

            int xPos = random.Next(20, 120);
            int yPos = random.Next(50, Height - 50);

            int method = random.Next(0, 2);
            var brushText = new SolidBrush(Color.FromArgb(random.Next(0, 100),
                                    random.Next(0, 100), random.Next(0, 100)));

            var font = new Font(fontNames[random.Next(fontNames.Length - 1)],
                         fontSizes[random.Next(fontSizes.Length - 1)],
                         fontStyles[random.Next(fontStyles.Length - 1)]);
            if (method == 0)
            {
                for (int i = 0; i <= text.Length - 1; i++)
                {
                    g.DrawString
                    (
                        text.Substring(i, 1),
                        font,
                        brushText,
                        xPos,
                        yPos

                    );
                    brushText = new SolidBrush(Color.FromArgb(random.Next(0, 100),
                                        random.Next(0, 100), random.Next(0, 100)));

                    font = new Font(fontNames[random.Next(fontNames.Length - 1)],
                                 fontSizes[random.Next(fontSizes.Length - 1)],
                                 fontStyles[random.Next(fontStyles.Length - 1)]);

                    xPos += random.Next(15, 18);
                }
            }
            else
            {
                g.DrawString(text,
                    font,
                    brushText,
                    xPos,
                    yPos);

                int height = yPos + 20;
                Pen pen = new Pen(colors[random.Next(colors.Length)], 5);
                g.DrawLine(pen,
                           new Point(xPos - 30, height),
                           new Point(Width - 50, height));
            }

            for (int i = 0; i < Width; ++i)
                for (int j = 0; j < Height; ++j)
                    if (random.Next() % 20 == 0)
                        result.SetPixel(i, j, Color.White);

            return result;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            UpdateImage();
        }

        private void UpdateImage()
        {
            pictureBox1.Image = this.CreateImage(pictureBox1.Width, pictureBox1.Height);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == this.text)
                Visible = false;
            else
            {
                MessageBox.Show("Неверно! Попробуйте ещё раз");
                textBox1.Clear();
                UpdateImage();
            }
        }
    }
}
