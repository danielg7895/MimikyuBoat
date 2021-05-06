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
        Utils utils;
        ImageManager imageManager;
        Player player;
        Target target;
        Thread thr;
        Thread updateThread;

        #region controls
        // Controls que son accedidos en botconfiguration.
        public string zoneComboBoxValue
        {
            get
            {
                return zoneComboBox.SelectedItem.ToString();
            }
        }
        public int playerStatsMarkerValue
        {
            get
            {
                return playerStatsMarker.Top;
            }
        }
        public int targetStatsMarkerValue
        {
            get
            {
                return targetStatsMarker.Top;
            }
        }
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
            utils = new Utils();
            bot = new Bot();
            botConfiguration = new BotConfiguration(this);
            imageManager = ImageManager.Instance;
            player = Player.Instance;
            target = Target.Instance;

            // Thread encargado de ejecutar el mainloop de bot.cs
            thr = new Thread(o=> GlobalHandler(bot.MainLoop));
            thr.IsBackground = true;

            playerStatsMarker.Height = 1;
            targetStatsMarker.Height = 1;
            playerStatsMarker.Hide();
            targetStatsMarker.Hide();
            statsConfigButton.Enabled = false;

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

            playerTrackRed.Value = BotSettings.PLAYER_BAR_R;
            playerTrackGreen.Value = BotSettings.PLAYER_BAR_G;
            playerTrackBlue.Value = BotSettings.PLAYER_BAR_B;
            playerTrackBrightness.Value = BotSettings.PLAYER_BAR_BRIGHTNESS;
            playerTrackHue.Value = BotSettings.PLAYER_BAR_HUE;

            targetTrackRed.Value = BotSettings.TARGET_BAR_R;
            targetTrackBlue.Value = BotSettings.TARGET_BAR_G;
            targetTrackGreen.Value = BotSettings.TARGET_BAR_B;
            targetTrackBrightness.Value = BotSettings.TARGET_BAR_BRIGHTNESS;
            targetTrackHue.Value = BotSettings.TARGET_BAR_HUE;

            if (!BotSettings.PLAYER_CONFIGURATION_RECTANGLE.IsEmpty)
                playerImage.Image = new Bitmap(BotSettings.PLAYER_IMAGE);
            if (!BotSettings.TARGET_CONFIGURATION_RECTANGLE.IsEmpty)
                targetImage.Image = new Bitmap(BotSettings.TARGET_IMAGE);
        }

        void ConfigWasSaved()
        {
            // Metodo llamado una vez que la configuracion fue guardada.
        }


        public void UpdateUI()
        {
            // metodo que se ejecuta cada intervalo definido, sirve para actualizar todo lo que es UI y pertenece a esta clase.
            
            int counter = 0;
            int GCCleanInterval = BotSettings.UPDATE_INTERVAL * 30;

            while (true)
            {
                if (!bot.botEnabled && BotSettings.L2_PROCESS_HANDLE != IntPtr.Zero)
                {
                    if (!imageManager.UpdateTargets())
                    {
                        Thread.Sleep(200);
                        continue;
                    }
                }

                hpPlayerLabel.Invoke((MethodInvoker)(() => 
                    hpPlayerLabel.Text = "HP: " + player.hp + "%"
                ));

                hpTargetLabel.Invoke((MethodInvoker)(() =>
                hpTargetLabel.Text = "HP: " + target.hp + "%"
                ));


                counter += BotSettings.UPDATE_INTERVAL;

                if (BotSettings.PLAYER_IMAGE != null && !BotSettings.PLAYER_CONFIGURATION_RECTANGLE.IsEmpty) {
                    Invoke((MethodInvoker)delegate
                    {
                        SetPlayerImage();
                    });
                }
                
                if (BotSettings.TARGET_IMAGE != null && !BotSettings.TARGET_CONFIGURATION_RECTANGLE.IsEmpty)
                {
                    Invoke((MethodInvoker)delegate
                    {
                        SetTargetImage();
                    });
                }

                if ((counter % GCCleanInterval) == 0)
                {
                    // ejecuto el colector de mugre cada updateinterval * 30
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    counter = 0;
                }
                Thread.Sleep(BotSettings.UPDATE_INTERVAL);
            }
        }

        public void SetPlayerImage()
        {
            if (playerImage.Image != null)
            {
                playerImage.Image.Dispose();
                playerImage.Image = null;
                BotSettings.LoadImageBars();
            }

            Bitmap bmp = new Bitmap(BotSettings.PLAYER_IMAGE);

            if (bmp == null)
            {
                ConsoleWrite("BMP ES NULL!!!!!!!!!! WTF !!!!");
                return;
            }

            playerPanel.Height = bmp.Height;
            playerPanel.Width = bmp.Width;
            playerStatsMarker.Width = bmp.Width;

            playerImage.Height = bmp.Height;
            playerImage.Width = bmp.Width;
            playerImage.Top = 0;
            playerImage.Left = 0;
            playerImage.Image = bmp;
        }

        public void SetTargetImage()
        {

            if (targetImage.Image != null)
            {
                targetImage.Image.Dispose();
                targetImage.Image = null;
                BotSettings.LoadImageBars();
            }

            Bitmap bmp = new Bitmap(BotSettings.TARGET_IMAGE);
            if (bmp == null)
            {
                ConsoleWrite("BMP ES NULL!!!!!!!!!! WTF !!!!");
                return;
            }

            targetPanel.Height = bmp.Height;
            targetPanel.Width = bmp.Width;
            targetStatsMarker.Width = bmp.Width;

            targetImage.Height = bmp.Height;
            targetImage.Width = bmp.Width;
            targetImage.Top = 0;
            targetImage.Left = 0;
            targetImage.Image = bmp;
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

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                enterPressed = true;
            }
        }
        private void PlayerButtonAreaConfig_Click(object sender, EventArgs e)
        {
            if (BotSettings.L2_PROCESS_HANDLE == IntPtr.Zero)
            {
                MessageBox.Show("Selecciona el proceso del L2 antes de configurar el area");
                return;
            }
            Thread th = new Thread(new ThreadStart(botConfiguration.StartPlayerAreaConfiguration));
            th.Start();
        }

        private void TargetButtonAreaConfig_Click(object sender, EventArgs e)
        {
            if (BotSettings.L2_PROCESS_HANDLE == IntPtr.Zero)
            {
                MessageBox.Show("Selecciona el proceso del L2 antes de configurar el area");
                return;
            }
            Thread th = new Thread(new ThreadStart(botConfiguration.StartTargetAreaConfiguration));
            th.Start();
        }

        private void WindowVisibilityCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = !this.TopMost;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            ChangeFormColor(Color.Red);
            // Verifico si estan cargadas las imagenes del player y del target
            if (!botConfiguration.playerRegionLoaded || !botConfiguration.targetRegionLoaded)
            {
                ConsoleWrite("Antes de iniciar hay que configurar el area del player y target.");
                return;
            }

            if (!BotSettings.PLAYER_HP_BARSTART_INITIALIZED || !BotSettings.TARGET_HP_BARSTART_INITIALIZED)
            {
                ConsoleWrite("Antes de iniciar hay que configurar la zona del HP del player y del target.");
                return;
            } 

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


        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            statsConfigButton.Enabled = true;
        }

        private void ZoneConfigButton_Click(object sender, EventArgs e)
        {
            // TODO: esto no sirve para cuando haya mas stats, cambiarlo.
            if (zoneComboBox.SelectedItem.ToString() == "Target HP")
            {
                targetStatsMarker.Top = targetImage.Height / 2;
                targetStatsMarker.Show();
            } else
            {
                playerStatsMarker.Top = playerImage.Height / 2;
                playerStatsMarker.Show();
            }
            statsSaveButton.Enabled = true;
            statsCancelButton.Enabled = true;
            zoneComboBox.Enabled = false; // deshabilito el combobox para que no estorbe al usar arriba y abajo
            KeyDown += ConfigureStatRow;
            ConsoleWrite("Move la barra celeste con las flecha arriba y abajo, poniendola encima del stat seleccionado.");
        }


        void ConfigureStatRow(object sender, KeyEventArgs e)
        {
            // funcion encargada de configurar la fila perteneciente al stat seleccionado
            // en el combobox, por ej playerhp, playermp, etc.

            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.S) // bajo el marcador un pixel
            {
                playerStatsMarker.Top += 1;
                targetStatsMarker.Top += 1;
            }
            else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W) // subo el marcador un pixel
            {
                playerStatsMarker.Top -= 1;
                targetStatsMarker.Top -= 1;
            }
        }

        private void StatsSaveButton_Click(object sender, EventArgs e)
        {
            // desactivo/activo botones relacionados con la zona
            DisableStatsConfigurationZone();
            // llamo al metodo encargado de configurar los limites.
            botConfiguration.ConfigureBarBounds();

            botConfiguration.SAVE_SETTINGS_TO_KYU();
        }

        private void StatsCancelButton_Click(object sender, EventArgs e)
        {
            DisableStatsConfigurationZone();

        }

        void DisableStatsConfigurationZone()
        {
            statsSaveButton.Enabled = false;
            statsCancelButton.Enabled = false;
            statsConfigButton.Enabled = true;
            zoneComboBox.Enabled = true;
            targetStatsMarker.Hide();
            playerStatsMarker.Hide();
            KeyDown -= ConfigureStatRow;
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

        private void TabContainer_Selecting(object sender, TabControlCancelEventArgs e)
        {
            // agrego el form skillconfiguration al tabpage configTabPage.
            if (e.TabPage.Name == skillConfigTabPage.Name)
            {
                if (skillConfigTabPage.Controls.Count == 0)
                {
                    Form f = new SkillConfiguration();
                    f.TopLevel = false;
                    f.FormBorderStyle = FormBorderStyle.None;
                    f.Dock = DockStyle.Fill;
                    skillConfigTabPage.Controls.Add(f);
                    f.Show();
                }
            }
            else if (e.TabPage.Name == targetConfigTabPage.Name)
            {
                if (targetConfigTabPage.Controls.Count == 0)
                {
                    Form f = new TargetConfiguration();
                    f.TopLevel = false;
                    f.FormBorderStyle = FormBorderStyle.None;
                    f.Dock = DockStyle.Fill;
                    targetConfigTabPage.Controls.Add(f);
                    f.Show();
                }
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

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

        private void userNameTextBox_KeyDown(object sender, KeyEventArgs e)
        {

        }

        volatile bool selectingWindowMode = false;

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.Cross;
            selectingWindowMode = true;
            Thread th = new Thread(new ThreadStart(DrawOnWindow));
            th.Start();
        }

        [DllImport("User32.dll")]
        static extern bool GetWindowRect(IntPtr hWnd, ref RECT rec);
        [DllImport("User32.dll")]
        static extern bool InvalidateRect(IntPtr hWnd, IntPtr a, bool bErase);

        void DrawOnWindow()
        {
            RECT rect = new RECT();
            IntPtr hwnd = IntPtr.Zero;

            while (true)
            {
                Thread.Sleep(100);

                if (selectingWindowMode)
                {
                    Point cursorPos = Cursor.Position;
                    if (hwnd == WindowFromPoint(cursorPos)) continue;
                    Invoke((MethodInvoker)delegate
                    {
                        InvalidateRect(hwnd, IntPtr.Zero, true); // invalido draws previos asi bien piola
                        hwnd = WindowFromPoint(cursorPos);
                        selectedWindowName.Text = GetWindowTitle(hwnd);
                    });

                    Debug.WriteLine(hwnd);

                    Graphics graphics = Graphics.FromHwnd(hwnd);
                    Pen pen = new Pen(Color.DeepPink, 8);
                    rect = new RECT();
                    GetWindowRect(hwnd, ref rect);
                    int width = Math.Abs(rect.Right - rect.Left);
                    int height = Math.Abs(rect.Bottom - rect.Top);
                    graphics.DrawRectangle(pen, 0, 0, width, height);

                }
                else break;
            }
        }

        public struct RECT
        {
            public int Left;       
            public int Top;        
            public int Right;      
            public int Bottom;     
        }

        private void button1_MouseUp(object sender, MouseEventArgs e)
        {
            InvalidateRect(IntPtr.Zero, IntPtr.Zero, true);
            IntPtr hwnd = WindowFromPoint(Cursor.Position);
            BotSettings.L2_PROCESS_HANDLE = hwnd;

            var windowName = GetWindowTitle(hwnd);
            selectedWindowName.Text = windowName;
            selectingWindowMode = false;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetWindowTextLength(IntPtr hWnd);

        public static string GetWindowTitle(IntPtr hWnd)
        {
            var length = GetWindowTextLength(hWnd) + 1;
            var title = new StringBuilder(length);
            GetWindowText(hWnd, title, length);
            return title.ToString();
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

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void assistTargetSelectButton_MouseDown(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.Cross;
            selectingWindowMode = true;
        }

        public int WM_NCHITTEST = 0x0084;
        public int WM_SETCURSOR = 0x0020;
        public int WM_LBUTTONDOWN = 0x0201;
        public int WM_LBUTTONUP = 0x0202;
        public int WM_NCACTIVATE = 0x0086;
        public int WM_ACTIVATEAPP = 0x001C;
        public int WM_SETFOCUS = 7;
        public int WM_MOUSEACTIVATE = 0x0021;
        public IntPtr CreateLParam(int LoWord, int HiWord)
        {
            return (IntPtr)((HiWord << 16) | (LoWord & 0xffff));
        }

        private void assistTargetSelectButton_MouseUp(object sender, MouseEventArgs e)
        {
            BotSettings.ASSIST_PLAYER_POS_X = e.X;
            BotSettings.ASSIST_PLAYER_POS_Y = e.Y;
            ConsoleWrite(BotSettings.ASSIST_PLAYER_POS_X.ToString() + " " + BotSettings.ASSIST_PLAYER_POS_Y.ToString());
            selectingWindowMode = false;
            
            Thread.Sleep(1000);
            int lparam = BotSettings.ASSIST_PLAYER_POS_X << 16;
            ConsoleWrite(lparam.ToString());
            lparam = lparam | BotSettings.ASSIST_PLAYER_POS_Y;
            ConsoleWrite(lparam.ToString());

            Point screenPoint = new Point(BotSettings.ASSIST_PLAYER_POS_X, BotSettings.ASSIST_PLAYER_POS_Y);
            IntPtr handle = WindowFromPoint(screenPoint);
            ConsoleWrite("handle: " + handle.ToString());
            if (handle != IntPtr.Zero)
            {
                IntPtr result = IntPtr.Zero;

                result = SendMessage((IntPtr)0x00140A74, WM_MOUSEACTIVATE, (IntPtr)0x00140A74, CreateLParam(1, WM_LBUTTONDOWN));
                result = SendMessage((IntPtr)0x00140A74, WM_SETFOCUS, IntPtr.Zero, IntPtr.Zero);
                result = SendMessage((IntPtr)0x00140A74, WM_ACTIVATEAPP, (IntPtr)1, IntPtr.Zero);
                result = SendMessage((IntPtr)0x00140A74, WM_NCACTIVATE, (IntPtr)1, IntPtr.Zero);
                result = SendMessage((IntPtr)0x00140A74, 0x0006, (IntPtr)1, IntPtr.Zero);

                Thread.Sleep(50);
                SendMessage((IntPtr)0x00140A74, WM_NCHITTEST, IntPtr.Zero, CreateLParam(891, 1766));
                Thread.Sleep(1000);
                PostMessage((IntPtr)0x00140A74, WM_LBUTTONDOWN, (IntPtr)0x00000001, CreateLParam(807, 582));

                SendMessage((IntPtr)0x00140A74, WM_NCHITTEST, IntPtr.Zero, CreateLParam(891, 1766));
                Thread.Sleep(1000);
                PostMessage((IntPtr)0x00140A74, WM_LBUTTONUP, (IntPtr)0x00000000, CreateLParam(807, 582));

            }
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

        private void playerTrackRed_Scroll(object sender, EventArgs e)
        {
            BotSettings.PLAYER_BAR_R = playerTrackRed.Value;
            botConfiguration.SAVE_SETTINGS_TO_KYU();
        }

        private void playerTrackGreen_Scroll(object sender, EventArgs e)
        {
            BotSettings.PLAYER_BAR_G = playerTrackGreen.Value;
            botConfiguration.SAVE_SETTINGS_TO_KYU();
        }

        private void playerTrackBlue_Scroll(object sender, EventArgs e)
        {
            BotSettings.PLAYER_BAR_B = playerTrackBlue.Value;
            botConfiguration.SAVE_SETTINGS_TO_KYU();
        }

        private void playerTrackBrightness_Scroll(object sender, EventArgs e)
        {
            BotSettings.PLAYER_BAR_BRIGHTNESS = playerTrackBrightness.Value;
            botConfiguration.SAVE_SETTINGS_TO_KYU();
        }

        private void targetTrackRed_Scroll(object sender, EventArgs e)
        {
            BotSettings.TARGET_BAR_R = targetTrackRed.Value;
            botConfiguration.SAVE_SETTINGS_TO_KYU();
        }

        private void targetTrackBlue_Scroll(object sender, EventArgs e)
        {
            BotSettings.TARGET_BAR_B = targetTrackGreen.Value;
            botConfiguration.SAVE_SETTINGS_TO_KYU();
        }
        private void playerTrackHue_Scroll(object sender, EventArgs e)
        {
            BotSettings.PLAYER_BAR_HUE = playerTrackHue.Value;
            botConfiguration.SAVE_SETTINGS_TO_KYU();
        }

        private void targetTrackGreen_Scroll(object sender, EventArgs e)
        {
            BotSettings.TARGET_BAR_G = targetTrackBlue.Value;
            botConfiguration.SAVE_SETTINGS_TO_KYU();
        }

        private void targetTrackBrightness_Scroll(object sender, EventArgs e)
        {
            BotSettings.TARGET_BAR_BRIGHTNESS = targetTrackBrightness.Value;
            botConfiguration.SAVE_SETTINGS_TO_KYU();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            BotSettings.TARGET_BAR_HUE = targetTrackHue.Value;
            botConfiguration.SAVE_SETTINGS_TO_KYU();
        }
    }
}
