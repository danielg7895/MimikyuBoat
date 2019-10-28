using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace MimikyuBoat
{
    class MimikyuBoat
    {
        // si estoy seleccionando el area del player o target
        public volatile bool playerAreaConfigurationPhase = false;
        public volatile bool targetAreaConfigurationPhase = false;
        public Rectangle playerRect;
        public Rectangle targetRect;
        public volatile int updateInterval = 1000;

        // private variables
        int GCCleanInterval;
        string playerImagePath = "temp/player.jpeg";
        string targetImagePath = "temp/target.jpeg";

        ImageManager imageManager;
        ImageRecognition imageRecognition;
        Player player;
        Target target;
        Form1 form1;

        #region player and target stats
        int playerCP;
        int playerHP;
        int playerMP;

        int targetHP;
        #endregion

        #region shurtcuts
        Keyboard.DirectXKeyStrokes hpPot = Keyboard.DirectXKeyStrokes.DIK_5;
        Keyboard.DirectXKeyStrokes target1 = Keyboard.DirectXKeyStrokes.DIK_8;
        Keyboard.DirectXKeyStrokes target2 = Keyboard.DirectXKeyStrokes.DIK_9;
        Keyboard.DirectXKeyStrokes attack = Keyboard.DirectXKeyStrokes.DIK_0;

        #endregion

        Keyboard.DirectXKeyStrokes previousTarget;
        Keyboard.DirectXKeyStrokes currentTarget;

        public MimikyuBoat(Form1 form1)
        {
            this.form1 = form1;
            imageManager = new ImageManager();
            imageRecognition = new ImageRecognition();
            player = Player.Instance;
            target = Target.Instance;

            Thread updateThread = new Thread(new ThreadStart(Update));
            updateThread.Start();
        }

        public void Update()
        {
            int counter = 0;
            GCCleanInterval = updateInterval * 30;

            // metodo que se ejecuta cada intervalo definido, sirve para actualizar todo lo que es UI y pertenece a esta clase.
            while (true)
            {
                counter += updateInterval;

                if (form1.playerRegionLoaded)
                {
                    // guardo la imagen del player
                    Bitmap bmp = imageManager.GetImageFromRect(playerRect);
                    if ((System.IO.File.Exists(playerImagePath)))
                    {
                        System.IO.File.Delete(playerImagePath);
                    }
                    bmp.Save(playerImagePath, System.Drawing.Imaging.ImageFormat.Jpeg);

                    // actualizo imagen del player en la interfaz
                    form1.Invoke((MethodInvoker)delegate
                    {
                        form1.SetPlayerImage(bmp);
                    });
                }

                if (form1.targetRegionLoaded)
                {
                    // guardo la imagen del target
                    Bitmap bmp = imageManager.GetImageFromRect(targetRect);
                    bmp.Save(targetImagePath, System.Drawing.Imaging.ImageFormat.Jpeg);

                    // actualizo imagen del target en la interfaz
                    form1.Invoke((MethodInvoker)delegate
                    {
                        form1.SetTargetImage(bmp);
                    });
                }
                if ( (counter % GCCleanInterval) == 0)
                {
                    // ejecuto el colector de mugre cada updateinterval * 30
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    counter = 0;
                }


                Thread.Sleep(updateInterval);
            }

        }

        public void UpdateStats()
        {

        }

        public void Start()
        {
            // Verifico si estan cargadas las imagenes del player y del target
            if (!form1.playerRegionLoaded)
            {
                StartPlayerAreaConfiguration();
            }
            if (!form1.targetRegionLoaded)
            {
                StartTargetAreaConfiguration();
            }

            // comienzo el bot
            PrintDebug("Comenzando a botear !");
            form1.Invoke((MethodInvoker)delegate
            {
                form1.ChangeFormColor(Color.Green);
            });

            while (true)
            {
                // obtengo stats del player y del target reconociendo las imagenes
                playerCP = imageRecognition.RecognizePlayerCP();
                playerHP = imageRecognition.RecognizePlayerHP();
                playerMP = imageRecognition.RecognizePlayerMP();
                targetHP = imageRecognition.RecognizeTargetHP();
                UpdateStats();
                PrintDebug("Player HP: " + playerHP.ToString());
                PrintDebug("Target HP: " + targetHP.ToString());

                if (playerHP < 50)
                {
                    PrintDebug("Player HP muy baja, usando pocion");
                    //UsePotion();
                }
                // TODO: si no hay target > buscar target
                // TODO: agregar timer
                if (targetHP <= 0)
                {
                    PrintDebug("Target Muerto, buscando siguiente target...");
                    previousTarget = currentTarget;
                    Thread.Sleep(500);
                }
                currentTarget = target1;
                //AttackTarget(currentTarget);
                Thread.Sleep(updateInterval);
            }

        }

        public void UsePotion()
        {
            UseShortCut(hpPot);
        }

        public void AttackTarget(Keyboard.DirectXKeyStrokes targetShortCut)
        {
            if(currentTarget == previousTarget)
            {

            }
            UseShortCut(targetShortCut); // targeteo mob
            Thread.Sleep(500);
            UseShortCut(attack); // ataco
        }

        public void UseShortCut(Keyboard.DirectXKeyStrokes key, int miliseconds = 121)
        {
            Keyboard.SendKey(key, false, Keyboard.InputType.Keyboard);
            Thread.Sleep(miliseconds);
            Keyboard.SendKey(key, true, Keyboard.InputType.Keyboard);
        }

        public void StartPlayerAreaConfiguration()
        {
            // TODO: verificar previamente si ya hay una area del player guardada
            PrintDebug("Pone el cursor en la esquina superior donde esta la vida de tu pj y apreta enter.");
            while(!form1.enterPressed)
            {
                Thread.Sleep(200);
            }
            form1.enterPressed = false;
            Point startPos = imageManager.GetCursorPosition();

            PrintDebug("Pone el cursor en la esquina inferior donde esta la vida de tu pj y apreta enter.");
            while (!form1.enterPressed)
            {
                Thread.Sleep(200);
            }
            form1.enterPressed = false;
            Point endPos = imageManager.GetCursorPosition();

            Debug.WriteLine(startPos);
            Debug.WriteLine(endPos);

            Size rectSize = new Size(Math.Abs(startPos.X - endPos.X), Math.Abs(startPos.Y - endPos.Y));
            playerRect = new Rectangle(startPos, rectSize);

            // seteo como que la region del player ya esta cargada para poder comenzar el update.
            form1.playerRegionLoaded = true;
            PrintDebug("Area del player guardada");

        }

        public void StartTargetAreaConfiguration()
        {
            // TODO: verificar previamente si ya hay una area del target guardada
            PrintDebug("Pone el cursor en la esquina superior donde esta la vida del target y apreta enter.");
            while (!form1.enterPressed)
            {
                Thread.Sleep(200);
            }
            form1.enterPressed = false;
            Point startPos = imageManager.GetCursorPosition();

            PrintDebug("Pone el cursor en la esquina inferior donde esta la vida del target y apreta enter.");
            while (!form1.enterPressed)
            {
                Thread.Sleep(200);
            }
            form1.enterPressed = false;
            Point endPos = imageManager.GetCursorPosition();

            Debug.WriteLine(startPos);
            Debug.WriteLine(endPos);

            Size rectSize = new Size(Math.Abs(startPos.X - endPos.X), Math.Abs(startPos.Y - endPos.Y));
            targetRect = new Rectangle(startPos, rectSize);

            // seteo como que la region del player ya esta cargada para poder comenzar el update.
            form1.targetRegionLoaded = true;
            PrintDebug("Area del target guardada");
        }

        void PrintDebug(string text)
        {
            form1.Invoke((MethodInvoker)delegate
            {
                form1.ConsoleWrite(text);
            });
        }

    }
}
