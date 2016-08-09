using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using AdConta;

namespace ModuloContabilidad.ObjModels
{
    public class Apunte : AdConta.Models.iObjModelBase
    {
        //public Apunte() { }
        public Apunte(aAsiento asiento)
        {
            this._Asiento = asiento;
        }

        #region fields
        private decimal _Amount;
        private DebitCredit _DebeHaber;
        private aAsiento _Asiento;
        #endregion

        #region properties
        public int Id { get; set; }
        public CuentaMayor Account { get; set; }
        public decimal Amount
        {
            get { return this._Amount; }
            set
            {
                if (this._Amount != value)
                {
                    decimal OldAmount = this._Amount;
                    this._Amount = value;
                    this._Asiento.ChangeBalance(this, OldAmount);
                }
            }
        }
        public DebitCredit DebeHaber
        {
            get { return this._DebeHaber; }
            set
            {
                if (this._DebeHaber != value)
                {
                    this._DebeHaber = value;
                    this._Asiento.ChangeBalance(this, this._Amount);
                }
            }
        }
        public string Concepto { get; set; }
        public bool Punteado { get; set; }
        //TODO definir bien los recibos y facturas
        public string Recibo { get; set; }
        public string Factura { get; set; }
        #endregion

        /*#region helpers
        public void SetAsiento(Asiento asiento)
        {
            this._Asiento = asiento;
        }
        #endregion*/
    }

    public abstract class aAsiento
    {
        #region fields
        protected ObservableCollection<Apunte> _Apuntes;
        protected int _Id;
        #endregion

        #region properties
        public int Id { get { return this._Id; } }
        public abstract DateTime Date { get; set; }
        public abstract decimal Balance { get; protected set; }
        public abstract bool IsNew { get; set; }
        public abstract Apunte this[int i] { get; }
        public virtual ObservableCollection<Apunte> Apuntes { get; }
        #endregion

        #region helpers
        /// <summary>
        /// Get accounting balance of list apuntes.
        /// </summary>
        /// <param name="apuntes"></param>
        /// <returns></returns>
        protected abstract decimal GetBalanceOfList(ObservableCollection<Apunte> apuntes);
        /// <summary>
        /// Set this.Balance property as the accounting balance of the apuntes stored in property this._Apuntes.
        /// </summary>
        protected virtual void SetBalance()
        {
            decimal sum = 0;
            int sign;
            foreach (Apunte ap in this._Apuntes)
            {
                sign = (ap.DebeHaber == DebitCredit.Debit) ? 1 : -1;
                sum += (ap.Amount * sign);
            }

            this.Balance = sum;
        }
        /// <summary>
        /// Modify this.Balance property to new accounting balance given the new apunte had been effectively added to the property this._Apuntes.
        /// </summary>
        /// <param name="apunte"></param>
        public virtual void SetBalance(Apunte apunte)
        {
            int sign = (apunte.DebeHaber == DebitCredit.Debit) ? 1 : -1;
            this.Balance += (apunte.Amount * sign);
        }
        /// <summary>
        /// Modify this.Balance property to new accounting balance given that apunte have been changed.
        /// </summary>
        /// <param name="apunte"></param>
        /// <param name="oldamount"></param>
        public abstract void ChangeBalance(Apunte apunte, decimal oldamount);
        #endregion

        #region public methods
        /// <summary>
        /// Get all apuntes on debit/credit.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public virtual List<Apunte> GetApuntesAl(DebitCredit target)
        {
            return this._Apuntes.ToList<Apunte>().FindAll(x => x.DebeHaber == target);
        }
        /// <summary>
        /// Add apunte and returns if sum=0. Devuelve true si el asiento queda cuadrado después de añadir apunte.
        /// </summary>
        /// <param name="apunte"></param>
        /// <returns></returns>
        public virtual bool AddApunte(Apunte apunte)
        {
            this._Apuntes.Add(apunte);
            this.SetBalance(apunte);
            return this.Balance == 0;
        }
        /// <summary>
        /// Remove apunte and returns if sum=0. Devuelve true si el asiento queda cuadrado después de borrar apunte.
        /// </summary>
        /// <param name="apunte"></param>
        /// <returns></returns>
        public virtual bool RemoveApunte(Apunte apunte)
        {
            this._Apuntes.Remove(apunte);
            this.SetBalance(apunte);
            return this.Balance == 0;
        }
        /*/// <summary>
        /// Set amount of apunte.
        /// </summary>
        /// <param name="apunte"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public bool SetApunteAmount(Apunte apunte, decimal amount)
        {
            Apunte ap = this._Apuntes.Find(x => x == apunte);
            ap.Amount = amount;
            return this.Balance == 0;
        }
        /// <summary>
        /// Set amount of apunte.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public bool SetApunteAmount(int index, decimal amount)
        {
            Apunte ap = this._Apuntes[index];
            ap.Amount = amount;
            this.SetBalance(ap);
            return this.Balance == 0;
        }*/
        /// <summary>
        /// Get index of apunte.
        /// </summary>
        /// <param name="apunte"></param>
        /// <returns></returns>
        public virtual int GetIndexOfApunte(Apunte apunte)
        {
            return this._Apuntes.IndexOf(apunte);
        }
        #endregion
    }
}
