using System;
using GestionHotelera.Modelos;
using GestionHotelera.Nodos;

namespace GestionHotelera.Estructuras
{
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
