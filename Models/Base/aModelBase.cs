using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdConta;

namespace AdConta.Models
{
    /// <summary>
    /// Clase de la que deben heredar todos los models para que incluyan gestión de AppModelcontrol y no tener que hacerla en cada VM(errores)
    /// ¿SEGURO? ¿Y LOS OBJMODELS?
    /// </summary>
    public class aModelBase <T> where T : class
    {
        private aModelBase()
        {

        }

        #region fields
        #endregion

        #region properties
        public T ObjModel { get; set; }
        #endregion

        #region helpers
        #endregion

        #region public methods
        public T NewObjModel(ref object sender, T objModel)
        {
            object oObjModel = (object)objModel;
            if (AppModelControlMessenger.AskForObjModel(ref sender, ref oObjModel)) return null;
            return objModel;
        }
        #endregion
    }
}
