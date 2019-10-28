using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MimikyuBoat
{
    class ImageRecognition
    {
        public int playerCPRow;
        public int playerHPRow;
        public int playerMPRow;
        public int targetHPRow;

        public ImageRecognition()
        {

        }

        public int RecognizePlayerCP()
        {

            return 0;
        }

        public int RecognizePlayerHP()
        {
            Bitmap bmp = new Bitmap("temp/player.jpeg");

            int percentageHP = 0;
            for (int i = 0; i < bmp.Width; i++)
            {
                // si cualquier valor de rgb es 255 significa que el pixel es blanco
                // porque previamente la imagen ya se hizo monocromatica
                if (bmp.GetPixel(i, 0).R == 255)
                {
                    // pixel blanco
                    continue;
                }
                else
                {
                    percentageHP = (i * 100) / bmp.Width;
                    break;
                }
            }
            bmp.Dispose();
            return percentageHP;
        }

        public int RecognizePlayerMP()
        {

            return 0;
        }

        public int RecognizeTargetHP()
        {
            Bitmap bmp = new Bitmap("temp/target.jpeg");

            int percentageHP = 0;
            for (int i = 0; i < bmp.Width; i++)
            {
                // si cualquier valor de rgb es 255 significa que el pixel es blanco
                // porque previamente la imagen ya se hizo monocromatica
                if (bmp.GetPixel(i, 0).R == 255 )
                {
                    // pixel blanco
                    continue;
                }
                else
                {
                    percentageHP = (i * 100) / bmp.Width;
                    break;
                }
            }
            bmp.Dispose();

            return percentageHP;
        }
    }
}
