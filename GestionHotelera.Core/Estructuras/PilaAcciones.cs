using System;
using GestionHotelera.Core.Nodos;

namespace GestionHotelera.Core.Estructuras
{
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
}
