using System;
using GestionHotelera.Core.Modelos;
using GestionHotelera.Core.Nodos;

namespace GestionHotelera.Core.Estructuras
{
    public class ArbolHabitacionesABB
    {
        public NodoABB Raiz = null;

        public void Insertar(Habitacion habitacion)
        {
            Raiz = InsertarRec(Raiz, habitacion);
        }

        private NodoABB InsertarRec(NodoABB nodo, Habitacion habitacion)
        {
            if (nodo == null)
            {
                return new NodoABB(habitacion);
            }

            if (habitacion.Numero < nodo.Dato.Numero)
            {
                nodo.Izquierda = InsertarRec(nodo.Izquierda, habitacion);
            }
            else if (habitacion.Numero > nodo.Dato.Numero)
            {
                nodo.Derecha = InsertarRec(nodo.Derecha, habitacion);
            }

            return nodo;
        }

        public NodoABB Buscar(int numero)
        {
            return BuscarRec(Raiz, numero);
        }

        private NodoABB BuscarRec(NodoABB nodo, int numero)
        {
            if (nodo == null)
            {
                return null;
            }

            if (numero == nodo.Dato.Numero)
            {
                return nodo;
            }

            if (numero < nodo.Dato.Numero)
            {
                return BuscarRec(nodo.Izquierda, numero);
            }

            return BuscarRec(nodo.Derecha, numero);
        }

        public void InOrden()
        {
            if (Raiz == null)
            {
                Console.WriteLine("No hay habitaciones en el ABB.");
                return;
            }

            InOrdenRec(Raiz);
        }

        private void InOrdenRec(NodoABB nodo)
        {
            if (nodo != null)
            {
                InOrdenRec(nodo.Izquierda);
                Console.WriteLine("Hab. " + nodo.Dato.Numero + " - " + nodo.Dato.Tipo +
                    " - S/ " + nodo.Dato.Precio + " - " + ObtenerEstado(nodo.Dato));
                InOrdenRec(nodo.Derecha);
            }
        }

        public void Dibujar()
        {
            if (Raiz == null)
            {
                Console.WriteLine("No hay habitaciones en el ABB.");
                return;
            }

            DibujarRec(Raiz, 0);
        }

        private void DibujarRec(NodoABB nodo, int nivel)
        {
            if (nodo != null)
            {
                DibujarRec(nodo.Derecha, nivel + 1);

                for (int i = 0; i < nivel; i++)
                {
                    Console.Write("    ");
                }

                Console.WriteLine(nodo.Dato.Numero + " - " + nodo.Dato.Tipo +
                    " - S/ " + nodo.Dato.Precio.ToString("0.00") + " - " + ObtenerEstado(nodo.Dato));
                DibujarRec(nodo.Izquierda, nivel + 1);
            }
        }

        public void MostrarDisponibles()
        {
            MostrarPorEstado(Raiz, "Disponible");
        }

        public void MostrarOcupadas()
        {
            MostrarPorEstado(Raiz, "Ocupada");
        }

        private void MostrarPorEstado(NodoABB nodo, string estado)
        {
            if (nodo != null)
            {
                MostrarPorEstado(nodo.Izquierda, estado);

                if (ObtenerEstado(nodo.Dato) == estado)
                {
                    Console.WriteLine("Hab. " + nodo.Dato.Numero + " - " + nodo.Dato.Tipo +
                        " - S/ " + nodo.Dato.Precio.ToString("0.00") + " - " + ObtenerEstado(nodo.Dato));
                }

                MostrarPorEstado(nodo.Derecha, estado);
            }
        }

        public int ContarDisponibles()
        {
            return ContarPorEstado(Raiz, "Disponible");
        }

        public int ContarOcupadas()
        {
            return ContarPorEstado(Raiz, "Ocupada");
        }

        public int ContarReservadas()
        {
            return ContarPorEstado(Raiz, "Reservada");
        }

        private int ContarPorEstado(NodoABB nodo, string estado)
        {
            if (nodo == null)
            {
                return 0;
            }

            int contador = 0;

            if (ObtenerEstado(nodo.Dato) == estado)
            {
                contador = 1;
            }

            return contador + ContarPorEstado(nodo.Izquierda, estado) +
                ContarPorEstado(nodo.Derecha, estado);
        }

        public void MostrarCatalogoVisual()
        {
            if (Raiz == null)
            {
                Console.WriteLine("No hay habitaciones en el catalogo.");
                return;
            }

            MostrarCatalogoVisualRec(Raiz);
        }

        private void MostrarCatalogoVisualRec(NodoABB nodo)
        {
            if (nodo != null)
            {
                MostrarCatalogoVisualRec(nodo.Izquierda);
                EscribirHabitacionConColor(nodo.Dato);
                MostrarCatalogoVisualRec(nodo.Derecha);
            }
        }

        private void EscribirHabitacionConColor(Habitacion habitacion)
        {
            ConsoleColor colorAnterior = Console.ForegroundColor;
            string estado = ObtenerEstado(habitacion);

            if (estado == "Ocupada")
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else if (estado == "Reservada")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }

            Console.WriteLine("  Hab. " + habitacion.Numero.ToString().PadRight(5) +
                "| " + habitacion.Tipo.PadRight(14) +
                "| S/ " + habitacion.Precio.ToString("0.00").PadRight(8) +
                "| " + estado);
            Console.ForegroundColor = colorAnterior;
        }

        public string ObtenerEstado(Habitacion habitacion)
        {
            if (!string.IsNullOrWhiteSpace(habitacion.Estado))
            {
                return habitacion.Estado;
            }

            if (habitacion.Disponible)
            {
                return "Disponible";
            }

            return "Reservada";
        }

        public bool Eliminar(int numero)
        {
            if (Buscar(numero) == null)
            {
                return false;
            }

            Raiz = EliminarRec(Raiz, numero);
            return true;
        }

        private NodoABB EliminarRec(NodoABB nodo, int numero)
        {
            if (nodo == null)
            {
                return null;
            }

            if (numero < nodo.Dato.Numero)
            {
                nodo.Izquierda = EliminarRec(nodo.Izquierda, numero);
            }
            else if (numero > nodo.Dato.Numero)
            {
                nodo.Derecha = EliminarRec(nodo.Derecha, numero);
            }
            else
            {
                if (nodo.Izquierda == null)
                {
                    return nodo.Derecha;
                }

                if (nodo.Derecha == null)
                {
                    return nodo.Izquierda;
                }

                NodoABB menor = MenorNodo(nodo.Derecha);
                nodo.Dato = menor.Dato;
                nodo.Derecha = EliminarRec(nodo.Derecha, menor.Dato.Numero);
            }

            return nodo;
        }

        private NodoABB MenorNodo(NodoABB nodo)
        {
            NodoABB actual = nodo;

            while (actual.Izquierda != null)
            {
                actual = actual.Izquierda;
            }

            return actual;
        }

        public int Contar()
        {
            return ContarRec(Raiz);
        }

        private int ContarRec(NodoABB nodo)
        {
            if (nodo == null)
            {
                return 0;
            }

            return 1 + ContarRec(nodo.Izquierda) + ContarRec(nodo.Derecha);
        }
    }
}
