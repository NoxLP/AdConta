using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdConta;
using ModuloGestion.Models;
using ModuloContabilidad.Models;

namespace AdConta.Models
{
    public class AppModelControl //: aAppModelControlBase
    {
        public AppModelControl()
        {
            AppModelControlMessenger.ModelAddedEvent += OnModelAddedEvent;
            AppModelControlMessenger.ModelAskedEvent += OnModelAskedEvent;

            this._Comunidades = new List<Comunidad>();
            this._Personas = new List<Persona>();
            this._Conceptos = new List<Concepto>();
        }

        #region fields
        private List<Comunidad> _Comunidades;
        private List<Persona> _Personas;
        private List<Concepto> _Conceptos;
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
        private void OnModelAddedEvent(object sender, ModelControlEventArgs e)
        {
            //TypeSwitch.Case<>(x=>)
            TypeSwitch.Do(e.Model,
                TypeSwitch.Case<Comunidad>(x => this._Comunidades.Add((Comunidad)e.Model)),
                TypeSwitch.Case<Persona>(x=> this._Personas.Add((Persona)e.Model)),
                TypeSwitch.Case<Concepto>(x => this._Conceptos.Add((Concepto)e.Model))
            );
        }
        private void OnModelAskedEvent(ref object sender, ModelControlEventArgs e)
        {
            bool ModelExists = false;

            TypeSwitch.Do(e.Model,
                TypeSwitch.Case<Comunidad>(x => ModelExists = this._Comunidades.Contains((Comunidad)e.Model)),
                TypeSwitch.Case<Persona>(x => ModelExists = this._Personas.Contains((Persona)e.Model)),
                TypeSwitch.Case<Concepto>(x => ModelExists = this._Conceptos.Contains((Concepto)e.Model))
                );

            AppModelControlMessenger.SetMsgFromAppModelcontrol(sender, ModelExists);
        }
        #endregion
    }

}
