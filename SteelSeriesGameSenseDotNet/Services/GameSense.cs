using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SteelSeriesGameSenseDotNet.Exceptions;
using SteelSeriesGameSenseDotNet.Models;
using SteelSeriesGameSenseDotNet.Utils;

namespace SteelSeriesGameSenseDotNet.Services
{
    // https://raw.githubusercontent.com/dwyl/english-words/master/words.txt
    public class GameSense
    {
        private string _gameName { get; set; }

        /// <summary>
        /// Creates a new GameSense instance bound to the game name that you pass in. All calls using this instance will use the same Game
        /// </summary>
        /// <param name="gameName">The name of your Game to bind this instance of GameSense to.</param>
        public GameSense(string gameName)
        {
            _gameName = gameName;
            HttpUtil.SetupHttpUtil();
        }

        public HttpResponseMessage RegisterGame(string gameDisplayName, int iconColourId)
        {
            var game = new Game
            {
                GameName = _gameName,
                GameDisplayName = gameDisplayName,
                IconColorId = iconColourId
            };
            CheckGameNameIsSet();
            return HttpUtil.PostAsync("game_metadata", game).Result;
        }

        /// <summary>
        /// Registers a new game event to your Game. Note: It is not necessary to both bind and register an event. The difference is that event registration does not specify default (pre user customization) behavior for an event, whereas event binding does.
        /// </summary>
        /// <param name="eventName">The name of your game event.</param>
        /// <param name="minValue">The minimum value of your event data. (0 - 100)</param>
        /// <param name="maxValue">The maximum value of your event data. (0 - 100)</param>
        /// <param name="iconId">Id of your Game icon.</param>
        public void RegisterGameEvent(string eventName, int minValue = 0, int maxValue = 100, int iconId = 0)
        {
            if (minValue < 0 || minValue > 100)
                throw new ArgumentOutOfRangeException($"minValue of {minValue} is not valid. Please specify a number between 0 and 100.");
            if (maxValue < 0 || maxValue > 100)
                throw new ArgumentOutOfRangeException($"minValue of {maxValue} is not valid. Please specify a number between 0 and 100.");
            CheckGameNameIsSet();

            throw new NotImplementedException();   
        }

        /// <summary>
        /// Binds a new event to your Game. Note: It is not necessary to both bind and register an event. The difference is that event registration does not specify default (pre user customization) behavior for an event, whereas event binding does.
        /// </summary>
        /// <param name="eventName">The name of your game event.</param>
        /// <param name="handlers">Your handlers to send. Dictionary&lt;string, string&gt; which will get converted into json.</param>
        /// <param name="minValue">The minimum value of your event data. (0 - 100)</param>
        /// <param name="maxValue">The maximum value of your event data. (0 - 100)</param>
        /// <param name="iconId">Id of your Game icon.</param>
        /// <returns></returns>
        public HttpResponseMessage BindGameEvent(string eventName, Dictionary<string, string> handlers, int minValue = 0, int maxValue = 100, int iconId = 0)
        {
            throw new NotImplementedException();
        }

        public async void RemoveGameEvent(string eventName)
        {
            CheckGameNameIsSet();
            var gameEvent = new GameEvent
            {
                Game = _gameName,
                Event = eventName
            };

            await HttpUtil.PostAsync("remove_game_event", gameEvent);
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
