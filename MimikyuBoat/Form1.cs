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

namespace MimikyuBoat
{
    public partial class Form1 : Form
    {
        #region imports
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        #endregion 

        public volatile bool playerRegionLoaded = false;
        public volatile bool targetRegionLoaded = false;
        public volatile bool enterPressed = false;
        [ThreadStatic]
        public static readonly bool IsMainThread = true;

        BotConfiguration botConfiguration;
        MimikyuBoat mimikyuBoat;
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

        public Form1()
        {
            InitializeComponent();
            this.Shown += new System.EventHandler(this.Form1_Shown);
            //Properties.Settings.Default.Reset();

        }

        #region referencias
        [DllImport("User32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);
        [DllImport("User32.dll")]
        public static extern void ReleaseDC(IntPtr hwnd, IntPtr dc);
        #endregion

        private void Form1_Shown(object sender, EventArgs e)
        {
            mimikyuBoat = new MimikyuBoat(this);
            botConfiguration = new BotConfiguration(this);
            imageManager = ImageManager.Instance;
            player = Player.Instance;
            target = Target.Instance;


            playerStatsMarker.Height = 1;
            targetStatsMarker.Height = 1;
            playerStatsMarker.Hide();
            targetStatsMarker.Hide();
            statsConfigButton.Enabled = false;

            ConsoleWrite("Cargando configuracion.");
            LOAD_SETTINGS();
            ConsoleWrite("Configuracion cargada.");

            // threads

            Thread updateThread = new Thread(new ThreadStart(UpdateUI));
            updateThread.Start();
        }

        public void LOAD_SETTINGS()
        {
            // intento cargar las configuraciones previas guardadas

            // Seteo de variables NO forms
            //System.Configuration.SettingsBase.this[string].get returned null
            player.cpBarStart = BotSettings.PLAYER_CP_BARSTART;
            player.hpBarStart = BotSettings.PLAYER_HP_BARSTART;
            player.mpBarStart = BotSettings.PLAYER_MP_BARSTART;
            player.cpRow = BotSettings.PLAYER_CP_ZONE;
            player.hpRow = BotSettings.PLAYER_HP_ZONE;
            player.mpRow = BotSettings.PLAYER_MP_ZONE;
            target.hpBarStart = BotSettings.TARGET_HP_BARSTART;
            target.hpRow = BotSettings.TARGET_HP_ZONE;

            botConfiguration.playerRect = BotSettings.PLAYER_CONFIGURATION_AREA;
            if (!botConfiguration.playerRect.IsEmpty) playerRegionLoaded = true;

            botConfiguration.targetRect = BotSettings.TARGET_CONFIGURATION_AREA;
            if (!botConfiguration.targetRect.IsEmpty) targetRegionLoaded = true;

            // En este caso guardo directamente en botsettings en vez de una variable clonada de una clase.
            BotSettings.PLAYER_CP_BARSTART_INITIALIZED = (bool)Properties.Settings.Default["PLAYER_CP_BARSTART_INITIALIZED"];
            BotSettings.PLAYER_HP_BARSTART_INITIALIZED = (bool)Properties.Settings.Default["PLAYER_HP_BARSTART_INITIALIZED"];
            BotSettings.PLAYER_MP_BARSTART_INITIALIZED = (bool)Properties.Settings.Default["PLAYER_MP_BARSTART_INITIALIZED"];
            BotSettings.TARGET_HP_BARSTART_INITIALIZED = (bool)Properties.Settings.Default["TARGET_HP_BARSTART_INITIALIZED"];


            // Seteo de variables Form
            autoPotCheckBox.Checked = BotSettings.AUTO_POT_ENABLED;
            autoPotHPTextBox.Text = BotSettings.AUTO_POT_PERCENTAGE.ToString();

            recoverMPCheckBox.Checked = BotSettings.RECOVER_MP_ENABLED;
            recoverMPSitTextBox.Text = BotSettings.MP_SIT_PERCENTAGE.ToString();
            recoverMPStandTextBox.Text = BotSettings.MP_STAND_PERCENTAGE.ToString();

            updateIntervalUpDown.Value = BotSettings.UPDATE_INTERVAL;
        }

        public void SAVE_SETTINGS()
        {
            // Por ahora guardo todo siempre, en un futuro ver como mejorar esto.

            Properties.Settings.Default["PLAYER_CP_BARSTART"] = player.cpBarStart;
            Properties.Settings.Default["PLAYER_HP_BARSTART"] = player.hpBarStart;
            Properties.Settings.Default["PLAYER_MP_BARSTART"] = player.mpBarStart;
            Properties.Settings.Default["TARGET_HP_BARSTART"] = target.hpBarStart;

            Properties.Settings.Default["PLAYER_CP_ZONE"] = player.cpRow;
            Properties.Settings.Default["PLAYER_HP_ZONE"] = player.hpRow;
            Properties.Settings.Default["PLAYER_MP_ZONE"] = player.mpRow;
            Properties.Settings.Default["TARGET_HP_ZONE"] = target.hpRow;

            Properties.Settings.Default["PLAYER_CONFIGURATION_AREA"] = botConfiguration.playerRect;
            Properties.Settings.Default["TARGET_CONFIGURATION_AREA"] = botConfiguration.targetRect;

            Properties.Settings.Default["AUTO_POT_ENABLED"] = autoPotCheckBox.Checked;
            Properties.Settings.Default["AUTO_POT_PERCENTAGE"] = autoPotHPTextBox.Text == "" ? 0 : int.Parse(autoPotHPTextBox.Text);
            Properties.Settings.Default["RECOVER_MP_ENABLED"] = recoverMPCheckBox.Checked;
            Properties.Settings.Default["MP_SIT_PERCENTAGE"] = recoverMPSitTextBox.Text == "" ? 0 : int.Parse(recoverMPSitTextBox.Text);
            Properties.Settings.Default["MP_STAND_PERCENTAGE"] = recoverMPStandTextBox.Text == "" ? 0 : int.Parse(recoverMPStandTextBox.Text);

            Properties.Settings.Default["UPDATE_INTERVAL"] = (int)updateIntervalUpDown.Value;
            Properties.Settings.Default["PLAYER_CP_BARSTART_INITIALIZED"] = BotSettings.PLAYER_CP_BARSTART_INITIALIZED;
            Properties.Settings.Default["PLAYER_HP_BARSTART_INITIALIZED"] = BotSettings.PLAYER_HP_BARSTART_INITIALIZED;
            Properties.Settings.Default["PLAYER_MP_BARSTART_INITIALIZED"] = BotSettings.PLAYER_MP_BARSTART_INITIALIZED;
            Properties.Settings.Default["TARGET_HP_BARSTART_INITIALIZED"] = BotSettings.TARGET_HP_BARSTART_INITIALIZED;

            Properties.Settings.Default.Save();
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

                if (this.playerRegionLoaded)
                {
                    // guardo la imagen del player
                    Bitmap bmp = imageManager.GetImageFromRect(botConfiguration.playerRect);

                    if ((System.IO.File.Exists(player.imagePath)))
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

                if (targetRegionLoaded)
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
            SAVE_SETTINGS();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            ChangeFormColor(Color.Red);
            // Verifico si estan cargadas las imagenes del player y del target
            if (!playerRegionLoaded)
            {
                botConfiguration.StartPlayerAreaConfiguration();
            }
            if (!targetRegionLoaded)
            {
                botConfiguration.StartTargetAreaConfiguration();
            }
            if (!BotSettings.PLAYER_HP_BARSTART_INITIALIZED || !BotSettings.TARGET_HP_BARSTART_INITIALIZED)
            {
                ConsoleWrite("Para comenzar el bot primero es necesario, como minimo, configurar la zona del HP del player y del target.");
                return;
            } 

            if (!mimikyuBoat.botEnabled)
            {
                ChangeFormColor(Color.Green);
                Thread thr = new Thread(new ThreadStart(mimikyuBoat.Start));
                thr.Start();
                startButton.Text = "Pausar";
            }
            else if (mimikyuBoat.botEnabled && mimikyuBoat.paused == false)
            {
                ChangeFormColor(Color.AliceBlue);
                mimikyuBoat.paused = true;
                startButton.Text = "Continuar";
            }
            else
            {
                ChangeFormColor(Color.Green);
                startButton.Text = "Pausar";
                mimikyuBoat.paused = false;
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

            SAVE_SETTINGS();
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
                autoPotHPTextBox.Enabled = true;
            } else
            {
                autoPotHPTextBox.Enabled = false;
            }
            SAVE_SETTINGS();
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
            SAVE_SETTINGS();
        }

        private void AutoPotHPTextBox_TextChanged(object sender, EventArgs e)
        {

            SAVE_SETTINGS();
        }

        private void RecoverMPSitTextBox_TextChanged(object sender, EventArgs e)
        {
            SAVE_SETTINGS();
        }

        private void RecoverMPStandTextBox_TextChanged(object sender, EventArgs e)
        {
            SAVE_SETTINGS();
        }

        private void UpdateIntervalUpDown_ValueChanged(object sender, EventArgs e)
        {
            SAVE_SETTINGS();
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
        }

    }
}
