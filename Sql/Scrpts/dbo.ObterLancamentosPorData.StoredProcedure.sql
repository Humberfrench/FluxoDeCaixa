USE [FluxoCaixa]
GO
/****** Object:  StoredProcedure [dbo].[ObterLancamentosPorData]    Script Date: 16/05/2023 11:17:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ObterLancamentosPorData] @data varchar(10)
AS 

DECLARE	@consolidadoDebito numeric(18,2) 
DECLARE	@consolidadoCredito numeric(18,2) 

DECLARE @consolidado table(
Data date not null, 
ConsolidadoDebito numeric(18,2) not null,
ConsolidadoCredito numeric(18,2) not null,
SaldoDoDia numeric(18,2) not null
)


SELECT	@consolidadoCredito = SUM(Valor) 
FROM	Lancamento
WHERE	CONVERT(VARCHAR(10), Data, 112) = @data
AND		TipoLancamentoId = 1
AND		Estornado = 0

SELECT	@consolidadoDebito = SUM(Valor) 
FROM	Lancamento
WHERE	CONVERT(VARCHAR(10), Data, 112) = @data
AND		TipoLancamentoId = 2
AND		Estornado = 0

INSERT INTO @consolidado (Data, ConsolidadoCredito, ConsolidadoDebito,SaldoDoDia)
VALUES (@data, @consolidadoCredito, @consolidadoDebito, @consolidadoCredito - @consolidadoDebito)

SELECT * FROM @consolidado

GO
