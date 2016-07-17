﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdConta;
using AdConta.Models;

namespace ModuloGestion.ObjModels
{
    public class Recibo : iOwnerComunidad
    {
        public Recibo()
        {
            this._Cobros = new CobrosDict();
            this._EntregasACuenta = new EntACtaDict();
        }
        public Recibo(int id, int idCdad, decimal importe, Date fecha, string concepto)
        {
            this._Id = id;
            this._IdOwnerCdad = idCdad;
            this._Importe = importe;
            this.Fecha = fecha;
            this.Concepto = concepto;
            this._Cobros = new CobrosDict();
            this._EntregasACuenta = new EntACtaDict();
        }

        #region fields
        private int _Id;
        private int _IdOwnerCdad;
        private decimal _Importe;

        private CobrosDict _Cobros;
        private EntACtaDict _EntregasACuenta;
        #endregion

        #region properties
        public int Id { get { return this._Id; } }
        public int IdOwnerCdad { get { return this._IdOwnerCdad; } }
        public decimal Importe { get { return this._Importe; } }
        public Date Fecha { get; set; }
        public string Concepto { get; set; }

        public CobrosDict Cobros { get { return this._Cobros; } }
        public EntACtaDict EntregasACuenta { get { return this._EntregasACuenta; } }
        #endregion

        #region helpers
        #endregion

        #region public methods
        public bool TryAddCobrosEntACta(ref CobrosDict cobros, ref EntACtaDict entregasACuenta)
        {
            decimal importeTotal = cobros.Total + entregasACuenta.Total;

            if (importeTotal != this.Importe)
                return false;

            this._Cobros = cobros;
            this._EntregasACuenta = entregasACuenta;
            return true;
        }
        #endregion
    }

}
