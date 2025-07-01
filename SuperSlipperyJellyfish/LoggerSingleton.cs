using BepInEx.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSlipperyJellyfish
{
    public sealed class Log
    {
        private static Log instance;
        private static readonly object singletonLock = new object();
        private static ManualLogSource source = new ManualLogSource(ModInfo.guid);
        Log() { }

        public static Log Instance
        {
            get
            {
                lock (singletonLock)
                {
                    if (instance == null)
                        instance = new Log();
                    return instance;
                }
            }
        }

        public static ManualLogSource Write
        {
            get { return source; }
        }
        public static void Init(string initMessage)
        {
            BepInEx.Logging.Logger.Sources.Add(source);
            source.LogMessage(initMessage);
            source.LogMessage("Logger component initialised.");
        }
        public static void Delete()
        {
            source.LogMessage("Bye :(");
            BepInEx.Logging.Logger.Sources.Remove(source);
        }

    }
}
