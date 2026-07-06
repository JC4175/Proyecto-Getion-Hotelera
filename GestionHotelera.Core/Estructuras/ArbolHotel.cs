using System;
using GestionHotelera.Core.Nodos;

namespace GestionHotelera.Core.Estructuras
{
    public class ArbolHotel
    {
        public NodoArbol Raiz = null;

        public void CargarDatos()
        {
            Raiz = new NodoArbol("Gerente");
            Raiz.Izquierda = new NodoArbol("Recepcion");
            Raiz.Derecha = new NodoArbol("Operaciones");
            Raiz.Izquierda.Izquierda = new NodoArbol("Reservas");
            Raiz.Izquierda.Derecha = new NodoArbol("Clientes frecuentes");
            Raiz.Derecha.Izquierda = new NodoArbol("Limpieza");
            Raiz.Derecha.Derecha = new NodoArbol("Seguridad");
        }

        public void PreOrden()
        {
            PreOrdenRec(Raiz);
            Console.WriteLine();
        }

        public void Dibujar()
        {
            DibujarRec(Raiz, 0);
        }

        private void DibujarRec(NodoArbol nodo, int nivel)
        {
            if (nodo != null)
            {
                DibujarRec(nodo.Derecha, nivel + 1);

                for (int i = 0; i < nivel; i++)
                {
                    Console.Write("    ");
                }

                Console.WriteLine(nodo.Cargo);
                DibujarRec(nodo.Izquierda, nivel + 1);
            }
        }

        private void PreOrdenRec(NodoArbol nodo)
        {
            if (nodo != null)
            {
                Console.Write(nodo.Cargo + " ");
                PreOrdenRec(nodo.Izquierda);
                PreOrdenRec(nodo.Derecha);
            }
        }

        public void InOrden()
        {
            InOrdenRec(Raiz);
            Console.WriteLine();
        }

        private void InOrdenRec(NodoArbol nodo)
        {
            if (nodo != null)
            {
                InOrdenRec(nodo.Izquierda);
                Console.Write(nodo.Cargo + " ");
                InOrdenRec(nodo.Derecha);
            }
        }

        public void PosOrden()
        {
            PosOrdenRec(Raiz);
            Console.WriteLine();
        }

        private void PosOrdenRec(NodoArbol nodo)
        {
            if (nodo != null)
            {
                PosOrdenRec(nodo.Izquierda);
                PosOrdenRec(nodo.Derecha);
                Console.Write(nodo.Cargo + " ");
            }
        }

        public int Contar()
        {
            return ContarRec(Raiz);
        }

        private int ContarRec(NodoArbol nodo)
        {
            if (nodo == null)
            {
                return 0;
            }

            return 1 + ContarRec(nodo.Izquierda) + ContarRec(nodo.Derecha);
        }
    }
}
