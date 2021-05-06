using System;
using System.Diagnostics;
using System.Drawing;

namespace Shizui
{
    public class BotSettings
    {
        // variables privadas


        // clase de variables estaticas que se usaran para guardar y cargar configuraciones.

        public static string KYU_FILE_PATH = "default.kyu";
        public static string KYU_FILE_DEFAULT_PATH = "default.kyu";
        public static string USER_NAME = "default";
        public static string SKILL_XML_PATH = @"config/skills.xml";
        public static IntPtr L2_PROCESS_HANDLE = IntPtr.Zero;

        // Las posiciones del rectangulo son relativas al cliente del l2, no a la screen
        public static Rectangle PLAYER_CONFIGURATION_RECTANGLE;
        public static Rectangle TARGET_CONFIGURATION_RECTANGLE;

        public static string PLAYER_NICKNAME;
        public static int PLAYER_CP_ZONE;
        public static int PLAYER_HP_ZONE;
        public static int PLAYER_MP_ZONE;
        public static int TARGET_HP_ZONE;

        public static int PLAYER_CP_BARSTART;
        public static int PLAYER_HP_BARSTART;
        public static int PLAYER_MP_BARSTART;
        public static int TARGET_HP_BARSTART;

        public static bool AUTO_POT_ENABLED;
        public static int AUTO_POT_PERCENTAGE;
        public static bool RECOVER_MP_ENABLED;
        public static int MP_SIT_PERCENTAGE; 
        public static int MP_STAND_PERCENTAGE;
        public static int PICKUP_TIMES;
        public static int DELAY_BETWEEN_PICKUPS;
        public static bool USE_SPOIL;
        public static int SPOIL_TIMES;

        public static bool ALWAYS_ON_TOP;
        public static bool BOT_PAUSE_CP;
        public static int UPDATE_INTERVAL;
        public static bool ASSIST_MODE_ENABLED;
        public static string ASSIST_PLAYER_NICKNAME;
        public static int ASSIST_PLAYER_POS_X;
        public static int ASSIST_PLAYER_POS_Y;

        public static bool PLAYER_CP_BARSTART_INITIALIZED;
        public static bool PLAYER_HP_BARSTART_INITIALIZED;
        public static bool PLAYER_MP_BARSTART_INITIALIZED;
        public static bool TARGET_HP_BARSTART_INITIALIZED;
        public static bool PLAYER_REGION_LOADED;
        public static bool TARGET_REGION_LOADED;

        // Variables que no se guardan, son de uso global
        public static Bitmap PLAYER_IMAGE;
        public static Bitmap TARGET_IMAGE;

        public static int PLAYER_BAR_R;
        public static int PLAYER_BAR_G;
        public static int PLAYER_BAR_B;
        public static int PLAYER_BAR_BRIGHTNESS;
        public static int PLAYER_BAR_HUE;

        public static int TARGET_BAR_R;
        public static int TARGET_BAR_G;
        public static int TARGET_BAR_B;
        public static int TARGET_BAR_BRIGHTNESS;
        public static int TARGET_BAR_HUE;


        public static void Reload()
        {
            Load();
        }
        public static void Load()
        {
            PLAYER_CONFIGURATION_RECTANGLE = (Rectangle)XMLParser.GET_VALUE_FROM_KYU("PLAYER_CONFIGURATION_RECTANGLE");
            TARGET_CONFIGURATION_RECTANGLE = (Rectangle)XMLParser.GET_VALUE_FROM_KYU("TARGET_CONFIGURATION_RECTANGLE");
            PLAYER_NICKNAME = (string)XMLParser.GET_VALUE_FROM_KYU("PLAYER_NICKNAME");
            PLAYER_CP_ZONE = (int)XMLParser.GET_VALUE_FROM_KYU("PLAYER_CP_ZONE");
            PLAYER_HP_ZONE = (int)XMLParser.GET_VALUE_FROM_KYU("PLAYER_HP_ZONE");
            PLAYER_MP_ZONE = (int)XMLParser.GET_VALUE_FROM_KYU("PLAYER_MP_ZONE");
            TARGET_HP_ZONE = (int)XMLParser.GET_VALUE_FROM_KYU("TARGET_HP_ZONE");

            PLAYER_CP_BARSTART = (int)XMLParser.GET_VALUE_FROM_KYU("PLAYER_CP_BARSTART");
            PLAYER_HP_BARSTART = (int)XMLParser.GET_VALUE_FROM_KYU("PLAYER_HP_BARSTART");
            PLAYER_MP_BARSTART = (int)XMLParser.GET_VALUE_FROM_KYU("PLAYER_MP_BARSTART");
            TARGET_HP_BARSTART = (int)XMLParser.GET_VALUE_FROM_KYU("TARGET_HP_BARSTART");
            AUTO_POT_ENABLED = (bool)XMLParser.GET_VALUE_FROM_KYU("AUTO_POT_ENABLED");
            AUTO_POT_PERCENTAGE = (int)XMLParser.GET_VALUE_FROM_KYU("AUTO_POT_PERCENTAGE");
            RECOVER_MP_ENABLED = (bool)XMLParser.GET_VALUE_FROM_KYU("RECOVER_MP_ENABLED");
            MP_SIT_PERCENTAGE = (int)XMLParser.GET_VALUE_FROM_KYU("MP_SIT_PERCENTAGE");
            MP_STAND_PERCENTAGE = (int)XMLParser.GET_VALUE_FROM_KYU("MP_STAND_PERCENTAGE");
            ALWAYS_ON_TOP = (bool)XMLParser.GET_VALUE_FROM_KYU("ALWAYS_ON_TOP");
            BOT_PAUSE_CP = (bool)XMLParser.GET_VALUE_FROM_KYU("BOT_PAUSE_CP");
            UPDATE_INTERVAL = (int)XMLParser.GET_VALUE_FROM_KYU("UPDATE_INTERVAL");

            PLAYER_CP_BARSTART_INITIALIZED = (bool)XMLParser.GET_VALUE_FROM_KYU("PLAYER_CP_BARSTART_INITIALIZED");
            PLAYER_HP_BARSTART_INITIALIZED = (bool)XMLParser.GET_VALUE_FROM_KYU("PLAYER_HP_BARSTART_INITIALIZED");
            PLAYER_MP_BARSTART_INITIALIZED = (bool)XMLParser.GET_VALUE_FROM_KYU("PLAYER_MP_BARSTART_INITIALIZED");
            TARGET_HP_BARSTART_INITIALIZED = (bool)XMLParser.GET_VALUE_FROM_KYU("TARGET_HP_BARSTART_INITIALIZED");
            //PLAYER_REGION_LOADED = (bool)XMLParser.GET_VALUE_FROM_KYU("PLAYER_REGION_LOADED");
            //TARGET_REGION_LOADED = (bool)XMLParser.GET_VALUE_FROM_KYU("TARGET_REGION_LOADED");

            PICKUP_TIMES = (int)XMLParser.GET_VALUE_FROM_KYU("PICKUP_TIMES");
            DELAY_BETWEEN_PICKUPS = (int)XMLParser.GET_VALUE_FROM_KYU("DELAY_BETWEEN_PICKUPS");
            USE_SPOIL = (bool)XMLParser.GET_VALUE_FROM_KYU("USE_SPOIL");
            SPOIL_TIMES = (int)XMLParser.GET_VALUE_FROM_KYU("SPOIL_TIMES");

            PLAYER_BAR_R = (int)XMLParser.GET_VALUE_FROM_KYU("PLAYER_BAR_R");
            PLAYER_BAR_G = (int)XMLParser.GET_VALUE_FROM_KYU("PLAYER_BAR_G");
            PLAYER_BAR_B = (int)XMLParser.GET_VALUE_FROM_KYU("PLAYER_BAR_B");
            PLAYER_BAR_BRIGHTNESS = (int)XMLParser.GET_VALUE_FROM_KYU("PLAYER_BAR_BRIGHTNESS");
            PLAYER_BAR_HUE = (int)XMLParser.GET_VALUE_FROM_KYU("PLAYER_BAR_HUE");

            TARGET_BAR_R = (int)XMLParser.GET_VALUE_FROM_KYU("TARGET_BAR_R");
            TARGET_BAR_G = (int)XMLParser.GET_VALUE_FROM_KYU("TARGET_BAR_G");
            TARGET_BAR_B = (int)XMLParser.GET_VALUE_FROM_KYU("TARGET_BAR_B");
            TARGET_BAR_BRIGHTNESS = (int)XMLParser.GET_VALUE_FROM_KYU("TARGET_BAR_BRIGHTNESS");
            TARGET_BAR_HUE = (int)XMLParser.GET_VALUE_FROM_KYU("TARGET_BAR_HUE");

            #region CARGA DE VARIABLES QUE NO SE GUARDAN
            LoadImageBars();

            #endregion
        }

        public static bool LoadImageBars()
        {
            try
            {
                // Para evitar tener problemas al borrar la imagen y crear una nueva 
                // necesito disposear la imagen apenas la obtengo pero previamente clonandola.
                Bitmap player_img = new Bitmap("temp/player.jpeg");
                PLAYER_IMAGE = new Bitmap(player_img);
                player_img.Dispose();

                Bitmap target_img = new Bitmap("temp/target.jpeg");
                TARGET_IMAGE = new Bitmap(target_img);
                target_img.Dispose();
            }
            catch (ArgumentException)
            {
                Debug.WriteLine("La imagen del target o del player que se intenta acceder no existe");
                return false;
            }
            return true;
        }
    }
}
