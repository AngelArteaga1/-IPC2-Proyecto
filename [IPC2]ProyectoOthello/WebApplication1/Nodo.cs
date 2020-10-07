using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    public class Nodo
    {
        private string dato;
        private Nodo siguiente = null;
        public string Dato { get => dato; set => dato = value; }
        internal Nodo Siguiente { get => siguiente; set => siguiente = value; }
    }
}