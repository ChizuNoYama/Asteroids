using System;
using System.Diagnostics;

namespace JumpMan.Utilities
{
    public static class Logger
    {
        public static void Log(object message)
        {
            #if DEBUG
                Debug.WriteLine($"***** {message.ToString()}");
            #else
                Console.WriteLine($"***** {message.ToString()}");
            #endif
        }
    }
}