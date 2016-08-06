using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdConta;
using AdConta.Models;
using Extensions;

namespace ModuloContabilidad.ObjModels
{
    public class Factura : iObjModelBase, iOwnerProveedor, iOwnerComunidad
    {
        public Factura()
        {
            this._GastosFra = new GastosPagosList<Gasto>();
            this._PagosFra = new GastosPagosList<Pago>();
            this.AutoCalc = false;
        }

        #region fields
        private int _Id;
        private int _IdOwnerProveedor;
        private int _IdOwnerComunidad;
        private string _NFactura;

        private GastosPagosList<Gasto> _GastosFra;
        private GastosPagosList<Pago> _PagosFra;
        
        private decimal _Subtotal;
        private double _PerUnitIGICIVA;
        private decimal _IGICIVA;
        private double _PerUnitIRPF;
        private decimal _IRPF;
        private decimal _APagar;
        private decimal _Pendiente;
        #endregion

        #region properties
        public int Id { get { return this._Id; } }
        public int IdOwnerProveedor { get { return this._IdOwnerProveedor; } }
        public int IdOwnerComunidad { get { return this._IdOwnerComunidad; } }

        public string NFactura { get { return this._NFactura; } }

        public ReadOnlyGastosPagosList<Gasto> GastosFra { get { return this._GastosFra.AsReadOnly(); } }
        public ReadOnlyGastosPagosList<Pago> PagosFra { get { return this._PagosFra.AsReadOnly(); } }

        public Date Fecha { get; set; }

        public bool AutoCalc { get; set; }
        public decimal Subtotal
        {
            get { return this._Subtotal; }
            set
            {
                if(this.AutoCalc)
                {
                    this._Subtotal = value;
                    ReCalculate();
                }
                else if (Cuadrado(value, this.PerUnitIGICIVA, this.IGICIVA, this.PerUnitIRPF, this.IRPF))
                    this._Subtotal = value;
            }
        }
        public double PerUnitIGICIVA
        {
            get { return this._PerUnitIGICIVA; }
            set
            {
                if (this.AutoCalc)
                {
                    this._PerUnitIGICIVA = value;
                    ReCalculate();
                }
                if (Cuadrado(this.Subtotal, value, this.IGICIVA, this.PerUnitIRPF, this.IRPF))
                    this._PerUnitIGICIVA = value;
            }
        }
        public decimal IGICIVA
        {
            get { return this._IGICIVA; }
            set
            {
                if (this.AutoCalc)
                {
                    this._IGICIVA = value;
                    ReCalculate();
                }
                if (Cuadrado(this.Subtotal, this.PerUnitIGICIVA, value, this.PerUnitIRPF, this.IRPF))
                    this._IGICIVA = value;
            }
        }
        public double PerUnitIRPF
        {
            get { return this._PerUnitIRPF; }
            set
            {
                if (this.AutoCalc)
                {
                    this._PerUnitIRPF = value;
                    ReCalculate();
                }
                if (Cuadrado(this.Subtotal, this.PerUnitIGICIVA, this.IGICIVA, value, this.IRPF))
                    this._PerUnitIRPF = value;
            }
        }
        public decimal IRPF
        {
            get { return this._IRPF; }
            set
            {
                if (this.AutoCalc)
                {
                    this._IRPF = value;
                    ReCalculate();
                }
                if (Cuadrado(this.Subtotal, this.PerUnitIGICIVA, this.IGICIVA, this.PerUnitIRPF, value))
                    this._IRPF = value;
            }
        }

        public decimal APagar { get { return this._APagar; } }
        public decimal Pendiente { get { return this._Pendiente; } }
        
        public string Concepto { get; set; }
        public TipoPagoFacturas TipoPago { get; set; }
        #endregion

        #region helpers
        public bool Cuadrado(decimal subtotal, double perUnitII, decimal igiciva, double perUnitIRPF, decimal irpf)
        {
            decimal II = subtotal.MultiplyDouble(perUnitII);
            decimal Irpf = subtotal.MultiplyDouble(perUnitIRPF);

            return (subtotal + igiciva - irpf) == this.APagar;
        }
        public void ReCalculate()
        {
            this._IGICIVA = this.Subtotal.MultiplyDouble(this.PerUnitIGICIVA);
            this._IRPF = this.Subtotal.MultiplyDouble(this.PerUnitIRPF);
            this._APagar = this.Subtotal + this.IGICIVA - this.IRPF;
            this._Pendiente = this.APagar - this.PagosFra.Total;
        }
        #endregion

        #region public methods
        public bool TrySetGastosPagos(GastosPagosList<Gasto> gastos, GastosPagosList<Pago> pagos)
        {
            decimal pendiente = gastos.Total - pagos.Total;

            if (this._Pendiente == pendiente || gastos.Total != this.APagar) return false;

            this._GastosFra = gastos;
            this._PagosFra = pagos;
            this._Pendiente = pendiente;

            return true;
        }
        public bool TryAddNewPago(Pago pago)
        {
            if (pago.Importe > this.Pendiente) return false;

            this._PagosFra.Add(pago);
            this._Pendiente = this.GastosFra.Total - this.PagosFra.Total;
            return true;
        }
        #endregion
    }

}
