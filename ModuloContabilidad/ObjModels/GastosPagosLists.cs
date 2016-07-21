using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdConta;

namespace ModuloContabilidad.ObjModels
{
    public class GastosList : aProtectedList<Gasto>
    {
        public GastosList()
        {

        }

        #region fields
        private decimal _Total;
        #endregion

        #region properties
        public decimal Total { get { return this._Total; } }
        #endregion

        #region helpers
        #endregion

        #region public methods
        public override void Add(Gasto item)
        {
            this._List.Add(item);
            this._Total += item.ImporteTotal;
        }
        public override void RemoveAt(int index)
        {
            if(index < 0 || index > this.Count)
                throw new IndexOutOfRangeException();

            this._Total -= this[index].ImporteTotal;
            this._List.RemoveAt(index);
        }
        public override void Clear()
        {
            this._Total = 0;
            this._List.Clear();
        }
        public override void AddRange(IEnumerable<Gasto> collection)
        {
            base._List.AddRange(collection);

            foreach (Gasto gasto in collection)
            {
                this._Total += gasto.ImporteTotal;
            }
        }
        public override void RemoveRange(int index, int count)
        {
            if (index < 0 || index > this.Count || (index + count) > this.Count)
                throw new IndexOutOfRangeException();

            for (int i = index; i < count; i++)
            {
                this._Total -= this[i].ImporteTotal;
            }

            base._List.RemoveRange(index, count);
        }
        #endregion
    }

}
