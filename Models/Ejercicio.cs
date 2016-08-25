using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using AdConta;

namespace AdConta.Models
{
    [Table("ejercicios")]
    public class Ejercicio : iObjModelBase, iOwnerComunidad
    {
        #region constructors
        public Ejercicio(int id, Date fechaComienzo, Date fechaFinal, int idOwnerComunidad, bool cerrado = false)
        {
            if (id < 0 || idOwnerComunidad < 0) throw new CustomException_ObjModels("sEjercicio's Id and IdOwnerComunidad have to be > 0");
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
        public Ejercicio(int idOwnerComunidad)
        {
            if (idOwnerComunidad < 0) throw new CustomException_ObjModels("sEjercicio's IdOwnerComunidad have to be > 0");
            else this.IdOwnerComunidad = idOwnerComunidad;

            this.Id = -1;
            this.FechaComienzo = new Date();
            this.FechaFinal = new Date();
            this.Cerrado = false;
        }
        #endregion

        #region properties
        public int Id { get; private set; }
        public Date FechaComienzo { get; private set; }
        public Date FechaFinal { get; private set; }
        public int IdOwnerComunidad { get; private set; }
        public bool Cerrado { get; private set; }
        #endregion

        public bool Contains(Date date)
        {
            if (date > this.FechaComienzo && date < this.FechaFinal)
                return true;

            return false;
        }
    }
}
