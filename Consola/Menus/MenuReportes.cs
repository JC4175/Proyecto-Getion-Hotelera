using System;
using GestionHotelera.Helpers;
using GestionHotelera.Estructuras;

namespace GestionHotelera.Menus
{
    // Reportes utiles para gestion hotelera — solo metricas de negocio
    public static class MenuReportes
    {
        public static void Mostrar(ListaReservas reservas, ArbolHabitacionesABB abb, ColaSolicitudes solicitudes)
        {
            while (true)
            {
                Pantalla.Limpiar();
                Pantalla.Titulo("REPORTES Y ESTADISTICAS");
                Console.WriteLine("  1. Resumen de ocupacion actual");
                Console.WriteLine("  2. Ingresos del dia");
                Console.WriteLine("  3. Ingresos de la semana");
                Console.WriteLine("  4. Reservas activas");
                Console.WriteLine("  5. Solicitudes pendientes");
                Console.WriteLine("  0. Volver");
                Console.WriteLine();

                int opcion = EntradaConsola.LeerOpcion("Opcion", 0, 5);

                switch (opcion)
                {
                    case 1: ReporteOcupacion(abb); break;
                    case 2: ReporteIngresosHoy(reservas); break;
                    case 3: ReporteIngresosSemana(reservas); break;
                    case 4: ReporteReservasActivas(reservas); break;
                    case 5: ReporteSolicitudes(solicitudes); break;
                    case 0: return;
                }
            }
        }

        private static void ReporteOcupacion(ArbolHabitacionesABB abb)
        {
            Pantalla.Limpiar();
            Pantalla.Titulo("RESUMEN DE OCUPACION");

            int disp = abb.ContarDisponibles();
            int ocup = abb.ContarOcupadas();
            int res = abb.ContarReservadas();
            int total = abb.Contar();

            Console.WriteLine();
            Console.WriteLine("  " + new string('─', 40));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  ✓ Disponibles:  " + disp + " habitaciones");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  ✗ Ocupadas:     " + ocup + " habitaciones");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  ◆ Reservadas:   " + res + " habitaciones");
            Console.ResetColor();
            Console.WriteLine("  " + new string('─', 40));
            Console.WriteLine("  Total:          " + total + " habitaciones");

            if (total > 0)
            {
                int pct = (ocup + res) * 100 / total;
                Console.WriteLine();
                Console.ForegroundColor = pct >= 80 ? ConsoleColor.Red :
                                          pct >= 50 ? ConsoleColor.Yellow : ConsoleColor.Green;
                Console.WriteLine("  Ocupacion:      " + pct + "%");
                Console.ResetColor();
            }

            Pantalla.Pausa();
        }

        private static void ReporteIngresosHoy(ListaReservas reservas)
        {
            Pantalla.Limpiar();
            Pantalla.Titulo("INGRESOS DEL DIA");

            double total = reservas.GananciaDelDia(DateTime.Today);

            Console.WriteLine();
            Console.WriteLine("  Fecha: " + DateTime.Today.ToString("dd/MM/yyyy"));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  Ingresos del dia: S/ " + total.ToString("0.00"));
            Console.ResetColor();

            Pantalla.Pausa();
        }

        private static void ReporteIngresosSemana(ListaReservas reservas)
        {
            Pantalla.Limpiar();
            Pantalla.Titulo("INGRESOS DE LA SEMANA");

            DateTime inicio = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 1);

            Console.WriteLine();
            for (int i = 0; i < 7; i++)
            {
                DateTime dia = inicio.AddDays(i);
                double ing = reservas.GananciaDelDia(dia);
                Console.Write("  " + dia.ToString("ddd dd/MM").PadRight(14) + "  S/ ");
                Console.ForegroundColor = ing > 0 ? ConsoleColor.Green : ConsoleColor.DarkGray;
                Console.WriteLine(ing.ToString("0.00"));
                Console.ResetColor();
            }

            Console.WriteLine("  " + new string('─', 30));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  Total semana:   S/ " + reservas.GananciaDeSemana(inicio).ToString("0.00"));
            Console.ResetColor();

            Pantalla.Pausa();
        }

        private static void ReporteReservasActivas(ListaReservas reservas)
        {
            Pantalla.Limpiar();
            Pantalla.Titulo("RESERVAS ACTIVAS");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("  " + "Codigo".PadRight(10) + "| " + "Huesped".PadRight(18) + "| Hab | Estado");
            Console.WriteLine("  " + new string('─', 50));
            Console.ResetColor();

            var actual = reservas.Cabecera;
            bool hayActivas = false;

            while (actual != null)
            {
                if (actual.Dato.Estado != "Finalizada" && actual.Dato.Estado != "Cancelada")
                {
                    hayActivas = true;
                    string est = actual.Dato.Estado ?? "Reservada";
                    ConsoleColor c = est == "Ocupada" ? ConsoleColor.Red : ConsoleColor.Yellow;
                    Console.Write("  " + actual.Dato.Codigo.PadRight(10) + "| " +
                        actual.Dato.Cliente.PadRight(18) + "| " +
                        actual.Dato.Habitacion.ToString().PadRight(4) + "| ");
                    Pantalla.ColorEscribirLinea(est, c);
                }
                actual = actual.Siguiente;
            }

            if (!hayActivas) Pantalla.Info("No hay reservas activas en este momento.");
            Pantalla.Pausa();
        }

        private static void ReporteSolicitudes(ColaSolicitudes solicitudes)
        {
            Pantalla.Limpiar();
            Pantalla.Titulo("SOLICITUDES PENDIENTES");

            int total = solicitudes.Contar();

            if (total == 0) { Pantalla.Info("No hay solicitudes pendientes."); Pantalla.Pausa(); return; }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  Solicitudes en cola: " + total);
            Console.ResetColor();
            Console.WriteLine();

            var actual = solicitudes.Inicio;
            int i = 1;
            while (actual != null)
            {
                Console.WriteLine("  " + i + ". [" + actual.Dato.Codigo + "] " +
                    actual.Dato.Cliente + " — " + actual.Dato.Detalle);
                actual = actual.Siguiente;
                i++;
            }

            Pantalla.Pausa();
        }
    }
}
