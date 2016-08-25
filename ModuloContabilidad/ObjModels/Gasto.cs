using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdConta;
using AdConta.Models;

namespace ModuloContabilidad.ObjModels
{
    public class Gasto : GastosPagosBase, iOwnerProveedor
    {
        public Gasto(int id, int idComunidad, int idProveedor, Date fecha) : base(id, idComunidad, fecha)
        {
            this._IdOwnerProveedor = idProveedor;
        }

        /*public Gasto(
            int id,
            int idProveedor,
            List<sImporteCuenta> cuentasAcreedoras,
            List<sImporteCuenta> cuentasDeudoras,
            Date fecha,
            string concepto,
            decimal importe) : base(id, cuentasAcreedoras, cuentasDeudoras, fecha, concepto, importe)
        {
            this._IdOwnerProveedor = idProveedor;
        }*/

        #region fields
        private int _IdOwnerProveedor;
        #endregion

        #region properties
        public int IdOwnerProveedor { get { return this._IdOwnerProveedor; } }
        #endregion
    }
}
