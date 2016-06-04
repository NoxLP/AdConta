using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdConta.Models;
using ModuloGestion.Models;

namespace AdConta.ModelControl
{
    public class Comunidad
    {
        public Comunidad(int id)
        {
            this._Id = id;
        }

        #region fields
        private int _Id;
        internal Dictionary<int, Finca> _Fincas; //
        internal Dictionary<int, sEjercicio> _Ejercicios;
        //internal Dictionary<int, Presupuesto> _Presupuestos;

        #endregion

        #region properties
        public int Id { get { return this._Id; } }

        #endregion

        #region helpers
        #endregion

        #region public methods
        #endregion
    }

}
