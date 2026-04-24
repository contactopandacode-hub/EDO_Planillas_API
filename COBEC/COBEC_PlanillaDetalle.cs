using System;
using System.Collections.Generic;
using System.Text;

namespace COBEC
{
    public class COBEC_PlanillaDetalle
    {
        public string Concepto { get; set; }
        public decimal? Monto { get; set; }
        public decimal? Cantidad { get; set; }
        public decimal? Saldo { get; set; }
        public string TipoConcepto { get; set; }
        public int? PlanillaOrden { get; set; }
        public string TextoImpresion { get; set; }
    }
}
