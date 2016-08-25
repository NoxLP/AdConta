using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdConta;
using AdConta.Models;

namespace ModuloGestion.ObjModels
{
    public interface iIngresoPropietario : iOwnerPersona, iOwnerRecibo
    {
        int Id { get; }
        Date Fecha { get; }
        decimal Importe { get; }
        Situacion_Recibo_Cobro_EntaCta Situacion { get; }
    }

    public struct sCobro : iIngresoPropietario, iOwnerCuota
    {
        public int Id { get; private set; }
        public int IdOwnerRecibo { get; private set; }
        public int IdOwnerPersona { get; private set; }
        public int IdOwnerCuota { get; private set; }
        public Date Fecha { get; private set; }
        public bool Total { get; private set; }
        public decimal Importe { get; private set; }
        public Situacion_Recibo_Cobro_EntaCta Situacion { get; private set; }

        public sCobro(int id, int idrecibo, int idcuota, decimal importe, Date fecha, int idPersona, 
            bool total = true, Situacion_Recibo_Cobro_EntaCta situacion = Situacion_Recibo_Cobro_EntaCta.Normal)
        {
            if (id < 0 || idrecibo < 0 || idcuota < 0) throw new System.Exception("sCobro's Ids have to be > 0");
            
            this.Id = id;
            this.IdOwnerRecibo = idrecibo;
            this.IdOwnerCuota = idcuota;
            this.IdOwnerPersona = idPersona;
            this.Fecha = fecha;
            this.Total = total;
            this.Importe = importe;
            this.Situacion = situacion;
        }
    }

    public struct sEntACta : iIngresoPropietario, iOwnerFinca
    {
        public int Id { get; private set; }
        public int IdOwnerRecibo { get; private set; }        
        public int IdOwnerPersona { get; private set; }
        public int IdOwnerFinca { get; private set; }
        public Date Fecha { get; private set; }
        public decimal Importe { get; private set; }
        public Situacion_Recibo_Cobro_EntaCta Situacion { get; private set; }

        public sEntACta(int id, int idrecibo, int idfinca, decimal importe, Date fecha, int idPersona,
            Situacion_Recibo_Cobro_EntaCta situacion = Situacion_Recibo_Cobro_EntaCta.Normal)
        {
            if (id < 0 || idrecibo < 0) throw new System.Exception("sEntACta's Ids have to be > 0");

            this.Id = id;
            this.IdOwnerRecibo = idrecibo;
            this.IdOwnerFinca = idfinca;
            this.IdOwnerPersona = idPersona;
            this.Fecha = fecha;
            this.Importe = importe;
            this.Situacion = situacion;
        }
    }
}
