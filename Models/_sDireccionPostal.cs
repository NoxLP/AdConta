using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdConta.Models
{
    public class DireccionPostal
    {
        public string TipoVia;
        public string Direccion;
        public int CP;
        public string Localidad;
        public string Provincia;

        public DireccionPostal(string tipoVia, string direccion, int cp, string localidad, string provincia)
        {
            this.TipoVia = tipoVia;
            this.Direccion = direccion;
            this.CP = cp;
            this.Localidad = localidad;
            this.Provincia = provincia;
        }
    }

    public class DireccionPostalCompleta
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

        public void GetDireccionPostalSimple(out DireccionPostal direccion)
        {
            direccion = new DireccionPostal();

            direccion.TipoVia = this.TipoVia;
            direccion.Direccion = string.Format("{0}, {1}{2}{3}{4}", this.NombreVia, this.NumeroVia, this.Portal, this.Piso, this.Puerta);
            direccion.CP = this.CP;
            direccion.Localidad = this.Localidad;
            direccion.Provincia = this.Provincia;
        }
    }
}
