using canchasfutbol.Domain.Common;
using System;
using System.Collections.Generic;

namespace canchasfutbol.Domain.Models;

public partial class Cancha : DomainBaseModel
{
   

    public string Name { get; set; } = null!;

    public int Tipo { get; set; }

    public decimal Preciohora { get; set; }

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
