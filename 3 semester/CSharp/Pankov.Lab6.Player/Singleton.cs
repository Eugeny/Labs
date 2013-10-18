using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pankov.Lab6.Player
{
    public abstract class Singleton<T> where T : class, new()
    {
        private static T _instance = null;
        private static object _lock = new object();

        public static T Get()
        {
            lock (_lock)
                if (_instance == null)
                    _instance = new T();
            return _instance;
        }
    }
}
