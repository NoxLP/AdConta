﻿
namespace AdConta.Models
{
    public struct sDireccionPostal
    {
        public string TipoVia;
        public string Direccion;
        public int CP;
        public string Localidad;
        public string Provincia;
    }

    public struct sDireccionPostalCompleta
    {
        public string TipoVia;
        public string NombreVia;
        public string NumeroVia;
        public string Portal;
        public string Piso;
        public string Puerta;
        public int CP;
        public string Localidad;
        public string Provincia;

        public void GetDireccionPostalSimple(out sDireccionPostal direccion)
        {
            direccion = new sDireccionPostal();

            direccion.TipoVia = this.TipoVia;
            direccion.Direccion = string.Format("{0}, {1}{2}{3}{4}", this.NombreVia, this.NumeroVia, this.Portal, this.Piso, this.Puerta);
            direccion.CP = this.CP;
            direccion.Localidad = this.Localidad;
            direccion.Provincia = this.Provincia;
        }
    }
}
