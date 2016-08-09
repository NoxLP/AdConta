using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace AdConta.Models
{
    public sealed class PersonaDBWrapper : aComplexDBWrapper<Persona>
    {
        public PersonaDBWrapper()
        {

        }

        protected readonly string _strCon = GlobalSettings.Properties.Settings.Default.conta1ConnectionString;

        protected override Func<IDbConnection, dynamic> SELECTQuery
        {
            get
            {
                return this.SelectQuery(con);
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        private dynamic SelectQuery(IDbConnection con)
        {
            return con.Query("");
        }

        #region public methods
        public Persona Get(int id)
        {

        }
        public IEnumerable<Persona> GetAll()
        { return null; }
        public bool Create(Persona objModel)
        { return false; }
        public bool CreateRange(IEnumerable<Persona> objModels)
        { return false; }
        public bool Remove(Persona objModel)
        { return false; }
        public bool RemoveRange(IEnumerable<Persona> objModels)
        { return false; }
        public bool Update(Persona objModel)
        { return false; }
        public IEnumerable<Persona> Query(string SQLCommand, object param = null)
        { return null; }
        public int? FindMaxId()
        { return null; }
        #endregion
    }

    public sealed class ConceptoDBWrapper : aDBWrapper<Concepto>
    {
        
    }

    public sealed class EjercicioDBWrapper : aDBWrapper<Ejercicio>
    {
        public EjercicioDBWrapper()
        {

        }

        #region public methods
        public bool Create(Ejercicio objModel)
        { return false; }
        public ErrorTryingDBRange CreateRange(IEnumerable<Ejercicio> objModels)
        { return ErrorTryingDBRange.DB_Other; }
        public bool Remove(Ejercicio objModel)
        { return false; }
        public ErrorTryingDBRange RemoveRange(IEnumerable<Ejercicio> objModels)
        { return ErrorTryingDBRange.DB_Other; }
        public bool Update(Ejercicio objModel)
        { return false; }
        public Ejercicio Find(int id)
        { return null; }
        public IEnumerable<Ejercicio> Find(string SQLCommand)
        { return null; }
        public IEnumerable<Ejercicio> GetAll()
        { return null; }
        #endregion
    }
}
