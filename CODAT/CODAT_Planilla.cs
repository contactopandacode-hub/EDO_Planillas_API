using COBE;
using COBEC;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;
using System.Text;
namespace CODAT
{
    public class CODAT_Planilla : DbContext

    {
        public DbContext context { get; set; }
        private readonly IConfiguration configuration;
        public CODAT_Planilla(IConfiguration _configuration, DbContext _context)
        {
            configuration = _configuration;
            this.context = _context;
        }

        public List<COBEC_PlanillaCabecera> Planilla_Boleta_ConsultarCabecera(COBEC_Planilla par_objeto)
        {
            List<COBEC_PlanillaCabecera> listaEmpleados = new List<COBEC_PlanillaCabecera> ();    
            string str_resultado = string.Empty;
            DbCommand _Command = context.Database.GetDbConnection().CreateCommand();
            COBEc_Error obj_error = new COBEc_Error();


            try
            {
                _Command.CommandType = System.Data.CommandType.StoredProcedure;
                _Command.CommandText = "SNP_API_Planilla_ObtenerDatos";
                _Command.Connection.Open();

                SqlParameter par_periodo = new SqlParameter("@par_periodo", par_objeto.periodo);
                _Command.Parameters.Add(par_periodo);

                SqlParameter par_compania = new SqlParameter("@par_compania", par_objeto.compania);
                _Command.Parameters.Add(par_compania);

                SqlParameter par_tipoproceso = new SqlParameter("@par_tipoproceso", par_objeto.tipoProceso);
                _Command.Parameters.Add(par_tipoproceso);

                SqlParameter par_tipo = new SqlParameter("@par_tipoplanilla", par_objeto.tipoPlanilla);
                _Command.Parameters.Add(par_tipo);

                SqlParameter par_empleado = new SqlParameter("@par_empleado", string.IsNullOrEmpty(par_objeto.empleado) ? (object)DBNull.Value : par_objeto.empleado);
                _Command.Parameters.Add(par_empleado);


                DbDataReader reader = _Command.ExecuteReader();


                while (reader.Read())
                {
                    COBEC_PlanillaCabecera objdatos = new COBEC_PlanillaCabecera
                    {
                        Codigo = Convert.ToInt32(reader["Codigo"]),
                        NombreEmpleado = Convert.IsDBNull(reader["NombreEmpleado"]) ? null : reader["NombreEmpleado"].ToString(),
                        TipoPlanilla = Convert.IsDBNull(reader["TipoPlanilla"]) ? null : reader["TipoPlanilla"].ToString(),
                        DescripcionPlanilla = Convert.IsDBNull(reader["DescripcionPlanilla"]) ? null : reader["DescripcionPlanilla"].ToString(),
                        TipoDocumento = Convert.IsDBNull(reader["TipoDocumento"]) ? null : reader["TipoDocumento"].ToString(),
                        NumeroDocumento = Convert.IsDBNull(reader["NumeroDocumento"]) ? null : reader["NumeroDocumento"].ToString(),
                        CodigoCargo = Convert.IsDBNull(reader["CodigoCargo"]) ? null : reader["CodigoCargo"].ToString(),
                        DescripcionCargo = Convert.IsDBNull(reader["DescripcionCargo"]) ? null : reader["DescripcionCargo"].ToString(),
                        Sucursal = Convert.IsDBNull(reader["Sucursal"]) ? null : reader["Sucursal"].ToString(),
                        descentrocosto = Convert.IsDBNull(reader["descentrocosto"]) ? null : reader["descentrocosto"].ToString(),
                        FechaIngreso = reader["FechaIngreso"] as DateTime?,
                        FechaCese = reader["FechaCese"] as DateTime?,
                        TipoContrato = Convert.IsDBNull(reader["TipoContrato"]) ? null : reader["TipoContrato"].ToString(),
                        DescripcionTipoContrato = Convert.IsDBNull(reader["DescripcionTipoContrato"]) ? null : reader["DescripcionTipoContrato"].ToString(),
                        CodigoAFP = Convert.IsDBNull(reader["CodigoAFP"]) ? null : reader["CodigoAFP"].ToString(),
                        NombreAFP = Convert.IsDBNull(reader["NombreAFP"]) ? null : reader["NombreAFP"].ToString(),
                        NumeroAFP = Convert.IsDBNull(reader["NumeroAFP"]) ? null : reader["NumeroAFP"].ToString(),
                        VacacionDesde = reader["VacacionDesde"] as DateTime?,
                        VacacionHasta = reader["VacacionHasta"] as DateTime?,
                        Sueldo = Convert.IsDBNull(reader["Sueldo"]) ? (decimal?)null : Convert.ToDecimal(reader["Sueldo"]),
                        SueldoBasicoDolar = Convert.IsDBNull(reader["SueldoBasicoDolar"]) ? (decimal?)null : Convert.ToDecimal(reader["SueldoBasicoDolar"].ToString()),
                        DiasTrabajados = reader["DiasTrabajados"] as int?,
                        HorasTrabajadas = Convert.IsDBNull(reader["HorasTrabajadas"]) ? (decimal?)null : Convert.ToDecimal(reader["HorasTrabajadas"].ToString()),
                        TotalIngresos = Convert.IsDBNull(reader["TotalIngresos"]) ? (decimal?)null : Convert.ToDecimal(reader["TotalIngresos"].ToString()),
                        TotalEgresos = Convert.IsDBNull(reader["TotalEgresos"]) ? (decimal?)null : Convert.ToDecimal(reader["TotalEgresos"].ToString()),
                        TotalPatronales = Convert.IsDBNull(reader["TotalPatronales"]) ? (decimal?)null : Convert.ToDecimal(reader["TotalPatronales"]),
                        TotalNeto = Convert.IsDBNull(reader["TotalNeto"]) ? (decimal?)null :Convert.ToDecimal(reader["TotalNeto"].ToString()),
                        CuentaAbono = Convert.IsDBNull(reader["CuentaAbono"]) ? null : reader["CuentaAbono"].ToString(),
                        MonedaPago = Convert.IsDBNull(reader["MonedaPago"]) ? null : reader["MonedaPago"].ToString()
                    }; 

                    listaEmpleados.Add(objdatos);
                }

                _Command.Connection.Close();
            }

            catch (Exception ex)
            {
                _Command.Connection.Close();
                throw ex;
            }
            return listaEmpleados;
        }

        public List<COBEC_PlanillaDetalle> Planilla_Boleta_ConsultarDetalle(COBEC_Planilla par_objeto, int par_idcodigo)
        {
            string str_resultado = string.Empty;
            DbCommand _Command = context.Database.GetDbConnection().CreateCommand();
            List<COBEC_PlanillaDetalle> listaDetalle = new List<COBEC_PlanillaDetalle>();
            COBEc_Error obj_error = new COBEc_Error();

            try
            {
                _Command.CommandType = System.Data.CommandType.StoredProcedure;
                _Command.CommandText = "SNP_API_Planilla_ObtenerDetalle";
                _Command.Connection.Open();

                SqlParameter par_periodo = new SqlParameter("@par_periodo", par_objeto.periodo);
                _Command.Parameters.Add(par_periodo);

                SqlParameter par_compania = new SqlParameter("@par_compania", par_objeto.compania);
                _Command.Parameters.Add(par_compania);

                SqlParameter par_tipoproceso = new SqlParameter("@par_tipoproceso", par_objeto.tipoProceso);
                _Command.Parameters.Add(par_tipoproceso);

                SqlParameter par_tipo = new SqlParameter("@par_tipoplanilla", par_objeto.tipoPlanilla);
                _Command.Parameters.Add(par_tipo);

                SqlParameter par_empleado = new SqlParameter("@par_empleado", par_idcodigo);
                _Command.Parameters.Add(par_empleado);

                DbDataReader reader = _Command.ExecuteReader();

                while (reader.Read())
                {
                    COBEC_PlanillaDetalle detalle = new COBEC_PlanillaDetalle
                    {
                        Concepto = Convert.IsDBNull(reader["Concepto"]) ? null : reader["Concepto"].ToString(),
                        Monto = Convert.IsDBNull(reader["monto"]) ? (decimal?)null : Convert.ToDecimal(reader["monto"]),
                        Cantidad = Convert.IsDBNull(reader["cantidad"]) ? (decimal?)null : Convert.ToDecimal(reader["cantidad"]),
                        Saldo = Convert.IsDBNull(reader["saldo"]) ? (decimal?)null : Convert.ToDecimal(reader["saldo"]),
                        TipoConcepto = Convert.IsDBNull(reader["TipoConcepto"]) ? null : reader["TipoConcepto"].ToString(),
                        PlanillaOrden = reader["PlanillaOrden"] as int?,
                        TextoImpresion = Convert.IsDBNull(reader["TextoImpresion"]) ? null : reader["TextoImpresion"].ToString()
                    };

                    listaDetalle.Add(detalle);
                }

                _Command.Connection.Close();
            }

            catch (Exception ex)
            {
                _Command.Connection.Close();
                throw ex;
            }

            return listaDetalle;
        }
    }
}
