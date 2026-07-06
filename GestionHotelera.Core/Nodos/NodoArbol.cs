namespace GestionHotelera.Core.Nodos
{
    public class NodoArbol
    {
        public string Cargo;
        public NodoArbol Izquierda;
        public NodoArbol Derecha;

        public NodoArbol(string cargo)
        {
            Cargo = cargo;
        }
    }
}
