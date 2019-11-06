using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace MimikyuBoat
{
    class BotConfiguration
    {
        public Rectangle playerRect;
        public Rectangle targetRect;
        public volatile bool playerRegionLoaded = false;
        public volatile bool targetRegionLoaded = false;
        public string currentConfigUser;

        Form1 form1;
        ImageManager imageManager;
        XMLParser xmlParser;
        Player player;
        Target target;

        #region events
        public delegate void OnConfigSaved();
        public event OnConfigSaved ConfigSaved;

        public delegate void OnConfigLoaded();
        public event OnConfigLoaded ConfigLoaded;
        #endregion


        public BotConfiguration(Form1 form1)
        {
            this.form1 = form1;
            imageManager = ImageManager.Instance;
            xmlParser = new XMLParser();
            player = Player.Instance;
            target = Target.Instance;
        }

        public void StartPlayerAreaConfiguration()
        {
            // TODO: verificar previamente si ya hay una area del player guardada
            form1.ConsoleWrite("Pone el cursor en la esquina superior donde esta la vida de tu pj y apreta A.");
            while (!form1.enterPressed)
            {
                Thread.Sleep(200);
            }
            Point startPos = imageManager.GetCursorPosition();
            form1.enterPressed = false;

            form1.ConsoleWrite("Pone el cursor en la esquina inferior donde esta la vida de tu pj y apreta A.");
            while (!form1.enterPressed)
            {
                Thread.Sleep(200);
            }
            Point endPos = imageManager.GetCursorPosition();
            form1.enterPressed = false;

            Debug.WriteLine(startPos);
            Debug.WriteLine(endPos);

            Size rectSize = new Size(Math.Abs(startPos.X - endPos.X), Math.Abs(startPos.Y - endPos.Y));
            playerRect = new Rectangle(startPos, rectSize);

            // seteo como que la region del player ya esta cargada para poder comenzar el update.
            playerRegionLoaded = true;
            form1.ConsoleWrite("Area del player guardada");

            SAVE_SETTINGS_TO_KYU();
        }

        public void StartTargetAreaConfiguration()
        {
            // TODO: verificar previamente si ya hay una area del target guardada
            form1.ConsoleWrite("Pone el cursor en la esquina superior donde esta la vida del target y apreta A.");
            while (!form1.enterPressed)
            {
                Thread.Sleep(200);
            }
            form1.enterPressed = false;
            Point startPos = imageManager.GetCursorPosition();

            form1.ConsoleWrite("Pone el cursor en la esquina inferior donde esta la vida del target y apreta A.");
            while (!form1.enterPressed)
            {
                Thread.Sleep(200);
            }
            form1.enterPressed = false;
            Point endPos = imageManager.GetCursorPosition();

            Debug.WriteLine(startPos);
            Debug.WriteLine(endPos);

            Size rectSize = new Size(Math.Abs(startPos.X - endPos.X), Math.Abs(startPos.Y - endPos.Y));
            targetRect = new Rectangle(startPos, rectSize);

            // seteo como que la region del player ya esta cargada para poder comenzar el update.
            targetRegionLoaded = true;
            form1.ConsoleWrite("Area del target guardada");

            SAVE_SETTINGS_TO_KYU();
        }

        public void ConfigureBarBounds()
        {

            // seteo la fila de pixeles de cada stat.
            if (form1.zoneComboBoxValue == "Player CP")
            {
                player.cpRow = form1.playerStatsMarkerValue;
                player.cpBarStart = ImageRecognition.Instance.GetPlayerBarPixelStart((int)player.cpRow);
                BotSettings.PLAYER_CP_BARSTART_INITIALIZED = true;
            }
            else if (form1.zoneComboBoxValue == "Player HP")
            {
                player.hpRow = form1.playerStatsMarkerValue;
                player.hpBarStart = ImageRecognition.Instance.GetPlayerBarPixelStart((int)player.hpRow);
                BotSettings.PLAYER_HP_BARSTART_INITIALIZED = true;

                Debug.WriteLine("Player Row: " + Player.Instance.hpRow.ToString());
            }
            else if (form1.zoneComboBoxValue == "Player MP")
            {
                player.mpRow = form1.playerStatsMarkerValue;
                player.mpBarStart = ImageRecognition.Instance.GetPlayerBarPixelStart((int)player.mpRow);
                BotSettings.PLAYER_MP_BARSTART_INITIALIZED = true;
            }
            else if (form1.zoneComboBoxValue == "Target HP")
            {
                Target.Instance.hpRow = form1.targetStatsMarkerValue;
                Target.Instance.hpBarStart = ImageRecognition.Instance.GetTargetBarPixelStart((int)Target.Instance.hpRow);
                BotSettings.TARGET_HP_BARSTART_INITIALIZED = true;

                Debug.WriteLine("Target HP bar start: " + Target.Instance.hpBarStart.ToString());
                Debug.WriteLine("Target Row: " + Target.Instance.hpRow.ToString());
            }
        }


        public void UPDATE_APP_DATA()
        {
            // intento cargar las configuraciones previas guardadas

            try
            {

                //player.cpBarStart = BotSettings.PLAYER_CP_BARSTART;
                //player.hpBarStart = BotSettings.PLAYER_HP_BARSTART;
                //player.mpBarStart = BotSettings.PLAYER_MP_BARSTART;
                player.cpRow =      BotSettings.PLAYER_CP_ZONE;
                player.hpRow =      BotSettings.PLAYER_HP_ZONE;
                player.mpRow =      BotSettings.PLAYER_MP_ZONE;
                target.hpBarStart = BotSettings.TARGET_HP_BARSTART;
                target.hpRow =      BotSettings.TARGET_HP_ZONE;
            
                playerRect =        BotSettings.PLAYER_CONFIGURATION_AREA;
                if (!playerRect.IsEmpty) playerRegionLoaded = true;
            
                targetRect =        BotSettings.TARGET_CONFIGURATION_AREA;
                if (!targetRect.IsEmpty) targetRegionLoaded = true;
            
                // Para el caso de variables donde se accede directamente desde botsettings las seteo asi
                // esto es un poco confuso asique debe ser cambiado.
                BotSettings.PLAYER_CP_BARSTART_INITIALIZED = (bool)XMLParser.GET_VALUE_FROM_KYU("PLAYER_CP_BARSTART_INITIALIZED");
                BotSettings.PLAYER_HP_BARSTART_INITIALIZED = (bool)XMLParser.GET_VALUE_FROM_KYU("PLAYER_HP_BARSTART_INITIALIZED");
                BotSettings.PLAYER_MP_BARSTART_INITIALIZED = (bool)XMLParser.GET_VALUE_FROM_KYU("PLAYER_MP_BARSTART_INITIALIZED");
                BotSettings.TARGET_HP_BARSTART_INITIALIZED = (bool)XMLParser.GET_VALUE_FROM_KYU("TARGET_HP_BARSTART_INITIALIZED");
            
                 // Config cargada, hora de llamar mis minions!
                 ConfigLoaded?.Invoke();

            } catch (TypeInitializationException err)
            {
                MessageBox.Show("Hubo un error al cargar la configuracion del usuario, archivo corrupto? Error: " + err.InnerException.Message);
            }

            // Seteo de variables NO forms
        }

        public void SAVE_SETTINGS_TO_KYU()
        {
            // Por ahora guardo todo siempre, en un futuro ver como mejorar esto.
            if(BotSettings.KYU_FILE_PATH == "default.kyu")
            {
                Debug.WriteLine("Se ignoro un intento de guardado en default.kyu");
                return;
            }

            xmlParser.SET_VALUE_TO_KYU("PLAYER_CP_BARSTART", player.cpBarStart);
            xmlParser.SET_VALUE_TO_KYU("PLAYER_HP_BARSTART", player.hpBarStart);
            xmlParser.SET_VALUE_TO_KYU("PLAYER_MP_BARSTART", player.mpBarStart);
            xmlParser.SET_VALUE_TO_KYU("TARGET_HP_BARSTART", target.hpBarStart);

            xmlParser.SET_VALUE_TO_KYU("PLAYER_CP_ZONE", player.cpRow);
            xmlParser.SET_VALUE_TO_KYU("PLAYER_HP_ZONE", player.hpRow);
            xmlParser.SET_VALUE_TO_KYU("PLAYER_MP_ZONE", player.mpRow);
            xmlParser.SET_VALUE_TO_KYU("TARGET_HP_ZONE", target.hpRow);

            xmlParser.SET_VALUE_TO_KYU("PLAYER_CONFIGURATION_AREA", playerRect);
            xmlParser.SET_VALUE_TO_KYU("TARGET_CONFIGURATION_AREA", targetRect);

            xmlParser.SET_VALUE_TO_KYU("AUTO_POT_ENABLED", form1.autoPotCheckBoxValue);
            xmlParser.SET_VALUE_TO_KYU("AUTO_POT_PERCENTAGE", form1.autoPotTextBoxValue);
            xmlParser.SET_VALUE_TO_KYU("RECOVER_MP_ENABLED", form1.recoverMPCheckBoxValue);
            xmlParser.SET_VALUE_TO_KYU("MP_SIT_PERCENTAGE", form1.recoverMPSitTextBoxValue);
            xmlParser.SET_VALUE_TO_KYU("MP_STAND_PERCENTAGE", form1.recoverMPStandTextBoxValue);

            xmlParser.SET_VALUE_TO_KYU("UPDATE_INTERVAL", form1.updateIntervalValue);
            xmlParser.SET_VALUE_TO_KYU("PLAYER_CP_BARSTART_INITIALIZED", BotSettings.PLAYER_CP_BARSTART_INITIALIZED);
            xmlParser.SET_VALUE_TO_KYU("PLAYER_HP_BARSTART_INITIALIZED", BotSettings.PLAYER_HP_BARSTART_INITIALIZED);
            xmlParser.SET_VALUE_TO_KYU("PLAYER_MP_BARSTART_INITIALIZED", BotSettings.PLAYER_MP_BARSTART_INITIALIZED);
            xmlParser.SET_VALUE_TO_KYU("TARGET_HP_BARSTART_INITIALIZED", BotSettings.TARGET_HP_BARSTART_INITIALIZED);

            // llamo a mis minions!
            // ConfigSaved?.Invoke();
        }

        public void GENERATE_DEFAULT_CONFIG_FILE()
        {
            // funcion ejecutada unicamente cuando no hay un archivo por defecto de guardado  generado.

            xmlParser.SET_VALUE_TO_KYU("PLAYER_CP_BARSTART", -1);
            xmlParser.SET_VALUE_TO_KYU("PLAYER_HP_BARSTART", -1);
            xmlParser.SET_VALUE_TO_KYU("PLAYER_MP_BARSTART", -1);
            xmlParser.SET_VALUE_TO_KYU("TARGET_HP_BARSTART", -1);

            xmlParser.SET_VALUE_TO_KYU("PLAYER_CP_ZONE", -1);
            xmlParser.SET_VALUE_TO_KYU("PLAYER_HP_ZONE", -1);
            xmlParser.SET_VALUE_TO_KYU("PLAYER_MP_ZONE", -1);
            xmlParser.SET_VALUE_TO_KYU("TARGET_HP_ZONE", -1);

            xmlParser.SET_VALUE_TO_KYU("PLAYER_CONFIGURATION_AREA", new Rectangle(0, 0, 0, 0));
            xmlParser.SET_VALUE_TO_KYU("TARGET_CONFIGURATION_AREA", new Rectangle(0, 0, 0, 0));

            xmlParser.SET_VALUE_TO_KYU("AUTO_POT_ENABLED", false);
            xmlParser.SET_VALUE_TO_KYU("AUTO_POT_PERCENTAGE", -1);
            xmlParser.SET_VALUE_TO_KYU("RECOVER_MP_ENABLED", false);
            xmlParser.SET_VALUE_TO_KYU("MP_SIT_PERCENTAGE", -1);
            xmlParser.SET_VALUE_TO_KYU("MP_STAND_PERCENTAGE", -1);

            xmlParser.SET_VALUE_TO_KYU("UPDATE_INTERVAL", form1.updateIntervalValue);
            xmlParser.SET_VALUE_TO_KYU("BOT_PAUSE_CP", false) ;
            xmlParser.SET_VALUE_TO_KYU("PLAYER_CP_BARSTART_INITIALIZED", false);
            xmlParser.SET_VALUE_TO_KYU("PLAYER_HP_BARSTART_INITIALIZED", false);
            xmlParser.SET_VALUE_TO_KYU("PLAYER_MP_BARSTART_INITIALIZED", false);
            xmlParser.SET_VALUE_TO_KYU("TARGET_HP_BARSTART_INITIALIZED", false);
            xmlParser.SET_VALUE_TO_KYU("ALWAYS_ON_TOP", form1.alwaysOnTopValue);

        }

        public void LOAD_USER_SETTINGS()
        {
            playerRegionLoaded = false;
            targetRegionLoaded = false;

            // Cargo la configuracion del usuario al programa
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Abrir .kyu archivo";
            fileDialog.Filter = "KYU files|*.kyu";
            fileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (fileDialog.OpenFile() != null)
                    {
                        // Archivo abierto, cargo la configuracion del usuario.
                        BotSettings.KYU_FILE_PATH = fileDialog.FileName;
                        Debug.WriteLine("kyu file path ahora es: " + BotSettings.KYU_FILE_PATH);

                        // recargo todos los datos del archivo y luego los datos de la app
                        BotSettings.Reload();
                        UPDATE_APP_DATA();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: No pude leer el arhivo del disco :(. Error original: " + ex.Message);
                }
            }
        }

        public void SAVE_USER_SETTINGS()
        {
            if (BotSettings.KYU_FILE_PATH == BotSettings.KYU_FILE_DEFAULT_PATH)
            {
                MessageBox.Show("No hay ningun usuario cargado, se procede a crear nuevo usuario.");
                ADD_USER_SETTINGS();
                return;
            }
            // guardo la data del usuario actual en BotSettings.KYU_FILENAME 
            SAVE_SETTINGS_TO_KYU();
            MessageBox.Show("Usuario guardado");
        }

        public void ADD_USER_SETTINGS()
        {
            playerRegionLoaded = false;
            targetRegionLoaded = false;

            // funcion llamada al crear un nuevo usuario
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Title = "Nuevo archivo .kyu";
            fileDialog.Filter = "KYU files|*.kyu";
            fileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                // Creo el archivo
                string filePath = fileDialog.FileName;
                string extension = ".kyu";
                Debug.WriteLine(filePath);

                try
                {
                    File.Copy(Directory.GetCurrentDirectory() + "/default.kyu", filePath);
                    if (BotSettings.KYU_FILE_PATH == BotSettings.KYU_FILE_DEFAULT_PATH)
                    {
                        BotSettings.KYU_FILE_PATH = filePath;
                        // si estamos en el caso en donde se clickeo el boton de guardar y no hay ningun usuario cargado, hago esto.
                        SAVE_SETTINGS_TO_KYU();
                    }
                    BotSettings.KYU_FILE_PATH = filePath;

                    // recargo todos los datos del archivo y luego los datos de la app
                    BotSettings.Reload();
                    UPDATE_APP_DATA();
                    Utils.Instance.ConsoleWrite("Usuario " + fileDialog.FileName + " creado.");
                } catch (Exception ex)
                {
                    MessageBox.Show("Error: No pude crear un nuevo archivo :(. Error original: " + ex.Message);
                }
            }
        }

    }
}
