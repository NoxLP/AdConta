#define DEBUG

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using AdConta.Models;
using ModuloContabilidad.ObjModels;
using ModuloGestion.ObjModels;
using Mapper;

namespace AdConta
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, iAppRepositories
    {
        public App()
        {
            InitializeComponent();

            FrameworkElement.StyleProperty.OverrideMetadata(typeof(Window), new FrameworkPropertyMetadata
            {
                DefaultValue = FindResource(typeof(Window))
            });

            ConfigMappers();
            //Genera lista de objModels en excel
            //NameSpaceObjectsList.NamespaceObjectsList objsList = new NameSpaceObjectsList.NamespaceObjectsList(
            //    new string[] { "ModuloContabilidad.ObjModels", "ModuloGestion.ObjModels", "AdConta.Models" });
            //objsList.PrintTypesWithPropsFields(@"E:\GoogleDrive\Conta\_Diseño\ListaObjetosAutoGenerada2.xlsx", false, false, false);
            //this.PersonasRepository = new PersonaRepository();
        }

        #region repositories
        //public PersonaRepository PersonasRepository { get; private set; }
        #endregion

        protected override void OnStartup(StartupEventArgs e)
        {
            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(
                    System.Windows.Markup.XmlLanguage.GetLanguage(
                    System.Globalization.CultureInfo.CurrentCulture.IetfLanguageTag)));
            base.OnStartup(e);
        }

        private void ConfigMappers()
        {
            string[] namespaces = new string[] { "ModuloContabilidad.ObjModels", "ModuloGestion.ObjModels", "AdConta.Models"};
            MapperConfig mConfig = new MapperConfig(namespaces);
            
            //Configuracion de todos los mappers:
            //Comunidad
            mConfig
                .AddConstructor<Comunidad>(x => new Comunidad(x.Id, x.CIF, x.Baja, x.Nombre, true))
                .AddMemberCreator<Comunidad>("_CuentaBancaria1", x => new CuentaBancaria(x.CuentaBancaria))
                .AddMemberCreator<Comunidad>("_CuentaBancaria2", x => new CuentaBancaria(x.CuentaBancaria2))
                .AddMemberCreator<Comunidad>("_CuentaBancaria3", x => new CuentaBancaria(x.CuentaBancaria3))
                .AddNestedProperty<Comunidad, DireccionPostal>(false, x => x.Direccion)
                .AddNestedProperty<Comunidad, Persona>(false, x => x.Presidente, x => x.Secretario, x => x.Tesorero)
                .AddNestedProperty<Comunidad, HashSet<int>>(false, x => x.Vocales)
                .AddNestedProperty<Comunidad, Ejercicio>(false, x => x.EjercicioActivo)
                .EndConfig<Comunidad>();
            mConfig
                .AddConstructor<ComunidadDLO>(x =>
                {
                    ComunidadDLO instance = new ComunidadDLO();
                    instance.SetProperties(x.Id, x.Codigo, new NIFModel(x.CIF), x.Baja, x.Nombre, x.TipoVia + x.Direccion, x.CuentaBancaria,
                        x.CuentaBancaria2, x.CuentaBancaria3, x.NombrePresidente, x.NombreSecretario, x.NombreTesorero, x.FechaPunteo,
                        x.UltimaFechaBanco);
                    return instance;
                })
                .MapOnlyConstructor<ComunidadDLO>()
                .EndConfig<ComunidadDLO>();
            //Ejercicio
            mConfig.EndConfig<Ejercicio>();
            mConfig
                .AddConstructor<Ejercicio.EjercicioDLO>(x =>
                {
                    Ejercicio.EjercicioDLO instance = new Ejercicio.EjercicioDLO();
                    instance.SetProperties(x.Id, x.FechaComienzo, x.FechaFinal, x.IdOwnerComunidad, x.Cerrado);
                    return instance;
                })
                .MapOnlyConstructor<Ejercicio.EjercicioDLO>()
                .EndConfig<Ejercicio.EjercicioDLO>();
            //Persona
            mConfig
                .AddConstructor<Persona>(x => new Persona(x.Id, x.NIF, x.Nombre, true))
                .AddMemberCreator<Persona>("_CuentaBancaria", x => new CuentaBancaria(x.CuentaBancaria))
                .AddNestedProperty<Persona, DireccionPostal>(false, x => x.Direccion)
                .AddMemberCreator<Persona, sTelefono>(x => x.Telefono1, x => new sTelefono(x.Telefono, x.Tipo))
                .AddMemberCreator<Persona, sTelefono>(x => x.Telefono2, x => new sTelefono(x.Telefono2, x.Tipo2))
                .AddMemberCreator<Persona, sTelefono>(x => x.Telefono3, x => new sTelefono(x.Telefono3, x.Tipo3))
                .EndConfig<Persona>();
            //GrupoCuentas
            mConfig
                .AddConstructor<GrupoCuentas>(x => new GrupoCuentas(x.Id, x.IdOwnerComunidadNullable, x.Grupo))
                .EndConfig<GrupoCuentas>();
            //iGrupoGastos
            mConfig
                .AddInterfaceToObjectCondition<iGrupoGastos>(x => !(bool)x.Aceptado, typeof(GrupoGastos))
                .AddInterfaceToObjectCondition<iGrupoGastos>(x => (bool)x.Aceptado, typeof(GrupoGastosAceptado))
                .EndConfig<iGrupoGastos>();
            //GrupoGastos
            mConfig
                .AddDictionary<GrupoGastos>("_FincasCoeficientes", new string[] { "FincasCoeficientes", "Coeficiente" })
                .AddNestedProperty<GrupoGastos, List<Cuota>>(false, x => x.Cuotas)
                .AddNestedProperty<GrupoGastos>(false, "_Cuentas")
                .EndConfig<GrupoGastos>();
            mConfig
                .AddConstructor<GrupoGastos.GrupoGastosDLO>(x =>
                {
                    GrupoGastos.GrupoGastosDLO instance = new GrupoGastos.GrupoGastosDLO();
                    instance.SetProperties(x.Id, x.IdOwnerComunidad, x.IdPresupuesto, x.Nombre, x.CoeficientesCustom, x.Importe);
                    return instance;
                })
                .MapOnlyConstructor<GrupoGastos.GrupoGastosDLO>()
                .EndConfig<GrupoGastos.GrupoGastosDLO>();
            //GrupoGastosAceptado y structs
            mConfig.EndConfig<GrupoGastosAceptado.sDatosFincaGGAceptado>();
            mConfig.EndConfig<GrupoGastosAceptado.sDatosCuotaGGAceptado>();
            mConfig.EndConfig<GrupoGastosAceptado.sDatosCuentaGGAceptado>();
            mConfig
                .AddNestedProperty<GrupoGastosAceptado>(false, "_Fincas")
                .AddNestedProperty<GrupoGastosAceptado>(false, "_Cuotas")
                .AddNestedProperty<GrupoGastosAceptado>(false, "_Cuentas")
                .EndConfig<GrupoGastosAceptado>();
            //CuentaParaPresupuesto
            mConfig
                .AddNestedProperty<GrupoGastos.CuentaParaPresupuesto, CuentaMayor>(false, x => x.Cuenta)
                .EndConfig<GrupoGastos.CuentaParaPresupuesto>();
            //Presupuesto
            mConfig
                .AddConstructor<Presupuesto>(x => 
                    new Presupuesto(x.Id, x.IdOwnerComunidad, x.IdOwnerEjercicio, x.Codigo, x.Aceptado, (TipoRepartoPresupuesto)x.TipoReparto))
                .AddNestedProperty<Presupuesto>(true, "_GruposDeGasto")
                .EndConfig<Presupuesto>();
            mConfig
                .AddConstructor<Presupuesto.PresupuestoDLO>(x =>
                {
                    Presupuesto.PresupuestoDLO instance = new Presupuesto.PresupuestoDLO();
                    instance.SetProperties(x.Id, x.IdOwnerComunidad, x.Titulo, x.Total, x.Aceptado, (TipoRepartoPresupuesto)x.TipoReparto);
                    return instance;
                })
                .MapOnlyConstructor<Presupuesto.PresupuestoDLO>()
                .EndConfig<Presupuesto.PresupuestoDLO>();
            //ObservableApuntesList
            mConfig
                .AddConstructor<ObservableApuntesList>(x =>
                {
                    var store = new MapperStore();
                    DapperMapper<Apunte> mapper = (DapperMapper<Apunte>)store.GetMapper(typeof(Apunte));
                    List<Apunte> lista = mapper.Map(x, "Id", false); //Obtiene lista de apuntes con sobrecarga de Map()

                    return new ObservableApuntesList(lista.First().Asiento, lista); //crea objeto a partir de esa lista
                })
                .MapOnlyConstructor<ObservableApuntesList>()
                .EndConfig<ObservableApuntesList>();
            //Apunte
            mConfig
                .AddNestedProperty<Apunte>(false, "_Asiento")
                .AddMemberCreator<Apunte>("_DebeHaber", x => (DebitCredit)x.DebeHaber)
                .EndConfig<Apunte>();
            //Asiento
            mConfig
                .AddConstructor<Asiento>(x => new Asiento(x.Id, x.IdOwnerComunidad, x.FechaValor))
                .AddNestedProperty<Asiento, ObservableApuntesList>(false, x => x.Apuntes)
                .AddIgnoreProperty<Asiento>("Item")
                .EndConfig<Asiento>();
            //Gasto-Pago-GastosPagosBase
            mConfig
                .AddPrefixes<GastosPagosBase.sImporteCuenta>(new string[] { "acreedora_", "deudora_"})
                .EndConfig<GastosPagosBase.sImporteCuenta>();
            mConfig
                .AddConstructor<GastosPagosBase>(x => new GastosPagosBase(x.Id, x.IdOwnerComunidad, x.IdProveedor, x.IdOwnerFactura, x.Fecha))
                .AddMemberCreator<GastosPagosBase>("_CuentasAcreedoras", x=>
                {
                    MapperStore store = new MapperStore();
                    DapperMapper<GastosPagosBase.sImporteCuenta> mapper =
                        (DapperMapper<GastosPagosBase.sImporteCuenta>)store.GetMapper(typeof(GastosPagosBase.sImporteCuenta));
                    IEnumerable<dynamic> distinctAcreedX = mapper.GetDistinctDapperResult(x, false);
                    distinctAcreedX = distinctAcreedX.Select(dyn =>
                    {
                        IDictionary<string, object> dict = dyn as IDictionary<string, object>;
                        var result = new System.Dynamic.ExpandoObject() as IDictionary<string, object>;
                        foreach (KeyValuePair<string, object> kvp in dict)
                            if (kvp.Key.Contains("acreedora_")) result.Add(kvp.Key, kvp.Value);

                        return result;
                    });
                    
                    return mapper.Map<List<GastosPagosBase.sImporteCuenta>>(distinctAcreedX, "Id", false);
                })
                .AddMemberCreator<GastosPagosBase>("_CuentasDeudoras", x =>
                {
                    MapperStore store = new MapperStore();
                    DapperMapper<GastosPagosBase.sImporteCuenta> mapper =
                        (DapperMapper<GastosPagosBase.sImporteCuenta>)store.GetMapper(typeof(GastosPagosBase.sImporteCuenta));
                    IEnumerable<dynamic> distinctDeudX = mapper.GetDistinctDapperResult(x, false);
                    distinctDeudX = distinctDeudX.Select(dyn =>
                    {
                        IDictionary<string, object> dict = dyn as IDictionary<string, object>;
                        var result = new System.Dynamic.ExpandoObject() as IDictionary<string, object>;
                        foreach (KeyValuePair<string, object> kvp in dict)
                            if (kvp.Key.Contains("deudora_")) result.Add(kvp.Key, kvp.Value);

                        return result;
                    });

                    return mapper.Map<List<GastosPagosBase.sImporteCuenta>>(distinctDeudX, "Id", false);
                })
                //.AddIgnoreProperty<GastosPagosBase>("_CuentasAcreedoras")
                //.AddIgnoreProperty<GastosPagosBase>("_CuentasDeudoras")
                //.AddIgnoreProperty<GastosPagosBase, System.Collections.ObjectModel.ReadOnlyCollection<GastosPagosBase.sImporteCuenta>>(
                //    x => x.CuentasAcreedoras)
                //.AddIgnoreProperty<GastosPagosBase, System.Collections.ObjectModel.ReadOnlyCollection<GastosPagosBase.sImporteCuenta>>(
                //    x => x.CuentasDeudoras)
                .EndConfig<GastosPagosBase>();
            mConfig
                .AddConstructor<Gasto>(x => new Gasto(x.Id, x.IdOwnerComunidad, x.IdProveedor, x.IdOwnerFactura, x.Fecha))
                .EndConfig<Gasto>();
            mConfig
                .AddConstructor<Pago>(x => new Pago(x.Id, x.IdOwnerComunidad, x.IdProveedor, x.IdOwnerFactura, x.Fecha))
                .EndConfig<Pago>();
            //GastosPagosList-ReadOnly
            mConfig
                .AddConstructor<GastosPagosList<Gasto>>(x =>
                {
                    MapperStore store = new MapperStore();
                    DapperMapper<Gasto> mapper = (DapperMapper<Gasto>)store.GetMapper(typeof(Gasto));
                    List<Gasto> lista = mapper.Map<List<Gasto>>(x, "Id", false);

                    return new GastosPagosList<Gasto>(lista);
                })
                .MapOnlyConstructor<GastosPagosList<Gasto>>()
                .EndConfig<GastosPagosList<Gasto>>();
            mConfig
                .AddConstructor<GastosPagosList<Pago>>(x =>
                {
                    MapperStore store = new MapperStore();
                    DapperMapper<Pago> mapper = (DapperMapper<Pago>)store.GetMapper(typeof(Pago));
                    List<Pago> lista = mapper.Map<List<Pago>>(x, "Id", false);

                    return new GastosPagosList<Pago>(lista);
                })
                .MapOnlyConstructor<GastosPagosList<Pago>>()
                .EndConfig<GastosPagosList<Pago>>();
            mConfig
                .AddConstructor<ReadOnlyGastosPagosList<Gasto>>(x =>
                {
                    MapperStore store = new MapperStore();
                    DapperMapper<Gasto> mapper = (DapperMapper<Gasto>)store.GetMapper(typeof(Gasto));
                    List<Gasto> lista = mapper.Map<List<Gasto>>(x, "Id", false);

                    return new ReadOnlyGastosPagosList<Gasto>(lista);
                })
                .MapOnlyConstructor<ReadOnlyGastosPagosList<Gasto>>()
                .EndConfig<ReadOnlyGastosPagosList<Gasto>>();
            mConfig
                .AddConstructor<ReadOnlyGastosPagosList<Pago>>(x =>
                {
                    MapperStore store = new MapperStore();
                    DapperMapper<Pago> mapper = (DapperMapper<Pago>)store.GetMapper(typeof(Pago));
                    List<Pago> lista = mapper.Map<List<Pago>>(x, "Id", false);

                    return new ReadOnlyGastosPagosList<Pago>(lista);
                })
                .MapOnlyConstructor<ReadOnlyGastosPagosList<Pago>>()
                .EndConfig<ReadOnlyGastosPagosList<Pago>>();
            //Factura
            mConfig
                .AddNestedProperty<Factura>(false, "_GastosFra")
                .AddIgnoreProperty<Factura, ReadOnlyGastosPagosList<Gasto>>(x => x.GastosFra)
                .AddNestedProperty<Factura>(false, "_PagosFra")
                .AddIgnoreProperty<Factura, ReadOnlyGastosPagosList<Pago>>(x => x.PagosFra)
                .AddMemberCreator<Factura, int?>(x => x.IdOwnerProveedor, x => x.IdProveedor)
                .AddMemberCreator<Factura, TipoPagoFacturas>(x => x.TipoPago, x => (TipoPagoFacturas)x.TipoPago)
                .AddIgnoreProperty<Factura, decimal>(x => x.TotalImpuestos)
                .EndConfig<Factura>();
            mConfig
                .AddConstructor<Factura.FacturaDLO>(x =>
                {
                    Factura.FacturaDLO instance = new Factura.FacturaDLO();
                    instance.SetProperties(x.Id, x.IdProveedor, x.IdOwnerComunidad, x.NFactura, x.Fecha, x.Concepto, x.Total, x.Pendiente, (TipoPagoFacturas)x.TipoPago);
                    return instance;
                })
                .MapOnlyConstructor<Presupuesto.PresupuestoDLO>()
                .EndConfig<Presupuesto.PresupuestoDLO>();
            //CuentaMayor
            mConfig
                .AddConstructor<CuentaMayor>(x => new CuentaMayor(x.Codigo, x.Id, x.IdOwnerComunidad, x.IdOwnerEjercicio, x.Nombre))
                .MapOnlyConstructor<CuentaMayor>()
                .EndConfig<CuentaMayor>();
            mConfig
                .AddConstructor<CuentaMayor.CuentaMayorDLO>(x =>
                {
                    CuentaMayor.CuentaMayorDLO instance = new CuentaMayor.CuentaMayorDLO();
                    instance.SetProperties(int.Parse(x.Codigo), x.Id, x.IdOwnerComunidad, x.Nombre);
                    return instance;
                })
                .MapOnlyConstructor<CuentaMayor.CuentaMayorDLO>()
                .EndConfig<CuentaMayor.CuentaMayorDLO>();
            //Proveedor
            mConfig
                .AddConstructor<Proveedor>(x => new Proveedor(x.Id, x.IdPersona, x.NIF, x.Nombre, true))
                .AddNestedProperty<Proveedor>(false, "_CuentaContableGasto")
                .AddNestedProperty<Proveedor>(false, "_CuentaContablePago")
                .AddNestedProperty<Proveedor>(false, "_CuentaContableProveedor")
                .AddMemberCreator<Proveedor, TipoPagoFacturas>(x => (TipoPagoFacturas)x.DefaultTipoPagoFacturas, x => (TipoPagoFacturas)x.DefaultTipoPagoFacturas)
                .EndConfig<Proveedor>();
            mConfig
                .AddConstructor<Proveedor.ProveedorDLO>(x =>
                {
                    Proveedor.ProveedorDLO instance = new Proveedor.ProveedorDLO();
                    instance.SetProperties(x.Id, x.IdOwnerComunidad, x.Nombre, x.NIF, string.Concat(x.TipoVia, " ", x.Direccion), x.CuentaBancaria,
                        x.Telefono, x.Email, x.RazonSocial, x.CuentaContableGasto, x.CuentaContablePago, x.CuentaContableProveedor);
                    return instance;
                })
                .MapOnlyConstructor<Proveedor.ProveedorDLO>()
                .EndConfig<Proveedor.ProveedorDLO>();

        }

        /*protected override void OnExit(ExitEventArgs e)
        {
            this._AppModelControl.UnsubscribeModelControlEvents();
            base.OnExit(e);
        }*/
    }
}
