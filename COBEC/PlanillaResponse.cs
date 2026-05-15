using System;
using System.Collections.Generic;
using System.Text;

namespace COBEC
{
    /// <summary>
    /// Respuesta de consulta de planilla del empleado
    /// </summary>
    /// 
    public class PlanillaResponseHeader
    {
        public int TotalEmpleados { get; set; }
        public List<PlanillaResponse> Empleados { get; set; } 
    
    }
    public class PlanillaResponse
    {
        /// <summary>Código del empleado -->Ejemplo: 10017</summary>        
        public string Codigo { get; set; }

        /// <summary>Nombre completo del empleado  -->Ejemplo:KAN**** A****, NATA** VER****</summary>        
        public string NombreEmpleado { get; set; }

        /// <summary>Tipo de Plantilla  -->Ejemplo: EM</summary>     
        public string TipoPlanilla { get; set; }

        /// <summary>Descripcion Planilla  -->Ejemplo: EM = Empleados</summary>   
        public string DescripcionPlanilla { get; set; }

        /// <summary>Tipo Documento  -->Ejemplo: D: D.N.I </summary>  
        public string TipoDocumento { get; set; }

        /// <summary>Numero Documento  -->Ejemplo: 0**5*5*5 </summary>  
        public string NumeroDocumento { get; set; }

        /// <summary>Codigo cargo  -->Ejemplo: 38 </summary>  
        public string CodigoCargo { get; set; }

        /// <summary>Descripcion cargo  -->Ejemplo: 38: ASISTENTE DE FINANZAS </summary> 
        public string DescripcionCargo { get; set; }

        /// <summary>Sucursal  -->Ejemplo: OFICINA PRINCIPAL </summary>   
        public string Sucursal { get; set; }

        /// <summary>Centro costo  -->Ejemplo: General Logística</summary> 
        public string CentroCosto { get; set; }

        /// <summary>Fecha ingreso  -->Ejemplo: 2009-06-01T00:00:00 </summary> 
        public DateTime? FechaIngreso { get; set; }

        /// <summary>Fecha cese  -->Ejemplo: 2009-06-01T00:00:00 </summary> 
        public DateTime? FechaCese { get; set; }

        /// <summary>Tipo contrato  -->Ejemplo: IN </summary> 
        public string TipoContrato { get; set; }

        /// <summary>Descripcion Tipo contrato  -->Ejemplo: IN : A Plazo Indeterminado </summary> 
        public string DescripcionTipoContrato { get; set; }

        /// <summary>Codigo AFP  -->Ejemplo: 04</summary> 
        public string CodigoAFP { get; set; }

        /// <summary>Nombre AFP  -->Ejemplo: 04: Prima</summary> 
        public string NombreAFP { get; set; }

        /// <summary>Numero AFP  -->Ejemplo: 56**0**KA**1</summary> 
        public string NumeroAFP { get; set; }
        public decimal? Sueldo { get; set; }
        public int? DiasTrabajados { get; set; }
        public decimal? HorasTrabajadas { get; set; }
        public decimal? TotalIngresos { get; set; }
        public decimal? TotalEgresos { get; set; }
        public decimal? TotalNeto { get; set; }
        public string CuentaAbono { get; set; }
        public string MonedaPago { get; set; }

        // Lista de DETALLE (conceptos)
        public List<PlanillaDetalleItem> Conceptos { get; set; }
    }

    // Clase para cada item del detalle
    public class PlanillaDetalleItem
    {
        /// <summary>Concepto -->Concepto--> codigo que identifica el concepto , la descripcion se encuentra en Texto Impresion  </summary> 
        public string Concepto { get; set; }
        public decimal? Monto { get; set; }
        public decimal? Cantidad { get; set; }
        public decimal? Saldo { get; set; }

        /// <summary>Tipo concepto-->Ejemplo: IN:Ingresos | DE:Deducciones </summary> 
        public string TipoConcepto { get; set; }
        public int? PlanillaOrden { get; set; }

        /// <summary>Texto Impresion --> Descripcion del concepto ejemplo: 0100:GRATIFICACION ORDINARIA | 0200:Vacaciones  </summary> 
        public string TextoImpresion { get; set; }
    }
}
