using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdConta.Models;

namespace AdConta.Models
{
    public class Comunidad : IObjModelBase, IObjModelConCodigo, IBaja, IObjWithDLO<ComunidadDLO>
    {
        public Comunidad(int id, string nif, bool baja, string nombre, int codigo, AutoCodigoData ACData, bool forceInvalidNIF = false)
        {
            this._Id = id;
            this._CIF = new NIFModel(nif);
            this._Baja = baja;
            this.Nombre = nombre;
            this.Codigo = new AutoCodigoNoOwner<Comunidad>(ACData, codigo);

            if (!this._CIF.IsValid && forceInvalidNIF)
                this._CIF.ForceInvalidNIF(ref nif);
        }

        #region fields
        private int _Id;
        private NIFModel _CIF;
        private bool _Baja;

        private CuentaBancaria _CuentaBancaria1;
        private CuentaBancaria _CuentaBancaria2;
        private CuentaBancaria _CuentaBancaria3;
        #endregion

        #region properties
        public int Id { get { return this._Id; } }
        public aAutoCodigoBase Codigo { get; private set; }
        public NIFModel CIF { get { return this._CIF; } }
        public bool Baja { get { return this._Baja; } }
        public string Nombre { get; set; }

        public DireccionPostal Direccion { get; set; }

        public CuentaBancaria CuentaBancaria1 { get { return this._CuentaBancaria1; } }
        public CuentaBancaria CuentaBancaria2 { get { return this._CuentaBancaria2; } }
        public CuentaBancaria CuentaBancaria3 { get { return this._CuentaBancaria3; } }

        public Persona Presidente { get; set; }
        public Persona Secretario { get; set; }
        public Persona Tesorero { get; set; }
        public HashSet<int> Vocales { get; set; }

        public Ejercicio EjercicioActivo { get; private set; }
        /// <summary>
        /// Fecha del último punteo efectuado, NO ES LA FECHA DEL ULTIMO ASIENTO PUNTEADO.
        /// </summary>
        public Date FechaPunteo { get; private set; }
        /// <summary>
        /// Fecha del último asiento grabado que incluye cualquier apunte al banco.
        /// </summary>
        public Date UltimaFechaBanco { get; private set; }
        #endregion

        #region public methods
        public bool TrySetEjercicioActivo(Ejercicio nuevoEjercicio)
        {
            if (this.EjercicioActivo == nuevoEjercicio || Baja) return false;

            this.EjercicioActivo = nuevoEjercicio;
            return true;
        }
        public bool TrySetFechaPunteo(ref Date fecha)
        {
            if (fecha == null || !this.EjercicioActivo.Contains(fecha) || fecha < this.FechaPunteo || Baja) return false;

            this.FechaPunteo = fecha;
            return true;
        }
        public bool TrySetUltimaFechaBanco(ref Date fecha)
        {
            if (fecha == null || !this.EjercicioActivo.Contains(fecha) || Baja) return false;

            this.UltimaFechaBanco = fecha;
            return true;
        }
        /// <summary>
        /// Devuelve mensaje de error, null si no hay error.
        /// </summary>
        /// <param name="nuevaCuenta"></param>
        /// <param name="cuentaN"></param>
        /// <returns></returns>
        public string TrySetCuentaBancariaN(string nuevaCuenta, int cuentaN)
        {
            if (Baja) return "La comunidad está dada de baja. No se pueden realizar cambios.";
            CuentaBancaria NuevaCuenta = new CuentaBancaria();

            try
            {
                NuevaCuenta.AccountNumber = nuevaCuenta;
            }
            catch(CustomException_ObjModels err)
            {
                return err.ToString();
            }

            switch(cuentaN)
            {
                case 1:
                    this._CuentaBancaria1 = NuevaCuenta;
                    break;
                case 2:
                    this._CuentaBancaria2 = NuevaCuenta;
                    break;
                case 3:
                    this._CuentaBancaria3 = NuevaCuenta;
                    break;
            }

            return null;
        }
        /// <summary>
        /// Devuelve mensaje de error, null si no hay error.
        /// </summary>
        /// <param name="nif"></param>
        /// <returns></returns>
        public string TrySetNIF(string nif)
        {
            if (Baja) return "La comunidad está dada de baja. No se pueden realizar cambios.";
            NIFModel nuevoNIF = new NIFModel(nif);

            if (nuevoNIF.InvalidMessage != null) return nuevoNIF.InvalidMessage;

            this._CIF = nuevoNIF;
            return null;
        }
        public bool DarDeBaja()
        {
            if (Baja) return false;
            this._Baja = true;
            return true;
        }
        public bool RecuperarBaja()
        {
            if (!Baja) return false;
            this._Baja = false;
            return true;
        }
        #endregion

        #region DLO
        public ComunidadDLO GetDLO()
        {
            return new ComunidadDLO(
                Id, Codigo.CurrentCodigo, CIF, Baja, Nombre, Direccion.GetDireccionSinCP(), CuentaBancaria1.AccountNumber,
                CuentaBancaria2.AccountNumber, CuentaBancaria3.AccountNumber, Presidente.Nombre, Secretario.Nombre, Tesorero.Nombre,
                FechaPunteo.ToString(), UltimaFechaBanco.ToString());
        }
        #endregion
    }

    public class ComunidadDLO : IObjModelBase, IDataListObject
    {
        public ComunidadDLO() { }
        public ComunidadDLO(
            int id,
            int codigo,
            NIFModel CIF,
            bool baja,
            string nombre,
            string direccion,
            string cuenta1,
            string cuenta2,
            string cuenta3,
            string nombrePresidente,
            string nombreSecretario,
            string nombreTesorero,
            string fechaPunteo,
            string ultimaFechaBanco)
        {
            this.Id = id;
            this.Codigo = codigo;
            this.CIF = CIF.NIF;
            this.Baja = baja;
            this.Nombre = nombre;
            this.Direccion = direccion;
            this.CuentaBancaria1 = cuenta1;
            this.CuentaBancaria2 = cuenta2;
            this.CuentaBancaria3 = cuenta3;
            this.NombrePresidente = nombrePresidente;
            this.NombreSecretario = nombreSecretario;
            this.NombreTesorero = nombreTesorero;
            this.FechaPunteo = fechaPunteo;
            this.UltimaFechaBanco = ultimaFechaBanco;
        }

        public int Id { get; private set; }
        public int Codigo { get; private set; }
        public string CIF { get; private set; }
        public bool Baja { get; private set; }
        public string Nombre { get; private set; }
        public string Direccion { get; private set; }
        public string CuentaBancaria1 { get; private set; }
        public string CuentaBancaria2 { get; private set; }
        public string CuentaBancaria3 { get; private set; }
        public string NombrePresidente { get; private set; }
        public string NombreSecretario { get; private set; }
        public string NombreTesorero { get; private set; }
        public string FechaPunteo { get; private set; }
        public string UltimaFechaBanco { get; private set; }
    }
}
