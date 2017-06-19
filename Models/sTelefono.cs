
namespace AdConta.Models
{
    public struct sTelefono
    {
        public string Numero;
        public TipoTelefono Tipo;

        public sTelefono(string numero, TipoTelefono tipo)
        {
            Numero = numero;
            Tipo = tipo;
        }
    }
}
