using System;
using GestionHotelera.Core.Modelos;
using GestionHotelera.Core.Nodos;

namespace GestionHotelera.Core.Estructuras
{
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
}
