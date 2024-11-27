using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLIMPO.Modelo
{
    public class ModeloFactura
    {
        public int Id { get; set; } // Identificador único de la factura

        public string Idu { get; set; } // Nombre del cliente

        public string Cliente { get; set; } // Nombre del cliente
        public string FechaEmision { get; set; } // Fecha de emisión de la factura

        public string Concepto { get; set; } // Nombre del cliente

        public decimal Total { get; set; }// Total calculado (Subtotal + Impuesto)
    }
}
