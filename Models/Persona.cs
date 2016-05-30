﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdConta.Models
{
    public class Persona
    {
        public Persona(int id, string nif, bool forceInvalidNIF)
        {
            this._id = id;
            this._NIF = new NIFModel(nif);

            if (!this._NIF.IsValid && forceInvalidNIF)
                this._NIF.ForceInvalidNIF(nif);
        }

        #region fields
        private int _id;
        private NIFModel _NIF;

        private BankAccount _CuentaBancaria;
        #endregion

        #region properties
        public int id { get { return this._id; } }
        public NIFModel NIF { get { return this._NIF; } }

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
