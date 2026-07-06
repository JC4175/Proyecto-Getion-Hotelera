using System;

namespace ProyectoFinal_T3_AVANCE_SIMPLE
{
    internal partial class Program
    {
        static void MenuPila()
        {
            while (true)
            {
                Limpiar();
                Titulo("HISTORIAL DE MOVIMIENTOS - PILA");
                Console.WriteLine("1. Mostrar historial apilado");
                Console.WriteLine("2. Desapilar ultima nota del historial");
                Console.WriteLine("0. Volver");

                int opcion = LeerOpcion("Opcion", 0, 2);

                switch (opcion)
                {
                    case 1: MostrarPila(); break;
                    case 2: DesapilarAccion(); break;
                    case 0: return;
                }
            }
        }

        static void MostrarPila()
        {
            Limpiar();
            Titulo("NOTAS GUARDADAS EN LA PILA");
            pila.Mostrar();
            Pausa();
        }

        static void DesapilarAccion()
        {
            Limpiar();
            Titulo("DESAPILAR NOTA DEL HISTORIAL");

            string accion = pila.Desapilar();
            if (accion == null)
            {
                Error("La pila esta vacia.");
            }
            else
            {
                Mensaje("Se retiro: " + accion);
            }
            Pausa();
        }

        static void MenuCola()
        {
            while (true)
            {
                Limpiar();
                Titulo("SOLICITUDES DE HUESPEDES - COLA");
                Console.WriteLine("1. Registrar solicitud del huesped");
                Console.WriteLine("2. Atender primera solicitud");
                Console.WriteLine("3. Mostrar cola de solicitudes");
                Console.WriteLine("4. Buscar solicitud");
                Console.WriteLine("0. Volver");

                int opcion = LeerOpcion("Opcion", 0, 4);

                switch (opcion)
                {
                    case 1: RegistrarSolicitud(); break;
                    case 2: AtenderSolicitud(); break;
                    case 3: MostrarCola(); break;
                    case 4: BuscarSolicitud(); break;
                    case 0: return;
                }
            }
        }

        static void RegistrarSolicitud()
        {
            Limpiar();
            Titulo("REGISTRAR SOLICITUD");

            Solicitud solicitud = new Solicitud();
            solicitud.Codigo = LeerCodigoNuevoSolicitud();
            solicitud.Cliente = LeerLetras("Cliente");
            solicitud.Detalle = LeerLetrasNumeros("Detalle", 3, 80);

            solicitudes.Encolar(solicitud);
            pila.Apilar("Solicitud registrada: " + solicitud.Codigo);
            Mensaje("Solicitud encolada.");
            Pausa();
        }

        static string LeerCodigoNuevoSolicitud()
        {
            while (true)
            {
                string codigo = LeerCodigo("Codigo");
                if (solicitudes.Buscar(codigo) == null)
                {
                    return codigo;
                }
                Error("Ese codigo ya existe.");
            }
        }

        static void AtenderSolicitud()
        {
            Limpiar();
            Titulo("ATENDER SOLICITUD");

            Solicitud solicitud = solicitudes.Desencolar();
            if (solicitud == null)
            {
                Error("No hay solicitudes.");
            }
            else
            {
                pila.Apilar("Solicitud atendida: " + solicitud.Codigo);
                Mensaje("Atendida: " + solicitud.Codigo + " - " + solicitud.Cliente);
            }
            Pausa();
        }

        static void MostrarCola()
        {
            Limpiar();
            Titulo("COLA DE SOLICITUDES");
            solicitudes.Mostrar();
            Pausa();
        }

        static void BuscarSolicitud()
        {
            Limpiar();
            Titulo("BUSCAR SOLICITUD");

            string codigo = LeerCodigo("Codigo");
            NodoSolicitud nodo = solicitudes.Buscar(codigo);

            if (nodo == null)
            {
                Error("Solicitud no encontrada.");
            }
            else
            {
                Console.WriteLine(nodo.Dato.Codigo + " - " + nodo.Dato.Cliente);
            }
            Pausa();
        }

    }
}
