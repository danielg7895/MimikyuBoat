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

        public int hpRow;
        public int mpRow;
        public int cpRow;

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
