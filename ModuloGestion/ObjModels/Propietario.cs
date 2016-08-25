using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdConta;
using AdConta.Models;

namespace ModuloGestion.ObjModels
{
    public class Propietario : Persona 
    {
        public Propietario(int id, string nif, bool forceInvalidNIF = false) : base(id, nif, forceInvalidNIF)
        {
            this._Cuotas = new Dictionary<int, Cuota>();
        }
        

        #region fields
        private Dictionary<int, Cuota> _Cuotas;
        #endregion

        #region properties
        public ReadOnlyDictionary<int, Cuota> Cuotas { get { return new ReadOnlyDictionary<int, Cuota>(this._Cuotas); } }
        #endregion

        #region helpers
        #endregion

        #region public methods
        public void CambioNombrePropietario(string nombre)
        {
            base.Nombre = nombre;
        }
        public void RemoveCuotas(ref List<Cuota> cuotasToRemove, Date fechaInicial, Date fechaFinal)
        {
            IEnumerable<Cuota> cuotasEnum = this._Cuotas.Where(x => (
                x.Value.Mes > fechaInicial && x.Value.Mes < fechaFinal
                )) as IEnumerable<Cuota>;

            cuotasToRemove.AddRange(cuotasEnum);
            foreach (Cuota cuota in cuotasEnum)
                this._Cuotas.Remove(cuota.Id);
        }
        public void AddCuotas(ref List<Cuota> cuotasToAdd)
        {
            this._Cuotas.Union(cuotasToAdd.ToDictionary(x => x.Id)); //Ya hace distinct => no es necesario comprobar si tiene las id
        }
        #endregion
    }

}
