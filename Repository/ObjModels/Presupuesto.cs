using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModuloContabilidad.ObjModels;
using ModuloGestion.ObjModels;

namespace AdConta.Models
{
    //--------------------TODO---------------------------
    //HECHO OK - Falta un objecto nuevo para GrupoGasto que sea protegido, para crearlo cuando un presupuesto se guarde.
    //Si se cambia el GrupoGasto original, cuando se abra el presupuesto debería haber un mensaje de advertencia, pidiendo si se quiere
    //usar los nuevos datos, o los ya guardados.
    //---------------------------------------------------
    public class Presupuesto : iObjModelBase, iOwnerComunidad
    {
        private Presupuesto() { }
        public Presupuesto(int id, int idComunidad, bool aceptado = false, TipoRepartoPresupuesto tipo = TipoRepartoPresupuesto.CoeficientesYGrupos)
        {
            this._Id = id;
            this._IdOwnerComunidad = idComunidad;
            this._Aceptado = aceptado;
            this._TipoReparto = tipo;
            this._GruposDeGasto = new List<iGrupoGastos>();
        }

        #region fields
        private int _Id;
        private int _IdOwnerComunidad;

        private decimal _Total;
        private bool _Aceptado;
        private TipoRepartoPresupuesto _TipoReparto;

        private List<iGrupoGastos> _GruposDeGasto;
        #endregion

        #region properties
        public int Id { get { return this._Id; } }
        public int IdOwnerComunidad { get { return this._IdOwnerComunidad; } }

        public string Titulo { get; set; }
        public decimal Total { get { return this._Total; } }
        public bool Aceptado { get { return this._Aceptado; } }
        public TipoRepartoPresupuesto TipoReparto { get { return this._TipoReparto; } }

        public ReadOnlyCollection<iGrupoGastos> GruposDeGasto { get { return this._GruposDeGasto.AsReadOnly(); } }
        #endregion

        #region public methods
        public bool TryAddGrupoDeGasto(ref GrupoGastos grupo)
        {
            if (this.Aceptado) return false;

            this._GruposDeGasto.Add(grupo);
            this._Total += grupo.Importe;
            return true;
        }
        public bool TryRemoveGrupoDeGasto(ref GrupoGastos grupo)
        {
            if (this.Aceptado || !this.GruposDeGasto.Contains(grupo)) return false;

            this._GruposDeGasto.RemoveAt(this.GruposDeGasto.IndexOf(grupo));
            this._Total -= grupo.Importe;
            return true;
        }
        public bool TrySetGruposDeGasto(ref IEnumerable<GrupoGastos> grupos)
        {
            if (this.Aceptado) return false;

            this._GruposDeGasto = (List<iGrupoGastos>)grupos;
            this._Total = grupos.Select(x => x.Importe).Sum();
            return true;
        }
        public bool TrySetTipoReparto(TipoRepartoPresupuesto tipo)
        {
            if (this.Aceptado) return false;

            this._TipoReparto = tipo;
            return true;
        }
        public void AceptaPresupuesto(int lastFId, int lastCuentasId, int LastCuotasId, Dictionary<Finca, decimal> ImportesPorFinca)
        {
            if (this.Aceptado) return;

            this._Aceptado = true;

            this._GruposDeGasto = (List<iGrupoGastos>)this.GruposDeGasto.Select(x =>
                ((GrupoGastos)x).AsAceptado(lastFId, lastCuentasId, LastCuotasId, ImportesPorFinca) as iGrupoGastos
                );
        }
        #endregion
    }

}
