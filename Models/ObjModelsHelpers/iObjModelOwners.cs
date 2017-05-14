using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdConta.Models
{
    public interface iOwnerComunidad
    {
        int IdOwnerComunidad { get; }
    }

    public interface iOwnerComunidadNullable
    {
        int? IdOwnerComunidad { get; }
    }

    public interface iOwnerEjercicio
    {
        int IdOwnerEjercicio { get; }
    }

    public interface iOwnerPersona
    {
        int IdOwnerPersona { get; }
    }

    public interface iOwnerFinca
    {
        int IdOwnerFinca { get; }
    }

    public interface iOwnerProveedor
    {
        int? IdOwnerProveedor { get; }
    }

    public interface iOwnerPropietario
    {
        int IdOwnerPropietario { get; }
    }

    public interface iOwnerCuota
    {
        int IdOwnerCuota { get; }
    }

    public interface iOwnerRecibo
    {
        int IdOwnerRecibo { get; }
    }

    public interface iOwnerFactura
    {
        int? IdOwnerFactura { get; }
    }

    public interface iOwnerDevolucion
    {
        int IdOwnerDevolucion { get; }
    }

    public interface iOwnerPresupuesto
    {
        int IdOwnerPresupuesto { get; }
    }

    public interface iOwnerGrupoGasto
    {
        int IdOwnerGrupoGasto { get; }
    }
}
