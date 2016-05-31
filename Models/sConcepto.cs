
namespace AdConta.Models
{
    public struct sConcepto
    {
        public int _Id { get; private set; }
        public string Nombre;
        public string NombreReducido;

        public sConcepto(int id)
        {
            this._Id = id;
            this.Nombre = "";
            this.NombreReducido = "";
        }
        public sConcepto(int id, string nombre, string nombreReducido)
        {
            this._Id = id;
            this.Nombre = nombre;
            this.NombreReducido = nombreReducido;
        }
    }
}
