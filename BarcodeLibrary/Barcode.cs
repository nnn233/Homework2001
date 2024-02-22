using System.Drawing;

namespace BarcodeLibrary
{
    public class Barcode
    {
        static uint[,] Lcode = new uint[10, 7]{
            {0,0,0,1,1,0,1},
            {0,0,1,1,0,0,1},
            {0,0,1,0,0,1,1},
            {0,1,1,1,1,0,1},
            {0,1,0,0,0,1,1},
            {0,1,1,0,0,0,1},
            {0,1,0,1,1,1,1},
            {0,1,1,1,0,1,1},
            {0,1,1,0,1,1,1},
            {0,0,0,1,0,1,1}
        };

        static uint[,] Rcode = new uint[10, 7]{
            {1,1,1,0,0,1,0},
            {1,1,0,0,1,1,0},
            {1,1,0,1,1,0,0},
            {1,0,0,0,0,1,0},
            {1,0,1,1,1,0,0},
            {1,0,0,1,1,1,0},
            {1,0,1,0,0,0,0},
            {1,0,0,0,1,0,0},
            {1,0,0,1,0,0,0},
            {1,1,1,0,1,0,0}
        };

        static uint[,] Divcode = new uint[3, 5] {
            {1,0,1,2,2},
            {0,1,0,1,0},
            {1,0,1,2,2}
        };

        public static Bitmap DrawEAN13(int width, int height, string code)
        {
            Bitmap result = new Bitmap(width, height);

            Pen pen = new Pen(Color.White, width / 13 / 7);

            using (Graphics g = Graphics.FromImage(result))
            {
                g.FillRectangle(Brushes.White, new Rectangle(0, 0, result.Width, result.Height));

                float x = pen.Width / 2;
                for (int i = 0; i < 3; i++, x += pen.Width)
                {
                    pen.Color = Divcode[0, i] == 0 ? Color.White : Color.Black;
                    g.DrawLine(pen, x, 0, x, height);
                }

                int n;
                for (int i = 0; i < 6; i++)
                {
                    n = int.Parse(code[i].ToString());
                    for (int j = 0; j < Lcode.GetLength(1); j++, x += pen.Width)
                    {
                        pen.Color = Lcode[n, j] == 0 ? Color.White : Color.Black;
                        g.DrawLine(pen, x, 0, x, height);
                    }
                }


                for (int i = 0; i < Divcode.GetLength(1); i++, x += pen.Width)
                {
                    pen.Color = Divcode[1, i] == 0 ? Color.White : Color.Black;
                    g.DrawLine(pen, x, 0, x, height);
                }

                for (int i = 6; i < code.Length; i++)
                {
                    n = int.Parse(code[i].ToString());
                    for (int j = 0; j < Rcode.GetLength(1); j++, x += pen.Width)
                    {
                        pen.Color = Rcode[n, j] == 0 ? Color.White : Color.Black;
                        g.DrawLine(pen, x, 0, x, height);
                    }
                }

                for (int i = 0; i < 3; i++, x += pen.Width)
                {
                    pen.Color = Divcode[2, i] == 0 ? Color.White : Color.Black;
                    g.DrawLine(pen, x, 0, x, height);
                }
            }

            return result;
        }
    }
}
