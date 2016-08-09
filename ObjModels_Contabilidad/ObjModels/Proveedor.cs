using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdConta;
using AdConta.Models;

namespace ModuloContabilidad.ObjModels
{
    public class Proveedor : Persona
    {
        public Proveedor(int id, int idPersona, string nif, bool forceInvalidNIF = false) 
            : base(idPersona, nif, forceInvalidNIF)
        {
            this._IdProveedor = id;
        }

        public Proveedor(
            CuentaMayor CuentaContProveedor, 
            CuentaMayor CuentaContGasto,
            CuentaMayor CuentaContPago,
            int id, 
            string Razon,
            double IGICIVA,
            double IRPF,
            TipoPagoFacturas DefTPagoFacturas,
            int idPersona, string nif, bool forceInvalidNIF = false) : base(idPersona, nif, forceInvalidNIF)
        {
            this._CuentaContableProveedor = CuentaContableProveedor;
            this._CuentaContableGasto = CuentaContGasto;
            this._CuentaContablePago = CuentaContPago;
            this._IdProveedor = id;
            this.RazonSocial = Razon;
            this.IGICIVAPercent = IGICIVA;
            this.IRPFPercent = IRPF;
            this.DefaultTipoPagoFacturas = DefTPagoFacturas;
        }

        #region fields
        private int _IdProveedor;

        private CuentaMayor _CuentaContableGasto;
        private CuentaMayor _CuentaContablePago;
        private CuentaMayor _CuentaContableProveedor;
        #endregion

        #region properties
        public int IdProveedor { get { return this._IdProveedor; } }
        public string RazonSocial { get; set; }

        public CuentaMayor CuentaContableGasto { get { return this._CuentaContableGasto; } }
        public CuentaMayor CuentacontablePago { get { return this._CuentaContablePago; } }
        public CuentaMayor CuentaContableProveedor { get { return this._CuentaContableProveedor; } }

        public double IGICIVAPercent { get; set; }
        public double IRPFPercent { get; set; }
        public TipoPagoFacturas DefaultTipoPagoFacturas { get; set; }
        #endregion
    }

}
