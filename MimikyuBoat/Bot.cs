using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace Shizui
{
    class Bot
    {
        // si estoy seleccionando el area del player o target
        public volatile int updateInterval = 1000;
        public bool botEnabled = false;
        bool monsterDead = false;

        // private variables
        readonly Utils utils;
        readonly ImageManager imageManager;
        readonly ImageRecognition imageRecognition;
        readonly Player player;
        readonly Target target;
        VirtualKeyBoard virtualKeyBoard;

        //List<Target> targets;
        //List<VirtualKeyBoard.VirtualKey> targets;

        VirtualKeyBoard.VirtualKey previousTarget;
        VirtualKeyBoard.VirtualKey currentTarget;
        VirtualKeyBoard.VirtualKey attack = VirtualKeyBoard.VirtualKey.VK_1;
        VirtualKeyBoard.VirtualKey spoil = VirtualKeyBoard.VirtualKey.VK_2;
        VirtualKeyBoard.VirtualKey sweep = VirtualKeyBoard.VirtualKey.VK_3;
        VirtualKeyBoard.VirtualKey pickup = VirtualKeyBoard.VirtualKey.VK_4;
        VirtualKeyBoard.VirtualKey potion = VirtualKeyBoard.VirtualKey.VK_5;

        // Assist shortcuts
        VirtualKeyBoard.VirtualKey assistTarget = VirtualKeyBoard.VirtualKey.VK_F9;
        VirtualKeyBoard.VirtualKey assistAction = VirtualKeyBoard.VirtualKey.VK_F10;

        // Conditional shortcuts
        VirtualKeyBoard.VirtualKey hpLessThan30Percent = VirtualKeyBoard.VirtualKey.VK_F11;


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

            VirtualKeyBoard.VirtualKey[] targets = new VirtualKeyBoard.VirtualKey[5]
            {
                VirtualKeyBoard.VirtualKey.VK_6,
                VirtualKeyBoard.VirtualKey.VK_7,
                VirtualKeyBoard.VirtualKey.VK_8,
                VirtualKeyBoard.VirtualKey.VK_9,
                VirtualKeyBoard.VirtualKey.VK_0
            };

            int totalTargets = 0;
            while (true)
            {
                if (!botEnabled)
                {
                    Thread.Sleep(500);
                    continue;
                }
                // comienzo un timer para detectar cannot see target cuando la hp target no baja en un tiempo
                // obtengo stats del player y del target reconociendo las imagenes
                imageManager.UpdateTargets(); // actualizo imagen de target y de player

                // target muere, si bicho no esta en target del playertoassist entonces la siguiente
                // foto que saca, es la del target al playertoassist
                // si frame anterior estaba muerto el bicho, entonces ahora no ejecuto nada hasta q el bicho tenga vida
                // eliminar Bmp.save
                player.hp = imageRecognition.RecognizePlayerStat(player.hpRow);
                target.hp = imageRecognition.RecognizeTargetHP();
                if (monsterDead && target.hp > 0)
                {
                    monsterDead = false;
                }

                if (!monsterDead || !BotSettings.ASSIST_MODE_ENABLED)
                {
                    if (AttackTimeOver())
                    {
                        TryEscapeCannotSeeTarget();
                        currentTarget = targets[totalTargets % targets.Length];
                        totalTargets++;
                    }

                    if(player.hp < 80)
                    {
                        utils.ConsoleWrite("Player HP baja, usando pocion!");
                        UsePotion();
                    }

                    if (target.hp <= 80 && target.hp >= 50)
                    {
                        UseShortCut(spoil);
                    }

                    // TODO: si no hay target > buscar target
                    if (target.hp <= 0)
                    {
                        Thread.Sleep(300);

                        utils.ConsoleWrite("Usando Sweeper...");
                        UseShortCut(sweep);
                        Thread.Sleep(300);

                        // intento pickear
                        UseShortCut(key: pickup, repeat: 4, delayPerAction: 100);

                        if (BotSettings.ASSIST_MODE_ENABLED)
                        {
                            utils.ConsoleWrite("Target Muerto, asistiendo...");
                            UseShortCut(assistTarget, repeat: 1, delayPerAction: 100);
                        }
                        else
                        {
                            utils.ConsoleWrite("Target Muerto, buscando siguiente target...");
                            previousTarget = currentTarget;
                            currentTarget = targets[totalTargets % targets.Length];
                            totalTargets++;
                        }
                        monsterDead = true;
                    }
                    if (BotSettings.ASSIST_MODE_ENABLED)
                    {

                        UseShortCut(assistTarget, repeat: 1, delayPerAction: 100);
                        UseShortCut(assistAction, 1, 100);
                        //Thread.Sleep(1000);
                        UseShortCut(attack, 2, 100);
                    } else
                    {
                        UseShortCut(currentTarget);
                    }
                    Thread.Sleep(1000);

                    previousTargetHP = (int)target.hp;
                } else
                {
                    watch.Restart();
                }

                //Thread.Sleep(updateInterval);
            }
        }

        void UsePotion()
        {
            virtualKeyBoard.ActivateWindow(BotSettings.L2_PROCESS_HANDLE);

            virtualKeyBoard.SendKeyToProcess(BotSettings.L2_PROCESS_HANDLE, potion, new Random().Next(1000, 3000));

        }

        bool AttackTimeOver()
        {

            if (watch.ElapsedMilliseconds > 20000 && previousTargetHP == target.hp || watch.ElapsedMilliseconds > (60000 * 10))
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
            virtualKeyBoard.ActivateWindow(BotSettings.L2_PROCESS_HANDLE);
            UseShortCut(VirtualKeyBoard.VirtualKey.VK_ESCAPE);
            UseShortCut(VirtualKeyBoard.VirtualKey.VK_ESCAPE);

            int num = new Random().Next(0, 2);

            virtualKeyBoard.SendKeyToProcess(BotSettings.L2_PROCESS_HANDLE, VirtualKeyBoard.VirtualKey.VK_S, new Random().Next(500, 2000));
            if(num == 0)
            {
                virtualKeyBoard.SendKeyToProcess(BotSettings.L2_PROCESS_HANDLE, VirtualKeyBoard.VirtualKey.VK_A, new Random().Next(500, 3000));
                virtualKeyBoard.SendKeyToProcess(BotSettings.L2_PROCESS_HANDLE, VirtualKeyBoard.VirtualKey.VK_W, new Random().Next(500, 3000));
            }
            else
            {
                virtualKeyBoard.SendKeyToProcess(BotSettings.L2_PROCESS_HANDLE, VirtualKeyBoard.VirtualKey.VK_D, new Random().Next(500, 2000));
                virtualKeyBoard.SendKeyToProcess(BotSettings.L2_PROCESS_HANDLE, VirtualKeyBoard.VirtualKey.VK_S, new Random().Next(500, 3000));
            }

        }

        public void UseShortCut(VirtualKeyBoard.VirtualKey key, int repeat = 1, int delayPerAction = 23)
        {
            while (repeat > 0)
            {
                repeat--;
                Debug.WriteLine("[" + watch.ElapsedMilliseconds.ToString() + "] utilice " + key.ToString());
                virtualKeyBoard.SendKeyToProcess(BotSettings.L2_PROCESS_HANDLE, key);
                Thread.Sleep(delayPerAction);
            }
        }

        public void UseSkill(Skill skill)
        {
            utils.ConsoleWrite("Usando: " + skill.name);
            Thread.Sleep(200);

        }

    }
}
