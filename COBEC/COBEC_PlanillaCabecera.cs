using System;
using System.Collections.Generic;
using System.Text;

namespace COBEC
{
    public class COBEC_PlanillaCabecera
    {
        public int Codigo { get; set; }
        public string NombreEmpleado { get; set; }
        public string TipoPlanilla { get; set; }
        public string DescripcionPlanilla { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string CodigoCargo { get; set; }
        public string DescripcionCargo { get; set; }
        public string Sucursal { get; set; }
        public string descentrocosto { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public DateTime? FechaCese { get; set; }
        public string TipoContrato { get; set; }
        public string DescripcionTipoContrato { get; set; }
        public string CodigoAFP { get; set; }
        public string NombreAFP { get; set; }
        public string NumeroAFP { get; set; }
        public DateTime? VacacionDesde { get; set; }
        public DateTime? VacacionHasta { get; set; }
        public decimal? Sueldo { get; set; }
        public decimal? SueldoBasicoDolar { get; set; }
        public int? DiasTrabajados { get; set; }
        public decimal? HorasTrabajadas { get; set; }
        public decimal? TotalIngresos { get; set; }
        public decimal? TotalEgresos { get; set; }
        public decimal? TotalPatronales { get; set; }
        public decimal? TotalNeto { get; set; }
        public string CuentaAbono { get; set; }
        public string MonedaPago { get; set; }
    }
}
