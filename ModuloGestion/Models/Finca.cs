using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private int _Owner_IdComunidad;
        private string _Nombre;
        private double _Coeficiente;
        private BankAccount _Account;
        private Persona _Propietario;

        private int[] _IdCopropietarios = new int[3];
        private int[] _IdPagadores = new int[3];

        private Dictionary<int, Cuota> _Cuotas;
        private EntACtaDict _EntregasACuenta;
        #endregion

        #region properties
        public int Id { get { return this._Id; } }
        public int OwnerIdComunidad { get { return this._Owner_IdComunidad; } }
        public string Nombre { get { return this._Nombre; } }
        public double Coeficiente { get { return this._Coeficiente; } }

        public sDireccionPostal Direccion { get; set; }
        public sDireccionPostal Direccion2 { get; set; }
        
        public BankAccount Account
        {
            get { return this._Account; }
            set { this._Account = value; }
        }
        public TipoPagoCuotas TipoPagoCuotas { get; set; }
        public Persona Propietario { get { return this._Propietario; } }
        public sTelefono Telefono1 { get; set; }
        public sTelefono Telefono2 { get; set; }
        public sTelefono Telefono3 { get; set; }
        public sTelefono Fax { get; set; }
        public string Email { get; set; }
        public int[] IdCopropietarios
        {
            get { return this._IdCopropietarios; }
            set { this._IdCopropietarios = value; }
        }
        public int[] IdPagadores
        {
            get { return this._IdPagadores; }
            set { this._IdPagadores = value; }
        }

        public ReadOnlyDictionary<int, Cuota> Cuotas { get { return new ReadOnlyDictionary<int, Cuota>(this._Cuotas); } }
        public EntACtaDict EntregasACuenta { get { return this._EntregasACuenta; } }
        
        public string Notas { get; set; }
        #endregion

        #region helpers
        private decimal DeudaPorCuotasImpagadas()
        {
            decimal deuda = 0;

            foreach (KeyValuePair<int, Cuota> kvp in this.Cuotas)
            {
                deuda += kvp.Value.GetDeuda();
            }

            return deuda;
        }
        /// <summary>
        /// Ingresos hasta el día de la fecha
        /// </summary>
        /// <param name="fechaFinal"></param>
        /// <returns></returns>
        private decimal DeudaPorCuotasImpagadas(Date fechaFinal)
        {
            decimal deuda = 0;

            foreach (KeyValuePair<int, Cuota> kvp in this.Cuotas)
            {
                if(kvp.Value.Mes <= fechaFinal) deuda += kvp.Value.GetDeuda();
            }

            return deuda;
        }
        /// <summary>
        /// Ingresos hasta el día de la fecha
        /// </summary>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <returns></returns>
        private decimal DeudaPorCuotasImpagadas(Date fechaInicial, Date fechaFinal)
        {
            decimal deuda = 0;

            foreach (KeyValuePair<int, Cuota> kvp in this.Cuotas)
            {
                if (kvp.Value.Mes >= fechaInicial && kvp.Value.Mes <= fechaFinal)
                    deuda += kvp.Value.GetDeuda();
            }

            return deuda;
        }
        /// <summary>
        /// Ingresos hasta fechaIngresos
        /// </summary>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <param name="fechaIngresos"></param>
        /// <returns></returns>
        private decimal DeudaPorCuotasImpagadas(Date fechaInicial, Date fechaFinal, Date fechaIngresos)
        {
            decimal deuda = 0;

            foreach (KeyValuePair<int, Cuota> kvp in this.Cuotas)
            {
                if (kvp.Value.Mes >= fechaInicial && kvp.Value.Mes <= fechaFinal)
                    deuda += kvp.Value.GetDeuda(fechaIngresos);
            }

            return deuda;
        }
        private decimal TotalEntregasACuentaAFecha(Date fechaInicial, Date fechaFinal)
        {
            decimal total = 0;

            foreach (KeyValuePair<int, sEntACta> kvp in this.EntregasACuenta.GetEnumerable())
            {
                if (kvp.Value.Fecha <= fechaInicial && kvp.Value.Fecha >= fechaFinal)
                    total += kvp.Value.Importe;
            }

            return total;
        }
        #endregion

        #region public methods
        #region deuda methods
        public Dictionary<int,Cuota> GetCuotasImpagadas()
        {
            return this._Cuotas.Where(x => x.Value.GetDeuda() > 0) as Dictionary<int,Cuota>;
        }
        /// <summary>
        /// Ingresos hasta el día de la fecha
        /// </summary>
        /// <returns></returns>
        public decimal DeudaALaFecha()
        {
            return DeudaPorCuotasImpagadas() - this.EntregasACuenta.Total;
        }
        /// <summary>
        /// Ingresos hasta el día de la fecha
        /// </summary>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <returns></returns>
        public decimal DeudaALaFecha(Date fechaInicial, Date fechaFinal)
        {
            return DeudaPorCuotasImpagadas(fechaInicial, fechaFinal) - TotalEntregasACuentaAFecha(fechaInicial, fechaFinal);
        }
        /// <summary>
        /// Ingresos hasta final de ejercicio
        /// </summary>
        /// <param name="ejercicio"></param>
        /// <returns></returns>
        public decimal DeudaALaFecha(sEjercicio ejercicio)
        {
            return DeudaPorCuotasImpagadas(ejercicio.FechaComienzo, ejercicio.FechaFinal) - 
                TotalEntregasACuentaAFecha(ejercicio.FechaComienzo, ejercicio.FechaFinal);
        }
        /// <summary>
        /// Ingresos hasta fechaIngresos
        /// </summary>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <param name="fechaIngresos"></param>
        /// <returns></returns>
        public decimal DeudaALaFecha(Date fechaInicial, Date fechaFinal, Date fechaIngresos)
        {
            return DeudaPorCuotasImpagadas(fechaInicial, fechaFinal, fechaIngresos) - TotalEntregasACuentaAFecha(fechaInicial, fechaIngresos);
        }
        #endregion

        public bool TryRemoveCuota(int key)
        {
            if (!this._Cuotas.ContainsKey(key)) return false;

            this._Cuotas.Remove(key);
            return true;
        }
        public bool TryAddCuota(int key, ref Cuota cuota)
        {
            if (this._Cuotas.ContainsKey(key)) return false;

            this._Cuotas.Add(key, cuota);
            return true;
        }

        #region propietario methods
        #endregion
        #endregion
    }

}
