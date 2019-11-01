using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace MimikyuBoat
{
    class BotConfiguration
    {
        public Rectangle playerRect;
        public Rectangle targetRect;

        Form1 form1;
        ImageManager imageManager;
        Player player;
        Target target;


        public BotConfiguration(Form1 form1)
        {
            this.form1 = form1;
            imageManager = ImageManager.Instance;
            player = Player.Instance;
            target = Target.Instance;
        }

        public void StartPlayerAreaConfiguration()
        {
            // TODO: verificar previamente si ya hay una area del player guardada
            form1.ConsoleWrite("Pone el cursor en la esquina superior donde esta la vida de tu pj y apreta A.");
            while (!form1.enterPressed)
            {
                Thread.Sleep(200);
            }
            Point startPos = imageManager.GetCursorPosition();
            form1.enterPressed = false;

            form1.ConsoleWrite("Pone el cursor en la esquina inferior donde esta la vida de tu pj y apreta A.");
            while (!form1.enterPressed)
            {
                Thread.Sleep(200);
            }
            Point endPos = imageManager.GetCursorPosition();
            form1.enterPressed = false;

            Debug.WriteLine(startPos);
            Debug.WriteLine(endPos);

            Size rectSize = new Size(Math.Abs(startPos.X - endPos.X), Math.Abs(startPos.Y - endPos.Y));
            playerRect = new Rectangle(startPos, rectSize);

            // seteo como que la region del player ya esta cargada para poder comenzar el update.
            form1.playerRegionLoaded = true;
            form1.ConsoleWrite("Area del player guardada");

            form1.Invoke((MethodInvoker)delegate
            {
                form1.SAVE_SETTINGS();
            });
        }

        public void StartTargetAreaConfiguration()
        {
            // TODO: verificar previamente si ya hay una area del target guardada
            form1.ConsoleWrite("Pone el cursor en la esquina superior donde esta la vida del target y apreta enter.");
            while (!form1.enterPressed)
            {
                Thread.Sleep(200);
            }
            form1.enterPressed = false;
            Point startPos = imageManager.GetCursorPosition();

            form1.ConsoleWrite("Pone el cursor en la esquina inferior donde esta la vida del target y apreta enter.");
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
            form1.ConsoleWrite("Area del target guardada");

            form1.Invoke((MethodInvoker)delegate
            {
                form1.SAVE_SETTINGS();
            });
        }

        public void ConfigureBarBounds()
        {

            // seteo la fila de pixeles de cada stat.
            if (form1.zoneComboBoxValue == "Player CP")
            {
                player.cpRow = form1.playerStatsMarkerValue;
                player.cpBarStart = ImageRecognition.Instance.GetPlayerBarPixelStart((int)player.cpRow);
                BotSettings.PLAYER_CP_BARSTART_INITIALIZED = true;
            }
            else if (form1.zoneComboBoxValue == "Player HP")
            {
                player.hpRow = form1.playerStatsMarkerValue;
                player.hpBarStart = ImageRecognition.Instance.GetPlayerBarPixelStart((int)player.hpRow);
                BotSettings.PLAYER_HP_BARSTART_INITIALIZED = true;

                Debug.WriteLine("Player Row: " + Player.Instance.hpRow.ToString());
            }
            else if (form1.zoneComboBoxValue == "Player MP")
            {
                player.mpRow = form1.playerStatsMarkerValue;
                player.mpBarStart = ImageRecognition.Instance.GetPlayerBarPixelStart((int)player.mpRow);
                BotSettings.PLAYER_MP_BARSTART_INITIALIZED = true;
            }
            else if (form1.zoneComboBoxValue == "Target HP")
            {
                Target.Instance.hpRow = form1.targetStatsMarkerValue;
                Target.Instance.hpBarStart = ImageRecognition.Instance.GetTargetBarPixelStart((int)Target.Instance.hpRow);
                BotSettings.TARGET_HP_BARSTART_INITIALIZED = true;

                Debug.WriteLine("Target HP bar start: " + Target.Instance.hpBarStart.ToString());
                Debug.WriteLine("Target Row: " + Target.Instance.hpRow.ToString());
            }
        }

    }
}
