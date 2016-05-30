using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace ModuloGestion.Models
{
    public class Persona
    {
        public Persona(int id)
        {
            this._id = id;
        }

        #region fields
        private int _id;
        private NIFModel _NIF;
        #endregion

        #region properties
        public NIFModel NIF
        {
            get { return this._NIF; }
        }
        public bool EsPropietario { get; set; }
        public bool EsPagador { get; set; }
        public bool EsCopropietario { get; set; }

        #endregion

        #region helpers
        #endregion

        #region public methods
        #endregion
    }

}
