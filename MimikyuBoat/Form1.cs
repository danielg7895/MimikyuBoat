using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using System.IO;

namespace MimikyuBoat
{
    public partial class Form1 : Form
    {
        #region imports
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("User32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("User32.dll")]
        public static extern void ReleaseDC(IntPtr hwnd, IntPtr dc);
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
        int GCCleanInterval;

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

        public Form1()
        {
            InitializeComponent();
            this.Shown += new System.EventHandler(this.Form1_Shown);
            //Properties.Settings.Default.Reset();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            utils = new Utils();
            bot = new Bot();
            botConfiguration = new BotConfiguration(this);
            imageManager = ImageManager.Instance;
            player = Player.Instance;
            target = Target.Instance;

            playerStatsMarker.Height = 1;
            targetStatsMarker.Height = 1;
            playerStatsMarker.Hide();
            targetStatsMarker.Hide();
            statsConfigButton.Enabled = false;

            botConfiguration.ConfigLoaded += ConfigWasLoaded;
            botConfiguration.ConfigSaved += ConfigWasSaved;

            if (File.Exists("default.kyu"))
            {
                ConsoleWrite("Cargando configuracion.");
                BotSettings.Load();
                botConfiguration.UPDATE_APP_DATA();
                ConsoleWrite("Configuracion cargada.");

                Thread updateThread = new Thread(new ThreadStart(UpdateUI));
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
            autoPotCheckBox.Checked = BotSettings.AUTO_POT_ENABLED;
            autoPotTextBox.Text = BotSettings.AUTO_POT_PERCENTAGE.ToString();

            recoverMPCheckBox.Checked = BotSettings.RECOVER_MP_ENABLED;
            recoverMPSitTextBox.Text = BotSettings.MP_SIT_PERCENTAGE.ToString();
            recoverMPStandTextBox.Text = BotSettings.MP_STAND_PERCENTAGE.ToString();

            updateIntervalUpDown.Value = BotSettings.UPDATE_INTERVAL;
            playerImage.Image = null;
            targetImage.Image = null;
        }

        void ConfigWasSaved()
        {
            // Metodo llamado una vez que la configuracion fue guardada.
        }


        public void UpdateUI()
        {
            ConsoleWrite("EN updateeeeeeeeeeeeeeeeeeeee!!");
            int counter = 0;

            GCCleanInterval = BotSettings.UPDATE_INTERVAL * 30;

            // metodo que se ejecuta cada intervalo definido, sirve para actualizar todo lo que es UI y pertenece a esta clase.
            while (true)
            {
                counter += BotSettings.UPDATE_INTERVAL;

                if (botConfiguration.playerRegionLoaded)
                {
                    // guardo la imagen del player
                    Bitmap bmp = imageManager.GetImageFromRect(botConfiguration.playerRect);

                    if (File.Exists(player.imagePath))
                    {
                        while (true)
                        {
                            try
                            {
                                System.IO.File.Delete(player.imagePath);
                                break;

                            }
                            catch (Exception e)
                            {
                                Debug.WriteLine("Imagen siendo usada, re intentando... ");
                                Thread.Sleep(200);
                            }
                        }
                    }
                    bmp.Save(player.imagePath, System.Drawing.Imaging.ImageFormat.Jpeg);

                    // actualizo imagen del player en la interfaz
                    Invoke((MethodInvoker)delegate {
                        SetPlayerImage(bmp);
                    });
                }

                if (botConfiguration.targetRegionLoaded)
                {
                    // guardo la imagen del target
                    Bitmap bmp = imageManager.GetImageFromRect(botConfiguration.targetRect);
                    if ((System.IO.File.Exists(target.imagePath)))
                    {
                        while (true)
                        {
                            try
                            {
                                System.IO.File.Delete(target.imagePath);
                                break;

                            }
                            catch (Exception e)
                            {
                                Debug.WriteLine("Imagen siendo usada, re intentando... ");
                                Thread.Sleep(200);
                            }
                        }
                    }
                    bmp.Save(target.imagePath, System.Drawing.Imaging.ImageFormat.Jpeg);

                    // actualizo imagen del target en la interfaz
                    Invoke((MethodInvoker)delegate {
                        SetTargetImage(bmp);
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
            Thread th = new Thread(new ThreadStart(botConfiguration.StartPlayerAreaConfiguration));
            th.Start();
        }


        private void TargetButtonAreaConfig_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(new ThreadStart(botConfiguration.StartTargetAreaConfiguration));
            th.Start();
        }

        public void SetPlayerImage(Bitmap bmp)
        {
            if (playerImage.Image != null)
                playerImage.Image.Dispose();
            playerPanel.Height = bmp.Height;
            playerPanel.Width = bmp.Width;
            playerStatsMarker.Width = bmp.Width;

            playerImage.Height = bmp.Height;
            playerImage.Width = bmp.Width;
            playerImage.Top = 0;
            playerImage.Left = 0;
            playerImage.Image = bmp;
        }

        public void SetTargetImage(Bitmap bmp)
        {
            if (targetImage.Image != null)
                targetImage.Image.Dispose();
            targetPanel.Height = bmp.Height;
            targetPanel.Width = bmp.Width;
            targetStatsMarker.Width = bmp.Width;

            targetImage.Height = bmp.Height;
            targetImage.Width = bmp.Width;
            targetImage.Top = 0;
            targetImage.Left = 0;
            targetImage.Image = bmp;
        }


        private void WindowVisibilityCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = !this.TopMost;
            botConfiguration.SAVE_SETTINGS_TO_KYU();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            ChangeFormColor(Color.Red);
            // Verifico si estan cargadas las imagenes del player y del target
            if (!botConfiguration.playerRegionLoaded)
            {
                botConfiguration.StartPlayerAreaConfiguration();
            }
            if (!botConfiguration.targetRegionLoaded)
            {
                botConfiguration.StartTargetAreaConfiguration();
            }
            if (!BotSettings.PLAYER_HP_BARSTART_INITIALIZED || !BotSettings.TARGET_HP_BARSTART_INITIALIZED)
            {
                ConsoleWrite("Para comenzar el bot primero es necesario, como minimo, configurar la zona del HP del player y del target.");
                return;
            } 

            if (!bot.botEnabled)
            {
                ChangeFormColor(Color.Green);
                Thread thr = new Thread(new ThreadStart(bot.MainLoop));
                thr.Start();
                startButton.Text = "Pausar";
            }
            else if (bot.botEnabled && bot.paused == false)
            {
                ChangeFormColor(Color.AliceBlue);
                bot.paused = true;
                startButton.Text = "Continuar";
            }
            else
            {
                ChangeFormColor(Color.Green);
                startButton.Text = "Pausar";
                bot.paused = false;
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
                targetStatsMarker.Show();
            } else
            {
                playerStatsMarker.Show(); // linea rosa que selecciona la fila de pixeles
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

            if (e.KeyCode == Keys.Down) // bajo el marcador un pixel
            {
                playerStatsMarker.Top += 1;
                targetStatsMarker.Top += 1;
            }
            else if (e.KeyCode == Keys.Up) // subo el marcador un pixel
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

        [DllImport("user32.dll")]
        static extern IntPtr WindowFromPoint(Point point);
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
                    Pen pen = new Pen(Color.BlueViolet, 8);
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

    }
}
