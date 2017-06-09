using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AdConta.Models;
using ModuloContabilidad.ObjModels;
using ModuloGestion.ObjModels;

namespace Repository
{
    public interface iAppRepositories
    {
        #region general
        PersonaRepository PersonaRepo { get; }

        /*Repository<Concepto> ConceptosRepository { get; }
        Repository<Ejercicio> EjerciciosRepository { get; }
        #endregion
        #region gestion
        Repository<Propietario> PropietariosRepository { get; }
        Repository<Cuota> CuotasRepository { get; }
        Repository<Devolucion> DevolucionesRepository { get; }
        Repository<Finca> FincasRepository { get; }
        Repository<Recibo> RecibosRepository { get; }
        Repository<Cobro> CobrosRepository { get; }
        Repository<EntACta> EntregasACtaRepository { get; }
        #endregion
        #region contabilidad
        Repository<Proveedor> ProveedoresRepository { get; }
        Repository<Apunte> ApuntesRepository { get; }
        Repository<aAsiento> AsientosRepository { get; }
        Repository<Factura> FacturasRepository { get; }
        Repository<Gasto> GastosRepository { get; }
        Repository<GruposCuentas> GruposCuentasRepository { get; }
        Repository<CuentaMayor> CuentasMayorRepository { get; }
        Repository<Pago> PagosRepository { get; }*/
        #endregion

        #region contabilidad
        #endregion

        #region gestion
        #endregion
    }
}
