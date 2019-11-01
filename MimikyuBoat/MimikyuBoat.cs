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

        public volatile int updateInterval = 1000;
        public bool paused = false;
        public bool botEnabled = false;

        // private variables
        readonly ImageManager imageManager;
        readonly ImageRecognition imageRecognition;
        readonly Player player;
        readonly Target target;
        readonly Form1 form1;

        #region shurtcuts
        Keyboard.DirectXKeyStrokes hpPot = Keyboard.DirectXKeyStrokes.DIK_5;
        Keyboard.DirectXKeyStrokes target1 = Keyboard.DirectXKeyStrokes.DIK_7;
        Keyboard.DirectXKeyStrokes target2 = Keyboard.DirectXKeyStrokes.DIK_8;
        Keyboard.DirectXKeyStrokes target3 = Keyboard.DirectXKeyStrokes.DIK_9;
        Keyboard.DirectXKeyStrokes attack = Keyboard.DirectXKeyStrokes.DIK_0;

        #endregion

        Keyboard.DirectXKeyStrokes previousTarget;
        Keyboard.DirectXKeyStrokes currentTarget;

        public MimikyuBoat(Form1 form1)
        {
            this.form1 = form1;
            player = Player.Instance;
            target = Target.Instance;
            imageManager = ImageManager.Instance;
            imageRecognition = new ImageRecognition();

        }

 
        public void Start()
        {

            // comienzo el bot, previamente ya se debio haber validado que toda la configuracion previa
            // este hecha.
            form1.ConsoleWrite("Comenzando a botear !");
            form1.Invoke((MethodInvoker)delegate
            {
                form1.ChangeFormColor(Color.Green);
            });

            Keyboard.DirectXKeyStrokes[] targets = new Keyboard.DirectXKeyStrokes[3] { target1, target2, target3 };
            int previousTargetHP = 1;
            var watch = Stopwatch.StartNew();
            var spoiled = true;
            int totalTargets = 0;
            while (true)
            {
                botEnabled = true;
                if (paused)
                {
                    Thread.Sleep(500);
                    continue;
                }
                // comienzo un timer para detectar cannot see target cuando la hp target no baja en un tiempo
                // obtengo stats del player y del target reconociendo las imagenes
                //playerCP = imageRecognition.RecognizePlayerStat();
                player.hp = imageRecognition.RecognizePlayerStat((int)player.hpRow);
                //playerMP = imageRecognition.RecognizePlayerStat();
                target.hp = imageRecognition.RecognizeTargetHP();
                form1.ConsoleWrite("Player HP: " + player.hp.ToString());
                form1.ConsoleWrite("Target HP: " + target.hp.ToString());

                if (previousTargetHP == target.hp && watch.ElapsedMilliseconds > 20000)
                {
                    watch.Restart();
                    TryEscapeCannotSeeTarget();
                    currentTarget = targets[totalTargets % targets.Length];
                    totalTargets++;
                }
                else if (previousTargetHP != target.hp)
                {
                    watch.Restart();
                }

                if (player.hp < 80)
                {
                    form1.ConsoleWrite("Player HP muy baja, usando pocion");
                    UsePotion();
                }

                if (target.hp <= 80 && target.hp >= 60)
                {
                    spoiled = false;
                }
                // TODO: si no hay target > buscar target
                // TODO: agregar timer
                if (target.hp <= 0)
                {
                    Thread.Sleep(200);
                    form1.ConsoleWrite("Usando: Sweeper");
                    UseShortCut(Keyboard.DirectXKeyStrokes.DIK_3);
                    form1.ConsoleWrite("Target Muerto, buscando siguiente target...");
                    Thread.Sleep(200);
                    UseShortCut(Keyboard.DirectXKeyStrokes.DIK_4);
                    Thread.Sleep(200);
                    UseShortCut(Keyboard.DirectXKeyStrokes.DIK_4);

                    previousTarget = currentTarget;
                    Thread.Sleep(500);
                    currentTarget = targets[totalTargets % targets.Length];
                    totalTargets++;
                }
                AttackTarget(currentTarget, spoiled);
                previousTargetHP = (int)target.hp;

                spoiled = true;
                Thread.Sleep(updateInterval);
            }

        }

        public void TryEscapeCannotSeeTarget()
        {
            form1.ConsoleWrite("Cannot see target?? intentando salir...");

            Keyboard.SendKey(Keyboard.DirectXKeyStrokes.DIK_S, false, Keyboard.InputType.Keyboard);
            Thread.Sleep(new Random().Next(1000, 3000));
            Keyboard.SendKey(Keyboard.DirectXKeyStrokes.DIK_S, true, Keyboard.InputType.Keyboard);

            Keyboard.SendKey(Keyboard.DirectXKeyStrokes.DIK_A, false, Keyboard.InputType.Keyboard);
            Thread.Sleep(new Random().Next(1000, 3000));
            Keyboard.SendKey(Keyboard.DirectXKeyStrokes.DIK_A, true, Keyboard.InputType.Keyboard);

            Keyboard.SendKey(Keyboard.DirectXKeyStrokes.DIK_W, false, Keyboard.InputType.Keyboard);
            Thread.Sleep(new Random().Next(1000, 3000));
            Keyboard.SendKey(Keyboard.DirectXKeyStrokes.DIK_W, true, Keyboard.InputType.Keyboard);
        }

        public void UsePotion()
        {
            UseShortCut(hpPot);
        }

        public void AttackTarget(Keyboard.DirectXKeyStrokes targetShortCut, bool spoiled = true)
        {
            if(currentTarget == previousTarget)
            {

            }
            UseShortCut(targetShortCut); // targeteo mob
            Thread.Sleep(500);
            if (!spoiled)
            {
                UseShortCut(Keyboard.DirectXKeyStrokes.DIK_2); // spoil
            } else
            {
                UseShortCut(attack); // ataco
            }
        }

        public void UseShortCut(Keyboard.DirectXKeyStrokes key, int miliseconds = 121)
        {
            Keyboard.SendKey(key, false, Keyboard.InputType.Keyboard);
            Thread.Sleep(miliseconds);
            Keyboard.SendKey(key, true, Keyboard.InputType.Keyboard);
        }

    }
}
