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

namespace Shizui
{
    class BotConfiguration
    {
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
            int widthScreen = Screen.PrimaryScreen.Bounds.Width;
            int heightScreen = Screen.PrimaryScreen.Bounds.Height;

            // TODO: verificar previamente si ya hay una area del player guardada
            form1.ConsoleWrite("Pone el cursor en la esquina superior donde esta la vida de tu pj y apreta A.");
            while (!form1.enterPressed)
            {
                Thread.Sleep(200);
            }
            Rectangle rect = imageManager.GetProcessRectangle();

            Point startPos = imageManager.GetCursorPosition();
            startPos.X -= rect.X;
            startPos.Y -= rect.Top;
            form1.enterPressed = false;

            form1.ConsoleWrite("Pone el cursor en la esquina inferior donde esta la vida de tu pj y apreta A.");
            while (!form1.enterPressed)
            {
                Thread.Sleep(200);
            }
            Point endPos = imageManager.GetCursorPosition();
            endPos.X -= rect.X;
            endPos.Y -= rect.Top;
            form1.enterPressed = false;

            Size rectSize = new Size(Math.Abs(startPos.X - endPos.X), Math.Abs(startPos.Y - endPos.Y));

            BotSettings.PLAYER_CONFIGURATION_RECTANGLE = new Rectangle(startPos, rectSize);

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
            Rectangle rect = imageManager.GetProcessRectangle();

            Point startPos = imageManager.GetCursorPosition();
            startPos.X -= rect.X;
            startPos.Y -= rect.Top;

            form1.ConsoleWrite("Pone el cursor en la esquina inferior donde esta la vida del target y apreta A.");
            while (!form1.enterPressed)
            {
                Thread.Sleep(200);
            }
            form1.enterPressed = false;
            Point endPos = imageManager.GetCursorPosition();
            endPos.X -= rect.X;
            endPos.Y -= rect.Top;

            Debug.WriteLine(startPos);
            Debug.WriteLine(endPos);

            Size rectSize = new Size(Math.Abs(startPos.X - endPos.X), Math.Abs(startPos.Y - endPos.Y));
            BotSettings.TARGET_CONFIGURATION_RECTANGLE = new Rectangle(startPos, rectSize);

            // seteo como que la region del player ya esta cargada para poder comenzar el update.
            targetRegionLoaded = true;
            form1.ConsoleWrite("Area del target guardada");

            SAVE_SETTINGS_TO_KYU();
        }

        public bool ConfigureBarBounds()
        {
            // TODO hacer una funcion de esto q se repite wacho
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
                if (player.hpBarStart == -1)
                {
                    form1.ConsoleWrite("[ERROR]: El marcador de HP(linea celeste) esta fuera de la imagen.");
                    return false;
                }
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
                target.hpRow = form1.targetStatsMarkerValue;
                target.hpBarStart = ImageRecognition.Instance.GetTargetBarPixelStart((int)target.hpRow);
                if (target.hpBarStart == -1)
                {
                    form1.ConsoleWrite("[ERROR]: El marcador de HP(linea celeste) esta fuera de la imagen.");
                    return false;
                }
                BotSettings.TARGET_HP_BARSTART_INITIALIZED = true;

                Debug.WriteLine("Target HP bar start: " + Target.Instance.hpBarStart.ToString());
                Debug.WriteLine("Target Row: " + Target.Instance.hpRow.ToString());
            }
            return true;
        }


        public void UPDATE_APP_DATA()
        {
            // intento cargar las configuraciones previas guardadas
            try
            {
                player.cpBarStart = BotSettings.PLAYER_CP_BARSTART;
                player.hpBarStart = BotSettings.PLAYER_HP_BARSTART;
                player.mpBarStart = BotSettings.PLAYER_MP_BARSTART;
                target.hpBarStart = BotSettings.TARGET_HP_BARSTART;
                player.cpRow =      BotSettings.PLAYER_CP_ZONE;
                player.hpRow =      BotSettings.PLAYER_HP_ZONE;
                player.mpRow =      BotSettings.PLAYER_MP_ZONE;
                target.hpRow =      BotSettings.TARGET_HP_ZONE;
            
                if (!BotSettings.PLAYER_CONFIGURATION_RECTANGLE.IsEmpty) playerRegionLoaded = true;
            
                if (!BotSettings.TARGET_CONFIGURATION_RECTANGLE.IsEmpty) targetRegionLoaded = true;
            
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

            xmlParser.SET_VALUE_TO_KYU("PLAYER_CONFIGURATION_RECTANGLE", BotSettings.PLAYER_CONFIGURATION_RECTANGLE);
            xmlParser.SET_VALUE_TO_KYU("TARGET_CONFIGURATION_RECTANGLE", BotSettings.TARGET_CONFIGURATION_RECTANGLE);

            xmlParser.SET_VALUE_TO_KYU("AUTO_POT_ENABLED", form1.autoPotCheckBoxValue);
            xmlParser.SET_VALUE_TO_KYU("AUTO_POT_PERCENTAGE", form1.autoPotTextBoxValue);
            xmlParser.SET_VALUE_TO_KYU("RECOVER_MP_ENABLED", form1.recoverMPCheckBoxValue);
            xmlParser.SET_VALUE_TO_KYU("MP_SIT_PERCENTAGE", form1.recoverMPSitTextBoxValue);
            xmlParser.SET_VALUE_TO_KYU("MP_STAND_PERCENTAGE", form1.recoverMPStandTextBoxValue);

            xmlParser.SET_VALUE_TO_KYU("UPDATE_INTERVAL", form1.updateIntervalValue);
            xmlParser.SET_VALUE_TO_KYU("ASSIST_MODE_ENABLED", form1.assistCheckBoxValue);
            xmlParser.SET_VALUE_TO_KYU("PLAYER_CP_BARSTART_INITIALIZED", BotSettings.PLAYER_CP_BARSTART_INITIALIZED);
            xmlParser.SET_VALUE_TO_KYU("PLAYER_HP_BARSTART_INITIALIZED", BotSettings.PLAYER_HP_BARSTART_INITIALIZED);
            xmlParser.SET_VALUE_TO_KYU("PLAYER_MP_BARSTART_INITIALIZED", BotSettings.PLAYER_MP_BARSTART_INITIALIZED);
            xmlParser.SET_VALUE_TO_KYU("TARGET_HP_BARSTART_INITIALIZED", BotSettings.TARGET_HP_BARSTART_INITIALIZED);

            xmlParser.SET_VALUE_TO_KYU("PICKUP_TIMES", BotSettings.PICKUP_TIMES);
            xmlParser.SET_VALUE_TO_KYU("DELAY_BETWEEN_PICKUPS", BotSettings.DELAY_BETWEEN_PICKUPS);
            xmlParser.SET_VALUE_TO_KYU("USE_SPOIL", BotSettings.USE_SPOIL);
            xmlParser.SET_VALUE_TO_KYU("SPOIL_TIMES", BotSettings.SPOIL_TIMES);

            xmlParser.SET_VALUE_TO_KYU("PLAYER_BAR_R", BotSettings.PLAYER_BAR_R);
            xmlParser.SET_VALUE_TO_KYU("PLAYER_BAR_G", BotSettings.PLAYER_BAR_G);
            xmlParser.SET_VALUE_TO_KYU("PLAYER_BAR_B", BotSettings.PLAYER_BAR_B);
            xmlParser.SET_VALUE_TO_KYU("PLAYER_BAR_BRIGHTNESS", BotSettings.PLAYER_BAR_BRIGHTNESS);
            xmlParser.SET_VALUE_TO_KYU("PLAYER_BAR_HUE", BotSettings.PLAYER_BAR_HUE);

            xmlParser.SET_VALUE_TO_KYU("TARGET_BAR_R", BotSettings.TARGET_BAR_R);
            xmlParser.SET_VALUE_TO_KYU("TARGET_BAR_G", BotSettings.TARGET_BAR_G);
            xmlParser.SET_VALUE_TO_KYU("TARGET_BAR_B", BotSettings.TARGET_BAR_B);
            xmlParser.SET_VALUE_TO_KYU("TARGET_BAR_BRIGHTNESS", BotSettings.TARGET_BAR_BRIGHTNESS);
            xmlParser.SET_VALUE_TO_KYU("TARGET_BAR_HUE", BotSettings.TARGET_BAR_HUE);



            // chekeo si hay skils pa guarda
            foreach (Skill skill in player.GetSkills())
            {
                xmlParser.SET_VALUE_TO_KYU(skill.name, skill);
            }
            
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


            // En que posicion en Y comienza la barra de hp, mp, cp, etc.
            xmlParser.SET_VALUE_TO_KYU("PLAYER_CP_ZONE", -1);
            xmlParser.SET_VALUE_TO_KYU("PLAYER_HP_ZONE", -1);
            xmlParser.SET_VALUE_TO_KYU("PLAYER_MP_ZONE", -1);
            xmlParser.SET_VALUE_TO_KYU("TARGET_HP_ZONE", -1);

            xmlParser.SET_VALUE_TO_KYU("PLAYER_CONFIGURATION_RECTANGLE", new Rectangle(0, 0, 0, 0));
            xmlParser.SET_VALUE_TO_KYU("TARGET_CONFIGURATION_RECTANGLE", new Rectangle(0, 0, 0, 0));

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
            xmlParser.SET_VALUE_TO_KYU("ASSIST_MODE_ENABLED", false);

            xmlParser.SET_VALUE_TO_KYU("PICKUP_TIMES", 4);
            xmlParser.SET_VALUE_TO_KYU("DELAY_BETWEEN_PICKUPS", 200);
            xmlParser.SET_VALUE_TO_KYU("USE_SPOIL", false);
            xmlParser.SET_VALUE_TO_KYU("SPOIL_TIMES", 1);

            xmlParser.SET_VALUE_TO_KYU("PLAYER_BAR_R", 0);
            xmlParser.SET_VALUE_TO_KYU("PLAYER_BAR_B", 0);
            xmlParser.SET_VALUE_TO_KYU("PLAYER_BAR_G", 0);
            xmlParser.SET_VALUE_TO_KYU("PLAYER_BAR_BRIGHTNESS", 0);
            xmlParser.SET_VALUE_TO_KYU("PLAYER_BAR_HUE", 0);

            xmlParser.SET_VALUE_TO_KYU("TARGET_BAR_R", 0);
            xmlParser.SET_VALUE_TO_KYU("TARGET_BAR_B", 0);
            xmlParser.SET_VALUE_TO_KYU("TARGET_BAR_G", 0);
            xmlParser.SET_VALUE_TO_KYU("TARGET_BAR_BRIGHTNESS", BotSettings.TARGET_BAR_BRIGHTNESS);
            xmlParser.SET_VALUE_TO_KYU("TARGET_BAR_HUE", 0);


        }

        public void LOAD_USER_SETTINGS()
        {
            string fileContent = null;
            playerRegionLoaded = false;
            targetRegionLoaded = false;

            // Cargo la configuracion del usuario al programa
            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
                fileDialog.Title = "Abrir .kyu archivo";
                fileDialog.Filter = "KYU files|*.kyu";
                fileDialog.InitialDirectory = Directory.GetCurrentDirectory();
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var fileStream = fileDialog.OpenFile();
                        using (StreamReader reader = new StreamReader(fileStream))
                        {
                            fileContent = reader.ReadToEnd();
                        }
                        if (fileContent != null)
                        {
                            // Archivo abierto, cargo la configuracion del usuario.
                            BotSettings.KYU_FILE_PATH = fileDialog.FileName;
                            BotSettings.USER_NAME = Path.GetFileNameWithoutExtension(fileDialog.FileName);
                            Debug.WriteLine("kyu file path ahora es: " + BotSettings.KYU_FILE_PATH);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: No pude leer el arhivo del disco :(. Error original: " + ex.Message);
                    }
                }
            }

            BotSettings.Reload();
            UPDATE_APP_DATA();
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
                    BotSettings.USER_NAME = Path.GetFileNameWithoutExtension(filePath);

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
