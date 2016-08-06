using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdConta.Models
{
    public class Persona : iObjModelBase
    {
        public Persona(int id, string nif, bool forceInvalidNIF = false)
        {
            this._Id = id;
            this._NIF = new NIFModel(nif);

            if (!this._NIF.IsValid && forceInvalidNIF)
                this._NIF.ForceInvalidNIF(ref nif);
        }

        #region fields
        private int _Id;
        private NIFModel _NIF;

        private BankAccount _CuentaBancaria;
        #endregion

        #region properties
        public int Id { get { return this._Id; } }
        public NIFModel NIF { get { return this._NIF; } }
        public string Nombre { get; protected set; }

        public bool EsPropietario { get; set; }
        public bool EsPagador { get; set; }
        public bool EsCopropietario { get; set; }

        public sDireccionPostal Direccion { get; set; }

        public BankAccount CuentaBancaria
        {
            get { return this._CuentaBancaria; }
            set { this._CuentaBancaria = value; }
        }

        public sTelefono Telefono1 { get; set; }
        public sTelefono Telefono2 { get; set; }
        public sTelefono Telefono3 { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Notas { get; set; }
        #endregion

        #region helpers
        #endregion

        #region public methods
        #endregion
    }

}
