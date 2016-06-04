using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdConta;

namespace ModuloGestion.Models
{
    public class Recibo
    {
        public Recibo()
        {

        }

        #region fields
        private int _Id;
        private int _OwnerIdCdad;
        private decimal _Importe;

        
        #endregion

        #region properties
        public int Id { get { return this._Id; } }
        public int OwnerIdCdad { get { return this._OwnerIdCdad; } }
        public decimal Importe { get { return this._Importe; } }
        #endregion

        #region helpers
        #endregion

        #region public methods
        #endregion
    }

}
