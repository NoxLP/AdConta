using System;

namespace AdConta.Models
{
    /// <summary>
    /// Interfaz para todos los objetos con auto código.
    /// </summary>
    public interface iConCodigo { aAutoCodigoBase Codigo { get; } }
    /// <summary>
    /// Interfaz únicamente para el tipo Comunidad. Con código, sin idOwners.
    /// </summary>
    public interface iObjModelConCodigo : iConCodigo { }
    /// <summary>
    /// Interfaz para los tipos que tengan auto código Y IdOwnerComunidad.
    /// </summary>
    public interface iObjModelConCodigoConComunidad : iOwnerComunidad, iObjModelConCodigo { int GetOwnerId(); }
    /// <summary>
    /// Interfaz para los objetos que tengan auto código, IdOwnerComunidad e IdOwnerEjercicio.
    /// </summary>
    public interface iObjModelConCodigoConComunidadYEjercicio : iConCodigo, iOwnerComunidad, iOwnerEjercicio { Tuple<int, int> GetOwnersIds(); }
}
