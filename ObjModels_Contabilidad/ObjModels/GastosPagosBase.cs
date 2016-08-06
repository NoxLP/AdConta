using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdConta;
using AdConta.Models;

namespace ModuloContabilidad.ObjModels
{
    public class GastosPagosBase : iObjModelBase, iOwnerComunidad
    {
        public GastosPagosBase(int id, int idComunidad, Date fecha)
        {
            this._Id = id;
            this._Fecha = fecha;
            this._IdOwnerComunidad = idComunidad;
        }

        /*public GastosPagosBase(
            int id,
            List<sImporteCuenta> cuentasAcreedoras,
            List<sImporteCuenta> cuentasDeudoras,
            Date fecha,
            string concepto,
            decimal importe)
        {
            this._Id = id;

            this._CuentasAcreedoras = cuentasAcreedoras;
            this._CuentasDeudoras = cuentasDeudoras;
            this._Fecha = fecha;
            this.Concepto = concepto;
            this._ImporteTotal = importe;
            this.CalculateCurrentTotal();
        }*/

        #region fields
        private int _Id;
        private int _IdOwnerComunidad;

        private List<sImporteCuenta> _CuentasAcreedoras;
        private List<sImporteCuenta> _CuentasDeudoras;

        private Date _Fecha;
        private decimal _Importe;
        #endregion

        #region properties
        public int Id { get { return this._Id; } }
        public int IdOwnerComunidad { get { return this._IdOwnerComunidad; } }

        public ReadOnlyCollection<sImporteCuenta> CuentasAcreedoras
        {
            get { return new ReadOnlyCollection<sImporteCuenta>(this._CuentasAcreedoras.AsReadOnly()); }
        }
        public ReadOnlyCollection<sImporteCuenta> CuentasDeudoras
        {
            get { return new ReadOnlyCollection<sImporteCuenta>(this._CuentasDeudoras.AsReadOnly()); }
        }

        public Date Fecha { get { return this._Fecha; } }
        public string Concepto { get; set; }
        public decimal Importe { get { return this._Importe; } }
        #endregion

        #region helpers
        private bool CalculateTotal(ref List<sImporteCuenta> deudoras, ref List<sImporteCuenta> acreedoras)
        {
            decimal SaldoDeudor = 0;
            decimal SaldoAcreedor = 0;
            foreach (sImporteCuenta gc in deudoras)
            {
                SaldoDeudor += gc.Importe;
            }
            foreach (sImporteCuenta gc in acreedoras)
            {
                SaldoAcreedor += gc.Importe;
            }

            if (SaldoDeudor != SaldoAcreedor) return false;

            this._Importe = SaldoDeudor;
            return true;
        }
        #endregion

        #region public methods
        public bool SetCuentasImportes(List<sImporteCuenta> deudores, List<sImporteCuenta> acreedores)
        {
            if (!this.CalculateTotal(ref deudores, ref acreedores)) return false;

            this._CuentasDeudoras = deudores;
            this._CuentasAcreedoras = acreedores;
            return true;
        }

        /*public bool AddCuentaDeudora(sImporteCuenta gc)
        {
            if (this.CuentasDeudoras.Contains(gc)) return false;

            this._CuentasDeudoras.Add(gc);
            this._ImporteTotal += gc.Importe;
            return true;
        }
        public bool AddCuentaAcreedora(sImporteCuenta gc)
        {
            if (this.CuentasAcreedoras.Contains(gc)) return false;

            this._CuentasAcreedoras.Add(gc);
            //this._ImporteTotal -= gc.Importe;
            return true;
        }
        public bool SetCuentasDeudoras(List<sImporteCuenta> gcList)
        {
            //foreach (sImporteCuenta gc in gcList) if (this.CuentasDeudoras.Contains(gc)) return false;

            if (this.CalculateTotal(ref gcList, ref this._CuentasAcreedoras))
            {
                this._CuentasDeudoras = gcList;
                return true;
            }

            return false;
        }
        public bool SetCuentasAcreedoras(List<sImporteCuenta> gcList)
        {
            //foreach (sImporteCuenta gc in gcList) if (this.CuentasAcreedoras.Contains(gc)) return false;

            if (this.CalculateTotal(ref this._CuentasDeudoras, ref gcList))
            {
                this._CuentasAcreedoras = gcList;
                return true;
            }

            return false;
        }
        /*public bool RemoveCuentaDeudora(sImporteCuenta gc)
        {
            if (!this.CuentasDeudoras.Contains(gc)) return false;

            int index = this.CuentasDeudoras.IndexOf(gc);
            this._CuentasDeudoras.RemoveAt(index);
            this._ImporteTotal -= gc.Importe;
            return true;
        }
        public bool RemoveCuentaAcreedora(sImporteCuenta gc)
        {
            if (!this.CuentasAcreedoras.Contains(gc)) return false;

            int index = this.CuentasAcreedoras.IndexOf(gc);
            this._CuentasAcreedoras.RemoveAt(index);
            //this.CalculateCurrentTotal();
            return true;
        }*/
        #endregion

    }

}
