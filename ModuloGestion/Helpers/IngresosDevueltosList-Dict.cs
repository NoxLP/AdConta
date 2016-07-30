using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdConta;

namespace ModuloGestion.ObjModels
{
    public class IngresosDevueltosList : aProtectedList<Devolucion>
    {
        public IngresosDevueltosList()
        {

        }

        #region fields
        private decimal _Total;
        private decimal _TotalGastos;
        #endregion

        #region properties
        public decimal Total { get { return this._Total; } }
        public decimal TotalGastos { get { return this._TotalGastos; } }
        #endregion

        #region helpers
        #endregion

        #region public methods
        public override void Add(Devolucion item)
        {
            this._List.Add(item);
            this._Total += item.Importe;
            this._TotalGastos += item.Gastos;
        }
        public override void AddRange(IEnumerable<Devolucion> collection)
        {
            base.AddRange(collection);
        }
        #endregion
    }

}
