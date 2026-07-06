using System;
using GestionHotelera.Modelos;
using GestionHotelera.Nodos;

namespace GestionHotelera.Estructuras
{
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
}
