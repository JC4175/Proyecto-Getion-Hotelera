using System;

namespace GestionHotelera.Modelos
{
    public class Reserva
    {
        public string Codigo;
        public string Cliente;
        public string Documento;
        public int Habitacion;
        public string TipoHabitacion;
        public double PrecioNoche;
        public DateTime FechaEntrada;
        public DateTime FechaHoraEntradaProgramada;
        public DateTime FechaHoraSalidaProgramada;
        public DateTime FechaHoraCheckIn;
        public DateTime FechaHoraCheckOut;
        public int Noches;
        public double TotalPagar;
        public string Estado;
        public string UsuarioRegistro;
        public string UsuarioCheckIn;
        public string UsuarioCheckOut;
    }
}
