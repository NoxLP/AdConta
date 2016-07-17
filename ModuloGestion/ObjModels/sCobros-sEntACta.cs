using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdConta;
using AdConta.Models;

namespace ModuloGestion.ObjModels
{
    public struct sCobro : iOwnerPropietario, iOwnerCuota, iOwnerRecibo
    {
        public int Id { get; private set; }
        public int IdOwnerRecibo { get; private set; }
        public int IdOwnerCuota { get; private set; }
        public int IdOwnerPropietario { get; private set; }
        public Date Fecha { get; private set; }
        public bool Total;
        public decimal Importe;

        public sCobro(int id, int idrecibo, int idcuota, decimal importe, Date fecha, int idPropietario, bool total = true)
        {
            if (id < 0 || idrecibo < 0 || idcuota < 0) throw new System.Exception("sCobro's Ids have to be > 0");
            
            this.Id = id;
            this.IdOwnerRecibo = idrecibo;
            this.IdOwnerCuota = idcuota;
            this.IdOwnerPropietario = idPropietario;
            this.Fecha = fecha;
            this.Total = total;
            this.Importe = importe;
        }
    }

    public struct sEntACta : iOwnerFinca, iOwnerPropietario, iOwnerRecibo 
    {
        public int Id { get; private set; }
        public int IdOwnerRecibo { get; private set; }
        public int IdOwnerFinca { get; private set; }
        public int IdOwnerPropietario { get; private set; }
        public Date Fecha { get; private set; }
        public decimal Importe;

        public sEntACta(int id, int idrecibo, int idfinca, decimal importe, Date fecha, int idPropietario)
        {
            if (id < 0 || idrecibo < 0) throw new System.Exception("sEntACta's Ids have to be > 0");

            this.Id = id;
            this.IdOwnerRecibo = idrecibo;
            this.IdOwnerFinca = idfinca;
            this.IdOwnerPropietario = idPropietario;
            this.Fecha = fecha;
            this.Importe = importe;
        }
    }
}
