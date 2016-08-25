﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModuloContabilidad.ObjModels;
using ModuloGestion.ObjModels;

namespace AdConta.Models
{
    //--------------------TODO---------------------------
    //HECHO OK - Falta un objecto nuevo para GrupoGasto que sea protegido, para crearlo cuando un presupuesto se guarde.
    //Si se cambia el GrupoGasto original, cuando se abra el presupuesto debería haber un mensaje de advertencia, pidiendo si se quiere
    //usar los nuevos datos, o los ya guardados.
    //---------------------------------------------------

    public interface iGrupoGasto : iObjModelBase, iOwnerComunidad, iOwnerPresupuesto
    {
        bool CoeficientesCustom { get; }
        decimal Importe { get; }
    }

    public class GrupoGasto : iGrupoGasto
    {
        private GrupoGasto() { }
        public GrupoGasto(
            int id, 
            int idComunidad, 
            int idPresupuesto, 
            List<Finca> fincas,
            List<CuentaParaPresupuesto> cuentas,
            List<Cuota> cuotas)
        {
            this._Id = id;
            this._IdOwnerComunidad = idComunidad;
            this._IdOwnerPresupuesto = idPresupuesto;

            this._FincasCoeficientes = (Dictionary<Finca,double>)fincas.Select(x => new KeyValuePair<Finca, double>(x, x.Coeficiente));
            this.Cuotas = cuotas;
            this._Cuentas = cuentas;

            SetCoeficientesCustom();
            this._Importe = this.Cuentas.Select(x => x.SaldoAlAñadirLaCuenta).Sum();
        }

        public class CuentaParaPresupuesto
        {
            private CuentaParaPresupuesto() { }
            public CuentaParaPresupuesto(CuentaMayor cuenta, string nombre, decimal saldo)
            {
                this.Cuenta = cuenta;
                this.NombreParaPresupuesto = nombre;
                this.SaldoAlAñadirLaCuenta = saldo;
            }

            public CuentaMayor Cuenta { get; set; }
            public string NombreParaPresupuesto { get; set; }
            public decimal SaldoAlAñadirLaCuenta { get; set; }
        }

        #region fields
        private int _Id;
        private int _IdOwnerComunidad;
        private int _IdOwnerPresupuesto;

        private Dictionary<Finca, double> _FincasCoeficientes;
        private List<CuentaParaPresupuesto> _Cuentas;

        private bool _CoeficientesCustom;
        private decimal _Importe;
        #endregion

        #region properties
        public int Id { get { return this._Id; } }
        public int IdOwnerComunidad { get { return this._IdOwnerComunidad; } }
        public int IdOwnerPresupuesto { get { return this._IdOwnerPresupuesto; } }

        public ReadOnlyDictionary<Finca, double> FincasCoeficientes { get { return new ReadOnlyDictionary<Finca, double>(this._FincasCoeficientes); } }
        public List<Cuota> Cuotas { get; set; }
        public ReadOnlyCollection<CuentaParaPresupuesto> Cuentas { get { return this._Cuentas.AsReadOnly(); } }

        public bool CoeficientesCustom { get { return this._CoeficientesCustom; } }
        public decimal Importe { get { return this._Importe; } }
        #endregion

        #region helpers
        private void SetCoeficientesCustom() { this._CoeficientesCustom = this.FincasCoeficientes.Any(x => x.Value != x.Key.Coeficiente); }
        #endregion

        #region public methods
        public bool TryAddFinca(ref Finca finca, double? coeficiente = null)
        {
            if (this.FincasCoeficientes.ContainsKey(finca)) return false;

            this._CoeficientesCustom = this._CoeficientesCustom || (finca.Coeficiente != coeficiente);
            this._FincasCoeficientes.Add(finca, coeficiente ?? finca.Coeficiente);
            return true;
        }
        public bool TryRemoveFinca(ref Finca finca)
        {
            if(!this.FincasCoeficientes.ContainsKey(finca)) return false;

            this._FincasCoeficientes.Remove(finca);
            SetCoeficientesCustom();
            return true;
        }
        public void AddRangeFincas(ref Dictionary<Finca, double> fincas)
        {
            this._FincasCoeficientes.Union(fincas);
            SetCoeficientesCustom();
        }
        public void SetFincas(ref Dictionary<Finca, double> fincas)
        {
            this._FincasCoeficientes = fincas;
            SetCoeficientesCustom();
        }
        public bool TryAddCuenta(ref CuentaMayor cuenta, string nombreParaPresupuesto, decimal saldoCuenta)
        {
            var c = new CuentaParaPresupuesto(cuenta, nombreParaPresupuesto, saldoCuenta);
            if (this._Cuentas.Contains(c)) return false;

            this._Cuentas.Add(c);
            this._Importe += saldoCuenta;
            return true;
        }
        public bool TryRemoveCuenta(ref CuentaMayor cuenta, string nombreParaPresupuesto, decimal saldoCuenta)
        {
            var c = new CuentaParaPresupuesto(cuenta, nombreParaPresupuesto, saldoCuenta);
            if (!this._Cuentas.Contains(c)) return false;

            this._Cuentas.RemoveAt(this._Cuentas.IndexOf(c));
            this._Importe -= saldoCuenta;
            return true;
        }
        public void SetCuentas(ref IEnumerable<CuentaParaPresupuesto> cuentas)
        {
            this._Cuentas = (List<CuentaParaPresupuesto>)cuentas;
            this._Importe = cuentas.Select(x => x.SaldoAlAñadirLaCuenta).Sum();
        }
        /// <summary>
        /// Devuelve dictionary con el importe que le corresponde a cada finca en este grupo.
        /// Si this.CoeficientesCustom == true y el parámetro coefGrupoReal != null, entonces se usará este parámetro como el total de coeficientes
        /// del grupo para los cálculos.
        /// </summary>
        /// <param name="tipo"></param>
        /// <param name="coefGrupoReal"></param>
        /// <returns></returns>
        public Dictionary<Finca, decimal> GetImportePorFinca(TipoRepartoPresupuesto tipo, double? coefGrupoReal = null)
        {
            switch (tipo)
            {
                case TipoRepartoPresupuesto.Lineal:
                    decimal importe = this.Importe / this.FincasCoeficientes.Count;
                    return (Dictionary<Finca, decimal>)this.FincasCoeficientes
                        .Select(x => new KeyValuePair<Finca, decimal>(x.Key, importe));

                case TipoRepartoPresupuesto.SoloCoeficientes:
                    return (Dictionary<Finca, decimal>)this.FincasCoeficientes
                        .Select(x => new KeyValuePair<Finca, decimal>(x.Key, Math.Round((decimal)x.Value * this.Importe, 2)));

                case TipoRepartoPresupuesto.CoeficientesYGrupos:

                    double coefGrupo = (this.CoeficientesCustom && coefGrupoReal != null) ?
                        (double)coefGrupoReal :
                        this.FincasCoeficientes.Select(x => x.Value).Sum();

                    return (Dictionary<Finca, decimal>)this.FincasCoeficientes
                        .Select(x =>
                        {
                            double coefRelativo = x.Key.Coeficiente / coefGrupo;
                            double dImp = (double)this.Importe;
                            return new KeyValuePair<Finca, decimal>(x.Key, (decimal)Math.Round(dImp * coefRelativo, 2));
                        });
                default:
                    return null;
            }
        }
        /// <summary>
        /// Create a new GrupoGastoAceptado with same values as this.
        /// </summary>
        /// <param name="lastFId"></param>
        /// <param name="lastCuentasId"></param>
        /// <param name="LastCuotasId"></param>
        /// <param name="ImportesPorFinca"></param>
        /// <returns></returns>
        public GrupoGastoAceptado AsAceptado(int lastFId, int lastCuentasId, int LastCuotasId, Dictionary<Finca, decimal> ImportesPorFinca)
        {
            List<GrupoGastoAceptado.DatosFincaGGAceptado> listFincas = new List<GrupoGastoAceptado.DatosFincaGGAceptado>();
            foreach(KeyValuePair<Finca, double> kvp in this.FincasCoeficientes)
            {
                listFincas.Add(new GrupoGastoAceptado.DatosFincaGGAceptado(
                    lastFId,
                    kvp.Key.Id,
                    this.Id,
                    kvp.Value,
                    ImportesPorFinca[kvp.Key],
                    kvp.Key.Nombre,
                    kvp.Key.PropietarioActual.Nombre,
                    kvp.Key.Codigo));
                lastFId++;
            }

            List<GrupoGastoAceptado.DatosCuentaGGAceptado> listCuentas = new List<GrupoGastoAceptado.DatosCuentaGGAceptado>();
            foreach(CuentaParaPresupuesto cuenta in this.Cuentas)
            {
                listCuentas.Add(new GrupoGastoAceptado.DatosCuentaGGAceptado(
                    lastCuentasId,
                    cuenta.Cuenta.Id,
                    this.Id,
                    cuenta.Cuenta.Codigo,
                    cuenta.NombreParaPresupuesto,
                    cuenta.SaldoAlAñadirLaCuenta));
                lastCuentasId++;
            }

            List<GrupoGastoAceptado.DatosCuotaGGAceptado> listCuotas = new List<GrupoGastoAceptado.DatosCuotaGGAceptado>();
            foreach(Cuota cuota in this.Cuotas)
            {
                listCuotas.Add(new GrupoGastoAceptado.DatosCuotaGGAceptado(
                    LastCuotasId,
                    cuota.Id,
                    this.Id,
                    cuota.Concepto.Nombre));
                LastCuotasId++;
            }

            GrupoGastoAceptado ggA = new GrupoGastoAceptado(
                this.Id, 
                this.IdOwnerComunidad,
                this.IdOwnerPresupuesto,
                listFincas,
                listCuentas,
                listCuotas,
                this.CoeficientesCustom,
                this.Importe);

            return ggA;
        }
        #endregion
    }
    
    public class GrupoGastoAceptado : iGrupoGasto
    {
        private GrupoGastoAceptado() { }
        public GrupoGastoAceptado(
            int id,
            int idComunidad,
            int idPresupuesto,
            List<DatosFincaGGAceptado> fincas,
            List<DatosCuentaGGAceptado> cuentas,
            List<DatosCuotaGGAceptado> cuotas,
            bool coeficientes,
            decimal importe)
        {
            this._Id = id;
            this._IdOwnerComunidad = idComunidad;
            this._IdOwnerPresupuesto = idPresupuesto;

            this._Fincas = fincas;
            this._Cuotas = cuotas;
            this._Cuentas = cuentas;

            this._CoeficientesCustom = coeficientes;
            this._Importe = importe;
        }

        #region data structs
        public struct DatosFincaGGAceptado : iOwnerFinca, iOwnerGrupoGasto
        {
            public int Id { get; private set; }
            public int IdOwnerFinca { get; private set; }
            public int IdOwnerGrupoGasto { get; private set; }

            public double Coeficiente { get; private set; }
            public decimal Importe { get; private set; }
            public string NombreFinca { get; private set; }
            public string NombrePropietario { get; private set; }
            public int CodigoFinca { get; private set; }

            public DatosFincaGGAceptado(int id, int idFinca, int idGG, double coef, decimal importe, string nombreF, string nombreP, int codigoF)
            {
                this.Id = id;
                this.IdOwnerFinca = idFinca;
                this.IdOwnerGrupoGasto = idGG;
                this.Coeficiente = coef;
                this.Importe = importe;
                this.NombreFinca = nombreF;
                this.NombrePropietario = nombreP;
                this.CodigoFinca = codigoF;
            }
        }
        public struct DatosCuotaGGAceptado : iOwnerGrupoGasto, iOwnerCuota
        {
            public int Id { get; private set; }
            public int IdOwnerGrupoGasto { get; private set; }
            public int IdOwnerCuota { get; private set; }

            public string Concepto { get; private set; }

            public DatosCuotaGGAceptado(int id, int idCuota, int idGG, string concepto)
            {
                this.Id = id;
                this.IdOwnerCuota = idCuota;
                this.IdOwnerGrupoGasto = idGG;
                this.Concepto = concepto;
            }
        }
        public struct DatosCuentaGGAceptado : iOwnerGrupoGasto
        {
            public int Id { get; private set; }
            public int IdOwnerGrupoGasto { get; private set; }
            public int IdCuenta { get; private set; }

            public string Codigo { get; private set; }
            public string NombreEnPresupuesto { get; private set; }
            public decimal SaldoAlAceptarPresupuesto { get; private set; }

            public DatosCuentaGGAceptado(int id, int idCuenta, int idGG, string codigo, string nombre, decimal saldo)
            {
                this.Id = id;
                this.IdCuenta = idCuenta;
                this.IdOwnerGrupoGasto = idGG;
                this.Codigo = codigo;
                this.NombreEnPresupuesto = nombre;
                this.SaldoAlAceptarPresupuesto = saldo;
            }
        }
        #endregion

        #region fields
        private int _Id;
        private int _IdOwnerComunidad;
        private int _IdOwnerPresupuesto;

        private List<DatosFincaGGAceptado> _Fincas;
        private List<DatosCuotaGGAceptado> _Cuotas;
        private List<DatosCuentaGGAceptado> _Cuentas;

        private bool _CoeficientesCustom;
        private decimal _Importe;
        #endregion

        #region properties
        public int Id { get { return this._Id; } }
        public int IdOwnerComunidad { get { return this._IdOwnerComunidad; } }
        public int IdOwnerPresupuesto { get { return this._IdOwnerPresupuesto; } }

        public ReadOnlyCollection<DatosFincaGGAceptado> Fincas { get { return this._Fincas.AsReadOnly(); } }
        public ReadOnlyCollection<DatosCuotaGGAceptado> Cuotas { get { return this._Cuotas.AsReadOnly(); } }
        public ReadOnlyCollection<DatosCuentaGGAceptado> Cuentas { get { return this._Cuentas.AsReadOnly(); } }

        public bool CoeficientesCustom { get { return this._CoeficientesCustom; } }
        public decimal Importe { get { return this._Importe; } }
        #endregion
    }
}
