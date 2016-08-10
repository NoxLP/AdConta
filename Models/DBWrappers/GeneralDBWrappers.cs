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
    public sealed class PersonaDBWrapper : aDirectDBWrapper<Persona>
    {
        protected override object SelectQuery(IDbConnection con, int? id, bool all = false, bool maxId = false)
        {
            if(!all && !maxId)
                return con.Query("SELECT * FROM personasgeneral WHERE Id=@IdParam", new { IdParam = id }).FirstOrDefault();
            if(all)
                return con.Query("SELECT * FROM personasgeneral").FirstOrDefault();

            return con.Query("SELECT Id FROM personasgeneral WHERE id=(SELECT max(id) FROM table)").FirstOrDefault();
        }
        protected override Persona GetObjModelFromDynamic(ref dynamic dyn)
        {
            Persona persona = null;

            try
            {
                persona = new Persona(dyn.Id, dyn.NIF, dyn.Nombre);
                persona.EsPropietario = dyn.EsPropietario;
                persona.EsPagador = dyn.EsPagador;
                persona.EsCopropietario = dyn.EsCopropietario;
                persona.Direccion = new sDireccionPostal(
                    dyn.TipoVia,
                    dyn.Direccion,
                    dyn.CP,
                    dyn.Localidad,
                    dyn.Provincia);
                persona.CuentaBancaria = new CuentaBancaria();
                persona.CuentaBancaria.AccountNumber = dyn.CuentaBancariaPersona;
                persona.Telefono1 = new sTelefono(dyn.Telefono, (TipoTelefono)dyn.TipoTelefono);
                persona.Telefono2 = new sTelefono(dyn.Telefono2, (TipoTelefono)dyn.TipoTelefono2);
                persona.Telefono3 = new sTelefono(dyn.Telefono3, (TipoTelefono)dyn.TipoTelefono3);
                persona.Fax = dyn.Fax;
                persona.Email = dyn.Email;
                persona.Notas = (string)dyn.Notas;
            }
            catch (Exception err)
            {
                MessageBox.Show("Error creating objModel Persona at PersonaDBWrapper.GetObjModelFromDynamic: "
                    + err.ToString());
            }

            return persona;
        }
        protected override int InsertQuery(IDbConnection con, ref Persona objModel)
        {
            return con.Execute(@"INSERT INTO personasgeneral(
NIF,Nombre,EsPropietario,EsPagador,EsCopropietario,TipoVia,Direccion,
CP,Localidad,Provincia,CuentaBancariaPersona,Telefono,TipoTelefono,
Telefono2,TipoTelefono2,Telefono3,TipoTelefono3,Fax,Email,Notas)
VALUES(@nif,@nombre,@esProp,@esPag,@esCop,@tipoVia,@direccion,
@cp,@localidad,@provincia,@cuenta,@telef,@tipoTelef,
@telef2,@tipoTelef2,@telef3,@tipoTelef3,@fax,@email,@notas)",
                new {
                    nif = objModel.NIF.NIF,
                    nombre = objModel.Nombre,
                    esProp = objModel.EsPropietario,
                    esPag = objModel.EsPagador,
                    esCop = objModel.EsCopropietario,
                    tipoVia = objModel.Direccion.TipoVia,
                    direccion = objModel.Direccion.Direccion,
                    cp = objModel.Direccion.CP,
                    localidad = objModel.Direccion.Localidad,
                    provincia = objModel.Direccion.Provincia,
                    cuenta = objModel.CuentaBancaria.AccountNumber,
                    telef = objModel.Telefono1.Numero,
                    tipoTelef = (int)objModel.Telefono1.Tipo,
                    telef2 = objModel.Telefono2.Numero,
                    tipoTelef2 = (int)objModel.Telefono2.Tipo,
                    telef3 = objModel.Telefono3.Numero,
                    tipoTelef3 = (int)objModel.Telefono3.Tipo,
                    fax = objModel.Fax,
                    email = objModel.Email,
                    notas = objModel.Notas
                });
        }
        protected override int InsertQuery(IDbConnection con, ref IEnumerable<Persona> objModel)
        {
            return 0;
        }
        protected override int UpdateQuery(IDbConnection con, ref Persona objModel)
        {
            return 0;
        }
        protected override int RemoveQuery(IDbConnection con, ref Persona objModel)
        {
            return 0;
        }
        protected override int RemoveQuery(IDbConnection con, ref IEnumerable<Persona> objModel)
        {
            return 0;
        }
    }

    public sealed class ConceptoDBWrapper : aPOCODBWrapper<Concepto>
    {
        public override int? FindMaxId()
        {
            using (SqlConnection con = new SqlConnection(this._strCon))
            {
                con.Open();
                int? max = con.Query<int?>("SELECT Id FROM conceptoscuotas WHERE id=(SELECT max(id) FROM table)").FirstOrDefault();
                con.Close();
                return max;
            }
        }
    }

    public sealed class EjercicioDBWrapper : aDirectDBWrapper<Ejercicio>
    {
        protected override object SelectQuery(IDbConnection con, int? id, bool all = false, bool maxID = false)
        {

        }
        protected override Ejercicio GetObjModelFromDynamic(ref dynamic dyn)
        {

        }
        protected override int InsertQuery(IDbConnection con, ref Ejercicio objModel)
        {

        }
        protected override int InsertQuery(IDbConnection con, ref IEnumerable<Ejercicio> objModel)
        {

        }
        protected override int UpdateQuery(IDbConnection con, ref Ejercicio objModel)
        {

        }
        protected override int RemoveQuery(IDbConnection con, ref Ejercicio objModel)
        {

        }
        protected override int RemoveQuery(IDbConnection con, ref IEnumerable<Ejercicio> objModel)
        {

        }
    }
}
