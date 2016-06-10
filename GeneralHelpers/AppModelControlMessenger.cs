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
        private static Dictionary<object, object> _MsgDict = new Dictionary<object, object>();
        #endregion

        #region events
        public delegate void ModelAddedEventHandler(object sender, ModelControlEventArgs e);
        public static event ModelAddedEventHandler ModelAddedEvent = delegate { };
        /// <summary>
        /// Add object Model to the corresponding dictionary, WITHOUT checking if owners exists. The model have to be asked first with
        /// AppModelControlMessenger.AskForModel
        /// </summary>
        /// <param name="model"></param>
        public static void AddModel(ref object model)
        {
            ModelControlEventArgs e = new ModelControlEventArgs(ref model);

            ModelAddedEvent(null, e);
        }

        public delegate void ObjModelAskedEventHandler(ref object sender, ModelControlEventArgs e);
        public static event ObjModelAskedEventHandler ObjModelAskedEvent = delegate { };
        public static bool AskForObjModel(ref object sender, ref object objModel)
        {
            ModelControlEventArgs e = new ModelControlEventArgs(ref objModel);

            if (!_MsgDict.ContainsKey(sender)) _MsgDict.Add(sender, null);
            ObjModelAskedEvent(ref sender, e);

            if (_MsgDict[sender] == null) return false;

            objModel = _MsgDict[sender];
            return true;

            /*bool ret =(_MsgDict[sender] == null ? false : (bool)_MsgDict[sender]);
            _MsgDict.Remove(sender);
            return ret;*/
        }
        #endregion

        #region public methods
        public static void SetMsgFromAppModelcontrol(ref object key, ref object objModel)
        {
            if (!_MsgDict.ContainsKey(key)) return;

            _MsgDict[key] = objModel;
        }
        #endregion
    }

    public class ModelControlEventArgs : EventArgs
    {
        public ModelControlEventArgs(ref object objectModel)
        {
            this._ObjectModel = objectModel;
        }

        private object _ObjectModel;
        public object ObjectModel { get { return this._ObjectModel; } }
    }
}
