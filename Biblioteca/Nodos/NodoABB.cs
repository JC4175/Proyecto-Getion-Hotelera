using GestionHotelera.Modelos;

namespace GestionHotelera.Nodos
{
    public class NodoABB
    {
        public Habitacion Dato;
        public NodoABB Izquierda;
        public NodoABB Derecha;

        public NodoABB(Habitacion habitacion)
        {
            Dato = habitacion;
        }
    }
}
