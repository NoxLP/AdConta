using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdConta;
using AdConta.Models;
using ModuloGestion.Helpers;

namespace ModuloGestion.Models
{
    public class Finca
    {
        public Finca()
        {

        }

        #region fields
        private int _Id;
        private int _Owner_IdComunidad;
        private BankAccount _Account;

        private Dictionary<int, Cuota> _Cuotas;
        private EntACtaDict _EntregasACuenta;
        #endregion

        #region properties
        public int Id { get { return this._Id; } }
        public int OwnerIdComunidad { get { return this._Owner_IdComunidad; } }
        public string Nombre { get; set; }
        public double Coeficiente { get; set; }

        public sDireccionPostal Direccion { get; set; }
        public sDireccionPostal Direccion2 { get; set; }
        
        public BankAccount Account
        {
            get { return this._Account; }
            set { this._Account = value; }
        }
        public TipoPagoCuotas TipoPagoCuotas { get; set; }

        public Dictionary<int, Cuota> Cuotas { get { return this._Cuotas; } }
        public EntACtaDict EntregasACuenta { get { return this._EntregasACuenta; } }
        public double DeudaALaFecha { get; set; }

        #endregion

        #region helpers
        #endregion

        #region public methods
        #endregion
    }

}
