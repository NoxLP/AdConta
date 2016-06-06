using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdConta;

namespace ModuloGestion.Models
{
    /// <summary>
    /// Simplemente lleva la contabilidad del total de los cobros según se añaden o borran, 
    /// y no permite añadir ni borrar sin llevar esa contabilidad
    /// </summary>
    public class CobrosList : aProtectedStructList<sCobro>
    {
        #region fields
        private decimal _Total;
        #endregion

        #region properties
        public decimal Total { get { return this._Total; } }
        #endregion

        #region public methods
        public override void Add(sCobro item)
        {
            base._List.Add(item);
            this._Total += item.Importe;
        }
        public override void AddRange(IEnumerable<sCobro> collection)
        {
            base._List.AddRange(collection);

            foreach(sCobro cobro in collection)
            {
                this._Total += cobro.Importe;
            }
        }
        public override void RemoveAt(int index)
        {
            this._Total -= this[index].Importe;
            base._List.RemoveAt(index);
        }
        public override void RemoveRange(int index, int count)
        {
            for(int i = index; i< count;i++)
            {
                this._Total -= this[i].Importe;
            }

            base._List.RemoveRange(index, count);
        }
        public override void Clear()
        {
            this._Total = 0;
            base._List.Clear();
        }
        #endregion
    }

    /// <summary>
    /// Simplemente lleva la contabilidad del total de las entregas a cuenta según se añaden o borran, 
    /// y no permite añadir ni borrar sin llevar esa contabilidad
    /// </summary>
    public class EntACtaList : aProtectedStructList<sEntACta>
    {
        #region fields
        private decimal _Total;
        #endregion

        #region properties
        public decimal Total { get { return this._Total; } }
        #endregion

        #region public methods
        public override void Add(sEntACta item)
        {
            base._List.Add(item);
            this._Total += item.Importe;
        }
        public override void AddRange(IEnumerable<sEntACta> collection)
        {
            base._List.AddRange(collection);

            foreach (sEntACta cobro in collection)
            {
                this._Total += cobro.Importe;
            }
        }
        public override void RemoveAt(int index)
        {
            this._Total -= this[index].Importe;
            base._List.RemoveAt(index);
        }
        public override void RemoveRange(int index, int count)
        {
            for (int i = index; i < count; i++)
            {
                this._Total -= this[i].Importe;
            }

            base._List.RemoveRange(index, count);
        }
        public override void Clear()
        {
            this._Total = 0;
            base._List.Clear();
        }
        #endregion
    }

    /// <summary>
    /// Simplemente lleva la contabilidad del total de los cobros según se añaden o borran, 
    /// y no permite añadir ni borrar sin llevar esa contabilidad
    /// </summary>
    public class CobrosDict : aProtectedStructDict<int, sCobro>
    {
        #region fields
        private decimal _Total;
        #endregion

        #region properties
        public decimal Total { get { return this._Total; } }
        #endregion

        #region public methods
        public override void Add(int key, sCobro item)
        {
            base._Dict.Add(key, item);
            this._Total += item.Importe;
        }
        public override void Remove(int key)
        {
            this._Total -= this[key].Importe;
            base._Dict.Remove(key);
        }
        public override void Clear()
        {
            this._Total = 0;
            base._Dict.Clear();
        }
        #endregion
    }

    /// <summary>
    /// Simplemente lleva la contabilidad del total de las entregas a cuenta según se añaden o borran, 
    /// y no permite añadir ni borrar sin llevar esa contabilidad
    /// </summary>
    public class EntACtaDict : aProtectedStructDict<int, sEntACta>
    {
        #region fields
        private decimal _Total;
        #endregion

        #region properties
        public decimal Total { get { return this._Total; } }
        #endregion

        #region public methods
        public override void Add(int key, sEntACta item)
        {
            base._Dict.Add(key, item);
            this._Total += item.Importe;
        }
        public override void Remove(int key)
        {
            this._Total -= this[key].Importe;
            base._Dict.Remove(key);
        }
        public override void Clear()
        {
            this._Total = 0;
            base._Dict.Clear();
        }
        #endregion
    }
}
