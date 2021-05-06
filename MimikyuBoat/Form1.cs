using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using System.IO;

namespace Shizui
{
    public partial class Form1 : Form
    {
        #region imports
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        private static extern IntPtr PostMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("User32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("User32.dll")]
        public static extern void ReleaseDC(IntPtr hwnd, IntPtr dc);

        [DllImport("user32.dll", EntryPoint = "WindowFromPoint", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr WindowFromPoint(Point point);
        #endregion 

        public volatile bool enterPressed = false;
        [ThreadStatic]
        public static readonly bool IsMainThread = true;

        BotConfiguration botConfiguration;
        Bot bot;
        Player player;
        Thread thr;
        Thread updateThread;

        Target target;


        #region controls
        // Controls que son accedidos en botconfiguration.
     
        public bool autoPotCheckBoxValue
        {
            get
            {
                return autoPotCheckBox.Checked;
            }
        }
        public int autoPotTextBoxValue
        {
            get
            {
                try
                {
                    return int.Parse(autoPotTextBox.Text);
                } catch (Exception)
                {
                    return 0;
                }
            }
        }
        public bool recoverMPCheckBoxValue
        {
            get
            {
                return recoverMPCheckBox.Checked;
            }
        }
        public int recoverMPSitTextBoxValue
        {
            get
            {
                try
                {
                    return int.Parse(recoverMPSitTextBox.Text);
                } catch(Exception)
                {
                    return 0;
                }
            }
        }
        public int recoverMPStandTextBoxValue
        {
            get
            {
                try
                {
                    return int.Parse(recoverMPStandTextBox.Text);
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }
        public int updateIntervalValue
        {
            get
            {
                return (int)updateIntervalUpDown.Value;
            }
        }
        public string userNameTextBoxValue
        {
            get
            {
                return userNameTextBox.Text;
            }
        }
        public bool alwaysOnTopValue
        {
            get
            {
                return alwaysOnTopCheckBox.Checked;
            }
        }
        public bool assistCheckBoxValue
        {
            get
            {
                return assistCheckBox.Checked;
            }
        }
        #endregion

        public static Form form;
        public Form1()
        {
            InitializeComponent();
            this.Shown += new EventHandler(this.Form1_Shown);
            //Properties.Settings.Default.Reset();
        }

        public static void GlobalHandler(ThreadStart threadStartTarget)
        {
            try
            {
                threadStartTarget.Invoke();
            }
            catch (Exception e)
            {
                File.AppendAllText("log.txt", "StackTrace: " + e.StackTrace + Environment.NewLine);
                File.AppendAllText("log.txt", "Message: " + e.Message + Environment.NewLine);
                File.AppendAllText("log.txt", "Source: " + e.Source + Environment.NewLine);
                File.AppendAllText("log.txt", "InnerException: " + e.InnerException + Environment.NewLine);
                File.AppendAllText("log.txt", "Data: " + e.Data + Environment.NewLine);
                MessageBox.Show("Ocurrio un error, guardado en el log.");
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            bot = new Bot();
            botConfiguration = new BotConfiguration(this);
            player = Player.Instance;
            target = Target.Instance;

            // Thread encargado de ejecutar el mainloop de bot.cs
            thr = new Thread(o=> GlobalHandler(bot.MainLoop));
            thr.IsBackground = true;

            botConfiguration.ConfigLoaded += ConfigWasLoaded;
            botConfiguration.ConfigSaved += ConfigWasSaved;

            if (File.Exists("default.kyu"))
            {
                BotSettings.Load();
                botConfiguration.UPDATE_APP_DATA();

                updateThread = new Thread(o=> GlobalHandler(UpdateUI));
                updateThread.IsBackground = true;
                updateThread.Start();
            } 
            else
            {
                ConsoleWrite("No existe el archivo de configuracion por defecto default.kyu, intentando generar uno...");
                try
                {
                    string direc = Directory.GetCurrentDirectory() + "\\" + "default.kyu";
                    Console.WriteLine(direc);
                    using (File.Create(direc));
                    botConfiguration.GENERATE_DEFAULT_CONFIG_FILE();
                    BotSettings.Load();
                    botConfiguration.UPDATE_APP_DATA();
                    ConsoleWrite("Archivo generado correctamente.");
                }
                catch (Exception err)
                {
                    MessageBox.Show("ERROR: Hubo un problema al crear el archivo por defecto, error original" + err.Message);
                }
            }
        }

        void ConfigWasLoaded()
        {
            // Metodo llamado una vez que la configuracion fue cargada.
            this.Text = "Shizui - " + BotSettings.USER_NAME.ToUpper();
            userNameTextBox.Text = BotSettings.USER_NAME.ToUpper();
            autoPotCheckBox.Checked = BotSettings.AUTO_POT_ENABLED;
            autoPotTextBox.Text = BotSettings.AUTO_POT_PERCENTAGE.ToString();

            recoverMPCheckBox.Checked = BotSettings.RECOVER_MP_ENABLED;
            recoverMPSitTextBox.Text = BotSettings.MP_SIT_PERCENTAGE.ToString();
            recoverMPStandTextBox.Text = BotSettings.MP_STAND_PERCENTAGE.ToString();

            updateIntervalUpDown.Value = BotSettings.UPDATE_INTERVAL;

            pickupTimesTextBox.Text = BotSettings.PICKUP_TIMES.ToString();
            pickupDelayTextBox.Text = BotSettings.DELAY_BETWEEN_PICKUPS.ToString();
            spoilTimesTextBox.Text = BotSettings.SPOIL_TIMES.ToString();
            useSpoilCheckBox.Checked = BotSettings.USE_SPOIL;

        }

        void ConfigWasSaved()
        {
            // Metodo llamado una vez que la configuracion fue guardada.
        }


        public void UpdateUI()
        {
            // metodo que se ejecuta cada intervalo definido, sirve para actualizar todo lo que es UI y pertenece a esta clase.

            while (true)
            {

                hpPlayerLabel.Invoke((MethodInvoker)(() => 
                    hpPlayerLabel.Text = "HP: " + player.hp
                ));

                hpTargetLabel.Invoke((MethodInvoker)(() =>
                hpTargetLabel.Text = "HP: " + target.hp
                ));

                if (!L2ProcessLoaded())
                {
                    LoadL2Process(0);
                    Thread.Sleep(1000);
                   
                } else
                {
                    if (!bot.botEnabled)
                    {
                        MemoryManager.Instance.GetPlayerHp();
                        MemoryManager.Instance.GetTargetHp();
                    }
                }

                Thread.Sleep(BotSettings.UPDATE_INTERVAL);
            }
        }

        public void ConsoleWrite(string text)
        {
            if (IsMainThread)
            {
                if (consoleDebugTextBox.Text != "")
                {
                    consoleDebugTextBox.Text += Environment.NewLine;
                }
                consoleDebugTextBox.AppendText(text);
            } else
            {
                Invoke((MethodInvoker)delegate {
                    ConsoleWrite(text);
               });
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: setear colores
        }


        private void WindowVisibilityCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = !this.TopMost;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {

            if (bot.botEnabled)
            {
                ChangeFormColor(Color.Green);
                bot.botEnabled = false;

                startButton.Text = "Continuar";
            }
            else
            {
                ChangeFormColor(Color.AliceBlue);
                bot.botEnabled = true;
                startButton.Text = "Pausar";

                if (!thr.IsAlive)
                    thr.Start();
            }
        }

        public void ChangeFormColor(Color color)
        {
            this.BackColor = color;
        }

        #region forms funcionamiento irrelevante
        private void AutoPotCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (autoPotCheckBox.Checked)
            {
                autoPotTextBox.Enabled = true;
            } else
            {
                autoPotTextBox.Enabled = false;
            }
            botConfiguration.SAVE_SETTINGS_TO_KYU();
        }

        private void RecoverMPCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (recoverMPCheckBox.Checked)
            {
                recoverMPSitTextBox.Enabled = true;
            } else
            {
                recoverMPSitTextBox.Enabled = false;
            }
            botConfiguration.SAVE_SETTINGS_TO_KYU();
        }

        private void AutoPotHPTextBox_TextChanged(object sender, EventArgs e)
        {

            botConfiguration.SAVE_SETTINGS_TO_KYU();
        }

        private void RecoverMPSitTextBox_TextChanged(object sender, EventArgs e)
        {
            botConfiguration.SAVE_SETTINGS_TO_KYU();
        }

        private void RecoverMPStandTextBox_TextChanged(object sender, EventArgs e)
        {
            botConfiguration.SAVE_SETTINGS_TO_KYU();
        }

        private void UpdateIntervalUpDown_ValueChanged(object sender, EventArgs e)
        {
            botConfiguration.SAVE_SETTINGS_TO_KYU();
        }

        #endregion


        private void configLoadButton_Click(object sender, EventArgs e)
        {
            // cargo la configuracion del usuario.
            botConfiguration.LOAD_USER_SETTINGS();

        }

        private void configSaveButton_Click(object sender, EventArgs e)
        {
            // Guardo la configuracion actual del usuario.

            botConfiguration.SAVE_USER_SETTINGS();
        }

        private void addUserButton_Click(object sender, EventArgs e)
        {
            // Creo un nuevo usuario de guardado.

            botConfiguration.ADD_USER_SETTINGS();

        }

        private void asistCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //ConsoleWrite("Clickea y mantene apretado el boton \"seleccionar target\" y solta en la barra de party del target a asistir.");
            if (assistCheckBoxValue)
                ConsoleWrite("Un macro con </target personaje> a asistir debe estar en F9, la accion /assist debe estar en F10.");
            else
                ConsoleWrite("Assist modo desactivado");

            BotSettings.ASSIST_MODE_ENABLED = assistCheckBoxValue;
        }

        private void pickupTimesTextBox_TextChanged(object sender, EventArgs e)
        {
            int times = BotSettings.PICKUP_TIMES;
            try
            {
                times = int.Parse(pickupTimesTextBox.Text);
                if (times <= 0)
                    times = 1;
                BotSettings.PICKUP_TIMES = times;
                botConfiguration.SAVE_SETTINGS_TO_KYU();
            }
            catch (Exception)
            {
                ConsoleWrite("Solo se aceptan numeros.");
            }
            pickupTimesTextBox.Text = times.ToString();

        }
        private void pickupDelayTextBox_TextChanged(object sender, EventArgs e)
        {
            int delay = BotSettings.DELAY_BETWEEN_PICKUPS;
            try
            {
                delay = int.Parse(pickupDelayTextBox.Text);
                if (delay <= 0)
                    delay = 50;
                BotSettings.DELAY_BETWEEN_PICKUPS = delay;
                botConfiguration.SAVE_SETTINGS_TO_KYU();
            }
            catch (Exception)
            {
                ConsoleWrite("Solo se aceptan numeros. El tiempo es en milisegundos");
            }
            pickupDelayTextBox.Text = delay.ToString();

        }

        private void useSpoilCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (useSpoilCheckBox.Checked)
                spoilTimesTextBox.Enabled = true;
            else
                spoilTimesTextBox.Enabled = false;

            BotSettings.USE_SPOIL = useSpoilCheckBox.Checked;
            botConfiguration.SAVE_SETTINGS_TO_KYU();
        }

        private void spoilTimesTextBox_TextChanged(object sender, EventArgs e)
        {
            int times = BotSettings.SPOIL_TIMES;
            try
            {
                times = int.Parse(spoilTimesTextBox.Text);
                if (times <= 0)
                    times = 1;
                BotSettings.SPOIL_TIMES = times;
                Bot.Instance.spoilTimes = times;

                botConfiguration.SAVE_SETTINGS_TO_KYU();
            } catch (Exception)
            {
                ConsoleWrite("Solo se aceptan numeros.");
            }
            spoilTimesTextBox.Text = times.ToString();
        }

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);
        private void l2ProcessComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int l2ProcessIndex = l2ProcessComboBox.SelectedIndex;
            if (l2ProcessIndex < 0) return;
            LoadL2Process(l2ProcessIndex);
        }

        private void l2ProcessComboBox_Click(object sender, EventArgs e)
        {
            l2ProcessComboBox.Items.Clear();
            Process[] l2processes = Process.GetProcessesByName("L2.bin");
            if (l2processes.Length == 0)
            {
                l2ProcessComboBox.Items.Insert(0, "No hay l2 abierto");

            }
            foreach (Process l2Process in l2processes)
            {
                int comboLen = l2ProcessComboBox.Items.Count;
                l2ProcessComboBox.Items.Insert(comboLen, l2Process.ProcessName + ", ventana numero: " + comboLen.ToString());
            }
        }

        void LoadL2Process(int l2ProcessIndex)
        {
            try
            {
                Process[] l2Processes = Process.GetProcessesByName("L2.bin");
                if (l2Processes.Length == 0)
                {
                    Console.WriteLine("L2 not opened");
                    return;
                }
                Process l2Process = l2Processes[l2ProcessIndex];
                BotSettings.L2_PROCESS = l2Process;
                BotSettings.L2_PROCESS_HANDLER = OpenProcess(0x001F0FFF, false, l2Process.Id);
                BotSettings.L2_WINDOW_HANDLE = l2Process.MainWindowHandle;
                MemoryManager.Instance.Run();
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        bool L2ProcessLoaded()
        {
            return 
                BotSettings.L2_PROCESS_HANDLER != IntPtr.Zero && 
                BotSettings.L2_PROCESS != null && 
                BotSettings.L2_WINDOW_HANDLE != IntPtr.Zero;
        }
    }
}
