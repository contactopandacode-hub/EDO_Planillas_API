CREATE PROCEDURE SNP_API_Planilla_ObtenerDetalle(
@par_periodo as varchar(8),
@par_compania as varchar(8),
@par_tipoproceso as varchar(3),
@par_tipoplanilla as varchar(2),
@par_empleado as int
)
AS
BEGIN
	SELECT 
		PR_Concepto.Concepto,
       CASE
           WHEN 'LO' = 'LO' THEN PR_PlanillaEmpleadoConcepto.Monto
           ELSE PR_PlanillaEmpleadoConcepto.MontoExtranjera
       END AS monto,
       PR_PlanillaEmpleadoConcepto.Cantidad,
       CASE
           WHEN 'LO' = 'LO' THEN PR_PlanillaEmpleadoConcepto.Saldo
           ELSE PR_PlanillaEmpleadoConcepto.SaldoExtranjera
       END AS saldo,
       PR_Concepto.TipoConcepto,
       PR_Concepto.PlanillaOrden,
       PR_Concepto.TextoImpresion
	FROM PR_Concepto,
		 PR_PlanillaEmpleadoConcepto
	WHERE (PR_Concepto.Concepto = PR_PlanillaEmpleadoConcepto.Concepto)
	  AND (PR_PlanillaEmpleadoConcepto.Periodo = @par_periodo)
	  AND (PR_PlanillaEmpleadoConcepto.TipoProceso = @par_tipoproceso)
	  AND (PR_PlanillaEmpleadoConcepto.TipoPlanilla = @par_tipoplanilla)
	  AND (PR_PlanillaEmpleadoConcepto.Empleado = @par_empleado)
	  AND (PR_PlanillaEmpleadoConcepto.CompaniaSocio = @par_compania)
	  AND (PR_PlanillaEmpleadoConcepto.Monto <> 0
		   OR PlanillaOrden < 0
		   OR PR_PlanillaEmpleadoConcepto.Saldo>0)
END 