
namespace AdConta.Models
{
    public class Concepto
    {
        public Concepto(int id)
        {
            this.Id = id;
            this.Nombre = "";
            this.NombreReducido = "";
        }
        public Concepto(int id, string nombre, string nombreReducido)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.NombreReducido = nombreReducido;
        }

        public int Id { get; private set; }
        public string Nombre { get; set; }
        public string NombreReducido { get; set; }
    }
}
