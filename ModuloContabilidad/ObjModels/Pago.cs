using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdConta;
using AdConta.Models;

namespace ModuloContabilidad.ObjModels
{
    public class Pago : GastosPagosBase, iOwnerProveedor, iOwnerFactura
    {
        public Pago(int id, int idComunidad, int idProveedor, int idFactura, Date fecha) : base(id, idComunidad, fecha)
        {
            this._IdOwnerProveedor = idProveedor;
            this._IdOwnerFactura = idFactura;
        }

        /*public Pago(
            int id,
            int idProveedor,
            int idFactura,
            List<sImporteCuenta> cuentasAcreedoras,
            List<sImporteCuenta> cuentasDeudoras,
            Date fecha,
            string concepto,
            decimal importe) : base(id, cuentasAcreedoras, cuentasDeudoras, fecha, concepto, importe)
        {
            this._IdOwnerProveedor = idProveedor;
            this._IdOwnerFactura = idFactura;
        }*/

        #region fields
        private int _IdOwnerProveedor;
        private int _IdOwnerFactura;
        #endregion

        #region properties
        public int IdOwnerProveedor { get { return this._IdOwnerProveedor; } }
        public int IdOwnerFactura { get { return this._IdOwnerFactura; } }
        #endregion
    }

}
