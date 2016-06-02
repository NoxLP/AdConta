using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AdConta
{
    public static class AppModelControlMessenger
    {

        #region fields
        private static Dictionary<object, bool?> _msgDict = new Dictionary<object, bool?>();
        #endregion

        #region events
        public delegate void ModelAddedEventHandler(object sender, ModelControlEventArgs e);
        public static event ModelAddedEventHandler ModelAddedEvent = delegate { };
        public static void AddModel(ref object model)
        {
            ModelControlEventArgs e = new ModelControlEventArgs(ref model);

            ModelAddedEvent(null, e);
        }

        public delegate void ModelAskedEventHandler(ref object sender, ModelControlEventArgs e);
        public static event ModelAskedEventHandler ModelAskedEvent = delegate { };
        public static bool AskForModel(ref object sender, ref object model)
        {
            ModelControlEventArgs e = new ModelControlEventArgs(ref model);

            if (!_msgDict.ContainsKey(sender)) _msgDict.Add(sender, null);
            ModelAskedEvent(ref sender, e);
            
            bool ret =(_msgDict[sender] == null ? false : (bool)_msgDict[sender]);
            _msgDict.Remove(sender);
            return ret;
        }
        #endregion

        #region public methods
        public static void SetMsgFromAppModelcontrol(object key, bool value)
        {
            if (!_msgDict.ContainsKey(key)) return;

            _msgDict[key] = value;
        }
        #endregion
    }

    public class ModelControlEventArgs : EventArgs
    {
        public ModelControlEventArgs(ref object model)
        {
            this._Model = model;
        }

        private object _Model;
        public object Model { get { return this._Model; } }
    }
}
