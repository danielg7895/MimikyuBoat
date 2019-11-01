using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MimikyuBoat
{
    class ImageRecognition
    {

        #region singleton
        private static ImageRecognition _instance;
        public static ImageRecognition Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ImageRecognition();
                }
                return _instance;
            }
        }
        #endregion

        public int RecognizePlayerCP()
        {

            return 0;
        }

        public void SetPlayerStatStart()
        {

        }

        public int RecognizePlayerStat(int statRow)
        {
            Bitmap bmp;
            while (true)
            {
                // TODO: Hacer un sistema atomico de uso de recursos compartidos como esto para evitar hacer este asqueroso hack.
                try
                {
                    bmp = new Bitmap("temp/player.jpeg");
                    break;

                } catch (System.ArgumentException e)
                {
                    Debug.WriteLine(e.Message);
                    Debug.WriteLine("[RecognizePlayerStatt]: la imagen q se intenta acceder esta borrada capo");
                    Thread.Sleep(200);
                }
            }
            // maxima cantidad negros de pixel que puede llegar a haber
            //int row = (int)Player.Instance.hpRow;
            int statPercentage = 0;

            // Como valor de i selecciono el numero de pixel en donde comienza la barra de hp, cp o mp
            // No estoy teniendo en cuenta en donde termina la barra de vida, por lo que el porcentaje no sera 100% preciso.
            int playerHPBarStart = (int)Player.Instance.hpBarStart;
            int realBar = Math.Abs(playerHPBarStart - bmp.Width);

            for (int i = 0; i < realBar; i++)
            {
                // si cualquier valor de rgb es 200 significa que el pixel es blanco
                // porque previamente la imagen ya se hizo monocromatica
                if (bmp.GetPixel(playerHPBarStart + i, statRow).R >= 200)
                {
                    // pixel blanco
                    if (i == realBar - 1)
                    {
                        statPercentage = ((realBar - Math.Abs(realBar - i)) * 100) / realBar;
                    }
                    continue;
                }
                else
                { 
                    if (AreNeighborsBlack(bmp, i))
                    {
                        // si mis vecinos son negros, calculo el porcentaje. Si son blancos, continuo con el for.
                        statPercentage = ((realBar - Math.Abs(realBar - i)) * 100) / realBar;
                        Console.WriteLine("Player percentage " + statPercentage.ToString());
                        break;
                    }
                }


            }
            bmp.Dispose();
            return statPercentage;
        }

        public bool AreNeighborsBlack(Bitmap bmp, int index)
        {
            // [1][6]
            // [2][7]  
            // [3][8]   ===> Verifico si los pixeles son negros, en ese orden
            // [4][9]
            // [5][10]
            int realIndex = index + (int)Player.Instance.hpBarStart;
            if (bmp.Width-1 == realIndex)
            {
                Debug.WriteLine("Ultimo Pixel es negro!");
                return true;
            }

            int row = (int)Player.Instance.hpRow;
            // retorno true si los pixel vecinos son todos negros
            for (int i = 0; i < 2; i++)
            {
                for (int y = -2; y < 3; y++)
                {
                    if (bmp.GetPixel(realIndex + i, row + y).R >= 200)
                    {
                        // si encuentro un blanco, retorno que no son negros mis vecinos.
                        return false;
                    }
                }
            }
            Debug.WriteLine("Vecinos son negros" + realIndex.ToString());
            return true;
        }

        public int RecognizePlayerMP()
        {

            return 0;
        }

        public int RecognizeTargetHP()
        {
            Bitmap bmp;
            while (true)
            {
                // TODO: Hacer un sistema atomico de uso de recursos compartidos como esto para evitar hacer este asqueroso hack.
                try
                {
                    bmp = new Bitmap("temp/target.jpeg");
                    break;

                }
                catch (System.ArgumentException e)
                {
                    Debug.WriteLine(e.Message);
                    Debug.WriteLine("[RecognizeTargetHP]: la imagen q se intenta acceder esta borrada capo");
                    Thread.Sleep(200);
                }
            }

            int targetBarStart = (int)Target.Instance.hpBarStart;
            int statPercentage = 0;
            // realbar elimina los primeros pixeles que no forman parte de la bar.
            int realBar = Math.Abs(bmp.Width - targetBarStart);

            for (int i = 0; i < realBar ; i++)
            {
                // si cualquier valor de rgb es 255 significa que el pixel es blanco
                // porque previamente la imagen ya se hizo monocromatica
                if (bmp.GetPixel(targetBarStart + i, (int)Target.Instance.hpRow).R >= 200 )
                {
                    // pixel blanco
                    if (i == realBar - 1)
                    {
                        Debug.WriteLine("Recognize Target HP -------->>> percentage es 100");
                        statPercentage = ((realBar - Math.Abs(realBar - i)) * 100) / realBar;
                    }
                    continue;
                }
                else
                {
                    statPercentage = ((realBar - Math.Abs(realBar - i)) * 100) / realBar;
                    Console.WriteLine("Target percentage" + statPercentage.ToString());
                    break;
                }

            }
            bmp.Dispose();

            return statPercentage;
        }

        public int GetTargetBarPixelStart(int statRow)
        {
            return GetBarPixelStart(Target.Instance.imagePath, statRow);
        }
        public int GetPlayerBarPixelStart(int statRow)
        {
            return GetBarPixelStart(Player.Instance.imagePath, statRow);
        }


        int GetBarPixelStart(string imagePath, int statRow)
        {
            // intenta obtener en que pixel comienza la barrita del stat (hp,mp,cp, hp target)
            // esta funcion falla si el primer pixel es blanco.
            Bitmap bmp = new Bitmap(imagePath);

            for (int i = 0; i < bmp.Width; i++)
            {
                // si cualquier valor de rgb es 255 significa que el pixel es blanco
                // porque previamente la imagen ya se hizo monocromatica
                if (bmp.GetPixel(i, statRow).R >= 200)
                {
                    // pixel blanco
                    bmp.Dispose();
                    return i;
                }
            }
            bmp.Dispose();

            return 0;
        }
    }
}
