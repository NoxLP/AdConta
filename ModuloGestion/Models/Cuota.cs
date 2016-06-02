using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdConta.Models;
using AdConta;
using ModuloGestion.Helpers;

namespace ModuloGestion.Models
{
    public class Cuota
    {
        public Cuota()
        {
            this._Caducidad = new Date();
            this._Cobros = new CobrosList();
        }

        #region fields
        private int _Id;
        private int _IdFinca;
        private int _IdCdad;
        private sEjercicio _Ejercicio;

        private Concepto _Concepto;

        private int _Mes;
        private Date _Caducidad;
        private decimal _ImporteTotal;

        private CobrosList _Cobros;
        #endregion

        #region properties
        public int Id { get { return this._Id; } }
        public int IdFinca { get { return this._IdFinca; } }
        public int IdCdad { get { return this._IdCdad; } }
        public sEjercicio Ejercicio { get { return this._Ejercicio; } }

        public Concepto Concepto { get { return this._Concepto; } }
        
        public int Mes { get { return this._Mes; } }        
        public Date Caducidad { get { return this._Caducidad; } }        
        public decimal ImporteTotal { get { return this._ImporteTotal; } }

        public CobrosList Cobros { get { return this._Cobros; } }
        #endregion

        #region helpers
        #endregion

        #region public methods
        #endregion
    }

}
