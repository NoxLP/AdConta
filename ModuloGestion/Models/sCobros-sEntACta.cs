﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloGestion.Models
{
    public struct sCobro
    {
        public int Id { get; private set; }
        public int IdRecibo { get; private set; }
        public int IdCuota { get; private set; }
        public bool Total;
        public decimal Importe;

        public sCobro(int id, int idrecibo, int idcuota, decimal importe, bool total = true)
        {
            if (id < 0 || idrecibo < 0 || idcuota < 0) throw new System.Exception("sCobro's Ids have to be > 0");

            this.Id = id;
            this.IdRecibo = idrecibo;
            this.IdCuota = idcuota;
            this.Total = total;
            this.Importe = importe;
        }
    }

    public struct sEntACta
    {
        public int Id { get; private set; }
        public int IdRecibo { get; private set; }
        public int IdFinca { get; private set; }
        public decimal Importe;

        public sEntACta(int id, int idrecibo, int idfinca, decimal importe)
        {
            if (id < 0 || idrecibo < 0) throw new System.Exception("sEntACta's Ids have to be > 0");

            this.Id = id;
            this.IdRecibo = idrecibo;
            this.IdFinca = idfinca;
            this.Importe = importe;
        }
    }
}
