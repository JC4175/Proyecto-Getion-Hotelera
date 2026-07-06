using System;
using GestionHotelera.Consola.Helpers;
using GestionHotelera.Core.Estructuras;
using GestionHotelera.Core.Modelos;

namespace GestionHotelera.Consola.Menus
{
    // Cola de solicitudes + Pila de notas unificadas en un solo menu operativo
    public static class MenuSolicitudes
    {
        public static void Mostrar(ColaSolicitudes solicitudes, PilaAcciones pila, ListaTurnosCircular turnos)
        {
            while (true)
            {
                Pantalla.Limpiar();
                Pantalla.Titulo("SOLICITUDES Y NOTAS INTERNAS");

                Pantalla.Seccion("Cola de solicitudes (atencion en orden)");
                Console.WriteLine("  1. Nueva solicitud de servicio");
                Console.WriteLine("  2. Atender siguiente solicitud");
                Console.WriteLine("  3. Ver solicitudes pendientes");
                Console.WriteLine();
                Pantalla.Seccion("Notas del turno (pila)");
                Console.WriteLine("  4. Agregar nota al historial");
                Console.WriteLine("  5. Ver notas del turno");
                Console.WriteLine("  6. Quitar ultima nota");
                Console.WriteLine();
                Pantalla.Seccion("Turnos del personal (lista circular)");
                Console.WriteLine("  7. Ver turnos del personal");
                Console.WriteLine("  8. Registrar nuevo turno");
                Console.WriteLine("  9. Siguiente turno en rotacion");
                Console.WriteLine("  0. Volver");
                Console.WriteLine();

                int opcion = EntradaConsola.LeerOpcion("Opcion", 0, 9);

                switch (opcion)
                {
                    case 1: NuevaSolicitud(solicitudes); break;
                    case 2: AtenderSolicitud(solicitudes, pila); break;
                    case 3: VerSolicitudes(solicitudes); break;
                    case 4: AgregarNota(pila); break;
                    case 5: VerNotas(pila); break;
                    case 6: QuitarNota(pila); break;
                    case 7: VerTurnos(turnos); break;
                    case 8: RegistrarTurno(turnos); break;
                    case 9: SiguienteTurno(turnos); break;
                    case 0: return;
                }
            }
        }

        private static void NuevaSolicitud(ColaSolicitudes solicitudes)
        {
            Pantalla.Limpiar();
            Pantalla.Titulo("NUEVA SOLICITUD DE SERVICIO");

            Solicitud sol = new Solicitud();
            sol.Codigo = EntradaConsola.LeerCodigo("Codigo de solicitud");
            sol.Cliente = EntradaConsola.LeerLetras("Nombre del cliente");
            sol.Detalle = EntradaConsola.LeerLetrasNumeros("Detalle del servicio", 3, 80);

            solicitudes.Encolar(sol);
            Pantalla.Ok("Solicitud registrada. En cola: " + solicitudes.Contar());
            Pantalla.Pausa();
        }

        private static void AtenderSolicitud(ColaSolicitudes solicitudes, PilaAcciones pila)
        {
            Pantalla.Limpiar();
            Pantalla.Titulo("ATENDER SIGUIENTE SOLICITUD");

            Solicitud s = solicitudes.Desencolar();
            if (s == null) { Pantalla.Info("No hay solicitudes pendientes."); Pantalla.Pausa(); return; }

            Console.WriteLine();
            Console.WriteLine("  Atendiendo: [" + s.Codigo + "] " + s.Cliente);
            Console.WriteLine("  Detalle: " + s.Detalle);
            pila.Apilar("Solicitud atendida: " + s.Codigo + " - " + s.Cliente);
            Pantalla.Ok("Solicitud marcada como atendida. Quedan: " + solicitudes.Contar());
            Pantalla.Pausa();
        }

        private static void VerSolicitudes(ColaSolicitudes solicitudes)
        {
            Pantalla.Limpiar();
            Pantalla.Titulo("SOLICITUDES PENDIENTES");

            int total = solicitudes.Contar();
            if (total == 0) { Pantalla.Info("Cola vacia. No hay solicitudes."); Pantalla.Pausa(); return; }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  " + total + " solicitudes en cola:");
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

        private static void AgregarNota(PilaAcciones pila)
        {
            Pantalla.Limpiar();
            Pantalla.Titulo("AGREGAR NOTA AL HISTORIAL");
            string nota = EntradaConsola.LeerLetrasNumeros("Nota del turno", 3, 100);
            pila.Apilar("[" + DateTime.Now.ToString("HH:mm") + "] " + nota);
            Pantalla.Ok("Nota guardada en la pila.");
            Pantalla.Pausa();
        }

        private static void VerNotas(PilaAcciones pila)
        {
            Pantalla.Limpiar();
            Pantalla.Titulo("NOTAS DEL TURNO");

            var actual = pila.Cima;
            if (actual == null) { Pantalla.Info("No hay notas en la pila."); Pantalla.Pausa(); return; }

            int i = 1;
            while (actual != null)
            {
                Console.WriteLine("  " + i + ". " + actual.Accion);
                actual = actual.Siguiente;
                i++;
            }
            Pantalla.Pausa();
        }

        private static void QuitarNota(PilaAcciones pila)
        {
            Pantalla.Limpiar();
            Pantalla.Titulo("QUITAR ULTIMA NOTA");
            string nota = pila.Desapilar();
            if (nota == null) { Pantalla.Info("La pila esta vacia."); Pantalla.Pausa(); return; }
            Pantalla.Ok("Nota quitada: " + nota);
            Pantalla.Pausa();
        }

        private static void VerTurnos(ListaTurnosCircular turnos)
        {
            Pantalla.Limpiar();
            Pantalla.Titulo("TURNOS DEL PERSONAL");
            if (turnos.Cabecera == null) { Pantalla.Info("No hay turnos registrados."); Pantalla.Pausa(); return; }
            turnos.Mostrar();
            Pantalla.Pausa();
        }

        private static void RegistrarTurno(ListaTurnosCircular turnos)
        {
            Pantalla.Limpiar();
            Pantalla.Titulo("REGISTRAR TURNO");
            Turno turno = new Turno();

            while (true)
            {
                turno.Codigo = EntradaConsola.LeerCodigo("Codigo de turno");
                if (turnos.Buscar(turno.Codigo) == null) break;
                Pantalla.Error("Ese codigo ya existe.");
            }

            turno.Empleado = EntradaConsola.LeerLetras("Nombre del empleado");
            turno.Area = EntradaConsola.LeerLetras("Area asignada");

            turnos.Insertar(turno);
            Pantalla.Ok("Turno registrado.");
            Pantalla.Pausa();
        }

        private static void SiguienteTurno(ListaTurnosCircular turnos)
        {
            Pantalla.Limpiar();
            Pantalla.Titulo("SIGUIENTE TURNO EN ROTACION");
            Turno t = turnos.SiguienteTurno();
            if (t == null) { Pantalla.Info("No hay turnos registrados."); Pantalla.Pausa(); return; }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  Empleado en turno: " + t.Empleado);
            Console.WriteLine("  Area:              " + t.Area);
            Console.WriteLine("  Codigo:            " + t.Codigo);
            Console.ResetColor();
            Pantalla.Pausa();
        }
    }
}
