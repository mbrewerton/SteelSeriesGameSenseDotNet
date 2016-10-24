using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteelSeriesGameSenseDotNet.Exceptions;
using SteelSeriesGameSenseDotNet.Utils;

namespace SteelSeriesGameSenseDotNet.Services
{
    // https://raw.githubusercontent.com/dwyl/english-words/master/words.txt
    public class GameSense
    {
        private string _gameName { get; set; }

        public GameSense(string gameName)
        {
            _gameName = gameName;
            HttpUtil.SetupHttpUtil();
        }
        //public GameSense()
        //{
        //    HttpUtil.SetupHttpUtil();
        //}

        public void RegisterGame(string gameDisplayName, string iconColourId)
        {
            CheckGameNameIsSet();
        }

        public void RegisterGameEvent(string eventName, int minValue = 0, int maxValue = 100, int iconId = 0)
        {
            CheckGameNameIsSet();
            throw new NotImplementedException();   
        }

        public void RemoveGameEvent(string eventName)
        {
            CheckGameNameIsSet();
            throw new NotImplementedException();
        }

        private bool CheckGameNameIsSet()
        {
            // If _gameName is null, somehow it's not been set and we need to throw an exception
            if (_gameName == null)
                throw new GameSenseException("There is no Game Name set. Please use the correct GameSense constructor, or the correct RegisterGame method.");

            // _gameName has been set, return true
            return true;
        }
    }
}
