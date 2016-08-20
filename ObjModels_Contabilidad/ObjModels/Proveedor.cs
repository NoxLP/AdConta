using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdConta;
using AdConta.Models;

namespace ModuloContabilidad.ObjModels
{
    public class Proveedor : Persona, iOwnerComunidad
    {
        public Proveedor(int id, int idPersona, int idComunidad, string nif, string nombre, bool forceInvalidNIF = false) 
            : base(idPersona, nif, nombre, forceInvalidNIF)
        {
            this._IdProveedor = id;
            this._IdOwnerComunidad = idComunidad;
        }

        public Proveedor(
            CuentaMayor CuentaContProveedor, 
            CuentaMayor CuentaContGasto,
            CuentaMayor CuentaContPago,
            int id, 
            int idComunidad,
            string Razon,
            double IGICIVA,
            double IRPF,
            TipoPagoFacturas DefTPagoFacturas,
            int idPersona, string nif, string nombre, bool forceInvalidNIF = false) : base(idPersona, nif, nombre, forceInvalidNIF)
        {
            this._CuentaContableProveedor = CuentaContableProveedor;
            this._CuentaContableGasto = CuentaContGasto;
            this._CuentaContablePago = CuentaContPago;
            this._IdProveedor = id;
            this._IdOwnerComunidad = idComunidad;
            this.RazonSocial = Razon;
            this.IGICIVAPercent = IGICIVA;
            this.IRPFPercent = IRPF;
            this.DefaultTipoPagoFacturas = DefTPagoFacturas;
        }

        #region fields
        private int _IdProveedor;
        private int _IdOwnerComunidad;
        
        private CuentaMayor _CuentaContableGasto;
        private CuentaMayor _CuentaContablePago;
        private CuentaMayor _CuentaContableProveedor;
        #endregion

        #region properties
        public int IdProveedor { get { return this._IdProveedor; } }
        public int IdOwnerComunidad { get { return this._IdOwnerComunidad; } }
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
