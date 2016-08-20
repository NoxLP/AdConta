using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdConta.Models;
using ModuloContabilidad.ObjModels;
using ModuloGestion.ObjModels;

namespace AdConta
{
    public class Comunidad : iObjModelBase
    {
        public Comunidad(int id, string nif, string nombre, bool forceInvalidNIF = false)
        {
            this._Id = id;
            this._CIF = new NIFModel(nif);
            this.Nombre = nombre;

            if (!this._CIF.IsValid && forceInvalidNIF)
                this._CIF.ForceInvalidNIF(ref nif);
        }

        #region fields
        private int _Id;
        private NIFModel _CIF;

        private CuentaBancaria _CuentaBancaria1;
        private CuentaBancaria _CuentaBancaria2;
        private CuentaBancaria _CuentaBancaria3;
        #endregion

        #region properties
        public int Id { get { return this._Id; } }
        public int Codigo { get; set; }
        public NIFModel CIF { get { return this._CIF; } }
        public string Nombre { get; set; }

        public DireccionPostal Direccion { get; set; }

        public CuentaBancaria CuentaBancaria1 { get { return this._CuentaBancaria1; } }
        public CuentaBancaria CuentaBancaria2 { get { return this._CuentaBancaria2; } }
        public CuentaBancaria CuentaBancaria3 { get { return this._CuentaBancaria3; } }

        public Persona Presidente { get; set; }
        public Persona Secretario { get; set; }
        public Persona Tesorero { get; set; }
        public HashSet<int> Vocales { get; set; }
        #endregion
    }

}
