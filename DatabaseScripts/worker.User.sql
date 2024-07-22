USE [FluxoCaixa]
GO
/****** Object:  User [worker]    Script Date: 20/07/2024 22:46:55 ******/
CREATE USER [worker] FOR LOGIN [worker] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_datareader] ADD MEMBER [worker]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [worker]
GO
