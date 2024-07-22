Select * From Lancamento

ObterLancamentosPorData '20230513'


GO
CREATE PROCEDURE ObterLancamentosPorData @data varchar(10)
AS 

DECLARE	@consolidadoDebito numeric(18,2) 
DECLARE	@consolidadoCredito numeric(18,2) 

DECLARE @consolidado table(
Data date not null, 
ConsolidadoDebito numeric(18,2) not null,
ConsolidadoCredito numeric(18,2) not null
)


SELECT	@consolidadoCredito = SUM(Valor) 
FROM	Lancamento
WHERE	CONVERT(VARCHAR(10), Data, 112) = @data
AND		TipoLancamentoId = 1

SELECT	@consolidadoDebito = SUM(Valor) 
FROM	Lancamento
WHERE	CONVERT(VARCHAR(10), Data, 112) = @data
AND		TipoLancamentoId = 2

INSERT INTO @consolidado (Data, ConsolidadoCredito, ConsolidadoDebito)
VALUES (getdate(), @consolidadoCredito, @consolidadoDebito)

SELECT * FROM @consolidado

GO