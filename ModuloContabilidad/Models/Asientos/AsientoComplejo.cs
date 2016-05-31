﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using AdConta;

namespace ModuloContabilidad.Models.Asientos
{
    public class AsientoComplejo : aAsiento
    {
        public AsientoComplejo(int nAsiento)
        {
            base._NAsiento = nAsiento;
            this.Date = DateTime.Today;
            base._Apuntes = new ObservableCollection<Apunte>();
            this.Balance = 0;
            this.IsNew = true;
        }
        public AsientoComplejo(DateTime date, ObservableCollection<Apunte> apuntes, bool isNew)
        {
            if (apuntes.Count < 2)
            {
                System.Windows.MessageBox.Show("No se puede crear un asiento con menos de dos apuntes.");
                return;
            }
            else if (GetBalanceOfList(apuntes) != 0)
            {
                System.Windows.MessageBox.Show("El asiento que está intentando crear no está cuadrado.");
                return;
            }

            this.Date = date;
            base._Apuntes = apuntes;
            this.SetBalance();
            this.IsNew = isNew;
        }

        #region properties
        public override DateTime Date { get; set; }
        public override decimal Balance { get; protected set; }
        public override bool IsNew { get; set; }
        public override Apunte this[int i]
        {
            get { return base._Apuntes[i]; }
        }
        /*public IEnumerable Apuntes
        {
            get
            {
                foreach (Apunte ap in this._Apuntes)
                    yield return ap;
            }
        }*/
        public override ObservableCollection<Apunte> Apuntes
        {
            get { return base._Apuntes; }
        }
        #endregion

        #region helpers
        /// <summary>
        /// Get accounting balance of list apuntes.
        /// </summary>
        /// <param name="apuntes"></param>
        /// <returns></returns>
        protected override decimal GetBalanceOfList(ObservableCollection<Apunte> apuntes)
        {
            decimal sum = 0;
            int sign;
            foreach (Apunte ap in apuntes)
            {
                sign = (ap.DebeHaber == DebitCredit.Debit) ? 1 : -1;
                sum += (ap.Amount * sign);
            }

            return sum;
        }
        /// <summary>
        /// Modify this.Balance property to new accounting balance given that apunte have been changed.
        /// </summary>
        /// <param name="apunte"></param>
        /// <param name="oldamount"></param>
        public override void ChangeBalance(Apunte apunte, decimal oldamount)
        {
            int sign = (apunte.DebeHaber == DebitCredit.Debit) ? 1 : -1;
            this.Balance -= (oldamount * sign);
            this.Balance += (apunte.Amount * sign);
        }
        #endregion

        #region public methods

        #endregion
    }
}
