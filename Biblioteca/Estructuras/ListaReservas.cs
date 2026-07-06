using System;
using GestionHotelera.Modelos;
using GestionHotelera.Nodos;

namespace GestionHotelera.Estructuras
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
}
