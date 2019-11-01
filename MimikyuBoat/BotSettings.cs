using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MimikyuBoat
{
    class BotSettings
    {

        // clase de variables estaticas que se usaran para guardar y cargar configuraciones.

        public static Rectangle PLAYER_CONFIGURATION_AREA = (Rectangle)Properties.Settings.Default["PLAYER_CONFIGURATION_AREA"];
        public static Rectangle TARGET_CONFIGURATION_AREA = (Rectangle)Properties.Settings.Default["TARGET_CONFIGURATION_AREA"];
        public static string PLAYER_NICKNAME = (string)Properties.Settings.Default["PLAYER_NICKNAME"];
        public static int PLAYER_CP_BARSTART = (int)Properties.Settings.Default["PLAYER_CP_BARSTART"];
        public static int PLAYER_CP_ZONE = (int)Properties.Settings.Default["PLAYER_CP_ZONE"];
        public static int PLAYER_HP_ZONE = (int)Properties.Settings.Default["PLAYER_HP_ZONE"];
        public static int PLAYER_MP_ZONE = (int)Properties.Settings.Default["PLAYER_MP_ZONE"];
        public static int TARGET_HP_ZONE = (int)Properties.Settings.Default["TARGET_HP_ZONE"];
        public static int PLAYER_HP_BARSTART = (int)Properties.Settings.Default["PLAYER_HP_BARSTART"];
        public static int PLAYER_MP_BARSTART = (int)Properties.Settings.Default["PLAYER_MP_BARSTART"];
        public static int TARGET_HP_BARSTART = (int)Properties.Settings.Default["TARGET_HP_BARSTART"];
        public static bool AUTO_POT_ENABLED = (bool)Properties.Settings.Default["AUTO_POT_ENABLED"];
        public static int AUTO_POT_PERCENTAGE = (int)Properties.Settings.Default["AUTO_POT_PERCENTAGE"];
        public static bool RECOVER_MP_ENABLED = (bool)Properties.Settings.Default["RECOVER_MP_ENABLED"];
        public static int MP_SIT_PERCENTAGE = (int)Properties.Settings.Default["MP_SIT_PERCENTAGE"];
        public static int MP_STAND_PERCENTAGE = (int)Properties.Settings.Default["MP_STAND_PERCENTAGE"];
        public static bool ALWAYS_ON_TOP = (bool)Properties.Settings.Default["ALWAYS_ON_TOP"];
        public static bool AUTO_ATTACK = (bool)Properties.Settings.Default["AUTO_ATTACK"];
        public static bool USE_NEXT_TARGET = (bool)Properties.Settings.Default["USE_NEXT_TARGET"];
        public static bool BOT_PAUSE_CP = (bool)Properties.Settings.Default["BOT_PAUSE_CP"];
        public static int UPDATE_INTERVAL = (int)Properties.Settings.Default["UPDATE_INTERVAL"];

        public static bool PLAYER_CP_BARSTART_INITIALIZED = (bool)Properties.Settings.Default["PLAYER_CP_BARSTART_INITIALIZED"];
        public static bool PLAYER_HP_BARSTART_INITIALIZED = (bool)Properties.Settings.Default["PLAYER_HP_BARSTART_INITIALIZED"];
        public static bool PLAYER_MP_BARSTART_INITIALIZED = (bool)Properties.Settings.Default["PLAYER_MP_BARSTART_INITIALIZED"];
        public static bool TARGET_HP_BARSTART_INITIALIZED = (bool)Properties.Settings.Default["TARGET_HP_BARSTART_INITIALIZED"];

    }
}
