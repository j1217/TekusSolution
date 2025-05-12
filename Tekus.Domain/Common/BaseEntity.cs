using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekus.Domain.Common;

/// <summary>
/// Clase base que contiene las propiedades comunes para todas las entidades.
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Identificador único de la entidad.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Fecha de creación de la entidad.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Fecha de última modificación de la entidad.
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}

