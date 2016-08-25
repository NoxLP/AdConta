using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdConta;
using AdConta.Models;

namespace ModuloGestion.ObjModels
{
    public class IngresoDevuelto : iOwnerDevolucion
    {
        private int _IdOwnerDevolucion;
        public int IdOwnerDevolucion { get { return this._IdOwnerDevolucion; } }


        public int IdOwnerDevolucion { get; private set; }
        public Date Fecha { get; private set; }
        public iIngresoPropietario Devuelto { get; private set; }
        public bool Total { get; private set; }
        public decimal Importe { get; private set; }
        public decimal Gastos { get; private set; }
    }

    public class Devolucion : iObjModelBase, iOwnerComunidad
    {
        private Devolucion() { }
        public Devolucion(int id, int idComunidad, Date fecha, List<IngresoDevuelto> devoluciones)
        {
            this._Id = id;
            this._IdOwnerComunidad = idComunidad;
            this._Fecha = fecha;
            this._IngresosDevueltos = devoluciones;

            foreach(IngresoDevuelto ingreso in devoluciones)
            {
                this._ImporteTotal += ingreso.Importe;
                this._GastosTotal += ingreso.Gastos;
            }
        }

        #region fields
        private int _Id;
        private int _IdOwnerComunidad;

        private Date _Fecha;
        
        private decimal _ImporteTotal = 0;
        private decimal _GastosTotal = 0;
        private List<IngresoDevuelto> _IngresosDevueltos;
        #endregion

        #region properties
        public int Id { get { return this._Id; } }
        public int IdOwnerComunidad { get { return this._IdOwnerComunidad; } }

        public Date Fecha { get { return this._Fecha; } }
        
        public decimal ImporteTotal { get { return this._ImporteTotal; } }
        public decimal GastosTotal { get { return this._GastosTotal; } }
        public ReadOnlyCollection<IngresoDevuelto> IngresosDevueltos { get { return this._IngresosDevueltos.AsReadOnly(); } }
        #endregion
    }
}
