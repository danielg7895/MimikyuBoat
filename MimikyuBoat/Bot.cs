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
        public int spoilTimes = 1;
        bool monsterAlive = false;

        // private variables
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

        VirtualKeyBoard.VirtualKey[] targets = new VirtualKeyBoard.VirtualKey[5]
        {
            VirtualKeyBoard.VirtualKey.VK_6,
            VirtualKeyBoard.VirtualKey.VK_7,
            VirtualKeyBoard.VirtualKey.VK_8,
            VirtualKeyBoard.VirtualKey.VK_9,
            VirtualKeyBoard.VirtualKey.VK_0
        };

        // Assist shortcuts
        VirtualKeyBoard.VirtualKey targetPlayer = VirtualKeyBoard.VirtualKey.VK_F9;
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
        int totalTargets = 0;
        int maxDetectedPlayerHp = 1;
        int maxDetectedTargetHp = 1;
        int playerHpPercentage = 0;
        int targetHpPercentage = 0;

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
            player = Player.Instance;
            target = Target.Instance;
            virtualKeyBoard = new VirtualKeyBoard();
        }


        public void MainLoop()
        {
            // comienzo el bot, previamente ya se debio haber validado que toda la configuracion previa
            // este hecha.
            Console.WriteLine("Comenzando a botear !");
            watch = new Stopwatch(); // comienzo el timer!
            spoilTimes = BotSettings.SPOIL_TIMES;

            while (true)
            {
                MemoryManager.Instance.GetPlayerHp();
                MemoryManager.Instance.GetTargetHp();

                // forma hacky de obtener hp maxima del player sin leer memoria, cambiar en futuro
                maxDetectedPlayerHp = 100; // player.hp > maxDetectedPlayerHp ? player.hp : maxDetectedPlayerHp;
                playerHpPercentage = 150; // (player.hp * 100) / maxDetectedPlayerHp;

                maxDetectedTargetHp = target.hp > maxDetectedTargetHp ? target.hp : maxDetectedTargetHp;
                targetHpPercentage = (target.hp * 100) / maxDetectedTargetHp;

                if (!botEnabled)
                {
                    Thread.Sleep(500);
                    continue;
                }
                if (BotSettings.ASSIST_MODE_ENABLED)
                {
                    AssistAttack();
                } else
                {
                    NormalAttack();
                }

               

                previousTargetHP = (int)target.hp;

                //Thread.Sleep(updateInterval);
            }
        }

        void NormalAttack()
        {
            if (target.hp > 0)
            {
                if (AttackTimeOver())
                {
                    TryEscapeCannotSeeTarget();
                    currentTarget = targets[totalTargets % targets.Length];
                    totalTargets++;
                    watch.Restart();
                }


                if (playerHpPercentage < 80)
                {
                    Console.WriteLine("Player HP baja, usando pocion!");
                    UsePotion();
                }

                if (BotSettings.USE_SPOIL && spoilTimes > 0 && targetHpPercentage <= 90)
                {
                    // target hp <= 90 porque sino va a intentar usar spoil mientras camina hacia el bicho
                    UseShortCut(spoil);
                    spoilTimes--;
                    Thread.Sleep(2500); // simulando el tiempo que demora en castear spoil + delay
                }

                UseShortCut(currentTarget);
            } else 
            {
                // target muerto
                spoilTimes = BotSettings.SPOIL_TIMES;

                // if use_spoil is enabled i should also use sweep
                if (BotSettings.USE_SPOIL)
                {
                    Thread.Sleep(300);
                    UseShortCut(sweep);
                    Thread.Sleep(200);
                }
                // destargeteo.
                UseShortCut(VirtualKeyBoard.VirtualKey.VK_ESCAPE);

                // intento pickear
                UseShortCut(key: pickup, repeat: BotSettings.PICKUP_TIMES, delayPerAction: BotSettings.DELAY_BETWEEN_PICKUPS);

                Console.WriteLine("Target Muerto, buscando siguiente target...");
                previousTarget = currentTarget;
                currentTarget = targets[totalTargets % targets.Length];
                totalTargets++;

                UseShortCut(currentTarget);
                Thread.Sleep(600); // por lag

                watch.Restart();
            }
        }

        void AssistAttack()
        {
            if (target.hp > 0)
            {
                if (AttackTimeOver())
                {
                    TryEscapeCannotSeeTarget();
                    currentTarget = targets[totalTargets % targets.Length];
                    totalTargets++;
                    watch.Restart();
                }


                if (playerHpPercentage < 80)
                {
                    Console.WriteLine("Player HP baja, usando pocion!");
                    UsePotion();
                }

                if (BotSettings.USE_SPOIL && spoilTimes > 0 && targetHpPercentage <= 80)
                {
                    // target hp <= 80 porque sino va a intentar usar spoil mientras camina hacia el bicho
                    UseShortCut(spoil);
                    spoilTimes--;
                    Thread.Sleep(2500); // simulando el tiempo que demora en castear spoil + delay
                }

                //utils.ConsoleWrite("TARGETEANDO -> ASISTIENDO -> ATACANDO!");

                UseShortCut(targetPlayer, repeat: 1, delayPerAction: 100);
                Thread.Sleep(700);
                UseShortCut(assistAction, 1, 100);
                Thread.Sleep(700);

                UseShortCut(attack, 1, 100);
            }

            // TODO: si no hay target > buscar target
            if (target.hp <= 0)
            {
                spoilTimes = BotSettings.SPOIL_TIMES;

                // if use_spoil is enabled i should also use sweep
                if (BotSettings.USE_SPOIL)
                {
                    Thread.Sleep(300);
                    Console.WriteLine("Utilizando key -> 3");
                    UseShortCut(sweep);
                    Thread.Sleep(200);
                }
                // destargeteo.
                UseShortCut(VirtualKeyBoard.VirtualKey.VK_ESCAPE);

                // intento pickear
                UseShortCut(key: pickup, repeat: BotSettings.PICKUP_TIMES, delayPerAction: BotSettings.DELAY_BETWEEN_PICKUPS);

                Console.WriteLine("Target Muerto, asistiendo...");
                UseShortCut(targetPlayer, repeat: 1, delayPerAction: 100);
                Thread.Sleep(600); // por lag

                watch.Restart();
            }
        }

        void UsePotion()
        {
            virtualKeyBoard.SendKeyToProcess(BotSettings.L2_WINDOW_HANDLE, potion, new Random().Next(1000, 3000));

        }

        bool AttackTimeOver()
        {

            if (watch.ElapsedMilliseconds > 20000 && previousTargetHP == target.hp 
                || watch.ElapsedMilliseconds > (60000 * 10))
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
            Console.WriteLine("Cannot see target?? intentando salir...");

            //virtualKeyBoard.ActivateWindow(BotSettings.L2_PROCESS_HANDLE);
            IntPtr hwnd = BotSettings.L2_WINDOW_HANDLE;
            UseShortCut(VirtualKeyBoard.VirtualKey.VK_ESCAPE);
            UseShortCut(VirtualKeyBoard.VirtualKey.VK_ESCAPE);

            //// classic 2.0
            //int s_endAt = new Random().Next(3000, 5000);
            //int d_endAt = new Random().Next(3000, 7000);

            //Dictionary<string, int> S_TIMES = new Dictionary<string, int>(){
            //    {"key", (int)VirtualKeyBoard.VirtualKey.VK_S},
            //    {"startAt", 0}, // comienzo a apretar S al segundo 0
            //    {"endAt", s_endAt},
            //    {"finished", 0 }
            //};

            //Dictionary<string, int> D_TIMES = new Dictionary<string, int>(){
            //    {"key", (int)VirtualKeyBoard.VirtualKey.VK_D},
            //    {"startAt", 1500},
            //    {"endAt", d_endAt},
            //    {"finished", 0 }
            //};

            //Dictionary<string, int> W_TIMES = new Dictionary<string, int>(){
            //    {"key", (int)VirtualKeyBoard.VirtualKey.VK_W},
            //    {"startAt", s_endAt},
            //    {"endAt", new Random().Next(1000, 4000)},
            //    {"finished", 0 }
            //};

            //Dictionary<string, int> A_TIMES = new Dictionary<string, int>(){
            //    {"key", (int)VirtualKeyBoard.VirtualKey.VK_A},
            //    {"startAt", 1500},
            //    {"endAt", d_endAt},
            //    {"finished", 0 }
            //};

            //List<Dictionary<string, int>> d_list = new List<Dictionary<string, int>>() { S_TIMES, D_TIMES, W_TIMES };
            //virtualKeyBoard.LongPressButton(BotSettings.L2_WINDOW_HANDLE, d_list);

            //List<Dictionary<string, int>> a_list = new List<Dictionary<string, int>>() { S_TIMES, A_TIMES, W_TIMES };
            //virtualKeyBoard.LongPressButton(BotSettings.L2_WINDOW_HANDLE, a_list);

            int num = new Random().Next(0, 2);

            if (num == 0)
            {


                virtualKeyBoard.SendKeyToProcess(hwnd, VirtualKeyBoard.VirtualKey.VK_S, 3500);
                virtualKeyBoard.SendKeyToProcess(hwnd, VirtualKeyBoard.VirtualKey.VK_D, 2000);
                virtualKeyBoard.SendKeyToProcess(hwnd, VirtualKeyBoard.VirtualKey.VK_S, 3500);
            }
            else
            {
                virtualKeyBoard.SendKeyToProcess(hwnd, VirtualKeyBoard.VirtualKey.VK_S, 3500);
                virtualKeyBoard.SendKeyToProcess(hwnd, VirtualKeyBoard.VirtualKey.VK_A, 2000);
                virtualKeyBoard.SendKeyToProcess(hwnd, VirtualKeyBoard.VirtualKey.VK_S, 3500);
            }
        }

        public void UseShortCut(VirtualKeyBoard.VirtualKey key, int repeat = 1, int delayPerAction = 23)
        {
            while (repeat > 0)
            {
                repeat--;
                Debug.WriteLine("[" + watch.ElapsedMilliseconds.ToString() + "] utilice " + key.ToString());
                virtualKeyBoard.SendKeyToProcess(BotSettings.L2_WINDOW_HANDLE, key);
                Thread.Sleep(delayPerAction);
            }
        }

        public void UseSkill(Skill skill)
        {
            Console.WriteLine("Usando: " + skill.name);
            Thread.Sleep(200);

        }

    }
}
