using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    public class Lista
    {
        //LISTA CIRCULAR SIMPLE
        private Nodo primero = new Nodo();
        private Nodo ultimo = new Nodo();
        private Nodo index = new Nodo();

        public Lista()
        {
            primero = null;
            ultimo = null;
            index = null;
        }
        public void Add(string e)
        {
            Nodo nuevo = new Nodo();
            nuevo.Dato = e;
            if(primero == null)
            {
                primero = nuevo;
                primero.Siguiente = primero;
                index = primero;
                ultimo = primero;
            }
            else
            {
                ultimo.Siguiente = nuevo;
                nuevo.Siguiente = primero;
                ultimo = nuevo;
                index = primero;
            }
        }
        public void Next()
        {
            index = index.Siguiente;
        }
        public String GetIndex()
        {
            return index.Dato;
        }
    }
}