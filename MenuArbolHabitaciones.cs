using System;

namespace ProyectoFinal_T3_AVANCE_SIMPLE
{
    internal partial class Program
    {
        static void MenuArbol()
        {
            while (true)
            {
                Limpiar();
                Titulo("ORGANIGRAMA DEL HOTEL - ARBOL");
                Console.WriteLine("1. Dibujar organigrama");
                Console.WriteLine("2. Recorrido PreOrden");
                Console.WriteLine("3. Recorrido InOrden");
                Console.WriteLine("4. Recorrido PosOrden");
                Console.WriteLine("0. Volver");

                int opcion = LeerOpcion("Opcion", 0, 4);

                switch (opcion)
                {
                    case 1: DibujarOrganigrama(); break;
                    case 2: MostrarPreOrden(); break;
                    case 3: MostrarInOrden(); break;
                    case 4: MostrarPosOrden(); break;
                    case 0: return;
                }
            }
        }

        static void DibujarOrganigrama()
        {
            Limpiar();
            Titulo("DIBUJO DEL ORGANIGRAMA");
            arbol.Dibujar();
            Pausa();
        }

        static void MostrarPreOrden()
        {
            Limpiar();
            Titulo("PREORDEN");
            arbol.PreOrden();
            Pausa();
        }

        static void MostrarInOrden()
        {
            Limpiar();
            Titulo("INORDEN");
            arbol.InOrden();
            Pausa();
        }

        static void MostrarPosOrden()
        {
            Limpiar();
            Titulo("POSORDEN");
            arbol.PosOrden();
            Pausa();
        }

        static void MenuABB()
        {
            while (true)
            {
                Limpiar();
                Titulo("CATALOGO DE HABITACIONES - ABB");
                Console.WriteLine("1. Agregar habitacion al ABB por numero");
                Console.WriteLine("2. Eliminar habitacion del ABB si no esta reservada");
                Console.WriteLine("3. Buscar habitacion");
                Console.WriteLine("4. Mostrar habitaciones ordenadas por numero");
                Console.WriteLine("5. Dibujar ABB por numero de habitacion");
                Console.WriteLine("6. Mostrar habitaciones disponibles");
                Console.WriteLine("7. Mostrar habitaciones ocupadas");
                Console.WriteLine("8. Mostrar catalogo visual por colores");
                Console.WriteLine("9. Ver ficha de una habitacion");
                Console.WriteLine("0. Volver");

                int opcion = LeerOpcion("Opcion", 0, 9);

                switch (opcion)
                {
                    case 1: RegistrarHabitacion(); break;
                    case 2: EliminarHabitacion(); break;
                    case 3: BuscarHabitacion(); break;
                    case 4: MostrarHabitaciones(); break;
                    case 5: DibujarABBHabitaciones(); break;
                    case 6: MostrarHabitacionesDisponibles(); break;
                    case 7: MostrarHabitacionesOcupadas(); break;
                    case 8: MostrarCatalogoVisual(); break;
                    case 9: VerFichaHabitacion(); break;
                    case 0: return;
                }
            }
        }

        static void RegistrarHabitacion()
        {
            if (!ValidarPermisoAdmin())
            {
                return;
            }

            Limpiar();
            Titulo("AGREGAR HABITACION AL ABB");

            Habitacion habitacion = new Habitacion();
            habitacion.Numero = LeerNumeroHabitacionNuevo();
            habitacion.Tipo = LeerLetras("Tipo");
            habitacion.Precio = LeerDecimal("Precio por noche", 1);
            habitacion.Disponible = true;
            habitacion.Estado = "Disponible";

            abb.Insertar(habitacion);
            pila.Apilar("Habitacion registrada: " + habitacion.Numero);
            Mensaje("Habitacion registrada.");
            Pausa();
        }

        static void EliminarHabitacion()
        {
            if (!ValidarPermisoAdmin())
            {
                return;
            }

            Limpiar();
            Titulo("ELIMINAR HABITACION DEL ABB");

            int numero = LeerEntero("Numero", 1, 999);

            if (reservas.ExisteHabitacion(numero))
            {
                Error("No se puede eliminar. Hay una reserva usando esta habitacion.");
                Console.WriteLine("Primero elimine la reserva si realmente desea retirar la habitacion del catalogo.");
                Pausa();
                return;
            }

            if (abb.Eliminar(numero))
            {
                pila.Apilar("Habitacion eliminada del ABB: " + numero);
                Mensaje("Habitacion eliminada.");
            }
            else
            {
                Error("Habitacion no encontrada.");
            }

            Pausa();
        }

        static int LeerNumeroHabitacionNuevo()
        {
            while (true)
            {
                int numero = LeerEntero("Numero", 1, 999);
                if (abb.Buscar(numero) == null)
                {
                    return numero;
                }
                Error("Esa habitacion ya existe.");
            }
        }

        static void BuscarHabitacion()
        {
            Limpiar();
            Titulo("BUSCAR HABITACION");

            int numero = LeerEntero("Numero", 1, 999);
            NodoABB nodo = abb.Buscar(numero);

            if (nodo == null)
            {
                Error("Habitacion no encontrada.");
            }
            else
            {
                Console.WriteLine("Hab. " + nodo.Dato.Numero + " - " + nodo.Dato.Tipo +
                    " - Precio por noche: S/ " + nodo.Dato.Precio.ToString("0.00") +
                    " - Estado: " + nodo.Dato.Estado);
            }
            Pausa();
        }

        static void MostrarHabitaciones()
        {
            Limpiar();
            Titulo("HABITACIONES EN ORDEN POR NUMERO");
            abb.InOrden();
            Pausa();
        }

        static void DibujarABBHabitaciones()
        {
            Limpiar();
            Titulo("DIBUJO DEL ABB POR NUMERO DE HABITACION");
            abb.Dibujar();
            Pausa();
        }

        static void MostrarHabitacionesDisponibles()
        {
            Limpiar();
            Titulo("HABITACIONES DISPONIBLES PARA HOSPEDARSE");
            abb.MostrarDisponibles();
            Pausa();
        }

        static void MostrarHabitacionesOcupadas()
        {
            Limpiar();
            Titulo("HABITACIONES OCUPADAS POR CHECK-IN");
            abb.MostrarOcupadas();
            Pausa();
        }

        static void MostrarCatalogoVisual()
        {
            Limpiar();
            Titulo("CATALOGO VISUAL DE HABITACIONES");
            Console.WriteLine("Verde: Disponible | Amarillo: Reservada | Rojo: Ocupada");
            Console.WriteLine();
            abb.MostrarCatalogoVisual();
            Pausa();
        }

        static void VerFichaHabitacion()
        {
            Limpiar();
            Titulo("FICHA DE HABITACION");

            int numero = LeerEntero("Numero", 1, 999);
            NodoABB habitacion = abb.Buscar(numero);

            if (habitacion == null)
            {
                Error("Habitacion no encontrada.");
                Pausa();
                return;
            }

            Console.WriteLine("Habitacion: " + habitacion.Dato.Numero);
            Console.WriteLine("Tipo: " + habitacion.Dato.Tipo);
            Console.WriteLine("Precio por noche: S/ " + habitacion.Dato.Precio.ToString("0.00"));
            Console.WriteLine("Estado: " + habitacion.Dato.Estado);
            Console.WriteLine();

            NodoReserva reserva = reservas.BuscarActivaPorHabitacion(numero);
            if (reserva == null)
            {
                Console.WriteLine("No hay reserva ni huesped activo en esta habitacion.");
            }
            else
            {
                Console.WriteLine("Ficha asociada:");
                MostrarFichaReserva(reserva.Dato);
            }

            Pausa();
        }

    }
}
