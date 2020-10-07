using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    public class Stack
    {
        private Nodo ancla;
        private Nodo trabajo;
        public Stack()
        {
            ancla = new Nodo();
            ancla.Siguiente = null;
        }
        // PUSH
        public void Push(string dato)
        {
            Nodo temporal = new Nodo();
            temporal.Dato = dato;
            temporal.Siguiente = ancla.Siguiente;
            ancla.Siguiente = temporal;
        }
        // POP
        public string Pop()
        {
            string valor = "";
            if (ancla.Siguiente != null)
            {
                trabajo = ancla.Siguiente;
                valor = trabajo.Dato;
                ancla.Siguiente = trabajo.Siguiente;
                trabajo.Siguiente = null;
            }
            return valor;
        }
    }
}