using System;

namespace ProyectoFinal_T3_AVANCE_SIMPLE
{
    internal partial class Program
    {
        static void MenuGrafo()
        {
            while (true)
            {
                Limpiar();
                Titulo("RUTAS INTERNAS DEL HOTEL - GRAFO");
                Console.WriteLine("1. Mostrar matriz de adyacencia");
                Console.WriteLine("2. Recorrido en anchura desde un area");
                Console.WriteLine("3. Recorrido en profundidad desde un area");
                Console.WriteLine("4. Ruta minima desde un area (Dijkstra)");
                Console.WriteLine("5. Distancias minimas entre todas las areas (Floyd-Warshall)");
                Console.WriteLine("6. Arbol de expansion minima (Prim)");
                Console.WriteLine("0. Volver");

                int opcion = LeerOpcion("Opcion", 0, 6);

                switch (opcion)
                {
                    case 1: MostrarMatriz(); break;
                    case 2: RecorrerAnchura(); break;
                    case 3: RecorrerProfundidad(); break;
                    case 4: MostrarDijkstra(); break;
                    case 5: MostrarFloydWarshall(); break;
                    case 6: MostrarPrim(); break;
                    case 0: return;
                }
            }
        }

        static void MostrarMatriz()
        {
            Limpiar();
            Titulo("MATRIZ DE ADYACENCIA");
            grafo.MostrarMatriz();
            Pausa();
        }

        static void RecorrerAnchura()
        {
            Limpiar();
            Titulo("RECORRIDO EN ANCHURA");

            string area = LeerAreaExistente();
            grafo.RecorridoAnchura(area);
            Pausa();
        }

        static void RecorrerProfundidad()
        {
            Limpiar();
            Titulo("RECORRIDO EN PROFUNDIDAD");

            string area = LeerAreaExistente();
            grafo.RecorridoProfundidad(area);
            Pausa();
        }

        static void MostrarDijkstra()
        {
            Limpiar();
            Titulo("RUTA MINIMA - DIJKSTRA");

            string area = LeerAreaExistente();
            grafo.Dijkstra(area);
            Pausa();
        }

        static void MostrarFloydWarshall()
        {
            Limpiar();
            Titulo("FLOYD-WARSHALL");

            grafo.FloydWarshall();
            Pausa();
        }

        static void MostrarPrim()
        {
            Limpiar();
            Titulo("ARBOL DE EXPANSION MINIMA - PRIM");

            string area = LeerAreaExistente();
            grafo.Prim(area);
            Pausa();
        }

        static string LeerAreaExistente()
        {
            while (true)
            {
                Console.Write("Areas registradas: ");
                grafo.MostrarAreas();
                string area = LeerLetras("Area de inicio");

                if (grafo.BuscarArea(area) != -1)
                {
                    return area;
                }

                Error("El area no existe.");
            }
        }

        static void Reporte()
        {
            Limpiar();
            Titulo("REPORTE GENERAL");
            Console.WriteLine("Reservas de habitaciones: " + reservas.Contar());
            Console.WriteLine("Clientes frecuentes: " + clientes.Contar());
            Console.WriteLine("Turnos del personal: " + turnos.Contar());
            Console.WriteLine("Notas en historial/pila: " + pila.Contar());
            Console.WriteLine("Solicitudes pendientes en cola: " + solicitudes.Contar());
            Console.WriteLine("Cargos en organigrama/arbol: " + arbol.Contar());
            Console.WriteLine("Habitaciones en catalogo/ABB: " + abb.Contar());
            Console.WriteLine("Habitaciones disponibles: " + abb.ContarDisponibles());
            Console.WriteLine("Habitaciones reservadas: " + abb.ContarReservadas());
            Console.WriteLine("Habitaciones ocupadas: " + abb.ContarOcupadas());
            Console.WriteLine("Ganancia estimada de hoy: S/ " + reservas.GananciaDelDia(DateTime.Today).ToString("0.00"));
            Console.WriteLine("Ganancia estimada proximos 7 dias: S/ " + reservas.GananciaDeSemana(DateTime.Today).ToString("0.00"));
            Console.WriteLine("Areas en grafo: " + grafo.ContarAreas());
            Console.WriteLine("Rutas internas en grafo: " + grafo.ContarRutas());
            Pausa();
        }

    }
}
