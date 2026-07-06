using System;

namespace ProyectoFinal_T3_AVANCE_SIMPLE
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

    public class Cliente
    {
        public string Codigo;
        public string Nombre;
        public int Puntos;
    }

    public class Turno
    {
        public string Codigo;
        public string Empleado;
        public string Area;
    }

    public class Solicitud
    {
        public string Codigo;
        public string Cliente;
        public string Detalle;
    }

    public class Habitacion
    {
        public int Numero;
        public string Tipo;
        public double Precio;
        public bool Disponible;
        public string Estado;
    }

    public class NodoReserva
    {
        public Reserva Dato;
        public NodoReserva Siguiente;
    }

    public class NodoCliente
    {
        public Cliente Dato;
        public NodoCliente Siguiente;
        public NodoCliente Anterior;
    }

    public class NodoTurno
    {
        public Turno Dato;
        public NodoTurno Siguiente;
    }

    public class NodoPila
    {
        public string Accion;
        public NodoPila Siguiente;
    }

    public class NodoSolicitud
    {
        public Solicitud Dato;
        public NodoSolicitud Siguiente;
    }

    public class NodoArbol
    {
        public string Cargo;
        public NodoArbol Izquierda;
        public NodoArbol Derecha;

        public NodoArbol(string cargo)
        {
            Cargo = cargo;
        }
    }

    public class NodoABB
    {
        public Habitacion Dato;
        public NodoABB Izquierda;
        public NodoABB Derecha;

        public NodoABB(Habitacion habitacion)
        {
            Dato = habitacion;
        }
    }
}
