using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdConta
{
    /// <summary>
    /// Strings for the header title of tabs.
    /// Strings are in the same order as of TabType enum, so you can do TabHeaders[TabType] to get the title.
    /// </summary>
    public class TabHeader
    {
        private readonly string[] _TabHeaders = new string[]
        {
            "",
            "Libro Mayor",
            "Libro Diario",
            "Propietarios",
            "Comunidad"
        };

        public string this[TabType index]
        {
            get { return this._TabHeaders[(int)index]; }
        }

        public string this[int index]
        {
            get { return this._TabHeaders[index]; }
        }
    }

    /// <summary>
    /// Class for switch custom type classes
    /// http://stackoverflow.com/questions/298976/is-there-a-better-alternative-than-this-to-switch-on-type
    /// </summary>
    static class TypeSwitch
    {
        public class CaseInfo
        {
            public bool IsDefault { get; set; }
            public Type Target { get; set; }
            public Action<object> Action { get; set; }
        }

        public static void Do(object source, params CaseInfo[] cases)
        {
            var type = source.GetType();
            foreach (var entry in cases)
            {
                if (entry.IsDefault || entry.Target.IsAssignableFrom(type))
                {
                    entry.Action(source);
                    break;
                }
            }
        }

        public static CaseInfo Case<T>(Action action)
        {
            return new CaseInfo()
            {
                Action = x => action(),
                Target = typeof(T)
            };
        }

        public static CaseInfo Case<T>(Action<T> action)
        {
            return new CaseInfo()
            {
                Action = (x) => action((T)x),
                Target = typeof(T)
            };
        }

        public static CaseInfo Default(Action action)
        {
            return new CaseInfo()
            {
                Action = x => action(),
                IsDefault = true
            };
        }
    }
}
