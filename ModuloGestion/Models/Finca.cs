using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdConta;
using AdConta.Models;

namespace ModuloGestion.Models
{
    public class Finca
    {
        public Finca()
        {

        }

        #region fields
        private int _Id;
        private BankAccount _Account;
        #endregion

        #region properties
        public int Id { get { return this._Id; } }
        public string Nombre { get; set; }
        public double Coeficiente { get; set; }

        public sDireccionPostal Direccion { get; set; }
        public sDireccionPostal Direccion2 { get; set; }
        
        public BankAccount Account
        {
            get { return _Account; }
            set { _Account = value; }
        }
        public TipoPagoCuotas TipoPagoCuotas { get; set; }
        public double DeudaALaFecha { get; set; }

        #endregion

        #region helpers
        #endregion

        #region public methods
        #endregion
    }

}
