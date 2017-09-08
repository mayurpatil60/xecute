USE [XPConnect]
GO
/****** Object:  StoredProcedure [dbo].[spValidateLoginInfo]    Script Date: 09/08/2017 21:02:38 ******/
DROP PROCEDURE [dbo].[spValidateLoginInfo]
GO
/****** Object:  StoredProcedure [dbo].[spValidateLoginInfo]    Script Date: 09/08/2017 21:02:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spValidateLoginInfo]
(
@username varchar(100),
@password varchar(max)
)
as

declare @exists bit=0;
if exists(
	select 1 from Login
	where Password=@password
	)
begin
	select 1
end
else
begin
	select 0
end
GO
