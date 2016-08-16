using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Reflection;
using System.Dynamic;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using AdConta;

namespace AdConta.Models
{
    public sealed class PersonaDBWrapper : aDirectDBWrapper<Persona>
    {
        protected override object SelectQuery(IDbConnection con, int? id, bool all = false, bool maxID = false)
        {
            if(!all && !maxID)
                return con.Query("SELECT * FROM personasgeneral WHERE Id=@IdParam", new { IdParam = id }).FirstOrDefault();
            if(all)
                return con.Query("SELECT * FROM personasgeneral").FirstOrDefault();

            return con.Query("SELECT Id FROM personasgeneral WHERE id=(SELECT max(id) FROM personasgeneral)").FirstOrDefault();
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
                throw new CustomException_DBWrapper("Error creando objModel Persona en PersonaDBWrapper.GetObjModelFromDynamic. Ver inner exception"
                    , err);
            }

            return persona;
        }
        protected override object ValuesObject(ref Persona objModel, bool withId = false)
        {
            if (!withId)
            {
                return new
                {
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
                };
            }
            else
            {
                return new
                {
                    id = objModel.Id,
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
                };
            }
        }
        private object ValuesObject(Persona objModel, bool withId = false)
        {
            return ValuesObject(ref objModel, withId);
        }
        protected override int InsertQuery(IDbConnection con, ref Persona objModel)
        {
            int result = con.Execute(@"INSERT INTO personasgeneral(
NIF,Nombre,EsPropietario,EsPagador,EsCopropietario,TipoVia,Direccion,
CP,Localidad,Provincia,CuentaBancariaPersona,Telefono,TipoTelefono,
Telefono2,TipoTelefono2,Telefono3,TipoTelefono3,Fax,Email,Notas) 
VALUES(@nif,@nombre,@esProp,@esPag,@esCop,@tipoVia,@direccion,
@cp,@localidad,@provincia,@cuenta,@telef,@tipoTelef,
@telef2,@tipoTelef2,@telef3,@tipoTelef3,@fax,@email,@notas)",
                ValuesObject(ref objModel));
            /*
            if (result > 0)
            {
                int id = con.Query<int>("SELECT LAST_INSERT_ID()").FirstOrDefault();
                Persona nueva = new Persona(id, objModel.NIF.NIF, objModel.Nombre, true);
                objModel.CopyId(ref nueva);
            }
            */
            return result;
        }
        /*private KeyValuePair<string, object> NewKVP(string s, object o)
        {
            return new KeyValuePair<string, object>(s, o);
        }*/
        protected override int InsertQuery(IDbConnection con, ref IEnumerable<Persona> objModels)
        {
            var builder = new StringBuilder();
            builder.Append("");
            var values = new ExpandoObject() as IDictionary<string, object>;
            //List<KeyValuePair<string, object>> list;
            int i = 0;

            foreach(Persona p in objModels)
            {
                builder.Append($@"INSERT INTO personasgeneral(
NIF,Nombre,EsPropietario,EsPagador,EsCopropietario,TipoVia,Direccion,
CP,Localidad,Provincia,CuentaBancariaPersona,Telefono,TipoTelefono,
Telefono2,TipoTelefono2,Telefono3,TipoTelefono3,Fax,Email,Notas) 
VALUES(@nif{i},@nombre{i},@esProp{i},@esPag{i},@esCop{i},@tipoVia{i},@direccion{i},
@cp{i},@localidad{i},@provincia{i},@cuenta{i},@telef{i},@tipoTelef{i},
@telef2{i},@tipoTelef2{i},@telef3{i},@tipoTelef3{i},@fax{i},@email{i},@notas{i})" + Environment.NewLine);
                
                //reflection to add values to ExpandoObject
                object v = ValuesObject(p);
                Type type = v.GetType();
                foreach(PropertyInfo pInfo in type.GetProperties())
                    values.Add(pInfo.Name + i.ToString(), pInfo.GetValue(v));

                i++;

                /*list = new List<KeyValuePair<string, object>>()
                {
                    NewKVP($@"nif{i}", p.NIF.NIF),
                    NewKVP(nombre{i}),
                    NewKVP(esProp{i}),
                    NewKVP(esPag{i}),
                    NewKVP(esCop{i}),
                    NewKVP(),
                    NewKVP(),
                    NewKVP(),
                    NewKVP(),
                    NewKVP(),
                    NewKVP(),
                    NewKVP(),
                    NewKVP(),
                };
                values.Add($@"nif{i}", p.NIF.NIF);
                    ,,,,tipoVia{i},direccion{i},
cp{i},localidad{i},provincia{i},cuenta{i},telef{i},tipoTelef{i},
telef2{i},tipoTelef2{i},telef3{i},tipoTelef3{i},fax{i},email{i},notas{i}", 
                p.NIF.NIF,
                p.Nombre,
                p.EsPropietario,);*/
            }

            var qMult = con.QueryMultiple(builder.ToString(), values);

            int result = 0;
            foreach(int read in qMult.Read<int>())
                result += read;

            return result;
        }
        protected override int UpdateQuery(IDbConnection con, ref Persona objModel, List<KeyValuePair<string,string>> dbParamPropsChanged = null)
        {
            if (dbParamPropsChanged == null)
            {
                int result = con.Execute(@"
UPDATE personasgeneral
SET 
NIF=@nif,Nombre=@nombre,EsPropietario=@esProp,EsPagador=@esPag,EsCopropietario=@esCop,TipoVia=@tipoVia,Direccion=@direccion,
CP=@cp,Localidad=@localidad,Provincia=@provincia,CuentaBancariaPersona=@cuenta,Telefono=@telef,TipoTelefono=@tipoTelef,
Telefono2=@telef2,TipoTelefono2=@tipoTelef2,Telefono3=@telef3,TipoTelefono3=@tipoTelef3,Fax=@fax,Email=@email,Notas=@notas 
WHERE Id=@id",
                ValuesObject(ref objModel, true));

                return result;
            }
            else
            {
                var sb = new StringBuilder();
                sb.Append("UPDATE personasgeneral SET ");
                var values = new ExpandoObject() as IDictionary<string, object>;
                var omValues = ValuesObject(ref objModel);
                Type type = omValues.GetType();

                foreach (KeyValuePair<string,string> kvp in dbParamPropsChanged)
                {
                    sb.Append(kvp.Key + "=@" + kvp.Value + ",");
                    values.Add(kvp.Value,
                        type
                        .GetProperties()
                        .TakeWhile(x => x.Name == kvp.Value)
                        .Select(x => x.GetValue(omValues)));
                }
                sb.Remove(sb.Length - 1, 1);
                sb.Append(" WHERE Id=");
                sb.Append(objModel.Id.ToString());

                int result = con.Execute(sb.ToString(), values);
                return result;
            }
        }
        protected override int RemoveQuery(IDbConnection con, ref Persona objModel)
        {
            return con.Execute("DELETE FROM personasgeneral WHERE Id=@id", new { id = objModel.Id });
        }
        protected override int RemoveQuery(IDbConnection con, ref IEnumerable<Persona> objModels)
        {
            var builder = new StringBuilder();
            builder.Append("");
            var values = new ExpandoObject() as IDictionary<string, object>;
            int i = 0;

            foreach(Persona p in objModels)
            {
                builder.Append($"DELETE FROM personasgeneral WHERE Id=@id{i}" + Environment.NewLine);
                values.Add($"id{i}", p.Id);
            }

            var qMult = con.QueryMultiple(builder.ToString(), values);

            int result = 0;
            foreach (int read in qMult.Read<int>())
                result += read;

            return result;
        }
        public override int? FindMaxId()
        {
            using (SqlConnection con = new SqlConnection(base._strCon))
            {
                con.Open();
                var result = SelectQuery(con, null, false, true);                
                bool resultInsert = result != null ? base.Update(new Persona((int)result + 1, null, null, true)) : false;
                int? maxId = resultInsert ? (int?)result : null;
                con.Close();
                return maxId;
            }
        }
    }

    public sealed class ConceptoDBWrapper : aPOCODBWrapper<Concepto>
    {
        public override int? FindMaxId()
        {
            using (SqlConnection con = new SqlConnection(this._strCon))
            {
                con.Open();
                int? max = con.Query<int?>("SELECT Id FROM conceptoscuotas WHERE id=(SELECT max(id) FROM conceptoscuotas)").FirstOrDefault();
                con.Close();
                return max;
            }
        }
    }

    public sealed class EjercicioDBWrapper : aDirectDBWrapper<Ejercicio>
    {
        protected override object SelectQuery(IDbConnection con, int? id, bool all = false, bool maxID = false)
        {
            if (!all && !maxID)
                return con.Query("SELECT * FROM ejercicios WHERE Id=@IdParam", new { IdParam = id }).FirstOrDefault();
            if (all)
                return con.Query("SELECT * FROM ejercicios").FirstOrDefault();

            return con.Query("SELECT Id FROM ejercicios WHERE id=(SELECT max(id) FROM ejercicios)").FirstOrDefault();
        }
        protected override Ejercicio GetObjModelFromDynamic(ref dynamic dyn)
        {
            Ejercicio ejercicio = null;

            try
            {
                ejercicio = new Ejercicio(
                    dyn.Id,
                    new Date(dyn.FechaComienzo),
                    new Date(dyn.FechaFinal),
                    dyn.CodigoComunidad,
                    dyn.Cerrado);
            }
            catch (Exception err)
            {
                throw new CustomException_DBWrapper("Error creando objModel Ejercicio en PersonaDBWrapper.GetObjModelFromDynamic. Ver inner exception"
                    , err);
            }

            return ejercicio;
        }
        protected override object ValuesObject(ref Ejercicio objModel, bool withId = false)
        {
            if (!withId)
            {
                return new
                {
                    fechaCom = objModel.FechaComienzo.GetDateTime(),
                    fechaFinal = objModel.FechaFinal.GetDateTime(),
                    codCdad = objModel.IdOwnerComunidad,
                    cerrado = objModel.Cerrado
                };
            }
            else
            {
                return new
                {
                    id = objModel.Id,
                    fechaCom = objModel.FechaComienzo.GetDateTime(),
                    fechaFinal = objModel.FechaFinal.GetDateTime(),
                    codCdad = objModel.IdOwnerComunidad,
                    cerrado = objModel.Cerrado
                };
            }
        }
        private object ValuesObject(Ejercicio objModel, bool withId = false)
        {
            return ValuesObject(ref objModel, withId);
        }
        protected override int InsertQuery(IDbConnection con, ref Ejercicio objModel)
        {
            int result = con.Execute(@"INSERT INTO ejercicios(
FechaComienzo,FechaFinal,CodigoComunidad,Cerrado) 
VALUES(@fechaCom,@fechaFinal,@codCdad,@cerrado)",
                ValuesObject(ref objModel));
            /*
            if (result > 0)
            {
                int id = con.Query<int>("SELECT LAST_INSERT_ID()").FirstOrDefault();
                Persona nueva = new Persona(id, objModel.NIF.NIF, objModel.Nombre, true);
                objModel.CopyId(ref nueva);
            }
            */
            return result;
        }
        protected override int InsertQuery(IDbConnection con, ref IEnumerable<Ejercicio> objModels)
        {
            var builder = new StringBuilder();
            builder.Append("");
            var values = new ExpandoObject() as IDictionary<string, object>;
            int i = 0;

            foreach (Ejercicio p in objModels)
            {
                builder.Append($@"INSERT INTO ejercicios(
FechaComienzo,FechaFinal,CodigoComunidad,Cerrado) 
VALUES(@fechaCom{i},@fechaFinal{i},@codCdad{i},@cerrado{i})" + Environment.NewLine);

                //reflection to add values to ExpandoObject
                object v = ValuesObject(p);
                Type type = v.GetType();
                foreach (PropertyInfo pInfo in type.GetProperties())
                    values.Add(pInfo.Name + i.ToString(), pInfo.GetValue(v));

                i++;
            }

            var qMult = con.QueryMultiple(builder.ToString(), values);

            int result = 0;
            foreach (int read in qMult.Read<int>())
                result += read;

            return result;
        }
        protected override int UpdateQuery(IDbConnection con, ref Ejercicio objModel, List<KeyValuePair<string, string>> dbParamPropsChanged = null)
        {
            if (dbParamPropsChanged == null)
            {
                int result = con.Execute(@"
UPDATE ejercicios
SET Fechacomienzo=@fechaCom,Fechafinal=@fechaFinal,CodigoComunidad=@codCdad,Cerrado=@cerrado
WHERE Id=@id",
                ValuesObject(ref objModel, true));

                return result;
            }
            else
            {
                var sb = new StringBuilder();
                sb.Append("UPDATE ejercicios SET ");
                var values = new ExpandoObject() as IDictionary<string, object>;
                var omValues = ValuesObject(ref objModel);
                Type type = omValues.GetType();

                foreach (KeyValuePair<string, string> kvp in dbParamPropsChanged)
                {
                    sb.Append(kvp.Key + "=@" + kvp.Value + ",");
                    values.Add(kvp.Value,
                        type
                        .GetProperties()
                        .TakeWhile(x => x.Name == kvp.Value)
                        .Select(x => x.GetValue(omValues)));
                }
                sb.Remove(sb.Length - 1, 1);
                sb.Append(" WHERE Id=");
                sb.Append(objModel.Id.ToString());

                int result = con.Execute(sb.ToString(), values);
                return result;
            }
        }
        protected override int RemoveQuery(IDbConnection con, ref Ejercicio objModel)
        {
            return con.Execute("DELETE FROM ejercicios WHERE Id=@id", new { id = objModel.Id });
        }
        protected override int RemoveQuery(IDbConnection con, ref IEnumerable<Ejercicio> objModels)
        {
            var builder = new StringBuilder();
            builder.Append("");
            var values = new ExpandoObject() as IDictionary<string, object>;
            int i = 0;

            foreach (Ejercicio p in objModels)
            {
                builder.Append($"DELETE FROM ejercicios WHERE Id=@id{i}" + Environment.NewLine);
                values.Add($"id{i}", p.Id);
            }

            var qMult = con.QueryMultiple(builder.ToString(), values);

            int result = 0;
            foreach (int read in qMult.Read<int>())
                result += read;

            return result;
        }
        public override int? FindMaxId()
        {
            using (SqlConnection con = new SqlConnection(base._strCon))
            {
                con.Open();
                var result = SelectQuery(con, null, false, true);
                bool resultInsert = result != null ? base.Update(new Ejercicio((int)result + 1, null, null, 0)) : false;
                int? maxId = resultInsert ? (int?)result : null;
                con.Close();
                return maxId;
            }
        }
    }
}
