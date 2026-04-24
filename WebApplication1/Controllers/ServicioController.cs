using COBE;
using COBEC;
using CODAT;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using Microsoft.OpenApi.Writers;
using Newtonsoft.Json;
using ServicioRSNetCore.Controllers.Clases;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

namespace ServicioRSNetCore.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ServicioController : ControllerBase
    {
        private readonly IConfiguration configuration;
        public DbContext context { get; set; }

        public ServicioController(IConfiguration _configuration, DbContext _context)
        {
            configuration = _configuration;
            this.context = _context;
        }

        /// <summary>
        /// Consulta la boleta de planilla de un empleado
        /// </summary>
        /// <param name="cobec">Parámetros de consulta</param>
        /// <returns>Datos de cabecera y detalle de la planilla</returns>
        /// <response code="200">Retorna la planilla correctamente</response>
        /// <response code="401">No autorizado</response>

        [HttpGet("[action]")]
        [ProducesResponseType(typeof(PlanillaResponse), 200)]  
        [ProducesResponseType(401)]
        public ActionResult ConsultarPlanilla ([FromQuery] COBEC_Planilla cobec)
        {
            COBEc_Error obj_error = new COBEc_Error();
            CODAT_Planilla obj_datos = new CODAT_Planilla(configuration, this.context);
            string strResultado = string.Empty;

            try
            {
                COBEC_PlanillaCabecera cabecera = obj_datos.Planilla_Boleta_ConsultarCabecera(cobec);

                List<COBEC_PlanillaDetalle> detalle = obj_datos.Planilla_Boleta_ConsultarDetalle(cobec);

                var respuesta = new PlanillaResponse
                {
                    // Mapear datos de cabecera
                    Codigo = cabecera?.Codigo,
                    NombreEmpleado = cabecera?.NombreEmpleado,
                    TipoPlanilla = cabecera?.TipoPlanilla,
                    DescripcionPlanilla = cabecera?.DescripcionPlanilla,
                    TipoDocumento = cabecera?.TipoDocumento,
                    NumeroDocumento = cabecera?.NumeroDocumento,
                    CodigoCargo = cabecera?.CodigoCargo,
                    DescripcionCargo = cabecera?.DescripcionCargo,
                    Sucursal = cabecera?.Sucursal,
                    CentroCosto = cabecera?.descentrocosto,
                    FechaIngreso = cabecera?.FechaIngreso,
                    FechaCese = cabecera?.FechaCese,
                    TipoContrato = cabecera?.TipoContrato,
                    DescripcionTipoContrato = cabecera?.DescripcionTipoContrato,
                    CodigoAFP = cabecera?.CodigoAFP,
                    NombreAFP = cabecera?.NombreAFP,
                    NumeroAFP = cabecera?.NumeroAFP,
                    Sueldo = cabecera?.Sueldo,
                    DiasTrabajados = cabecera?.DiasTrabajados,
                    HorasTrabajadas = cabecera?.HorasTrabajadas,
                    TotalIngresos = cabecera?.TotalIngresos,
                    TotalEgresos = cabecera?.TotalEgresos,
                    TotalNeto = cabecera?.TotalNeto,
                    CuentaAbono = cabecera?.CuentaAbono,
                    MonedaPago = cabecera?.MonedaPago,                    

                    // Mapear lista de detalle
                    Conceptos = detalle?.Select(d => new PlanillaDetalleItem
                    {
                        Concepto = d.Concepto,
                        Monto = d.Monto,
                        Cantidad = d.Cantidad,
                        Saldo = d.Saldo,
                        TipoConcepto = d.TipoConcepto,
                        PlanillaOrden = d.PlanillaOrden,
                        TextoImpresion = d.TextoImpresion?.Trim()
                    }).ToList()
                };

                return Ok(respuesta);
            }
            catch (Exception e)
            {
                obj_error.codigo = "99";
                obj_error.mensaje = e.Message;
                return Ok(obj_error);
            }          

            //return Ok(obj_error);

        }


    }
}

