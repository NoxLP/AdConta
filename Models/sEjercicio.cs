
namespace AdConta.Models
{
    public struct sEjercicio
    {
        public int Id { get; private set; }
        public Date FechaComienzo { get; private set; }
        public Date FechaFinal { get; private set; }
        public int IdCdad { get; private set; }
        public bool Cerrado;

        public sEjercicio(int id, Date fechaComienzo, Date fechaFinal, int idCdad, bool cerrado = false)
        {
            if (id < 0 || idCdad < 0) throw new System.Exception("sEjercicio's Id and IdCdad have to be > 0");
            else
            {
                this.Id = id;
                this.IdCdad = idCdad;
            }

            this.FechaComienzo = fechaComienzo;
            this.FechaFinal = fechaFinal;
            this.Cerrado = cerrado;
        }
        /// <summary>
        /// Usar este constructor para nuevos ejercicios.
        /// </summary>
        /// <param name="idCdad"></param>
        public sEjercicio(int idCdad)
        {
            if (idCdad < 0) throw new System.Exception("sEjercicio's IdCdad have to be > 0");
            else this.IdCdad = idCdad;

            this.Id = -1;
            this.FechaComienzo = new Date();
            this.FechaFinal = new Date();
            this.Cerrado = false;
        }
    }
}
