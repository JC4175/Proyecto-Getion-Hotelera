using System;

namespace GestionHotelera.Core.Estructuras
{
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

        // Dijkstra completo desde un origen (para vista tecnica)
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

        // Dijkstra con origen Y destino — para la guia de huesped
        public void RutaEntreZonas(string origen, string destino)
        {
            int inicio = BuscarArea(origen);
            int fin = BuscarArea(destino);

            if (inicio == -1 || fin == -1)
            {
                Console.WriteLine("Area no encontrada.");
                return;
            }

            if (inicio == fin)
            {
                Console.WriteLine("Ya se encuentra en esa zona.");
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

            if (distancia[fin] == INFINITO)
            {
                Console.WriteLine("No existe ruta entre esas zonas.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("  Ruta mas corta: " + areas[inicio] + " -> " + areas[fin]);
            Console.Write("  Camino: ");
            ImprimirRutaDijkstra(anterior, fin);
            Console.WriteLine();
            Console.WriteLine("  Distancia total: " + distancia[fin] + " metros");
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

        // Devuelve el listado de areas para mostrar en menu
        public string[] ObtenerAreas()
        {
            string[] resultado = new string[cantidad];
            for (int i = 0; i < cantidad; i++)
            {
                resultado[i] = areas[i];
            }
            return resultado;
        }

        public int ObtenerCantidad()
        {
            return cantidad;
        }
    }
}
