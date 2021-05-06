using System;
using System.Diagnostics;
using System.Drawing;

namespace Shizui
{
    public class BotSettings
    {

        // clase de variables estaticas que se usaran para guardar y cargar configuraciones.

        public static string KYU_FILE_PATH = "default.kyu";
        public static string KYU_FILE_DEFAULT_PATH = "default.kyu";
        public static string USER_NAME = "default";
        public static string SKILL_XML_PATH = @"config/skills.xml";

        // handler for send keystrokes
        public static IntPtr L2_WINDOW_HANDLE = IntPtr.Zero;

        // handler for read memory
        public static IntPtr L2_PROCESS_HANDLER = IntPtr.Zero;
        public static Process L2_PROCESS = null;


        public static string PLAYER_NICKNAME;
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


        public static void Reload()
        {
            Load();
        }
        public static void Load()
        {

            PLAYER_NICKNAME = (string)XMLParser.GET_VALUE_FROM_KYU("PLAYER_NICKNAME");

            AUTO_POT_ENABLED = (bool)XMLParser.GET_VALUE_FROM_KYU("AUTO_POT_ENABLED");
            AUTO_POT_PERCENTAGE = (int)XMLParser.GET_VALUE_FROM_KYU("AUTO_POT_PERCENTAGE");
            RECOVER_MP_ENABLED = (bool)XMLParser.GET_VALUE_FROM_KYU("RECOVER_MP_ENABLED");
            MP_SIT_PERCENTAGE = (int)XMLParser.GET_VALUE_FROM_KYU("MP_SIT_PERCENTAGE");
            MP_STAND_PERCENTAGE = (int)XMLParser.GET_VALUE_FROM_KYU("MP_STAND_PERCENTAGE");
            ALWAYS_ON_TOP = (bool)XMLParser.GET_VALUE_FROM_KYU("ALWAYS_ON_TOP");
            BOT_PAUSE_CP = (bool)XMLParser.GET_VALUE_FROM_KYU("BOT_PAUSE_CP");
            UPDATE_INTERVAL = (int)XMLParser.GET_VALUE_FROM_KYU("UPDATE_INTERVAL");

            PICKUP_TIMES = (int)XMLParser.GET_VALUE_FROM_KYU("PICKUP_TIMES");
            DELAY_BETWEEN_PICKUPS = (int)XMLParser.GET_VALUE_FROM_KYU("DELAY_BETWEEN_PICKUPS");
            USE_SPOIL = (bool)XMLParser.GET_VALUE_FROM_KYU("USE_SPOIL");
            SPOIL_TIMES = (int)XMLParser.GET_VALUE_FROM_KYU("SPOIL_TIMES");

        }

    }
}
