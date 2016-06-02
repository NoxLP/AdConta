using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdConta.Models;
using AdConta;

namespace ModuloGestion.Models
{
    public class Cuota
    {
        public Cuota()
        {
            this._Cobros = new List<sCobro>();
        }

        #region fields
        private int _Id;
        private int _IdFinca;
        private int _IdCdad;
        private sEjercicio _Ejercicio;

        private sConcepto _Concepto;

        private int _Mes;
        private Date _Caducidad;
        private decimal _Importe;

        private List<sCobro> _Cobros;
        #endregion

        #region properties
        public int Id { get { return this._Id; } }
        public int IdFinca { get { return this._IdFinca; } }
        public int IdCdad { get { return this._IdCdad; } }
        public sEjercicio Ejercicio { get { return this._Ejercicio; } }

        public sConcepto Concepto { get { return this._Concepto; } }
        
        public int Mes { get { return this._Mes; } }        
        public Date Caducidad { get { return this._Caducidad; } }        
        public decimal Importe { get { return this._Importe; } }
        #endregion

        #region helpers
        #endregion

        #region public methods

        #endregion
    }

}
