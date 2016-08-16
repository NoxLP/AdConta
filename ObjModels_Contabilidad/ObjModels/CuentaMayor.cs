using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ModuloContabilidad.ObjModels
{
    public struct GrupoContable
    {
        public int Digits { get; private set; }

        public GrupoContable(string accountNumber)
        {
            this.Digits = int.Parse(accountNumber.Substring(0, 1)) * 100;
        }
        public GrupoContable(int accountNumber)
        {
            //Get total default digits
            int digits = GlobalSettings.Properties.Settings.Default.DIGITOSCUENTAS - 1;
            //Get first digit
            this.Digits = (int)Math.Truncate(accountNumber / Math.Pow(10, digits)) * 100;
        }

        public void SetGrupoByAccNumber(string accountNumber)
        {
            this.Digits = int.Parse(accountNumber.Substring(0, 1)) * 100;
        }
        public void SetGrupoByAccNumber(int accountNumber)
        {
            //Get total default digits
            int digits = GlobalSettings.Properties.Settings.Default.DIGITOSCUENTAS - 1;
            //Get first digit
            this.Digits = (int)Math.Truncate(accountNumber / Math.Pow(10, digits)) * 100;
        }
        
        /// <summary>
        /// Carefull, this method don't check if the string provided is a correct ledge account number
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int GetGrupoDigitsFromString(ref string s)
        {
            return int.Parse(s.Substring(0, 1)) * 100;
        }
    }
    public struct SubgrupoContable
    {
        public int Digits { get; private set; }

        public SubgrupoContable(string accountNumber)
        {
            this.Digits = int.Parse(accountNumber.Substring(1, 2));
        }
        public SubgrupoContable(int accountNumber)
        {
            //Get total default digits
            int digits = GlobalSettings.Properties.Settings.Default.DIGITOSCUENTAS - 1;
            //Get second and third digit
            this.Digits = (int)Math.Truncate(accountNumber / Math.Pow(10, digits - 2)) % 100;
        }

        public void SetSubgrupoByAccNumber(string accountNumber)
        {
            this.Digits = int.Parse(accountNumber.Substring(1, 2));
        }
        public void SetSubgrupoByAccNumber(int accountNumber)
        {
            //Get total default digits
            int digits = GlobalSettings.Properties.Settings.Default.DIGITOSCUENTAS - 1;
            //Get second and third digit
            this.Digits = (int)Math.Truncate(accountNumber / Math.Pow(10, digits - 2)) % 100;
        }

        /// <summary>
        /// Carefull, this method don't check if the string provided is a correct ledge account number
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int GetSubgrupoDigitsFromString(ref string s)
        {
            return int.Parse(s.Substring(1, 2));
        }
    }

    /// <summary>
    /// Class for ledge account
    /// </summary>
    public class CuentaMayor : AdConta.Models.iObjModelBase
    {
        public CuentaMayor(string accountNumber)
        {
            this.Codigo = accountNumber;
            this.IsFakeAccount = false;
        }

        #region fields
        private int _Id;
        private string _Codigo;
        private GrupoContable _Grupo;
        private SubgrupoContable _Subgrupo;
        private int _Sufijo;
        #endregion

        #region properties
        public int Id
        {
            get { return _Id; }
            set
            {
                if (value == this._Id ||
                    value < GlobalSettings.Properties.Settings.Default.MINCODCUENTAS ||
                    value > GlobalSettings.Properties.Settings.Default.MAXCODCUENTAS)
                    return;
                
                this._Id = value;
                this._Grupo = new GrupoContable();
                this._Subgrupo = new SubgrupoContable();
                this._Grupo.SetGrupoByAccNumber(value);
                this._Subgrupo.SetSubgrupoByAccNumber(value);
                //Get total default digits
                int digits = GlobalSettings.Properties.Settings.Default.DIGITOSCUENTAS - 1;
                //Get the rest of the digits as a whole number
                this._Sufijo = value - (int)Math.Truncate((this.Grupo + this.Subgrupo) * Math.Pow(10, digits - 2));
                /*
                //Get first digit
                this._Grupo = (int)Math.Truncate(value / Math.Pow(10, digits)) * 100;
                //Get second and third digit
                this._SubGrupo = (int)Math.Truncate(value / Math.Pow(10, digits - 2)) % 100;
                */
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
                this._Grupo = new GrupoContable();
                this._Subgrupo = new SubgrupoContable();
                this._Grupo.SetGrupoByAccNumber(value);
                this._Subgrupo.SetSubgrupoByAccNumber(value);
                /*
                this._Grupo = int.Parse(value.Substring(0, 1)) * 100;
                this._SubGrupo = int.Parse(value.Substring(1, 2));*/
                this._Sufijo = int.Parse(value.Substring(3));

                int sufDigits = GlobalSettings.Properties.Settings.Default.DIGITOSCUENTAS - 3;
                sufDigits = (int)Math.Truncate(Math.Pow(10, sufDigits));
                this._Id = (this.Grupo + this.Subgrupo) * sufDigits + this.Sufijo;                
            }
        }
        public string Nombre { get; set; }
        public int Grupo { get { return this._Grupo.Digits; } }
        public int Subgrupo { get { return this._Subgrupo.Digits; } }
        public int Sufijo { get { return this._Sufijo; } }
        public bool IsFakeAccount { get; set; }
        #endregion

        #region public methods
        public bool IsLastAccount()
        {
            return this.Id == GlobalSettings.Properties.Settings.Default.MAXCODCUENTAS;
        }
        public bool IsFirstAccount()
        {
            return this.Id == GlobalSettings.Properties.Settings.Default.MINCODCUENTAS;
        }
        public bool IsProveedor_Propietario(List<GruposCuentas> cuentasProveedores_Cobros)
        {
            foreach (GruposCuentas gc in cuentasProveedores_Cobros)
                if (gc.Contains(this)) return true;

            return false;
        }
        #endregion
    }
}
