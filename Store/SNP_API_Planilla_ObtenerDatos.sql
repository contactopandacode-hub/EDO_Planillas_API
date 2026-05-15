
ALTER PROCEDURE SNP_API_Planilla_ObtenerDatos(
@par_periodo as varchar(8),
@par_compania as varchar(8),
@par_tipoproceso as varchar(3),
@par_tipoplanilla as varchar(2),
@par_empleado as int
)
AS
BEGIN
	SELECT 
		LTRIM(RTRIM(EmpleadoMast.Empleado)) As Codigo,
		LTRIM(RTRIM(PersonaMast.NombreCompleto)) As NombreEmpleado,
		LTRIM(RTRIM(IsNull(PR_PlanillaEmpleado.TipoPlanilla, PR_TipoPlanilla.TipoPlanilla))) As TipoPlanilla,
		LTRIM(RTRIM(PR_TipoPlanilla.Descripcion)) AS DescripcionPlanilla,
		LTRIM(RTRIM(PersonaMast.TipoDocumento)) As TipoDocumento,
		LTRIM(RTRIM(CASE WHEN PersonaMast.tipodocumento = 'D' THEN PersonaMast.Documento ELSE  PersonaMast.DocumentoFiscal END)) AS NumeroDocumento,
		LTRIM(RTRIM(HR_PuestoEmpresa.CodigoPuesto)) As CodigoCargo, 
		LTRIM(RTRIM(HR_PuestoEmpresa.descripcion)) AS DescripcionCargo,
		LTRIM(RTRIM(AC_Sucursal.DescripcionLocal)) AS Sucursal,
		LTRIM(RTRIM(AC_CostCenterMst.LocalName)) AS descentrocosto,
		PR_PlanillaEmpleado.fechaingresoboleta AS FechaIngreso,
		PR_PlanillaEmpleado.FechaCeseboleta AS FechaCese,
		LTRIM(RTRIM(PR_PlanillaEmpleado.TipoContrato)) AS TipoContrato,
		LTRIM(RTRIM(HR_TipoContrato.Descripcion)) AS DescripcionTipoContrato,
		LTRIM(RTRIM(HR_AFP.CodigoAFP)) As CodigoAFP,
		LTRIM(RTRIM(HR_AFP.NombreAFP)) AS NombreAFP,
		LTRIM(RTRIM(EmpleadoMast.NumeroAFP))As NumeroAFP,	   
		LTRIM(RTRIM(PR_PlanillaEmpleado.VacacionDesde)) As VacacionDesde,
		LTRIM(RTRIM(PR_PlanillaEmpleado.VacacionHasta)) As VacacionHasta,
		LTRIM(RTRIM(PR_PlanillaEmpleado.SueldoBasicoLocal)) As Sueldo,
		LTRIM(RTRIM(PR_PlanillaEmpleado.SueldoBasicoDolar)) As SueldoBasicoDolar,
		PR_PlanillaEmpleado.DiasTrabajados As DiasTrabajados,
		LTRIM(RTRIM(PR_PlanillaEmpleado.HorasTrabajadas)) As HorasTrabajadas,
		LTRIM(RTRIM(PR_PlanillaEmpleado.TotalIngresos)) As TotalIngresos,
		LTRIM(RTRIM(PR_PlanillaEmpleado.TotalEgresos)) As TotalEgresos,
		LTRIM(RTRIM(PR_PlanillaEmpleado.TotalPatronales)) As TotalPatronales,
		LTRIM(RTRIM(PR_PlanillaEmpleado.TotalNeto)) As TotalNeto,
		LTRIM(RTRIM(PR_PlanillaEmpleado.Cuenta)) As CuentaAbono,
		CASE WHEN EmpleadoMast.MonedaPago = 'LO' THEN 'Soles' ELSE 'Dolares' END As MonedaPago
	FROM PR_PlanillaEmpleado
	INNER JOIN PersonaMast ON (PR_PlanillaEmpleado.Empleado = PersonaMast.Persona)
	INNER JOIN EmpleadoMast ON (PR_PlanillaEmpleado.Empleado = EmpleadoMast.Empleado
								AND PR_PlanillaEmpleado.CompaniaSocio = EmpleadoMast.CompaniaSocio)
	INNER JOIN CompaniaMast ON (SUBSTRING(PR_PlanillaEmpleado.CompaniaSocio, 1, 6) = CompaniaMast.companiacodigo)
	INNER JOIN PR_TipoPlanilla ON (PR_PlanillaEmpleado.TipoPlanilla = PR_TipoPlanilla.TipoPlanilla)
	LEFT JOIN MA_UnidadNegocio ON (EmpleadoMast.LocaciondePago = MA_UnidadNegocio.UnidadNegocio)
	LEFT JOIN AC_Sucursal ON (EmpleadoMast.Sucursal = AC_Sucursal.Sucursal)
	LEFT JOIN HR_AFP ON (PR_PlanillaEmpleado.CodigoAFP = HR_AFP.CodigoAFP)
	LEFT JOIN HR_Empleado ON (PR_PlanillaEmpleado.Empleado = HR_Empleado.Empleado)
	LEFT JOIN PR_PlanillaComentario PC ON (PR_PlanillaEmpleado.Empleado = PC.Empleado
										   AND PR_PlanillaEmpleado.TipoPlanilla = PC.TipoPlanilla
										   AND PR_PlanillaEmpleado.TipoProceso = PC.TipoProceso
										   AND PR_PlanillaEmpleado.Periodo = PC.Periodo)
	LEFT JOIN AC_CostCenterMst ON AC_CostCenterMst.CostCenter = PR_PlanillaEmpleado.CentroCosto
	LEFT JOIN HR_PuestoEmpresa ON HR_PuestoEmpresa.CodigoPuesto = PR_PlanillaEmpleado.CodigoCargo
	LEFT JOIN HR_TipoContrato ON HR_TipoContrato.TipoContrato = PR_PlanillaEmpleado.TipoContrato
	LEFT JOIN Banco ON Banco.BANCO = PR_PlanillaEmpleado.Banco
	WHERE (PR_PlanillaEmpleado.Periodo = @par_periodo)
	  AND (PR_PlanillaEmpleado.TipoProceso = @par_tipoproceso)
	  AND (PR_PlanillaEmpleado.CompaniaSocio = @par_compania)
	  AND (PR_PlanillaEmpleado.TipoPlanilla = @par_tipoplanilla
		   OR @par_tipoplanilla = 'All')
	  AND (PR_PlanillaEmpleado.Empleado = @par_empleado OR @par_empleado IS NULL)
END