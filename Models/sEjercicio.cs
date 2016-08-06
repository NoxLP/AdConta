
namespace AdConta.Models
{
    public struct sEjercicio : iObjModelBase, iOwnerComunidad
    {
        public int Id { get; private set; }
        public Date FechaComienzo { get; private set; }
        public Date FechaFinal { get; private set; }
        public int IdOwnerComunidad { get; private set; }
        public bool Cerrado;

        public bool Contains(Date date)
        {
            if (date > this.FechaComienzo && date < this.FechaFinal)
                return true;

            return false;
        }

        public sEjercicio(int id, Date fechaComienzo, Date fechaFinal, int idOwnerComunidad, bool cerrado = false)
        {
            if (id < 0 || idOwnerComunidad < 0) throw new System.Exception("sEjercicio's Id and IdOwnerComunidad have to be > 0");
            else
            {
                this.Id = id;
                this.IdOwnerComunidad = idOwnerComunidad;
            }

            this.FechaComienzo = fechaComienzo;
            this.FechaFinal = fechaFinal;
            this.Cerrado = cerrado;
        }
        /// <summary>
        /// Usar este constructor para nuevos ejercicios.
        /// </summary>
        /// <param name="idCdad"></param>
        public sEjercicio(int idOwnerComunidad)
        {
            if (idOwnerComunidad < 0) throw new System.Exception("sEjercicio's IdOwnerComunidad have to be > 0");
            else this.IdOwnerComunidad = idOwnerComunidad;

            this.Id = -1;
            this.FechaComienzo = new Date();
            this.FechaFinal = new Date();
            this.Cerrado = false;
        }
    }
}
