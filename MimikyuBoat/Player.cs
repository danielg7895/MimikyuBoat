using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MimikyuBoat
{
    class Player
    {
        public int cp;
        public int hp;
        public int mp;
        public int race;
        public int playerClass;
        public string nickName = "";

        // en que pixel comienza la barra
        public int cpBarStart;
        public int hpBarStart;
        public int mpBarStart;

        public int hpRow = -1;
        public int mpRow = -1;
        public int cpRow = -1;

        public string imagePath = "temp/player.jpeg";

        #region singleton
        private static Player _instance;
        public static Player Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Player();
                }
                return _instance;
            }
        }
        #endregion
    }
}
