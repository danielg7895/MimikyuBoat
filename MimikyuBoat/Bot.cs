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
    class Bot
    {
        // si estoy seleccionando el area del player o target
        public volatile int updateInterval = 1000;
        public bool paused = false;
        public bool botEnabled = false;

        // private variables
        readonly Utils utils;
        readonly ImageManager imageManager;
        readonly ImageRecognition imageRecognition;
        readonly Player player;
        readonly Target target;
        VirtualKeyBoard virtualKeyBoard;

        List<Target> targets;

        Target previousTarget;
        Target currentTarget;

        #region events
        public delegate void OnEnemyDead();
        public event OnEnemyDead EnemyDead;

        public delegate void OnPlayerDead();
        public event OnPlayerDead PlayerDead;

        public delegate void OnAttackStart();
        public event OnAttackStart AttackStart;

        public delegate void OnHittedByPlayer();
        public event OnHittedByPlayer HittedByPlayer;

        #endregion

        Stopwatch watch;
        int previousTargetHP = 1;

        #region singleton
        private static Bot _instance;
        public static Bot Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Bot();
                }
                return _instance;
            }
        }
        #endregion

        public Bot()
        {
            utils = Utils.Instance;
            player = Player.Instance;
            target = Target.Instance;
            imageManager = ImageManager.Instance;
            imageRecognition = new ImageRecognition();
            virtualKeyBoard = new VirtualKeyBoard();
        }

 
        public void MainLoop()
        {
            // comienzo el bot, previamente ya se debio haber validado que toda la configuracion previa
            // este hecha.
            utils.ConsoleWrite("Comenzando a botear !");
            watch = new Stopwatch(); // comienzo el timer!

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
                player.mp = imageRecognition.RecognizePlayerStat(player.mpRow);
                player.hp = imageRecognition.RecognizePlayerStat(player.hpRow);
                target.hp = imageRecognition.RecognizeTargetHP();
                utils.ConsoleWrite("Player HP: " + player.hp.ToString() + " %" );
                utils.ConsoleWrite("Target HP: " + target.hp.ToString() + " %");

                if (AttackTimeOver())
                {
                    TryEscapeCannotSeeTarget();
                    currentTarget = targets[totalTargets % targets.Count];
                    totalTargets++;
                }


                // TODO: si no hay target > buscar target
                if (target.isDead)
                {
                    utils.ConsoleWrite("Target Muerto, buscando siguiente target...");
                    previousTarget = currentTarget;
                    Thread.Sleep(500);
                    currentTarget = targets[totalTargets % targets.Count];
                    totalTargets++;
                }
                previousTargetHP = (int)target.hp;

                Thread.Sleep(updateInterval);
            }
        }

        bool AttackTimeOver()
        {

            if (watch.ElapsedMilliseconds > 20000 && previousTargetHP == target.hp)
            {
                watch.Restart();
                return true;
            }
            if (previousTargetHP != target.hp)
            {
                watch.Restart();
            }

            return false;
        }

        public void TryEscapeCannotSeeTarget()
        {
            utils.ConsoleWrite("Cannot see target?? intentando salir...");

            virtualKeyBoard.SendKeyToProcess(BotSettings.L2_PROCESS_HANDLE, VirtualKeyBoard.VirtualKey.VK_W, new Random().Next(1000, 3000));

            virtualKeyBoard.SendKeyToProcess(BotSettings.L2_PROCESS_HANDLE, VirtualKeyBoard.VirtualKey.VK_D, new Random().Next(1000, 3000));
            
            virtualKeyBoard.SendKeyToProcess(BotSettings.L2_PROCESS_HANDLE, VirtualKeyBoard.VirtualKey.VK_S, new Random().Next(1000, 3000));

        }

        public void UseShortCut(VirtualKeyBoard.VirtualKey key)
        {
            virtualKeyBoard.SendKeyToProcess(BotSettings.L2_PROCESS_HANDLE, key);
        }

        public void UseSkill(Skill skill)
        {
            utils.ConsoleWrite("Usando: " + skill.name);
            Thread.Sleep(200);

        }

    }
}
