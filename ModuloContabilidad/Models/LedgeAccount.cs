using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ModuloContabilidad.Models
{
    /// <summary>
    /// Class for ledge account
    /// </summary>
    public class LedgeAccount
    {
        public LedgeAccount(string accountNumber)
        {
            this.Codigo = accountNumber;
            this.IsFakeAccount = false;
        }

        #region fields
        private int _iCodigo;
        private string _Codigo;
        private string _Nombre;
        #endregion

        #region properties
        public int iCodigo
        {
            get { return _iCodigo; }
            set
            {
                if (value == this._iCodigo ||
                    value < GlobalSettings.Properties.Settings.Default.MINCODCUENTAS ||
                    value > GlobalSettings.Properties.Settings.Default.MAXCODCUENTAS)
                    return;
                
                this._iCodigo = value;
                //Get total default digits
                int digits = GlobalSettings.Properties.Settings.Default.DIGITOSCUENTAS - 1;
                //Get first digit
                this.Grupo = (int)Math.Truncate(value / Math.Pow(10, digits)) * 100;
                //Get second and third digit
                this.SubGrupo = (int)Math.Truncate(value / Math.Pow(10, digits - 2)) % 100;
                //Get the rest of the digits as a whole number
                this.Sufijo = value - (int)Math.Truncate((this.Grupo + this.SubGrupo) * Math.Pow(10, digits - 2));
            }
        }
        public string Codigo
        {
            get { return _Codigo; }
            set
            {
                if (value == this._Codigo) return;

                int account;

                if (value.Length > GlobalSettings.Properties.Settings.Default.DIGITOSCUENTAS ||
                    !int.TryParse(value, out account) ||
                    value.Substring(0, 1) == "0")
                {
                    MessageBox.Show("Número de cuenta contable incorrecto");
                    return;
                }

                this._Codigo = value;
                this.Grupo = int.Parse(value.Substring(0, 1)) * 100;
                this.SubGrupo = int.Parse(value.Substring(1, 2));
                this.Sufijo = int.Parse(value.Substring(3));

                int sufDigits = GlobalSettings.Properties.Settings.Default.DIGITOSCUENTAS - 3;
                sufDigits = (int)Math.Truncate(Math.Pow(10, sufDigits));
                this._iCodigo = (this.Grupo + this.SubGrupo) * sufDigits + this.Sufijo;
            }
        }
        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }
        public int Grupo { get; set; }
        public int SubGrupo { get; set; }
        public int Sufijo { get; set; }
        public bool IsFakeAccount { get; set; }
        #endregion

        #region helpers
        public bool IsLastAccount()
        {
            
            return this.iCodigo == GlobalSettings.Properties.Settings.Default.MAXCODCUENTAS;
        }
        public bool IsFirstAccount()
        {
            return this.iCodigo == GlobalSettings.Properties.Settings.Default.MINCODCUENTAS;
        }
        #endregion
    }
}
