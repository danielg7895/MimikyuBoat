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
using System.IO;
using System.Threading;

namespace Shizui
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
        [DllImport("dwmapi.dll")]
        public static extern int DwmGetWindowAttribute(IntPtr hwnd, int dwAttribute, out Rectangle pvAttribute, int cbAttribute);
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool PrintWindow(IntPtr hwnd, IntPtr hDC, uint nFlags);
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

        [Flags]
        private enum DwmWindowAttribute : uint
        {
            DWMWA_NCRENDERING_ENABLED = 1,
            DWMWA_NCRENDERING_POLICY,
            DWMWA_TRANSITIONS_FORCEDISABLED,
            DWMWA_ALLOW_NCPAINT,
            DWMWA_CAPTION_BUTTON_BOUNDS,
            DWMWA_NONCLIENT_RTL_LAYOUT,
            DWMWA_FORCE_ICONIC_REPRESENTATION,
            DWMWA_FLIP3D_POLICY,
            DWMWA_EXTENDED_FRAME_BOUNDS,
            DWMWA_HAS_ICONIC_BITMAP,
            DWMWA_DISALLOW_PEEK,
            DWMWA_EXCLUDED_FROM_PEEK,
            DWMWA_CLOAK,
            DWMWA_CLOAKED,
            DWMWA_FREEZE_REPRESENTATION,
            DWMWA_LAST
        }

        public Point GetCursorPosition()
        {
            Point cursorPos = Cursor.Position;
            return cursorPos;
        }

        public bool UpdateTargets()
        {
            if (!BotSettings.PLAYER_CONFIGURATION_RECTANGLE.Size.IsEmpty)
            {
                // guardo la imagen del player
                Bitmap bmp = GetImageFromRect(BotSettings.PLAYER_CONFIGURATION_RECTANGLE, "player");
                if (bmp == null)
                    return false;

                if (File.Exists(Player.Instance.imagePath))
                {
                    while (true)
                    {
                        try
                        {
                            File.Delete(Player.Instance.imagePath);
                            break;
                        }
                        catch (Exception)
                        {
                            Debug.WriteLine("Imagen siendo usada, re intentando... ");
                            Thread.Sleep(200);
                        }
                    }
                }
                bmp.Save(Player.Instance.imagePath, ImageFormat.Jpeg);
                BotSettings.PLAYER_IMAGE = new Bitmap(bmp);
                bmp.Dispose();
            }

            if (!BotSettings.TARGET_CONFIGURATION_RECTANGLE.Size.IsEmpty)
            {
                // guardo la imagen del target
                Bitmap bmp = GetImageFromRect(BotSettings.TARGET_CONFIGURATION_RECTANGLE, "target");
                if (bmp == null) return false;

                if (File.Exists(Target.Instance.imagePath))
                {
                    while (true)
                    {
                        try
                        {
                            File.Delete(Target.Instance.imagePath);
                            break;
                        }
                        catch (Exception)
                        {
                            Debug.WriteLine("Imagen siendo usada, re intentando... ");
                            Thread.Sleep(200);
                        }
                    }
                }
                bmp.Save(Target.Instance.imagePath, ImageFormat.Jpeg);
                BotSettings.TARGET_IMAGE = new Bitmap(bmp);
                bmp.Dispose();
            }
            return true;
        }

        public Bitmap GetImageFromRect(Rectangle rect, string type)
        {
            IntPtr hwnd = BotSettings.L2_PROCESS_HANDLE;
            Rectangle windowRect = GetProcessRectangle();
            if (windowRect.IsEmpty)
                return null;

            Bitmap bmp = new Bitmap(windowRect.Width - windowRect.Left, windowRect.Height - windowRect.Top, PixelFormat.Format32bppRgb);
            Graphics gfxBmp = Graphics.FromImage(bmp);

            IntPtr hdcBitmap;
            try
            {
                hdcBitmap = gfxBmp.GetHdc();
            }
            catch
            {
                return null;
            }
            bool succeeded = PrintWindow(hwnd, hdcBitmap, 0);

            gfxBmp.ReleaseHdc(hdcBitmap);
            gfxBmp.Dispose();
            if (!succeeded) return null;

            Bitmap subBmp;
            try
            {
                subBmp = bmp.Clone(rect, PixelFormat.Format32bppRgb);
            }
            catch (OutOfMemoryException)
            {
                Debug.WriteLine("Juego minimizado, interrumpiendo");
                return null;
            }

            ToGrayScale(subBmp, type);

            bmp.Dispose();

            return subBmp;
        }

        public Rectangle GetProcessRectangle()
        {
            if (BotSettings.L2_PROCESS_HANDLE == IntPtr.Zero) return Rectangle.Empty;

            Rectangle rect;

            int size = Marshal.SizeOf(typeof(Rectangle));
            DwmGetWindowAttribute(BotSettings.L2_PROCESS_HANDLE, (int)DwmWindowAttribute.DWMWA_EXTENDED_FRAME_BOUNDS, out rect, size);

            return rect;
        }

        public void ToGrayScale(Bitmap Bmp, string type)
        {
            Color c;
            for (int y = 0; y < Bmp.Height; y++)
                for (int x = 0; x < Bmp.Width; x++)
                {
                    c = Bmp.GetPixel(x, y);
                    float hue = c.GetHue();
                    if (type == "player" ) { 
                        if(GetBrightness(c) > BotSettings.PLAYER_BAR_BRIGHTNESS 
                            && c.R >= BotSettings.PLAYER_BAR_R
                            && c.G >= BotSettings.PLAYER_BAR_G
                            && c.B >= BotSettings.PLAYER_BAR_B
                            &&  hue >= BotSettings.PLAYER_BAR_HUE
                            )
                        {
                        
                            Bmp.SetPixel(x, y, Color.FromArgb(255, 255, 255));

                        } else
                        {
                            Bmp.SetPixel(x, y, Color.FromArgb(0, 0, 0));
                        }
                    }

                    if (type == "target")
                    {
                        if (GetBrightness(c) > BotSettings.TARGET_BAR_BRIGHTNESS
                            && c.R >= BotSettings.TARGET_BAR_R
                            && c.G >= BotSettings.TARGET_BAR_G
                            && c.B >= BotSettings.TARGET_BAR_B
                            && hue >= BotSettings.TARGET_BAR_HUE
                            )
                        {

                            Bmp.SetPixel(x, y, Color.FromArgb(255, 255, 255));

                        }
                        else
                        {
                            Bmp.SetPixel(x, y, Color.FromArgb(0, 0, 0));
                        }
                    }

                }
        }

        public double GetBrightness(Color color)
        {
            return (0.2126 * color.R + 0.7152 * color.G + 0.0722 * color.B);
        }

    }
}
