using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdConta.Models;
using ModuloGestion.Models;
using ModuloContabilidad.Models;

namespace AdConta.ModelControl
{
    public class AppModelControl //: aAppModelControlBase
    {
        public AppModelControl()
        {
            AppModelControlMessenger.ModelAddedEvent += OnModelAddedEvent;
            AppModelControlMessenger.ModelAskedEvent += OnModelAskedEvent;

            this._Comunidades = new Dictionary<int, Comunidad>();
            this._Personas = new Dictionary<int, Persona>();
            this._Conceptos = new Dictionary<int, Concepto>();
        }

        #region fields
        private Dictionary<int, Comunidad> _Comunidades;
        private Dictionary<int, Persona> _Personas;
        private Dictionary<int, Concepto> _Conceptos;
        #endregion

        #region properties
        /*public List<Comunidad> Comunidades { get; }
        public List<Persona> Personas { get; private set; }
        public List<sConcepto> Conceptos { get { return this._Conceptos; } }*/
        #endregion

        #region helpers
        #endregion

        #region public methods
        public void UnsubscribeModelControlEvents()
        {
            AppModelControlMessenger.ModelAddedEvent -= OnModelAddedEvent;
            AppModelControlMessenger.ModelAskedEvent -= OnModelAskedEvent;
        }
        #endregion

        #region events
        /// <summary>
        /// Add object e.Model to the corresponding dictionary, WITHOUT checking if owners exists. The model have to be asked first with 
        /// AppModelControlMessenger.AskForModel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnModelAddedEvent(object sender, ModelControlEventArgs e)
        {
            //TypeSwitch.Case<>(x=>)
            TypeSwitch.Do(e.Model,
                TypeSwitch.Case<Comunidad>(x => 
                {
                    Comunidad model = (Comunidad)e.Model;
                    this._Comunidades.Add(model.Id, model);
                }),
                TypeSwitch.Case<Persona>(x => 
                {
                    Persona model = (Persona)e.Model;
                    this._Personas.Add(model.Id, model);
                }),
                TypeSwitch.Case<Concepto>(x => 
                {
                    Concepto model = (Concepto)e.Model;
                    this._Conceptos.Add(model.Id, model);
                }),
#if (MGESTION)
                TypeSwitch.Case<Finca>(x =>
                {
                    Finca model = (Finca)e.Model;

                    this._Comunidades[model.OwnerIdComunidad]._Fincas.Add(model.Id, model);
                }),
                TypeSwitch.Case<Cuota>(x =>
                {
                    Cuota model = (Cuota)e.Model;

                    this._Comunidades[model.OwnerIdCdad]._Fincas[model.OwnerIdFinca].Cuotas.Add(model.Id, model);
                }),
                TypeSwitch.Case<Recibo>(x =>
                {

                })
#endif
#if (MCONTABILIDAD)

#endif
            );
        }
        private void OnModelAskedEvent(ref object sender, ModelControlEventArgs e)
        {
            bool ModelExists = false;

            TypeSwitch.Do(e.Model,
                TypeSwitch.Case<Comunidad>(x => ModelExists = this._Comunidades.ContainsKey(((Comunidad)e.Model).Id)),
                TypeSwitch.Case<Persona>(x => ModelExists = this._Personas.ContainsKey(((Persona)e.Model).Id)),
                TypeSwitch.Case<Concepto>(x => ModelExists = this._Conceptos.ContainsKey(((Concepto)e.Model).Id))
                );

            AppModelControlMessenger.SetMsgFromAppModelcontrol(sender, ModelExists);
        }
        #endregion
    }

}
