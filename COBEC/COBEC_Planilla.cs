using System;
using System.Collections.Generic;
using System.Text;

namespace COBEC
{
    public class COBEC_Planilla
    {
        /// <summary>Codigo de la compania ejemplo: 01000000 = EDO SOKO S.A.C. </summary>  
        /// <example>01000000</example>
        public string compania { get; set; }

        /// <summary>Periodo en formato yyyyMMdd ejemplo: 20161212</summary>
        /// <example>20161212</example>
        public string periodo { get; set; }

        /// <summary>Codigo Tipo Proceso ejemplo: NO0 = Planilla Mensual</summary>
        /// <example> NO0 </example>
        public string tipoProceso { get; set; }

        /// <summary>Codigo Tipo Planilla ejemplo: EM = Empleados</summary>
        /// <example> EM </example>
        public string tipoPlanilla { get; set; }

        /// <summary>Codigo Empleado ejemplo: 10017 = KANASHIRO ARAKAKI, NATALIA VERONICA </summary>
        /// <example> 10017 </example>
        public string empleado { get; set; }
    }
}
