using System;

namespace ProyectoFinal_T3_AVANCE_SIMPLE
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
                    " - S/ " + nodo.Dato.Precio + " - " + Estado(nodo.Dato));
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
                    " - S/ " + nodo.Dato.Precio.ToString("0.00") + " - " + Estado(nodo.Dato));
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

                if (Estado(nodo.Dato) == estado)
                {
                    Console.WriteLine("Hab. " + nodo.Dato.Numero + " - " + nodo.Dato.Tipo +
                        " - S/ " + nodo.Dato.Precio.ToString("0.00") + " - " + Estado(nodo.Dato));
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

            if (Estado(nodo.Dato) == estado)
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
                Console.WriteLine("No hay habitaciones en el ABB.");
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
            string estado = Estado(habitacion);

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

            Console.WriteLine("Hab. " + habitacion.Numero + " - " + habitacion.Tipo +
                " - S/ " + habitacion.Precio.ToString("0.00") + " - " + estado);
            Console.ForegroundColor = colorAnterior;
        }

        private string Estado(Habitacion habitacion)
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

    public class GrafoHotel
    {
        private const int INFINITO = 999999;
        private string[] areas = new string[8];
        private int[,] matriz = new int[8, 8];
        private int cantidad = 0;

        public bool AgregarArea(string area)
        {
            if (area == null || cantidad == areas.Length || BuscarArea(area) != -1)
            {
                return false;
            }

            areas[cantidad] = area;
            cantidad++;
            return true;
        }

        public int BuscarArea(string area)
        {
            if (area == null)
            {
                return -1;
            }

            for (int i = 0; i < cantidad; i++)
            {
                if (areas[i].ToUpper() == area.ToUpper())
                {
                    return i;
                }
            }
            return -1;
        }

        public void AgregarRuta(string origen, string destino, int distancia)
        {
            int i = BuscarArea(origen);
            int j = BuscarArea(destino);

            if (i != -1 && j != -1)
            {
                matriz[i, j] = distancia;
                matriz[j, i] = distancia;
            }
        }

        public void MostrarAreas()
        {
            for (int i = 0; i < cantidad; i++)
            {
                Console.Write(areas[i]);

                if (i < cantidad - 1)
                {
                    Console.Write(", ");
                }
            }

            Console.WriteLine();
        }

        public void MostrarMatriz()
        {
            Console.Write("Area".PadRight(14));
            for (int i = 0; i < cantidad; i++)
            {
                Console.Write(areas[i].PadRight(14));
            }
            Console.WriteLine();

            for (int i = 0; i < cantidad; i++)
            {
                Console.Write(areas[i].PadRight(14));
                for (int j = 0; j < cantidad; j++)
                {
                    Console.Write(matriz[i, j].ToString().PadRight(14));
                }
                Console.WriteLine();
            }
        }

        public void RecorridoAnchura(string inicio)
        {
            int indice = BuscarArea(inicio);
            if (indice == -1)
            {
                Console.WriteLine("Area no encontrada.");
                return;
            }

            bool[] visitado = new bool[areas.Length];
            int[] cola = new int[areas.Length];
            int frente = 0;
            int final = 0;

            visitado[indice] = true;
            cola[final] = indice;
            final++;

            while (frente < final)
            {
                int actual = cola[frente];
                frente++;
                Console.Write(areas[actual] + " ");

                for (int i = 0; i < cantidad; i++)
                {
                    if (matriz[actual, i] > 0 && visitado[i] == false)
                    {
                        visitado[i] = true;
                        cola[final] = i;
                        final++;
                    }
                }
            }
            Console.WriteLine();
        }

        public void RecorridoProfundidad(string inicio)
        {
            int indice = BuscarArea(inicio);
            if (indice == -1)
            {
                Console.WriteLine("Area no encontrada.");
                return;
            }

            bool[] visitado = new bool[areas.Length];
            ProfundidadRec(indice, visitado);
            Console.WriteLine();
        }

        private void ProfundidadRec(int actual, bool[] visitado)
        {
            visitado[actual] = true;
            Console.Write(areas[actual] + " ");

            for (int i = 0; i < cantidad; i++)
            {
                if (matriz[actual, i] > 0 && visitado[i] == false)
                {
                    ProfundidadRec(i, visitado);
                }
            }
        }

        public void Dijkstra(string origen)
        {
            int inicio = BuscarArea(origen);
            if (inicio == -1)
            {
                Console.WriteLine("Area no encontrada.");
                return;
            }

            int[] distancia = new int[areas.Length];
            bool[] visitado = new bool[areas.Length];
            int[] anterior = new int[areas.Length];

            for (int i = 0; i < cantidad; i++)
            {
                distancia[i] = INFINITO;
                visitado[i] = false;
                anterior[i] = -1;
            }

            distancia[inicio] = 0;

            for (int vuelta = 0; vuelta < cantidad; vuelta++)
            {
                int actual = IndiceMenorValor(distancia, visitado);

                if (actual == -1)
                {
                    break;
                }

                visitado[actual] = true;

                for (int i = 0; i < cantidad; i++)
                {
                    if (matriz[actual, i] > 0 && visitado[i] == false)
                    {
                        int nuevaDistancia = distancia[actual] + matriz[actual, i];

                        if (nuevaDistancia < distancia[i])
                        {
                            distancia[i] = nuevaDistancia;
                            anterior[i] = actual;
                        }
                    }
                }
            }

            Console.WriteLine("Rutas minimas desde " + areas[inicio] + ":");
            for (int i = 0; i < cantidad; i++)
            {
                if (i != inicio)
                {
                    Console.Write("Hacia " + areas[i].PadRight(14));

                    if (distancia[i] == INFINITO)
                    {
                        Console.WriteLine("Sin ruta");
                    }
                    else
                    {
                        Console.Write(" | Distancia: " + distancia[i].ToString().PadLeft(3) + " metros | Ruta: ");
                        ImprimirRutaDijkstra(anterior, i);
                        Console.WriteLine();
                    }
                }
            }
        }

        private void ImprimirRutaDijkstra(int[] anterior, int actual)
        {
            if (actual == -1)
            {
                return;
            }

            if (anterior[actual] == -1)
            {
                Console.Write(areas[actual]);
                return;
            }

            ImprimirRutaDijkstra(anterior, anterior[actual]);
            Console.Write(" -> " + areas[actual]);
        }

        public void FloydWarshall()
        {
            int[,] distancia = new int[areas.Length, areas.Length];

            for (int i = 0; i < cantidad; i++)
            {
                for (int j = 0; j < cantidad; j++)
                {
                    if (i == j)
                    {
                        distancia[i, j] = 0;
                    }
                    else if (matriz[i, j] > 0)
                    {
                        distancia[i, j] = matriz[i, j];
                    }
                    else
                    {
                        distancia[i, j] = INFINITO;
                    }
                }
            }

            for (int k = 0; k < cantidad; k++)
            {
                for (int i = 0; i < cantidad; i++)
                {
                    for (int j = 0; j < cantidad; j++)
                    {
                        if (distancia[i, k] + distancia[k, j] < distancia[i, j])
                        {
                            distancia[i, j] = distancia[i, k] + distancia[k, j];
                        }
                    }
                }
            }

            Console.Write("Area".PadRight(14));
            for (int i = 0; i < cantidad; i++)
            {
                Console.Write(areas[i].PadRight(14));
            }
            Console.WriteLine();

            for (int i = 0; i < cantidad; i++)
            {
                Console.Write(areas[i].PadRight(14));
                for (int j = 0; j < cantidad; j++)
                {
                    string valor = "-";

                    if (distancia[i, j] != INFINITO)
                    {
                        valor = distancia[i, j].ToString();
                    }

                    Console.Write(valor.PadRight(14));
                }
                Console.WriteLine();
            }
        }

        public void Prim(string inicio)
        {
            int indiceInicio = BuscarArea(inicio);
            if (indiceInicio == -1)
            {
                Console.WriteLine("Area no encontrada.");
                return;
            }

            bool[] seleccionado = new bool[areas.Length];
            int[] peso = new int[areas.Length];
            int[] padre = new int[areas.Length];

            for (int i = 0; i < cantidad; i++)
            {
                seleccionado[i] = false;
                peso[i] = INFINITO;
                padre[i] = -1;
            }

            peso[indiceInicio] = 0;

            for (int vuelta = 0; vuelta < cantidad; vuelta++)
            {
                int actual = IndiceMenorValor(peso, seleccionado);

                if (actual == -1)
                {
                    break;
                }

                seleccionado[actual] = true;

                for (int i = 0; i < cantidad; i++)
                {
                    if (matriz[actual, i] > 0 && seleccionado[i] == false && matriz[actual, i] < peso[i])
                    {
                        peso[i] = matriz[actual, i];
                        padre[i] = actual;
                    }
                }
            }

            int total = 0;
            int rutas = 0;
            Console.WriteLine("Arbol de expansion minima desde " + areas[indiceInicio] + ":");

            for (int i = 0; i < cantidad; i++)
            {
                if (padre[i] != -1)
                {
                    Console.WriteLine(areas[padre[i]] + " - " + areas[i] + " : " + peso[i] + " metros");
                    total = total + peso[i];
                    rutas++;
                }
            }

            Console.WriteLine("Distancia total: " + total + " metros");

            if (rutas < cantidad - 1)
            {
                Console.WriteLine("No todas las areas estan conectadas.");
            }
        }

        private int IndiceMenorValor(int[] valores, bool[] usado)
        {
            int menor = INFINITO;
            int indice = -1;

            for (int i = 0; i < cantidad; i++)
            {
                if (usado[i] == false && valores[i] < menor)
                {
                    menor = valores[i];
                    indice = i;
                }
            }

            return indice;
        }

        public int ContarAreas()
        {
            return cantidad;
        }

        public int ContarRutas()
        {
            int contador = 0;

            for (int i = 0; i < cantidad; i++)
            {
                for (int j = i + 1; j < cantidad; j++)
                {
                    if (matriz[i, j] > 0)
                    {
                        contador++;
                    }
                }
            }

            return contador;
        }
    }
}
