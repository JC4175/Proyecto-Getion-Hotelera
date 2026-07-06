using System;

namespace ProyectoFinal_T3_AVANCE_SIMPLE
{
    internal partial class Program
    {
        static void MenuClientes()
        {
            while (true)
            {
                Limpiar();
                Titulo("CLIENTES FRECUENTES CON PUNTOS - LISTA DOBLE");
                Console.WriteLine("1. Registrar cliente frecuente");
                Console.WriteLine("2. Mostrar de inicio a fin");
                Console.WriteLine("3. Mostrar de fin a inicio");
                Console.WriteLine("4. Buscar cliente frecuente");
                Console.WriteLine("0. Volver");

                int opcion = LeerOpcion("Opcion", 0, 4);

                switch (opcion)
                {
                    case 1: RegistrarCliente(); break;
                    case 2: MostrarClientesAdelante(); break;
                    case 3: MostrarClientesAtras(); break;
                    case 4: BuscarCliente(); break;
                    case 0: return;
                }
            }
        }

        static void RegistrarCliente()
        {
            Limpiar();
            Titulo("REGISTRAR CLIENTE FRECUENTE");

            Cliente cliente = new Cliente();
            cliente.Codigo = LeerCodigoNuevoCliente();
            cliente.Nombre = LeerLetras("Nombre");
            cliente.Puntos = LeerEntero("Puntos", 0, 100000);

            clientes.Insertar(cliente);
            pila.Apilar("Cliente frecuente registrado: " + cliente.Codigo);
            Mensaje("Cliente frecuente registrado.");
            Pausa();
        }

        static string LeerCodigoNuevoCliente()
        {
            while (true)
            {
                string codigo = LeerCodigo("Codigo");
                if (clientes.Buscar(codigo) == null)
                {
                    return codigo;
                }
                Error("Ese codigo ya existe.");
            }
        }

        static void MostrarClientesAdelante()
        {
            Limpiar();
            Titulo("CLIENTES FRECUENTES DE INICIO A FIN");
            clientes.MostrarAdelante();
            Pausa();
        }

        static void MostrarClientesAtras()
        {
            Limpiar();
            Titulo("CLIENTES FRECUENTES DE FIN A INICIO");
            clientes.MostrarAtras();
            Pausa();
        }

        static void BuscarCliente()
        {
            Limpiar();
            Titulo("BUSCAR CLIENTE FRECUENTE");

            string codigo = LeerCodigo("Codigo");
            NodoCliente nodo = clientes.Buscar(codigo);

            if (nodo == null)
            {
                Error("Cliente no encontrado.");
            }
            else
            {
                Console.WriteLine(nodo.Dato.Codigo + " - " + nodo.Dato.Nombre);
            }
            Pausa();
        }

        static void MenuTurnos()
        {
            while (true)
            {
                Limpiar();
                Titulo("TURNOS DEL PERSONAL - LISTA CIRCULAR");
                Console.WriteLine("1. Registrar turno del personal");
                Console.WriteLine("2. Mostrar turnos registrados");
                Console.WriteLine("3. Buscar turno");
                Console.WriteLine("4. Ver siguiente turno rotativo");
                Console.WriteLine("0. Volver");

                int opcion = LeerOpcion("Opcion", 0, 4);

                switch (opcion)
                {
                    case 1: RegistrarTurno(); break;
                    case 2: MostrarTurnos(); break;
                    case 3: BuscarTurno(); break;
                    case 4: SiguienteTurno(); break;
                    case 0: return;
                }
            }
        }

        static void RegistrarTurno()
        {
            Limpiar();
            Titulo("REGISTRAR TURNO");

            Turno turno = new Turno();
            turno.Codigo = LeerCodigoNuevoTurno();
            turno.Empleado = LeerLetras("Empleado");
            turno.Area = LeerLetrasNumeros("Area", 3, 40);

            turnos.Insertar(turno);
            pila.Apilar("Turno registrado: " + turno.Codigo);
            Mensaje("Turno registrado.");
            Pausa();
        }

        static void SiguienteTurno()
        {
            Limpiar();
            Titulo("SIGUIENTE TURNO ROTATIVO");

            Turno turno = turnos.SiguienteTurno();

            if (turno == null)
            {
                Error("No hay turnos registrados.");
            }
            else
            {
                Console.WriteLine(turno.Codigo + " - " + turno.Empleado + " - Area: " + turno.Area);
                Console.WriteLine("Al volver a elegir esta opcion se avanza al siguiente nodo circular.");
            }

            Pausa();
        }

        static string LeerCodigoNuevoTurno()
        {
            while (true)
            {
                string codigo = LeerCodigo("Codigo");
                if (turnos.Buscar(codigo) == null)
                {
                    return codigo;
                }
                Error("Ese codigo ya existe.");
            }
        }

        static void MostrarTurnos()
        {
            Limpiar();
            Titulo("TURNOS REGISTRADOS");
            turnos.Mostrar();
            Pausa();
        }

        static void BuscarTurno()
        {
            Limpiar();
            Titulo("BUSCAR TURNO");

            string codigo = LeerCodigo("Codigo");
            NodoTurno nodo = turnos.Buscar(codigo);

            if (nodo == null)
            {
                Error("Turno no encontrado.");
            }
            else
            {
                Console.WriteLine(nodo.Dato.Codigo + " - " + nodo.Dato.Empleado);
            }
            Pausa();
        }

    }
}
