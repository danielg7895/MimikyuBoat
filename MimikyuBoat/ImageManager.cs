using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace MimikyuBoat
{
    class ImageManager
    {
        #region referencias
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetDesktopWindow();
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetWindowDC(IntPtr window);
        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern uint GetPixel(IntPtr dc, int x, int y);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int ReleaseDC(IntPtr window, IntPtr dc);
        #endregion

        private static ImageManager _Instance;
        public static ImageManager Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new ImageManager();
                }
                return _Instance;
            }
        }

        Bitmap bmp;

        public Point GetCursorPosition()
        {
            Point cursorPos = Cursor.Position;
            return cursorPos;
        }


        public Bitmap GetImageFromRect(Rectangle rect)
        {
            bmp = new Bitmap(rect.Width, rect.Height, PixelFormat.Format24bppRgb);

            Graphics g = Graphics.FromImage(bmp);
            g.CopyFromScreen(rect.Left, rect.Top, 0, 0, bmp.Size, CopyPixelOperation.SourceCopy);

            ToGrayScale(bmp);

            return bmp;
        }

        public void ToGrayScale(Bitmap Bmp)
        {
            Color c;

            for (int y = 0; y < Bmp.Height; y++)
                for (int x = 0; x < Bmp.Width; x++)
                {
                    c = Bmp.GetPixel(x, y);
                    if(GetBrightness(c) > 70)
                    {
                        Bmp.SetPixel(x, y, Color.FromArgb(255, 255, 255));

                    } else
                    {
                        Bmp.SetPixel(x, y, Color.FromArgb(0, 0, 0));
                    }
                }
        }

        public double GetBrightness(Color color)
        {
            return (0.2126 * color.R + 0.7152 * color.G + 0.0722 * color.B);
        }

        public string GetImageFromPoint(Point startPos, Point endPos)
        {
            string path = "";
            return path;
        }

    }
}
