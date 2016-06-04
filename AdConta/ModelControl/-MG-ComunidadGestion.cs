#if (MGESTION)

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModuloGestion.Models;

namespace AdConta.ModelControl
{
    public partial class Comunidad
    {
        #region fields
        internal Dictionary<int, Finca> _Fincas = new Dictionary<int, Finca>();
        internal Dictionary<int, Recibo> _Recibos = new Dictionary<int, Recibo>();
        #endregion

        #region properties
        public ReadOnlyDictionary<int, Finca> Fincas { get; set; }
        public ReadOnlyDictionary<int, Recibo> Recibos { get; set; }
        #endregion

        #region helpers
        private void InitGestion()
        {
            this.Fincas = new ReadOnlyDictionary<int, Finca>(this._Fincas);
            this.Recibos = new ReadOnlyDictionary<int, Recibo>(this._Recibos);
        }
        #endregion

        #region public methods
        #endregion
    }

}
#endif
