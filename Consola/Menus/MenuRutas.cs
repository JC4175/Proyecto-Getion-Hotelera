using System;
using GestionHotelera.Helpers;
using GestionHotelera.Estructuras;

namespace GestionHotelera.Menus
{
    // Rutas internas: funcional para el recepcionista/huesped usando Dijkstra origen->destino
    public static class MenuRutas
    {
        public static void Mostrar(GrafoHotel grafo)
        {
            while (true)
            {
                Pantalla.Limpiar();
                Pantalla.Titulo("RUTAS INTERNAS DEL HOTEL");
                Console.WriteLine("  1. Como llegar de una zona a otra");
                Console.WriteLine("  2. Ver mapa de zonas del hotel");
                Console.WriteLine("  0. Volver");
                Console.WriteLine();

                int opcion = EntradaConsola.LeerOpcion("Opcion", 0, 2);

                switch (opcion)
                {
                    case 1: GuiaRuta(grafo); break;
                    case 2: MapaZonas(grafo); break;
                    case 0: return;
                }
            }
        }

        private static void GuiaRuta(GrafoHotel grafo)
        {
            Pantalla.Limpiar();
            Pantalla.Titulo("GUIA DE RUTA PARA HUESPED");

            string[] zonas = grafo.ObtenerAreas();
            int total = grafo.ObtenerCantidad();

            if (total == 0) { Pantalla.Error("No hay zonas configuradas."); Pantalla.Pausa(); return; }

            Pantalla.Seccion("Zonas disponibles");
            for (int i = 0; i < total; i++)
            {
                Console.WriteLine("  " + (i + 1) + ". " + zonas[i]);
            }

            Console.WriteLine();
            int desde = EntradaConsola.LeerOpcion("Desde (numero de zona)", 1, total) - 1;
            int hasta = EntradaConsola.LeerOpcion("Hasta (numero de zona)", 1, total) - 1;

            Console.WriteLine();
            Pantalla.LineaDivisora();
            grafo.RutaEntreZonas(zonas[desde], zonas[hasta]);
            Pantalla.LineaDivisora();
            Pantalla.Pausa();
        }

        private static void MapaZonas(GrafoHotel grafo)
        {
            Pantalla.Limpiar();
            Pantalla.Titulo("MAPA DE ZONAS DEL HOTEL");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  Zonas conectadas y distancias (metros):");
            Console.ResetColor();
            Console.WriteLine();

            // Mapa textual de las zonas del hotel
            Console.WriteLine("  Recepcion");
            Console.WriteLine("   ├── Habitaciones  (40m)");
            Console.WriteLine("   │    └── Limpieza (15m)");
            Console.WriteLine("   │    └── Piscina  (35m)");
            Console.WriteLine("   └── Restaurante   (25m)");
            Console.WriteLine("        └── Piscina  (20m)");
            Console.WriteLine("        └── Limpieza (45m)");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("  Las distancias son en metros de recorrido.");
            Console.ResetColor();

            Pantalla.Pausa();
        }
    }
}
