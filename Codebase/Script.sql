USE [XPConnect]
GO
/****** Object:  StoredProcedure [dbo].[spResetPassword]    Script Date: 09/09/2017 22:09:10 ******/
DROP PROCEDURE [dbo].[spResetPassword]
GO
/****** Object:  StoredProcedure [dbo].[spValidateLoginInfo]    Script Date: 09/09/2017 22:09:10 ******/
DROP PROCEDURE [dbo].[spValidateLoginInfo]
GO
/****** Object:  StoredProcedure [dbo].[spValidateLoginInfo]    Script Date: 09/09/2017 22:09:10 ******/
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
	where Password=@password and UserName=@username
	)
begin
	select 1,IsPasswordSet from Login
	where Password=@password and UserName=@username
end
else
begin
	select 0
end
GO
/****** Object:  StoredProcedure [dbo].[spResetPassword]    Script Date: 09/09/2017 22:09:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spResetPassword]
(
@username varchar(100),
@password varchar(max)
)
as

update Login
set Password=@password, IsPasswordSet=1
where UserName=@username
GO
