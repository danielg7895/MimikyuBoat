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

        // si bicho sube hp, le sumo 1, si bicho baja hp, no sumo nada. si bicho muere, seteo a 0
        int targetHPChangingRatio = 0; // no es un ratio pero we

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
            spoilTimes = BotSettings.SPOIL_TIMES;

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
                // Este sleep es para que espere un toque antes de sacar foto al rectangulo
                // sino puede pasar que el bot mande la accion y por el delay no se vea reflejada
                // al sacar la foto al rectangulo y tome que la target esta muerta cuando
                // en realidad se targeteo un bicho nuevo, entrando en un bucle de no salir.
                Thread.Sleep(1000);

                if (!imageManager.UpdateTargets()) continue;

                //utils.ConsoleWrite("NUEVO LOOP " + DateTime.Now.ToString());

                // target muere, si bicho no esta en target del playertoassist entonces la siguiente
                // foto que saca, es la del target al playertoassist
                // si frame anterior estaba muerto el bicho, entonces ahora no ejecuto nada hasta q el bicho tenga vida
                // eliminar Bmp.save
                player.hp = imageRecognition.RecognizePlayerStat(player.hpRow);
                target.hp = imageRecognition.RecognizeTargetHP();
                //utils.ConsoleWrite("target hp: " + target.hp.ToString() + ", monster alive? " + monsterAlive.ToString());
                if (!monsterAlive && target.hp > 0)
                {
                    monsterAlive = true;
                }

                if (monsterAlive || !BotSettings.ASSIST_MODE_ENABLED)
                {
                    // Verifico si la target hp va subiendo y bajando su vida
                    // esto pasa veces cuando el rectangulo q se saca foto es en una area
                    // rojiza movediza, suele pasar cuando el rectangulo esta en un arbol
                    // que se mueve en un atardecer y la hp del target va variando dejando el pj quieto
                    // por que piensa que le esta dando a un bicho.
                    targetHPChangingRatio += target.hp > previousTargetHP ? 1 : 0;

                    if (AttackTimeOver() || targetHPChangingRatio > 4)
                    {
                        TryEscapeCannotSeeTarget();
                        currentTarget = targets[totalTargets % targets.Length];
                        totalTargets++;
                        targetHPChangingRatio = 0;
                        watch.Restart();
                    }

                    if (player.hp < 80)
                    {
                        utils.ConsoleWrite("Player HP baja, usando pocion!");
                        UsePotion();
                    }

                    if (BotSettings.USE_SPOIL && spoilTimes > 0 && target.hp <= 90)
                    {
                        // target hp <= 90 porque sino va a intentar usar spoil mientras camina hacia el bicho
                        UseShortCut(spoil);
                        spoilTimes--;
                        if (spoilTimes > 0)
                            Thread.Sleep(2500); // simulando el tiempo que demora en castear spoil + delay
                    }

                    // TODO: si no hay target > buscar target
                    if (target.hp <= 0)
                    {
                        monsterAlive = false;
                        spoilTimes = BotSettings.SPOIL_TIMES;
                        targetHPChangingRatio = 0;

                        if (BotSettings.USE_SPOIL)
                        {
                            Thread.Sleep(300);
                            utils.ConsoleWrite("Utilizando key -> 3");
                            UseShortCut(sweep);
                            Thread.Sleep(200);
                        }
                        // destargeteo.
                        UseShortCut(VirtualKeyBoard.VirtualKey.VK_ESCAPE);

                        // intento pickear
                        UseShortCut(key: pickup, repeat: BotSettings.PICKUP_TIMES, delayPerAction: BotSettings.DELAY_BETWEEN_PICKUPS);

                        if (BotSettings.ASSIST_MODE_ENABLED)
                        {
                            utils.ConsoleWrite("Target Muerto, asistiendo...");
                            UseShortCut(targetPlayer, repeat: 1, delayPerAction: 100);
                        }
                        else
                        {
                            utils.ConsoleWrite("Target Muerto, buscando siguiente target...");
                            previousTarget = currentTarget;
                            currentTarget = targets[totalTargets % targets.Length];
                            totalTargets++;
                        }
                    }
                    if (BotSettings.ASSIST_MODE_ENABLED)
                    {
                        //utils.ConsoleWrite("TARGETEANDO -> ASISTIENDO -> ATACANDO!");

                        UseShortCut(targetPlayer, repeat: 1, delayPerAction: 100);
                        Thread.Sleep(700);
                        UseShortCut(assistAction, 1, 100);
                        Thread.Sleep(700);

                        UseShortCut(attack, 1, 100);
                    } else
                    {
                        UseShortCut(currentTarget);
                    }

                    previousTargetHP = (int)target.hp;
                } else
                {
                    if (BotSettings.ASSIST_MODE_ENABLED)
                    {
                        UseShortCut(targetPlayer, repeat: 1, delayPerAction: 100);
                        Thread.Sleep(700);
                        UseShortCut(assistAction, 1, 100);
                        Thread.Sleep(700);
                        UseShortCut(attack, 1, 100);
                    }
                    watch.Restart();
                }

                //Thread.Sleep(updateInterval);
            }
        }

        void UsePotion()
        {
            virtualKeyBoard.SendKeyToProcess(BotSettings.L2_PROCESS_HANDLE, potion, new Random().Next(1000, 3000));

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
            utils.ConsoleWrite("Cannot see target?? intentando salir...");
            IntPtr hwnd = BotSettings.L2_PROCESS_HANDLE;
            int num = new Random().Next(0, 2);
            //virtualKeyBoard.ActivateWindow(BotSettings.L2_PROCESS_HANDLE);
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

            if (num == 0)
            {
                //List<Dictionary<string, int>> d_list = new List<Dictionary<string, int>>() { S_TIMES, D_TIMES, W_TIMES };
                //virtualKeyBoard.LongPressButton(hwnd, d_list);

                virtualKeyBoard.LongPressButton2(VirtualKeyBoard.VirtualKey.VK_S, 3500);
                virtualKeyBoard.LongPressButton2(VirtualKeyBoard.VirtualKey.VK_D, 2000);
                virtualKeyBoard.LongPressButton2(VirtualKeyBoard.VirtualKey.VK_S, 3500);
            }
            else
            {
                //List<Dictionary<string, int>> a_list = new List<Dictionary<string, int>>() { S_TIMES, A_TIMES, W_TIMES };
                //virtualKeyBoard.LongPressButton(hwnd, a_list);
                virtualKeyBoard.LongPressButton2(VirtualKeyBoard.VirtualKey.VK_S, 3500);
                virtualKeyBoard.LongPressButton2(VirtualKeyBoard.VirtualKey.VK_A, 2000);
                virtualKeyBoard.LongPressButton2(VirtualKeyBoard.VirtualKey.VK_S, 3500);
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
