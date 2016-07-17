using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdConta.Models;

namespace ModuloContabilidad.ObjModels
{
    public class Factura : iOwnerProveedor
    {
        public Factura()
        {

        }

        #region fields
        private int _Id;
        private int _IdOwnerProveedor;
        private string _NFactura;

        private Gasto _GastoFra;
        
        #endregion

        #region properties
        public int Id { get { return this._Id; } }
        public int IdOwnerProveedor { get { return this._IdOwnerProveedor; } }
        public string NFactura { get { return this._NFactura; } }

        public Gasto GastoFra { get { return this._GastoFra; } }
        
        #endregion

        #region helpers
        #endregion

        #region public methods
        #endregion
    }

}
