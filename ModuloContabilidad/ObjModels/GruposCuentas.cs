using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdConta.Models;

namespace ModuloContabilidad.ObjModels
{
    public struct GruposCuentas : iOwnerComunidad
    {
        public int Id { get; private set; }
        public int IdOwnerComunidad { get; private set; }

        public GrupoContable Grupo { get; private set; }
        public SubgrupoContable Subgrupo { get; private set; }

        public GruposCuentas(int id, int idComunidad, string accountNumber)
        {
            this.Id = id;
            this.IdOwnerComunidad = idComunidad;
            this.Grupo = new GrupoContable(accountNumber);
            this.Subgrupo = new SubgrupoContable(accountNumber);
        }
        public GruposCuentas(int id, int idComunidad, int accountNumber)
        {
            this.Id = id;
            this.IdOwnerComunidad = idComunidad;
            this.Grupo = new GrupoContable(accountNumber);
            this.Subgrupo = new SubgrupoContable(accountNumber);
        }
        
        public bool Contains(ref LedgeAccount acc)
        {
            return (acc.Grupo == this.Grupo.Digits) && (acc.Subgrupo == this.Subgrupo.Digits);
        }
        public bool Contains(ref string acc)
        {
            return (GrupoContable.GetGrupoDigitsFromString(ref acc) == this.Grupo.Digits) && 
                (SubgrupoContable.GetSubgrupoDigitsFromString(ref acc) == this.Subgrupo.Digits);
        }
    }
}
