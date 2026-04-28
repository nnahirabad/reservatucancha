using canchasfutbol.Domain.Common;
using System;
using System.Collections.Generic;

namespace canchasfutbol.Domain.Models;

public partial class Usuario : DomainBaseModel
{
    
    public string? Username { get; set; }

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Rol { get; set; }

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
