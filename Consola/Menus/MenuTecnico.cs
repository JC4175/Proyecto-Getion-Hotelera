using System;
using GestionHotelera.Helpers;
using GestionHotelera.Estructuras;

namespace GestionHotelera.Menus
{
    // Vista tecnica: toda la parte academica agrupada, sin cambios en logica
    public static class MenuTecnico
    {
        public static void Mostrar(ArbolHotel arbol, ArbolHabitacionesABB abb, GrafoHotel grafo)
        {
            while (true)
            {
                Pantalla.Limpiar();
                Pantalla.Titulo("VISTA TECNICA — ESTRUCTURAS DE DATOS");
                Pantalla.Info("Seccion de sustentacion academica");
                Console.WriteLine();

                Pantalla.Seccion("Arbol Organigrama del hotel");
                Console.WriteLine("  1. Dibujar organigrama");
                Console.WriteLine("  2. Recorrido PreOrden");
                Console.WriteLine("  3. Recorrido InOrden");
                Console.WriteLine("  4. Recorrido PosOrden");
                Console.WriteLine("  5. Contar departamentos");
                Console.WriteLine();

                Pantalla.Seccion("ABB de Habitaciones");
                Console.WriteLine("  6. Dibujar arbol ABB");
                Console.WriteLine("  7. Recorrido InOrden del ABB (habitaciones ordenadas)");
                Console.WriteLine();

                Pantalla.Seccion("Grafo — Algoritmos internos");
                Console.WriteLine("  8.  Matriz de adyacencia");
                Console.WriteLine("  9.  Recorrido en anchura BFS");
                Console.WriteLine("  10. Recorrido en profundidad DFS");
                Console.WriteLine("  11. Dijkstra desde un area (todas las rutas)");
                Console.WriteLine("  12. Floyd-Warshall (distancias minimas globales)");
                Console.WriteLine("  13. Arbol de expansion minima (Prim)");
                Console.WriteLine("  0.  Volver al menu principal");
                Console.WriteLine();

                int opcion = EntradaConsola.LeerOpcion("Opcion", 0, 13);

                switch (opcion)
                {
                    case 1: DibujarOrganigrama(arbol); break;
                    case 2: PreOrden(arbol); break;
                    case 3: InOrden(arbol); break;
                    case 4: PosOrden(arbol); break;
                    case 5: ContarDepartamentos(arbol); break;
                    case 6: DibujarABB(abb); break;
                    case 7: InOrdenABB(abb); break;
                    case 8: MatrizAdyacencia(grafo); break;
                    case 9: RecorridoBFS(grafo); break;
                    case 10: RecorridoDFS(grafo); break;
                    case 11: DijkstraDesdeArea(grafo); break;
                    case 12: FloydWarshall(grafo); break;
                    case 13: Prim(grafo); break;
                    case 0: return;
                }
            }
        }

        private static void DibujarOrganigrama(ArbolHotel arbol)
        {
            Pantalla.Limpiar(); Pantalla.Titulo("ORGANIGRAMA DEL HOTEL — ARBOL BINARIO");
            arbol.Dibujar(); Pantalla.Pausa();
        }

        private static void PreOrden(ArbolHotel arbol)
        {
            Pantalla.Limpiar(); Pantalla.Titulo("RECORRIDO PREORDEN");
            Console.Write("  ");
            arbol.PreOrden(); Pantalla.Pausa();
        }

        private static void InOrden(ArbolHotel arbol)
        {
            Pantalla.Limpiar(); Pantalla.Titulo("RECORRIDO INORDEN");
            Console.Write("  ");
            arbol.InOrden(); Pantalla.Pausa();
        }

        private static void PosOrden(ArbolHotel arbol)
        {
            Pantalla.Limpiar(); Pantalla.Titulo("RECORRIDO POSORDEN");
            Console.Write("  ");
            arbol.PosOrden(); Pantalla.Pausa();
        }

        private static void ContarDepartamentos(ArbolHotel arbol)
        {
            Pantalla.Limpiar(); Pantalla.Titulo("CONTEO DE DEPARTAMENTOS");
            Console.WriteLine("  Total de departamentos en el arbol: " + arbol.Contar());
            Pantalla.Pausa();
        }

        private static void DibujarABB(ArbolHabitacionesABB abb)
        {
            Pantalla.Limpiar(); Pantalla.Titulo("ARBOL ABB DE HABITACIONES");
            abb.Dibujar(); Pantalla.Pausa();
        }

        private static void InOrdenABB(ArbolHabitacionesABB abb)
        {
            Pantalla.Limpiar(); Pantalla.Titulo("HABITACIONES EN ORDEN (InOrden ABB)");
            Console.WriteLine();
            abb.InOrden(); Pantalla.Pausa();
        }

        private static void MatrizAdyacencia(GrafoHotel grafo)
        {
            Pantalla.Limpiar(); Pantalla.Titulo("MATRIZ DE ADYACENCIA — GRAFO");
            Console.WriteLine();
            grafo.MostrarMatriz(); Pantalla.Pausa();
        }

        private static void RecorridoBFS(GrafoHotel grafo)
        {
            Pantalla.Limpiar(); Pantalla.Titulo("RECORRIDO EN ANCHURA (BFS)");
            string area = EntradaConsola.LeerTexto("Area de inicio");
            Console.Write("  Orden BFS: ");
            grafo.RecorridoAnchura(area); Pantalla.Pausa();
        }

        private static void RecorridoDFS(GrafoHotel grafo)
        {
            Pantalla.Limpiar(); Pantalla.Titulo("RECORRIDO EN PROFUNDIDAD (DFS)");
            string area = EntradaConsola.LeerTexto("Area de inicio");
            Console.Write("  Orden DFS: ");
            grafo.RecorridoProfundidad(area); Pantalla.Pausa();
        }

        private static void DijkstraDesdeArea(GrafoHotel grafo)
        {
            Pantalla.Limpiar(); Pantalla.Titulo("DIJKSTRA — RUTAS MINIMAS DESDE UN AREA");
            string area = EntradaConsola.LeerTexto("Area de origen");
            Console.WriteLine();
            grafo.Dijkstra(area); Pantalla.Pausa();
        }

        private static void FloydWarshall(GrafoHotel grafo)
        {
            Pantalla.Limpiar(); Pantalla.Titulo("FLOYD-WARSHALL — DISTANCIAS MINIMAS GLOBALES");
            Console.WriteLine();
            grafo.FloydWarshall(); Pantalla.Pausa();
        }

        private static void Prim(GrafoHotel grafo)
        {
            Pantalla.Limpiar(); Pantalla.Titulo("PRIM — ARBOL DE EXPANSION MINIMA");
            string area = EntradaConsola.LeerTexto("Area de inicio");
            Console.WriteLine();
            grafo.Prim(area); Pantalla.Pausa();
        }
    }
}
