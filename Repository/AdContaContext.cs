using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using AdConta.Models;
using ModuloContabilidad.ObjModels;
using ModuloGestion.ObjModels;

namespace EFDBContext
{
    class AdContaContext : DbContext
    {
        #region general
        public virtual DbSet<Persona> Personas { get; set; }
        public virtual DbSet<Concepto> Conceptos { get; set; }
        public virtual DbSet<Ejercicio> Ejercicios { get; set; }
        public virtual DbSet<DireccionPostalCompleta> Direcciones { get; set; }
        //OJO FALTA COMUNIDAD
        #endregion

        #region contabilidad
        public virtual DbSet<Proveedor> Proveedores { get; set; }
        public virtual DbSet<Apunte> Apuntes { get; set; }
        public virtual DbSet<Factura> Facturas { get; set; }
        public virtual DbSet<Gasto> Gastos { get; set; }
        public virtual DbSet<GruposCuentas> GruposCuentas { get; set; }
        public virtual DbSet<CuentaMayor> CuentasMayor { get; set; }
        public virtual DbSet<Pago> Pagos { get; set; }
        #endregion

        #region gestion
        public virtual DbSet<Cuota> Cuotas { get; set; }
        public virtual DbSet<Devolucion> Devoluciones { get; set; }
        public virtual DbSet<Finca> Fincas { get; set; }
        public virtual DbSet<Recibo> Recibos { get; set; }
        public virtual DbSet<Cobro> Cobros { get; set; }
        public virtual DbSet<EntACta> EntregasACta { get; set; }
        #endregion
    }
}
