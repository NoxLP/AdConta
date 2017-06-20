
namespace AdConta.Models
{
    public struct sDireccionPostal
    {
        public string TipoVia { get; private set; }
        public string Direccion { get; private set; }
        public int CP { get; private set; }
        public string Localidad { get; private set; }
        public string Provincia { get; private set; }

        public sDireccionPostal(string tipoVia, string direccion, int cp, string localidad, string provincia)
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
        public string TipoVia { get; set; }
        public string NombreVia { get; set; }
        public string NumeroVia { get; set; }
        public string Portal { get; set; }
        public string Piso { get; set; }
        public string Puerta { get; set; }
        public int CP { get; set; }
        public string Localidad { get; set; }
        public string Provincia { get; set; }

        public void GetDireccionPostalSimple(out sDireccionPostal direccion)
        {
            direccion = new sDireccionPostal(
                this.TipoVia,
                string.Format("{0}, {1}{2}{3}{4}", this.NombreVia, this.NumeroVia, this.Portal, this.Piso, this.Puerta),
                this.CP,
                this.Localidad,
                this.Provincia);

            /*direccion.TipoVia = this.TipoVia;
            direccion.Direccion = string.Format("{0}, {1}{2}{3}{4}", this.NombreVia, this.NumeroVia, this.Portal, this.Piso, this.Puerta);
            direccion.CP = this.CP;
            direccion.Localidad = this.Localidad;
            direccion.Provincia = this.Provincia;*/
        }
    }
}
