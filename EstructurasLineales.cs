using System;

namespace ProyectoFinal_T3_AVANCE_SIMPLE
{
    public class ListaReservas
    {
        public NodoReserva Cabecera = null;

        public void Insertar(Reserva reserva)
        {
            NodoReserva nuevo = new NodoReserva();
            nuevo.Dato = reserva;

            if (Cabecera == null)
            {
                Cabecera = nuevo;
            }
            else
            {
                NodoReserva actual = Cabecera;
                while (actual.Siguiente != null)
                {
                    actual = actual.Siguiente;
                }
                actual.Siguiente = nuevo;
            }
        }

        public NodoReserva Buscar(string codigo)
        {
            NodoReserva actual = Cabecera;
            while (actual != null)
            {
                if (actual.Dato.Codigo == codigo)
                {
                    return actual;
                }
                actual = actual.Siguiente;
            }
            return null;
        }

        public bool ExisteHabitacion(int numero)
        {
            NodoReserva actual = Cabecera;
            while (actual != null)
            {
                if (actual.Dato.Habitacion == numero && actual.Dato.Estado != "Finalizada")
                {
                    return true;
                }
                actual = actual.Siguiente;
            }
            return false;
        }

        public NodoReserva BuscarActivaPorHabitacion(int numero)
        {
            NodoReserva reservada = null;
            NodoReserva actual = Cabecera;

            while (actual != null)
            {
                if (actual.Dato.Habitacion == numero && actual.Dato.Estado != "Finalizada")
                {
                    if (actual.Dato.Estado == "Ocupada")
                    {
                        return actual;
                    }

                    if (reservada == null)
                    {
                        reservada = actual;
                    }
                }

                actual = actual.Siguiente;
            }

            return reservada;
        }

        public bool Eliminar(string codigo)
        {
            if (Cabecera == null)
            {
                return false;
            }

            if (Cabecera.Dato.Codigo == codigo)
            {
                Cabecera = Cabecera.Siguiente;
                return true;
            }

            NodoReserva anterior = Cabecera;
            NodoReserva actual = Cabecera.Siguiente;

            while (actual != null)
            {
                if (actual.Dato.Codigo == codigo)
                {
                    anterior.Siguiente = actual.Siguiente;
                    return true;
                }

                anterior = actual;
                actual = actual.Siguiente;
            }

            return false;
        }

        public void Mostrar()
        {
            NodoReserva actual = Cabecera;
            if (actual == null)
            {
                Console.WriteLine("No hay reservas.");
            }

            while (actual != null)
            {
                Console.WriteLine(actual.Dato.Codigo + " - " + actual.Dato.Cliente +
                    " - Hab. " + actual.Dato.Habitacion + " (" + actual.Dato.TipoHabitacion + ")" +
                    " - Entrada: " + actual.Dato.FechaHoraEntradaProgramada.ToString("dd/MM/yyyy HH:mm") +
                    " - Salida: " + actual.Dato.FechaHoraSalidaProgramada.ToString("dd/MM/yyyy HH:mm") +
                    " - Estado: " + EstadoReserva(actual.Dato) +
                    " - Total: S/ " + actual.Dato.TotalPagar.ToString("0.00"));
                actual = actual.Siguiente;
            }
        }

        public double GananciaDelDia(DateTime fecha)
        {
            double total = 0;
            NodoReserva actual = Cabecera;

            while (actual != null)
            {
                DateTime entrada = actual.Dato.FechaHoraEntradaProgramada.Date;
                DateTime salida = entrada.AddDays(actual.Dato.Noches);

                if (fecha.Date >= entrada && fecha.Date < salida)
                {
                    total = total + actual.Dato.PrecioNoche;
                }

                actual = actual.Siguiente;
            }

            return total;
        }

        public double GananciaDeSemana(DateTime fechaInicio)
        {
            double total = 0;

            for (int i = 0; i < 7; i++)
            {
                total = total + GananciaDelDia(fechaInicio.Date.AddDays(i));
            }

            return total;
        }

        public int Contar()
        {
            int contador = 0;
            NodoReserva actual = Cabecera;
            while (actual != null)
            {
                contador++;
                actual = actual.Siguiente;
            }
            return contador;
        }

        private string EstadoReserva(Reserva reserva)
        {
            if (string.IsNullOrWhiteSpace(reserva.Estado))
            {
                return "Reservada";
            }

            return reserva.Estado;
        }
    }

    public class ListaClientesDoble
    {
        public NodoCliente Cabecera = null;
        public NodoCliente Ultimo = null;

        public void Insertar(Cliente cliente)
        {
            NodoCliente nuevo = new NodoCliente();
            nuevo.Dato = cliente;

            if (Cabecera == null)
            {
                Cabecera = nuevo;
                Ultimo = nuevo;
            }
            else
            {
                Ultimo.Siguiente = nuevo;
                nuevo.Anterior = Ultimo;
                Ultimo = nuevo;
            }
        }

        public NodoCliente Buscar(string codigo)
        {
            NodoCliente actual = Cabecera;
            while (actual != null)
            {
                if (actual.Dato.Codigo == codigo)
                {
                    return actual;
                }
                actual = actual.Siguiente;
            }
            return null;
        }

        public void MostrarAdelante()
        {
            NodoCliente actual = Cabecera;
            if (actual == null)
            {
                Console.WriteLine("No hay clientes.");
            }

            while (actual != null)
            {
                Console.WriteLine(actual.Dato.Codigo + " - " + actual.Dato.Nombre +
                    " - Puntos: " + actual.Dato.Puntos);
                actual = actual.Siguiente;
            }
        }

        public void MostrarAtras()
        {
            NodoCliente actual = Ultimo;
            if (actual == null)
            {
                Console.WriteLine("No hay clientes.");
            }

            while (actual != null)
            {
                Console.WriteLine(actual.Dato.Codigo + " - " + actual.Dato.Nombre +
                    " - Puntos: " + actual.Dato.Puntos);
                actual = actual.Anterior;
            }
        }

        public int Contar()
        {
            int contador = 0;
            NodoCliente actual = Cabecera;
            while (actual != null)
            {
                contador++;
                actual = actual.Siguiente;
            }
            return contador;
        }
    }

    public class ListaTurnosCircular
    {
        public NodoTurno Cabecera = null;
        public NodoTurno Ultimo = null;
        private NodoTurno turnoActual = null;

        public void Insertar(Turno turno)
        {
            NodoTurno nuevo = new NodoTurno();
            nuevo.Dato = turno;

            if (Cabecera == null)
            {
                Cabecera = nuevo;
                Ultimo = nuevo;
                Ultimo.Siguiente = Cabecera;
                turnoActual = Cabecera;
            }
            else
            {
                Ultimo.Siguiente = nuevo;
                nuevo.Siguiente = Cabecera;
                Ultimo = nuevo;
            }
        }

        public NodoTurno Buscar(string codigo)
        {
            if (Cabecera == null)
            {
                return null;
            }

            NodoTurno actual = Cabecera;
            do
            {
                if (actual.Dato.Codigo == codigo)
                {
                    return actual;
                }
                actual = actual.Siguiente;
            }
            while (actual != Cabecera);

            return null;
        }

        public Turno SiguienteTurno()
        {
            if (turnoActual == null)
            {
                return null;
            }

            Turno turno = turnoActual.Dato;
            turnoActual = turnoActual.Siguiente;
            return turno;
        }

        public void Mostrar()
        {
            if (Cabecera == null)
            {
                Console.WriteLine("No hay turnos.");
                return;
            }

            NodoTurno actual = Cabecera;
            do
            {
                Console.WriteLine(actual.Dato.Codigo + " - " + actual.Dato.Empleado +
                    " - Area: " + actual.Dato.Area);
                actual = actual.Siguiente;
            }
            while (actual != Cabecera);
        }

        public int Contar()
        {
            if (Cabecera == null)
            {
                return 0;
            }

            int contador = 0;
            NodoTurno actual = Cabecera;
            do
            {
                contador++;
                actual = actual.Siguiente;
            }
            while (actual != Cabecera);

            return contador;
        }
    }

    public class PilaAcciones
    {
        public NodoPila Cima = null;

        public void Apilar(string accion)
        {
            NodoPila nuevo = new NodoPila();
            nuevo.Accion = accion;
            nuevo.Siguiente = Cima;
            Cima = nuevo;
        }

        public string Desapilar()
        {
            if (Cima == null)
            {
                return null;
            }

            string accion = Cima.Accion;
            Cima = Cima.Siguiente;
            return accion;
        }

        public void Mostrar()
        {
            NodoPila actual = Cima;
            if (actual == null)
            {
                Console.WriteLine("La pila esta vacia.");
            }

            while (actual != null)
            {
                Console.WriteLine(actual.Accion);
                actual = actual.Siguiente;
            }
        }

        public int Contar()
        {
            int contador = 0;
            NodoPila actual = Cima;
            while (actual != null)
            {
                contador++;
                actual = actual.Siguiente;
            }
            return contador;
        }
    }

    public class ColaSolicitudes
    {
        public NodoSolicitud Inicio = null;
        public NodoSolicitud Final = null;

        public void Encolar(Solicitud solicitud)
        {
            NodoSolicitud nuevo = new NodoSolicitud();
            nuevo.Dato = solicitud;

            if (Inicio == null)
            {
                Inicio = nuevo;
                Final = nuevo;
            }
            else
            {
                Final.Siguiente = nuevo;
                Final = nuevo;
            }
        }

        public Solicitud Desencolar()
        {
            if (Inicio == null)
            {
                return null;
            }

            Solicitud solicitud = Inicio.Dato;
            Inicio = Inicio.Siguiente;

            if (Inicio == null)
            {
                Final = null;
            }

            return solicitud;
        }

        public NodoSolicitud Buscar(string codigo)
        {
            NodoSolicitud actual = Inicio;
            while (actual != null)
            {
                if (actual.Dato.Codigo == codigo)
                {
                    return actual;
                }
                actual = actual.Siguiente;
            }
            return null;
        }

        public void Mostrar()
        {
            NodoSolicitud actual = Inicio;
            if (actual == null)
            {
                Console.WriteLine("No hay solicitudes.");
            }

            while (actual != null)
            {
                Console.WriteLine(actual.Dato.Codigo + " - " + actual.Dato.Cliente +
                    " - " + actual.Dato.Detalle);
                actual = actual.Siguiente;
            }
        }

        public int Contar()
        {
            int contador = 0;
            NodoSolicitud actual = Inicio;
            while (actual != null)
            {
                contador++;
                actual = actual.Siguiente;
            }
            return contador;
        }
    }
}
