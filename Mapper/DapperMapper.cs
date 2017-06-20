using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Collections;

namespace AdConta.Models
{
    public class DapperMapper<T>
    {
        public DapperMapper()
        {
            this.TType = typeof(T);
            this.pInfos = this.TType.GetProperties().Where(x => x.CanWrite && x.GetSetMethod(true).IsPublic).ToArray();
        }

        #region fields
        private readonly List<string> _CustomNamespaces = new List<string>()
        {
            "AdConta",
            "AdConta.Models",
            "AdConta.ViewModel",
            "ModuloContabilidad",
            "ModuloContabilidad.ObjModels",
            "ModuloGestion",
            "ModuloGestion.ObjModels"
        };
        #endregion

        #region properties
        public PropertyInfo[] pInfos { get; private set; }
        public Type TType { get; private set; }
        #endregion

        #region helpers
        /// <summary>
        /// Get object with parameterless constructor, WITHOUT setting any property that does not be setted in the constructor.
        /// </summary>
        /// <returns></returns>
        public T NewObject()
        {
            try
            {
                ConstructorInfo constOK = this.TType.GetConstructor(Type.EmptyTypes);

                return (T)constOK.Invoke(Type.EmptyTypes);
            }
            catch (Exception err)
            {
                throw new CustomException_Mapper("Creating new object in Mapper.NewObject() failed.", err);
            }
        }
        /// <summary>
        /// Get object using constructor that match the given types, WITHOUT setting any property that does not be setted in the constructor.
        /// </summary>
        /// <param name="paramsTypes"></param>
        /// <param name="constructorParams"></param>
        /// <returns></returns>
        public T NewObject(ref Type[] paramsTypes, object[] constructorParams)
        {
            try
            {
                ConstructorInfo constOK = this.TType.GetConstructor(paramsTypes);

                return (T)constOK.Invoke(constructorParams);
            }
            catch (Exception err)
            {
                throw new CustomException_Mapper(
                    "Creating new object in Mapper.NewObject(Type[] paramsTypes, params object[] constructorParams) failed.",
                    err);
            }
        }
        /// <summary>
        /// Set properties that are built-in(built-in = not defined in predefined namespaces in this._CustomNamespaces), AND are not IEnumerable except string.
        /// TODO: Are there more "built-in" types that inherit from IEnumerable apart from string?
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="pInfos"></param>
        /// <param name="values"></param>
        private void SetPublicBuiltInTypesProperties(ref T obj, PropertyInfo[] pInfos, ref dynamic values)
        {
            try
            {
                var query = values as IDictionary<string, object>;

                foreach (PropertyInfo pInfo in pInfos)
                {
                    Type propType = pInfo.PropertyType;
                    //If property is an IEnumerable, except string(which inherits from IEnumerable),
                    //OR it's custom type(afaik there're no way to know this, so it just checks if property type namespace is contained in mapper dictionary)
                    //------
                    //Si la propiedad es un IEnumerable, excepto una string(hereda de IEnumerable por defecto),
                    //ó es un tipo custom(ya que no hay manera de saber esto, se comprueba que no es de un namespace custom de los del diccionario)
                    if ((typeof(IEnumerable).IsAssignableFrom(propType) && !typeof(string).IsAssignableFrom(propType))
                        || this._CustomNamespaces.Contains(propType.Namespace))
                        continue;

                    pInfo.SetValue(obj, query[pInfo.Name]);
                }
            }
            catch (Exception err)
            {
                throw new CustomException_Mapper(
                    "Setting properties in Mapper.SetPublicBuiltInTypesProperties(ref T obj, ref PropertyInfo[] pInfos, ref dynamic values) failed.",
                    err);
            }
        }
        #endregion

        #region public methods
        #region GetValuesFromDynamic
        /// <summary>
        /// Get dyn's properties values that match with names in the Enumerable.
        /// </summary>
        /// <param name="dyn"></param>
        /// <param name="names"></param>
        /// <returns>Prepared for using directly as constructor parameters.</returns>
        public object[] GetValuesFromDynamic(dynamic dyn, IEnumerable<string> names)//, IEnumerable<int> order)
        {
            try
            {
                var d = dyn as IDictionary<string, object>;

                /*//Filter possible other object's properties(JOINS etc)
                IEnumerable<object> dFiltered = d
                    .Where(kvp => names.Contains(kvp.Key))//TODO: OJO ¿mantiene el orden original?
                    .Select(kvp => kvp.Value);*/
                object[] result = new object[names.Count()];

                int i = 0;
                foreach (string name in names)
                {
                    result[i] = d[name];
                }

                return result;
            }
            catch (Exception err)
            {
                throw new CustomException_Mapper("Error getting values in Mapper.object[] GetValuesFromDynamic(dynamic dyn, IEnumerable<string> names).",
                    err);
            }
        }
        /// <summary>
        /// Get each dyn's properties values that match with names in the Enumerable.
        /// </summary>
        /// <param name="dyn"></param>
        /// <param name="names"></param>
        /// <returns>First dimension = each dynamic object. Second dimension = values of correspondent dynamic object's properties. 
        /// Prepared for using directly as constructor parameters.</returns>
        public object[][] GetValuesFromDynamic(IEnumerable<dynamic> dyn, IEnumerable<string> names)//, IEnumerable<int> order)
        {
            try
            {
                object[][] result = new object[dyn.Count()][];
                var d = dyn.Select(x => x as IDictionary<string, object>).ToArray();//dyn as IDictionary<string, object>;
                for (int j = 0; j < dyn.Count(); j++)
                {
                    /*//Filter possible other object's properties(JOINS etc)
                    object[] dFiltered = d[j]
                        .Where(kvp => names.Contains(kvp.Key))//TODO: OJO ¿mantiene el orden original?
                        .Select(kvp => kvp.Value)
                        .ToArray();*/
                    object[] result_j = new object[names.Count()];

                    int i = 0;
                    foreach (string name in names)
                    {
                        result_j[i] = d[j][name];
                    }

                    result[j] = result_j;
                }
                return result;
            }
            catch (Exception err)
            {
                throw new CustomException_Mapper(
                    "Error getting values in Mapper.object[][] GetValuesFromDynamic(IEnumerable<dynamic> dyn, IEnumerable<string> names).",
                    err);
            }
        }
        #endregion

        #region GetObject
        /// <summary>
        /// Get object with parameterless constructor, with public built-in properties setted with values in queryResult, setting them by name matches.
        /// </summary>
        /// <param name="queryResult"></param>
        /// <returns></returns>
        public T GetObject(IEnumerable<dynamic> queryResult, int dynamicIndex = -1)
        {
            try
            {
                T obj = NewObject();
                dynamic dyn = (dynamicIndex == -1) ? queryResult.First() : (queryResult.ToArray())[dynamicIndex];
                SetPublicBuiltInTypesProperties(ref obj, this.pInfos, ref dyn);

                return obj;
            }
            catch (Exception err)
            {
                throw new CustomException_Mapper(
                    "Error creating object in Mapper.GetObject(IEnumerable<dynamic> queryResult, int dynamicIndex = -1).",
                    err);
            }
        }
        /// <summary>
        /// Get object using constructor that match the given types, with public built-in properties setted with values in queryResult, setting them by name matches.
        /// </summary>
        /// <param name="queryResult"></param>
        /// <param name="paramsTypes"></param>
        /// <param name="constructorParams"></param>
        /// <returns></returns>
        public T GetObject(IEnumerable<dynamic> queryResult, Type[] paramsTypes, object[] constructorParams, int dynamicIndex = -1)
        {
            try
            {
                T obj = NewObject(ref paramsTypes, constructorParams);
                dynamic dyn = (dynamicIndex == -1) ? queryResult.First() : (queryResult.ToArray())[dynamicIndex];
                SetPublicBuiltInTypesProperties(ref obj, this.pInfos, ref dyn);

                return obj;
            }
            catch (Exception err)
            {
                throw new CustomException_Mapper(
                    "Error creating object in Mapper.GetObject(IEnumerable<dynamic> queryResult, Type[] paramsTypes, object[] constructorParams, int dynamicIndex = -1).",
                    err);
            }
        }
        /// <summary>
        /// Get object with parameterless constructor, with public built-in properties setted with values in queryResult, setting them by name matches.
        /// Apply Action mapProperties to ne object before returning(create nested objects via nested-object's mapper).
        /// </summary>
        /// <param name="queryResult"></param>
        /// <param name="mapProperties"></param>
        /// <param name="dynamicIndex"></param>
        /// <returns></returns>
        public T GetObject(IEnumerable<dynamic> queryResult, Action<T> mapProperties, int dynamicIndex = -1)
        {
            try
            {
                T obj = GetObject(queryResult, dynamicIndex);

                mapProperties(obj);

                return obj;
            }
            catch (Exception err)
            {
                throw new CustomException_Mapper(
                    "Error creating object in Mapper.GetObject(IEnumerable<dynamic> queryResult, Action<T> mapProperties, int dynamicIndex = -1).",
                    err);
            }
        }
        /// <summary>
        /// Get object using constructor that match the given types, with public built-in properties setted with values in queryResult, setting them by name matches.
        /// Apply Action mapProperties to ne object before returning(create nested objects via nested-object's mapper).
        /// </summary>
        /// <param name="queryResult"></param>
        /// <param name="paramsTypes"></param>
        /// <param name="constructorParams"></param>
        /// <param name="mapProperties"></param>
        /// <param name="dynamicIndex"></param>
        /// <returns></returns>
        public T GetObject(IEnumerable<dynamic> queryResult, Type[] paramsTypes, object[] constructorParams, Action<T> mapProperties, int dynamicIndex = -1)
        {
            try
            {
                T obj = GetObject(queryResult, paramsTypes, constructorParams, dynamicIndex);

                mapProperties(obj);

                return obj;
            }
            catch (Exception err)
            {
                throw new CustomException_Mapper(
                    "Error creating object in Mapper.GetObject(IEnumerable<dynamic> queryResult, Type[] paramsTypes, object[] constructorParams, Action<T> mapProperties, int dynamicIndex = -1).",
                    err);
            }
        }
        #endregion

        #region GetObjectList(IEnumerable<dynamic>)
        /// <summary>
        /// Get list of objects with parameterless constructor from list of dynamics.
        /// </summary>
        /// <param name="queryResult"></param>
        /// <returns></returns>
        public IEnumerable<T> GetObjectList(IEnumerable<dynamic> queryResult)
        {
            try
            {
                List<T> result = new List<T>();

                foreach (dynamic dyn in queryResult)
                {
                    dynamic d = dyn;
                    T obj = NewObject();
                    SetPublicBuiltInTypesProperties(ref obj, this.pInfos, ref d);
                    result.Add(obj);
                }

                return result;
            }
            catch (Exception err)
            {
                throw new CustomException_Mapper("Error creating object list in Mapper.GetObjectList(IEnumerable<dynamic> queryResult).",
                    err);
            }
        }
        /// <summary>
        /// Get list of objects with parameterless constructor from list of dynamics, after applying Action mapProperties to each object.
        /// </summary>
        /// <param name="queryResult"></param>
        /// <param name="mapProperties"></param>
        /// <returns></returns>
        public IEnumerable<T> GetObjectList(IEnumerable<dynamic> queryResult, Action<T> mapProperties)
        {
            try
            {
                List<T> result = new List<T>();

                foreach (dynamic dyn in queryResult)
                {
                    dynamic d = dyn;
                    T obj = NewObject();
                    SetPublicBuiltInTypesProperties(ref obj, this.pInfos, ref d);
                    mapProperties(obj);
                    result.Add(obj);
                }

                return result;
            }
            catch (Exception err)
            {
                throw new CustomException_Mapper(
                    "Error creating object list in Mapper.GetObjectList(IEnumerable<dynamic> queryResult).",
                    err);
            }
        }
        /// <summary>
        /// Get list of objects using constructor that match the given types from list of dynamics.
        /// </summary>
        /// <param name="queryResult"></param>
        /// <param name="paramsTypes"></param>
        /// <param name="constructorParams"></param>
        /// <returns></returns>
        public IEnumerable<T> GetObjectList(IEnumerable<dynamic> queryResult, Type[] paramsTypes, object[][] constructorParams)
        {
            try
            {
                List<dynamic> queryIEnum = queryResult.ToList();
                List<T> result = new List<T>();

                for (int i = 0; i < queryIEnum.Count(); i++)
                {
                    dynamic d = queryIEnum[i];
                    T obj = NewObject(ref paramsTypes, constructorParams[i]);
                    SetPublicBuiltInTypesProperties(ref obj, this.pInfos, ref d);
                    result.Add(obj);
                }

                return result;
            }
            catch (Exception err)
            {
                throw new CustomException_Mapper(
                    "Error creating object list in Mapper.GetObjectList(IEnumerable<dynamic> queryResult, Type[] paramsTypes, object[][] constructorParams).",
                    err);
            }
        }
        /// <summary>
        /// Get list of objects using constructor that match the given types from list of dynamics, after applying Action mapProperties to each object.
        /// </summary>
        /// <param name="queryResult"></param>
        /// <param name="paramsTypes"></param>
        /// <param name="constructorParams"></param>
        /// <param name="mapProperties"></param>
        /// <returns></returns>
        public IEnumerable<T> GetObjectList(IEnumerable<dynamic> queryResult, Type[] paramsTypes, object[][] constructorParams, Action<T> mapProperties)
        {
            try
            {
                List<dynamic> queryIEnum = queryResult.ToList();
                List<T> result = new List<T>();

                for (int i = 0; i < queryIEnum.Count(); i++)
                {
                    dynamic d = queryIEnum[i];
                    T obj = NewObject(ref paramsTypes, constructorParams[i]);
                    SetPublicBuiltInTypesProperties(ref obj, this.pInfos, ref d);
                    mapProperties(obj);
                    result.Add(obj);
                }

                return result;
            }
            catch (Exception err)
            {
                throw new CustomException_Mapper(
                    "Error creating object list in Mapper.GetObjectList(IEnumerable<dynamic> queryResult, Type[] paramsTypes, object[][] constructorParams, Action<T> mapProperties).",
                    err);
            }
        }
        #endregion

        #region TODO? : GetObjectList(SQLMapper.GridReader)
        #endregion

        /*public void SetProperties(ref T destObject, IDictionary<string, object> NamesValues)
        {
            try
            {
                Type t = destObject.GetType();
                PropertyInfo[] pInfos = t.GetProperties(BindingFlags.SetProperty | BindingFlags.Public);

                /*bool OK = true;
                foreach(PropertyInfo pInfo in pInfos)
                {
                    string propName = pInfo.Name;
                    OK = OK && NamesValues.ContainsKey(propName) && NamesValues[propName].GetType() == pInfo.GetType();
                    if (!OK) return false;
                }*/
        /*foreach (KeyValuePair<string, object> kvp in NamesValues)
        {
            t.GetProperty(kvp.Key).SetValue(destObject, kvp.Value);
        }
    }
    catch (Exception)
    {
        throw new Exception("Setting properties in Mapper.SetProperties() failed.");
    }
}*/
        #endregion
    }
}
