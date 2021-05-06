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
            xmlParser = new XMLParser();
            player = Player.Instance;
            target = Target.Instance;
        }


        public void UPDATE_APP_DATA()
        {
            try
            {
                 // Config cargada, hora de llamar mis minions!
                 ConfigLoaded?.Invoke();

            } catch (TypeInitializationException err)
            {
                MessageBox.Show("Hubo un error al cargar la configuracion del usuario, archivo corrupto? Error: " + err.InnerException.Message);
            }

        }

        public void SAVE_SETTINGS_TO_KYU()
        {
            // Por ahora guardo todo siempre, en un futuro ver como mejorar esto.
            if(BotSettings.KYU_FILE_PATH == "default.kyu")
            {
                Debug.WriteLine("Se ignoro un intento de guardado en default.kyu");
                return;
            }

            xmlParser.SET_VALUE_TO_KYU("AUTO_POT_ENABLED", form1.autoPotCheckBoxValue);
            xmlParser.SET_VALUE_TO_KYU("AUTO_POT_PERCENTAGE", form1.autoPotTextBoxValue);
            xmlParser.SET_VALUE_TO_KYU("RECOVER_MP_ENABLED", form1.recoverMPCheckBoxValue);
            xmlParser.SET_VALUE_TO_KYU("MP_SIT_PERCENTAGE", form1.recoverMPSitTextBoxValue);
            xmlParser.SET_VALUE_TO_KYU("MP_STAND_PERCENTAGE", form1.recoverMPStandTextBoxValue);

            xmlParser.SET_VALUE_TO_KYU("UPDATE_INTERVAL", form1.updateIntervalValue);
            xmlParser.SET_VALUE_TO_KYU("ASSIST_MODE_ENABLED", form1.assistCheckBoxValue);
            xmlParser.SET_VALUE_TO_KYU("PICKUP_TIMES", BotSettings.PICKUP_TIMES);
            xmlParser.SET_VALUE_TO_KYU("DELAY_BETWEEN_PICKUPS", BotSettings.DELAY_BETWEEN_PICKUPS);
            xmlParser.SET_VALUE_TO_KYU("USE_SPOIL", BotSettings.USE_SPOIL);
            xmlParser.SET_VALUE_TO_KYU("SPOIL_TIMES", BotSettings.SPOIL_TIMES);


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

            xmlParser.SET_VALUE_TO_KYU("AUTO_POT_ENABLED", false);
            xmlParser.SET_VALUE_TO_KYU("AUTO_POT_PERCENTAGE", -1);
            xmlParser.SET_VALUE_TO_KYU("RECOVER_MP_ENABLED", false);
            xmlParser.SET_VALUE_TO_KYU("MP_SIT_PERCENTAGE", -1);
            xmlParser.SET_VALUE_TO_KYU("MP_STAND_PERCENTAGE", -1);

            xmlParser.SET_VALUE_TO_KYU("UPDATE_INTERVAL", form1.updateIntervalValue);
            xmlParser.SET_VALUE_TO_KYU("BOT_PAUSE_CP", false) ;
            xmlParser.SET_VALUE_TO_KYU("ALWAYS_ON_TOP", form1.alwaysOnTopValue);
            xmlParser.SET_VALUE_TO_KYU("ASSIST_MODE_ENABLED", false);

            xmlParser.SET_VALUE_TO_KYU("PICKUP_TIMES", 4);
            xmlParser.SET_VALUE_TO_KYU("DELAY_BETWEEN_PICKUPS", 200);
            xmlParser.SET_VALUE_TO_KYU("USE_SPOIL", false);
            xmlParser.SET_VALUE_TO_KYU("SPOIL_TIMES", 1);

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
                    Console.WriteLine("Usuario " + fileDialog.FileName + " creado.");
                } catch (Exception ex)
                {
                    MessageBox.Show("Error: No pude crear un nuevo archivo :(. Error original: " + ex.Message);
                }
            }
        }

    }
}
