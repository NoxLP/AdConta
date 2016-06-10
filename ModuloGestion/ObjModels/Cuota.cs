using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdConta.Models;
using AdConta;

namespace ModuloGestion.ObjModels
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
        private int _OwnerIdFinca;
        private int _OwnerIdCdad;
        private int _OwnerIdPropietario;
        private sEjercicio _Ejercicio;

        private Concepto _Concepto;

        private Date _Mes;
        private Date _Caducidad;
        private decimal _ImporteTotal;

        private CobrosList _Cobros;
        #endregion

        #region properties
        public int Id { get { return this._Id; } }
        public int OwnerIdFinca { get { return this._OwnerIdFinca; } }
        public int OwnerIdCdad { get { return this._OwnerIdCdad; } }
        public int OwnerIdPropietario { get { return this._OwnerIdPropietario; } }
        public sEjercicio Ejercicio { get { return this._Ejercicio; } }

        public Concepto Concepto { get { return this._Concepto; } }
        
        public Date Mes { get { return this._Mes; } }        
        public Date Caducidad { get { return this._Caducidad; } }        
        public decimal ImporteTotal { get { return this._ImporteTotal; } }

        public CobrosList Cobros { get { return this._Cobros; } }
        #endregion

        #region helpers
        #endregion

        #region public methods
        public decimal GetDeuda()
        {
            return this.ImporteTotal - this.Cobros.Total;
        }
        public decimal GetDeuda(Date fechaIngresos)
        {
            decimal deuda = this.ImporteTotal;

            foreach(sCobro cobro in this.Cobros.GetEnumerable())
            {
                if (cobro.Fecha <= fechaIngresos) deuda -= cobro.Importe;
            }

            return deuda;
        }
        /*public decimal GetDeuda(Date fechaInicial, Date fechaIngresos)
        {
            decimal deuda = this.ImporteTotal;

            foreach (sCobro cobro in this.Cobros.GetEnumerable())
            {
                if (cobro.Fecha >= fechaInicial && cobro.Fecha <= fechaIngresos)
                    deuda -= cobro.Importe;
            }

            return deuda;
        }*/
        #endregion
    }

}
