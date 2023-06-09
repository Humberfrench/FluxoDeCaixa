USE [FluxoCaixa]
GO
/****** Object:  Table [dbo].[Lancamento]    Script Date: 16/05/2023 11:17:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lancamento](
	[LancamentoId] [int] IDENTITY(1,1) NOT NULL,
	[Data] [date] NOT NULL,
	[Valor] [numeric](18, 2) NOT NULL,
	[TipoLancamentoId] [int] NOT NULL,
	[Descricao] [varchar](50) NOT NULL,
	[Estornado] [bit] NOT NULL,
	[Observacao] [varchar](100) NULL,
 CONSTRAINT [PK_Lancamento] PRIMARY KEY CLUSTERED 
(
	[LancamentoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Lancamento]  WITH CHECK ADD  CONSTRAINT [FK_Lancamento_TipoLancamento] FOREIGN KEY([TipoLancamentoId])
REFERENCES [dbo].[TipoLancamento] ([TipoLancamentoId])
GO
ALTER TABLE [dbo].[Lancamento] CHECK CONSTRAINT [FK_Lancamento_TipoLancamento]
GO
