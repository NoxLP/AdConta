using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using AdConta;
using AdConta.Models;

namespace ModuloContabilidad.ObjModels
{
    public class Apunte : iObjModelBase, iOwnerComunidad
    {
        public Apunte() { }
        public Apunte(int id, int idComunidad, Asiento asiento, string FacturaId = null)
        {
            this._Id = id;
            this._IdOwnerComunidad = idComunidad;
            this._Asiento = asiento;
            this._Factura = FacturaId;
        }

        #region fields
        private int _Id;
        private int _IdOwnerComunidad;
        private int _OrdenEnAsiento;
        private Asiento _Asiento;
        private DebitCredit _DebeHaber;
        private decimal _Importe;
        private string _Factura;
        #endregion

        #region properties
        public int Id { get { return this._Id; } }        
        public int IdOwnerComunidad { get { return this._IdOwnerComunidad; } }
        public int OrdenEnAsiento { get { return this._OrdenEnAsiento; } }
        public Asiento Asiento { get { return this._Asiento; } }
        public string Concepto { get; set; }        
        public DebitCredit DebeHaber
        {
            get { return this._DebeHaber; }
            set
            {
                if (this._DebeHaber != value)
                {                    
                    if(!_Asiento.Abierto)
                        throw new CustomException_ObjModels(
                            $"Error cambiando DebeHaber de apunte numero {Id} de asiento numero {Asiento.Id}. Asiento cerrado.");
                    this._Asiento.CambiaSaldo(this, value);
                    this._DebeHaber = value;
                }
            }
        }        
        public decimal Importe
        {
            get { return this._Importe; }
            set
            {
                if (this._Importe != value)
                {
                    if (!_Asiento.Abierto)
                        throw new CustomException_ObjModels(
                            $"Error cambiando DebeHaber de apunte numero {Id} de asiento numero {Asiento.Id}. Asiento cerrado.");
                    this._Asiento.CambiaSaldo(this, value);
                    this._Importe = value;
                }
            }
        }
        public CuentaMayor Cuenta { get; set; }
        public bool Punteo { get; set; }        
        public string Factura { get { return this._Factura; } }
        #endregion
    }

    public class Asiento : iObjModelBase, iObjModelConCodigo, iOwnerComunidad
    {
        public Asiento(int id, int idComunidad)
        {
            this._Id = id;
            this._IdOwnerComunidad = idComunidad;
            _Abierto = false;
            FechaValor = DateTime.Today;
        }
        public Asiento(int id, int idComunidad, DateTime fechaValor)
        {
            this._Id = id;
            this._IdOwnerComunidad = idComunidad;
            _Abierto = false;
            FechaValor = FechaValor;
        }

        #region fields
        private int _Id;
        private int _IdOwnerComunidad;
        private decimal _Saldo;
        private bool _Abierto;
        #endregion

        #region properties
        public int Id { get { return this._Id; } }
        public int IdOwnerComunidad { get { return this._IdOwnerComunidad; } }
        public int Codigo { get; private set; }
        public ObservableApuntesList Apuntes { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaValor { get; private set; }
        public decimal Saldo { get { return this._Saldo; } }
        public Apunte this[int i] { get { return this.Apuntes[i]; } }     
        public bool Abierto { get { return this._Abierto; } }
        #endregion

        #region helpers
        /// <summary>
        /// Set this.Balance property as the accounting balance of the apuntes currently stored in this._Apuntes.
        /// </summary>
        public void CalculaSaldo()
        {
            _Saldo = Apuntes.SumaDebe - Apuntes.SumaHaber;
        }
        /// <summary>
        /// Get accounting balance of list apuntes.
        /// </summary>
        /// <param name="apuntes"></param>
        /// <returns></returns>
        public decimal GetSaldoDe(IEnumerable<Apunte> apuntes)
        {
            decimal sum = 0;
            int sign;
            foreach (Apunte ap in this.Apuntes)
            {
                sign = (ap.DebeHaber == DebitCredit.Debit) ? 1 : -1;
                sum += (ap.Importe * sign);
            }

            return sum;
        }
        #endregion

        #region public methods

        /// <summary>
        /// Modify this._Saldo property to new accounting balance given that apunte have not been changed yet.
        /// </summary>
        /// <param name="apunte"></param>
        /// <param name="oldamount"></param>
        public void CambiaSaldo(Apunte apunte, decimal nuevoImporte)
        {
            if (apunte.DebeHaber == DebitCredit.Debit) _Saldo = _Saldo - apunte.Importe + nuevoImporte;
            else _Saldo = _Saldo + apunte.Importe - nuevoImporte;
        }
        /// <summary>
        /// Modify this._Saldo property to new accounting balance given that apunte have not been changed yet.
        /// nuevoDebeHaber es diferente que apunte.DebeHaber.
        /// </summary>
        /// <param name="apunte"></param>
        /// <param name="oldamount"></param>
        public void CambiaSaldo(Apunte apunte, DebitCredit nuevoDebeHaber)
        {
            if (apunte.DebeHaber == DebitCredit.Debit) _Saldo = _Saldo - (apunte.Importe*2);
            else _Saldo = _Saldo + (apunte.Importe*2);
        }
        /// <summary>
        /// Get all apuntes on specified debit/credit.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public List<Apunte> GetApuntesAl(DebitCredit target)
        {
            List<Apunte> result = new List<Apunte>(this.Apuntes.ToList().FindAll(x => x.DebeHaber == target));
            return result;
        }
        /// <summary>
        /// Add apunte and returns if sum=0. Devuelve true si el asiento queda cuadrado después de añadir apunte.
        /// </summary>
        /// <param name="apunte"></param>
        /// <returns></returns>
        public bool AddApunte(Apunte apunte)
        {
            if (!Abierto) return false;

            CalculaSaldo();
            return true;
        }
        /// <summary>
        /// Remove apunte and returns if sum=0. Devuelve true si el asiento queda cuadrado después de borrar apunte.
        /// </summary>
        /// <param name="apunte"></param>
        /// <returns></returns>
        public bool RemoveApunte(Apunte apunte)
        {
            if (!Abierto) return false;

            CalculaSaldo();
            return true;
        }
        public bool ReplaceApunte(Apunte oldApunte, Apunte newApunte)
        {
            if (!Abierto) return false;

            if (oldApunte.DebeHaber == DebitCredit.Debit) _Saldo -= oldApunte.Importe;
            else _Saldo += oldApunte.Importe;

            if (newApunte.DebeHaber == DebitCredit.Debit) _Saldo += newApunte.Importe;
            else _Saldo -= newApunte.Importe;

            return true;
        }
        /// <summary>
        /// Si está abierto el asiento puede estar descuadrado (saldo != 0) y se puede modificar.
        /// Si intenta cerrarse con saldo descuadrado, devuelve false y no cierra.
        /// Si intenta cerrarse sin fecha (this.Fecha == null), devuelve false y no cierra.
        /// Si ya estaba cerrado, devuelve verdadero.
        /// El asiento no se puede modificar si no está abierto.
        /// </summary>
        /// <returns></returns>
        public bool TryCerrar()
        {
            if (!Abierto) return true;
            if (Saldo != 0) return false;
            if (Fecha == null) return false;

            _Abierto = false;
            return true;
        }

        public bool TrySetCodigo(int codigo, ref List<int> codigos)
        {
            return false;
        }
        #endregion
    }

    #region old
    //public class Apunte : iObjModelBase, iOwnerComunidad
    //{
    //    //public Apunte() { }
    //    public Apunte(aAsiento asiento)
    //    {
    //        this._Asiento = asiento;
    //    }

    //    #region fields
    //    private int _Id;
    //    private int _IdOwnerComunidad;
    //    private decimal _Importe;
    //    private DebitCredit _DebeHaber;
    //    private aAsiento _Asiento;
    //    #endregion

    //    #region properties
    //    public int Id { get { return this._Id; } }
    //    public int IdOwnerComunidad { get { return this._IdOwnerComunidad; } }
    //    public CuentaMayor Account { get; set; }
    //    public decimal Importe
    //    {
    //        get { return this._Importe; }
    //        set
    //        {
    //            if (this._Importe != value)
    //            {
    //                decimal OldAmount = this._Importe;
    //                this._Importe = value;
    //                this._Asiento.CambiaSaldo(this, OldAmount);
    //            }
    //        }
    //    }
    //    public DebitCredit DebeHaber
    //    {
    //        get { return this._DebeHaber; }
    //        set
    //        {
    //            if (this._DebeHaber != value)
    //            {
    //                this._DebeHaber = value;
    //                this._Asiento.CambiaSaldo(this, this._Importe);
    //            }
    //        }
    //    }
    //    public string Concepto { get; set; }
    //    public bool Punteado { get; set; }
    //    //TODO definir bien los recibos y facturas
    //    public string Recibo { get; set; }
    //    public string Factura { get; set; }
    //    #endregion

    //    /*#region helpers
    //    public void SetAsiento(Asiento asiento)
    //    {
    //        this._Asiento = asiento;
    //    }
    //    #endregion*/
    //}

    //public abstract class aAsiento : iObjModelBase, iOwnerComunidad
    //{
    //    #region fields
    //    protected ObservableCollection<Apunte> _Apuntes;
    //    protected int _Id;
    //    private int _IdOwnerComunidad;
    //    #endregion

    //    #region properties
    //    public int Id { get { return this._Id; } }
    //    public int IdOwnerComunidad { get { return this._IdOwnerComunidad; } }
    //    public abstract DateTime Fecha { get; set; }
    //    public abstract decimal Balance { get; protected set; }
    //    public abstract bool IsNew { get; set; }
    //    public abstract Apunte this[int i] { get; }
    //    public virtual ObservableCollection<Apunte> Apuntes { get; }
    //    #endregion

    //    #region helpers
    //    /// <summary>
    //    /// Get accounting balance of list apuntes.
    //    /// </summary>
    //    /// <param name="apuntes"></param>
    //    /// <returns></returns>
    //    protected abstract decimal GetSaldoDe(ObservableCollection<Apunte> apuntes);
    //    /// <summary>
    //    /// Set this.Balance property as the accounting balance of the apuntes stored in property this._Apuntes.
    //    /// </summary>
    //    protected virtual void SetSaldo()
    //    {
    //        decimal sum = 0;
    //        int sign;
    //        foreach (Apunte ap in this._Apuntes)
    //        {
    //            sign = (ap.DebeHaber == DebitCredit.Debit) ? 1 : -1;
    //            sum += (ap.Importe * sign);
    //        }

    //        this.Balance = sum;
    //    }
    //    /// <summary>
    //    /// Modify this.Balance property to new accounting balance given the new apunte had been effectively added to the property this._Apuntes.
    //    /// </summary>
    //    /// <param name="apunte"></param>
    //    public virtual void SetSaldo(Apunte apunte)
    //    {
    //        int sign = (apunte.DebeHaber == DebitCredit.Debit) ? 1 : -1;
    //        this.Balance += (apunte.Importe * sign);
    //    }
    //    /// <summary>
    //    /// Modify this.Balance property to new accounting balance given that apunte have been changed.
    //    /// </summary>
    //    /// <param name="apunte"></param>
    //    /// <param name="oldamount"></param>
    //    public abstract void CambiaSaldo(Apunte apunte, decimal oldamount);
    //    #endregion

    //    #region public methods
    //    /// <summary>
    //    /// Get all apuntes on debit/credit.
    //    /// </summary>
    //    /// <param name="target"></param>
    //    /// <returns></returns>
    //    public virtual List<Apunte> GetApuntesAl(DebitCredit target)
    //    {
    //        return this._Apuntes.ToList<Apunte>().FindAll(x => x.DebeHaber == target);
    //    }
    //    /// <summary>
    //    /// Add apunte and returns if sum=0. Devuelve true si el asiento queda cuadrado después de añadir apunte.
    //    /// </summary>
    //    /// <param name="apunte"></param>
    //    /// <returns></returns>
    //    public virtual bool AddApunte(Apunte apunte)
    //    {
    //        this._Apuntes.Add(apunte);
    //        this.SetSaldo(apunte);
    //        return this.Balance == 0;
    //    }
    //    /// <summary>
    //    /// Remove apunte and returns if sum=0. Devuelve true si el asiento queda cuadrado después de borrar apunte.
    //    /// </summary>
    //    /// <param name="apunte"></param>
    //    /// <returns></returns>
    //    public virtual bool RemoveApunte(Apunte apunte)
    //    {
    //        this._Apuntes.Remove(apunte);
    //        this.SetSaldo(apunte);
    //        return this.Balance == 0;
    //    }
    //    /*/// <summary>
    //    /// Set amount of apunte.
    //    /// </summary>
    //    /// <param name="apunte"></param>
    //    /// <param name="amount"></param>
    //    /// <returns></returns>
    //    public bool SetApunteAmount(Apunte apunte, decimal amount)
    //    {
    //        Apunte ap = this._Apuntes.Find(x => x == apunte);
    //        ap.Amount = amount;
    //        return this.Balance == 0;
    //    }
    //    /// <summary>
    //    /// Set amount of apunte.
    //    /// </summary>
    //    /// <param name="index"></param>
    //    /// <param name="amount"></param>
    //    /// <returns></returns>
    //    public bool SetApunteAmount(int index, decimal amount)
    //    {
    //        Apunte ap = this._Apuntes[index];
    //        ap.Amount = amount;
    //        this.SetBalance(ap);
    //        return this.Balance == 0;
    //    }*/
    //    /// <summary>
    //    /// Get index of apunte.
    //    /// </summary>
    //    /// <param name="apunte"></param>
    //    /// <returns></returns>
    //    public virtual int GetIndexOfApunte(Apunte apunte)
    //    {
    //        return this._Apuntes.IndexOf(apunte);
    //    }
    //    #endregion
    //}
    #endregion
}
