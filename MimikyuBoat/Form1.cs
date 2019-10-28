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

        MimikyuBoat mimikyuBoat;

        public Form1()
        {
            InitializeComponent();
            this.Shown += new System.EventHandler(this.Form1_Shown);
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

            playerStatsMarker.Height = 1;
            targetStatsMarker.Height = 1;
            playerStatsMarker.Hide();
            targetStatsMarker.Hide();
            statsConfigButton.Enabled = false;
        }


        public void ConsoleWrite(string text)
        {
            if (consoleDebugTextBox.Text != "")
            {
                consoleDebugTextBox.Text += Environment.NewLine;
            }
            consoleDebugTextBox.AppendText(text);
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
            Thread th = new Thread(new ThreadStart(mimikyuBoat.StartPlayerAreaConfiguration));
            th.Start();
        }


        private void TargetButtonAreaConfig_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(new ThreadStart(mimikyuBoat.StartTargetAreaConfiguration));
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
            //if (playerPanelContainer.BackgroundImage != null)
            //    playerPanelContainer.BackgroundImage.Dispose();
            //playerPanelContainer.BackgroundImage = bmp;
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
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            Thread thr = new Thread(new ThreadStart(mimikyuBoat.Start));
            thr.Start();
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
            DisableStatsConfigurationZone();


            // seteo la fila de pixeles de cada stat.
            if (zoneComboBox.SelectedItem.ToString() == "Player CP")
            {
                Player.Instance.cpRow = playerStatsMarker.Top;
            }
            else if (zoneComboBox.SelectedItem.ToString() == "Player HP")
            {
                Player.Instance.hpRow = playerStatsMarker.Top;
                Debug.WriteLine(Player.Instance.hpRow);
            }

            else if (zoneComboBox.SelectedItem.ToString() == "Player MP")
            {
                Player.Instance.mpRow = playerStatsMarker.Top;
            }
            else if (zoneComboBox.SelectedItem.ToString() == "Target HP")
            {
                Target.Instance.hpRow = targetStatsMarker.Top;
                Debug.WriteLine(Target.Instance.hpRow);
            }
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
    }
}
