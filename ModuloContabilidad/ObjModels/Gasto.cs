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
    public class Gasto : iOwnerProveedor, iOwnerFactura
    {
        public Gasto(
            int id, 
            int idProveedor, 
            int idFactura,
            List<LedgeAccount> cuentasAcreedoras,
            List<LedgeAccount> cuentasDeudoras,
            Date fecha,
            string concepto,
            decimal importe)
        {
            this._Id = id;
            this._IdOwnerProveedor = idProveedor;
            this._IdOwnerFactura = idFactura;

            this._CuentasAcreedoras = cuentasAcreedoras;
            this._CuentasDeudoras = cuentasDeudoras;
            this._Fecha = fecha;
            this.Concepto = concepto;
            this._ImporteTotal = importe;
        }

        #region fields
        private int _Id;
        private int _IdOwnerProveedor;
        private int _IdOwnerFactura;

        private List<LedgeAccount> _CuentasAcreedoras;
        private List<LedgeAccount> _CuentasDeudoras;
        
        private Date _Fecha;
        private decimal _ImporteTotal;
        #endregion

        #region properties
        public int Id { get { return this._Id; } }
        public int IdOwnerProveedor { get { return this._IdOwnerProveedor; } }
        public int IdOwnerFactura { get { return this._IdOwnerFactura; } }

        public ReadOnlyCollection<LedgeAccount> CuentasAcreedoras { get { return this._CuentasAcreedoras.AsReadOnly(); } }
        public ReadOnlyCollection<LedgeAccount> CuentasDeudoras { get { return this._CuentasDeudoras.AsReadOnly(); } }

        public Date Fecha { get { return this._Fecha; } }
        public string Concepto { get; set; }
        public decimal ImporteTotal { get { return this._ImporteTotal; } }
        #endregion

        #region helpers
        #endregion

        #region public methods
        #endregion
    }
}
