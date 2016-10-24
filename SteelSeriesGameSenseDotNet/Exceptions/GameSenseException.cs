using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SteelSeriesGameSenseDotNet.Exceptions
{
    public class GameSenseException : Exception
    {
        public GameSenseException(string message, Exception innerException = null) : base(message, innerException) { }
    }

    public class GameSenseHttpException : GameSenseException
    {
        public GameSenseHttpException(string message, Exception innerException = null) : base(message, innerException) { }
    }
}
