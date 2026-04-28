using canchasfutbol.Domain.Common;
using Npgsql.Internal;
using System;
using System.Collections.Generic;

namespace canchasfutbol.Domain.Models;

public partial class Reserva : DomainBaseModel
{
    public enum EstadoReserva
    {
        
        Activa,
        Cancelada
    }

    public Guid UserId { get; private set; }

    public Guid CanchaId { get; private set; }

    public DateOnly Fecha { get; private set; }

    public TimeOnly HoraInicio { get; private set; }

    public TimeOnly HoraFin { get; private set; }

    public decimal Costo { get; private set; }

    public virtual Cancha Cancha { get; private  set; } = null!;

    public virtual Usuario User { get; private set; } = null!;

    public EstadoReserva Estado { get; private set; } = EstadoReserva.Activa;


    private Reserva() { }

    public static Reserva Create(
        Guid userId, Guid canchaId, DateOnly fecha, TimeOnly horaInicio, TimeOnly horaFin, decimal precioHora)
    {
        if (horaFin <= horaInicio)
            throw new Exception("La hora final debe ser mayor a la hora de inicio");

        var totalHoras = (horaFin.ToTimeSpan() - horaInicio.ToTimeSpan());

        return new Reserva()
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            CanchaId = canchaId,
            Fecha = fecha,
            HoraInicio = horaInicio,
            HoraFin = horaFin,
            Costo = (decimal)totalHoras.TotalHours * precioHora
        };
    } 

    public void Cancelar()
    {
               if (Estado == EstadoReserva.Cancelada)
            throw new Exception("La reserva ya está cancelada.");
        Estado = EstadoReserva.Cancelada;
    }

    public void CambiarHorario(TimeOnly nuevaHoraInicio, TimeOnly nuevaHoraFin, decimal precioHora)
    {
        if (nuevaHoraFin <= nuevaHoraInicio)
            throw new Exception("La hora final debe ser mayor a la hora de inicio");

        var totalHoras = (nuevaHoraFin.ToTimeSpan() - nuevaHoraInicio.ToTimeSpan());

        HoraInicio = nuevaHoraInicio;
        HoraFin = nuevaHoraFin;
        Costo = (decimal)totalHoras.TotalHours * precioHora;
    }

    public void CambiarFecha(DateOnly nuevaFecha)
    {
        Fecha = nuevaFecha;
    }
     public void CambiarCancha(Guid nuevaCanchaId)
    {
        CanchaId = nuevaCanchaId;
    }

    public void Reprogramar(DateOnly nuevaFecha, TimeOnly nuevaHoraInicio, TimeOnly nuevaHoraFin, decimal precioHora)
    {

        CambiarFecha(nuevaFecha);
        CambiarHorario(nuevaHoraInicio, nuevaHoraFin, precioHora);
    }
}
