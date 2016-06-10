using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdConta;

namespace ModuloContabilidad.ObjModels
{
    public class ComunidadContabilidad
    {
        public ComunidadContabilidad(Date primeraFecha, int ownerIdCdad)
        {
            this.PrimeraFecha = new ReadOnlyDate(primeraFecha);
            this.OwnerIdCdad = ownerIdCdad;
        }

        #region fields
        #endregion

        #region properties
        public int OwnerIdCdad { get; private set; }
        public ReadOnlyDate PrimeraFecha { get; private set; }
        public ReadOnlyDate FechaPunteo { get; private set; }
        public ReadOnlyDate FechaBanco { get; private set; }
        #endregion

        #region helpers
        #endregion

        #region public methods
        public void SetFechaPunteo(ref Date date)
        {
            this.FechaPunteo = new ReadOnlyDate(date);
        }
        public void SetFechaBanco(ref Date date)
        {
            this.FechaBanco = new ReadOnlyDate(date);
        }
        #endregion
    }

}
