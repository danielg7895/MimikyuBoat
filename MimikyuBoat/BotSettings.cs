using System;
using System.Diagnostics;
using System.Drawing;

namespace MimikyuBoat
{
    public class BotSettings
    {
        // variables privadas


        // clase de variables estaticas que se usaran para guardar y cargar configuraciones.

        public static string KYU_FILE_PATH = "default.kyu";
        public static string KYU_FILE_DEFAULT_PATH = "default.kyu";
        public static string SKILL_XML_PATH = @"config/skills.xml";
        public static IntPtr L2_PROCESS_HANDLE = IntPtr.Zero;

        public static Rectangle PLAYER_CONFIGURATION_AREA;
        public static Rectangle TARGET_CONFIGURATION_AREA;
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
        public static bool ALWAYS_ON_TOP;
        public static bool BOT_PAUSE_CP;
        public static int UPDATE_INTERVAL;

        public static bool PLAYER_CP_BARSTART_INITIALIZED;
        public static bool PLAYER_HP_BARSTART_INITIALIZED;
        public static bool PLAYER_MP_BARSTART_INITIALIZED;
        public static bool TARGET_HP_BARSTART_INITIALIZED;

        public static void Reload()
        {
            Load();
        }
        public static void Load()
        {
            PLAYER_CONFIGURATION_AREA = (Rectangle)XMLParser.GET_VALUE_FROM_KYU("PLAYER_CONFIGURATION_AREA");
            TARGET_CONFIGURATION_AREA = (Rectangle)XMLParser.GET_VALUE_FROM_KYU("TARGET_CONFIGURATION_AREA");
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
        }
    }
}
