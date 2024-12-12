namespace OlimpoBlazor.Models
{
    public class Factura
    {
        public int Id { get; set; } // Identificador único de la factura
        public int UsuarioId { get; set; } // Identificador del cliente asociado
        public string NombreCliente { get; set; } // Nombre del cliente
        public string Tipo { get; set; } // Tipo de factura (Ingreso/Egreso)
        public DateTime FechaEmision { get; set; } // Fecha de emisión de la factura
        public string Concepto { get; set; } // Detalle del concepto facturado
        public decimal Total { get; set; } // Total a pagar
    }
}
