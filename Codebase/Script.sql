USE [XPConnect]
GO
/****** Object:  StoredProcedure [dbo].[spAddApplicantBasicDetails]    Script Date: 09/13/2017 22:31:01 ******/
DROP PROCEDURE [dbo].[spAddApplicantBasicDetails]
GO
/****** Object:  StoredProcedure [dbo].[spAddApplicantDetailedInfo]    Script Date: 09/13/2017 22:31:01 ******/
DROP PROCEDURE [dbo].[spAddApplicantDetailedInfo]
GO
/****** Object:  StoredProcedure [dbo].[spAddNewJoinee]    Script Date: 09/13/2017 22:31:01 ******/
DROP PROCEDURE [dbo].[spAddNewJoinee]
GO
/****** Object:  StoredProcedure [dbo].[spGetApplicantBasicDetails]    Script Date: 09/13/2017 22:31:01 ******/
DROP PROCEDURE [dbo].[spGetApplicantBasicDetails]
GO
/****** Object:  StoredProcedure [dbo].[spGetApplicantDetailedInfo]    Script Date: 09/13/2017 22:31:01 ******/
DROP PROCEDURE [dbo].[spGetApplicantDetailedInfo]
GO
/****** Object:  StoredProcedure [dbo].[spGetNewJoinee]    Script Date: 09/13/2017 22:31:01 ******/
DROP PROCEDURE [dbo].[spGetNewJoinee]
GO
/****** Object:  StoredProcedure [dbo].[spGetNewJoineeList]    Script Date: 09/13/2017 22:31:01 ******/
DROP PROCEDURE [dbo].[spGetNewJoineeList]
GO
/****** Object:  StoredProcedure [dbo].[spGetQueries]    Script Date: 09/13/2017 22:31:01 ******/
DROP PROCEDURE [dbo].[spGetQueries]
GO
/****** Object:  StoredProcedure [dbo].[spGetUserSpecificQueriesAndReplies]    Script Date: 09/13/2017 22:31:01 ******/
DROP PROCEDURE [dbo].[spGetUserSpecificQueriesAndReplies]
GO
/****** Object:  StoredProcedure [dbo].[spResetPassword]    Script Date: 09/13/2017 22:31:01 ******/
DROP PROCEDURE [dbo].[spResetPassword]
GO
/****** Object:  StoredProcedure [dbo].[spSaveQuery]    Script Date: 09/13/2017 22:31:01 ******/
DROP PROCEDURE [dbo].[spSaveQuery]
GO
/****** Object:  StoredProcedure [dbo].[spValidateLoginInfo]    Script Date: 09/13/2017 22:31:01 ******/
DROP PROCEDURE [dbo].[spValidateLoginInfo]
GO
/****** Object:  StoredProcedure [dbo].[spValidateLoginInfo]    Script Date: 09/13/2017 22:31:01 ******/
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
/****** Object:  StoredProcedure [dbo].[spSaveQuery]    Script Date: 09/13/2017 22:31:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spSaveQuery](
@Email varchar(100),
@Query varchar(max),
@Subject varchar(100)=null,
@IsAnswered bit=nul
)
as

insert into Query
values(@Email,@Subject,@Query,@IsAnswered,GETDATE())

select @@IDENTITY
GO
/****** Object:  StoredProcedure [dbo].[spResetPassword]    Script Date: 09/13/2017 22:31:01 ******/
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
/****** Object:  StoredProcedure [dbo].[spGetUserSpecificQueriesAndReplies]    Script Date: 09/13/2017 22:31:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spGetUserSpecificQueriesAndReplies](
@Email varchar(100)
)
as

select * from Query q left join QueryReply qr on q.QueryId=qr.QueryId
where Email=@Email
order by CreatedDateTime
GO
/****** Object:  StoredProcedure [dbo].[spGetQueries]    Script Date: 09/13/2017 22:31:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spGetQueries]
as

select * from Query
order by CreatedDateTime desc
GO
/****** Object:  StoredProcedure [dbo].[spGetNewJoineeList]    Script Date: 09/13/2017 22:31:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[spGetNewJoineeList]

as
Select * from BasicInfo
GO
/****** Object:  StoredProcedure [dbo].[spGetNewJoinee]    Script Date: 09/13/2017 22:31:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[spGetNewJoinee]
(@Email varchar(300))

AS
Select * from BasicInfo
Where EmailId=@Email
GO
/****** Object:  StoredProcedure [dbo].[spGetApplicantDetailedInfo]    Script Date: 09/13/2017 22:31:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[spGetApplicantDetailedInfo]
(@Email varchar(300))
as
select BC.EmailId,BC.FirstName,BC.LastName,BC.MiddleName,BC.PhoneNo,
UD.BloodGroup,UD.DOB,UD.EmergencyContactNumber,UD.CurrentAddress,UD.Gender,UD.PassportNumber from BasicInfo BC
INNER JOIN UserDetailedInfo UD ON BC.EmailId =UD.EmailId 
where BC.EmailId=@Email
GO
/****** Object:  StoredProcedure [dbo].[spGetApplicantBasicDetails]    Script Date: 09/13/2017 22:31:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spGetApplicantBasicDetails]
(@Email varchar(300))
as

select * from BasicInfo
where EmailId=@Email
GO
/****** Object:  StoredProcedure [dbo].[spAddNewJoinee]    Script Date: 09/13/2017 22:31:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[spAddNewJoinee]
(@Email varchar(300),
@FirstName nvarchar(300)
,@MiddleName nvarchar(300)= null
,@LastName nvarchar(300))
as


INSERT INTO [dbo].[BasicInfo]
           ([FirstName]
           ,[MiddleName]
           ,[LastName]
           ,[EmailId]
           )
     VALUES
           (
           @FirstName
           ,@MiddleName
           ,@LastName
           ,@Email
           )

INSERT INTO [dbo].[Login]
           (
           [UserName]
           ,[RoleId])
     VALUES
           (@Email,
		   1)
GO
/****** Object:  StoredProcedure [dbo].[spAddApplicantDetailedInfo]    Script Date: 09/13/2017 22:31:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[spAddApplicantDetailedInfo]
(@PassportNo nvarchar(50)=null,
@CurrentAddress nvarchar(500)=null
,@BloodGroup nvarchar(5)=null
,@Gender varchar(20)=null
,@EmergencyContactNumber nvarchar(20)=null
,@DOB DATE=null
,@Email nvarchar
)
as

IF exists (select 1 from UserDetailedInfo where EmailId = @Email)
 BEGIN
 UPDATE [dbo].[UserDetailedInfo]
SET [CurrentAddress] = @CurrentAddress, [PassportNumber] = @PassportNo, [BloodGroup]=@BloodGroup
,[Gender]=@Gender,[EmergencyContactNumber]=@EmergencyContactNumber,[DOB]=@DOB
WHERE EmailId = @Email;
END
ELSE
BEGIN
INSERT INTO [dbo].[UserDetailedInfo]
           ([CurrentAddress]
           ,[PassportNumber]
           ,[BloodGroup]
		   ,[Gender]
		   ,[EmergencyContactNumber]
		   ,[DOB]
		   ,[EmailId])
     VALUES
           (@CurrentAddress
           ,@PassportNo
           ,@BloodGroup
		   ,@Gender
		   ,@EmergencyContactNumber
		   ,@DOB
		   ,@Email)
END
GO
/****** Object:  StoredProcedure [dbo].[spAddApplicantBasicDetails]    Script Date: 09/13/2017 22:31:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spAddApplicantBasicDetails]
(@Email varchar(300),
@FirstName nvarchar(300)=null
,@MiddleName nvarchar(300)= null
,@LastName nvarchar(300)=null
,@PhoneNo nvarchar(300))
as

IF exists (select 1 from BasicInfo where EmailId = @Email)
 BEGIN
 UPDATE [dbo].[BasicInfo]
SET [FirstName] = @FirstName, [MiddleName] = @MiddleName, [LastName]=@LastName
,[PhoneNo]=@PhoneNo
WHERE EmailId = @Email;

update UserDetailedInfo 
SET [FirstName] = @FirstName, [MiddleName] = @MiddleName, [LastName]=@LastName,EmailId = @Email
WHERE EmailId = @Email;
END
ELSE
BEGIN
INSERT INTO [dbo].[BasicInfo]
           ([FirstName]
           ,[MiddleName]
           ,[LastName]
           ,[EmailId]
           ,[PhoneNo])
     VALUES
           (
           @FirstName
           ,@MiddleName
           ,@LastName
           ,@Email
           ,@PhoneNo)

	INSERT INTO [dbo].[UserDetailedInfo]
           ([FirstName]
           ,[MiddleName]
           ,[LastName]
           ,[EmailId]
           )
     VALUES
           (
           @FirstName
           ,@MiddleName
           ,@LastName
           ,@Email
           )
END

IF exists (select 1 from UserDetailedInfo where EmailId = @Email)
 BEGIN
 

update UserDetailedInfo 
SET [FirstName] = @FirstName, [MiddleName] = @MiddleName, [LastName]=@LastName,EmailId = @Email
WHERE EmailId = @Email;
END
ELSE
BEGIN

	INSERT INTO [dbo].[UserDetailedInfo]
           ([FirstName]
           ,[MiddleName]
           ,[LastName]
           ,[EmailId]
           )
     VALUES
           (
           @FirstName
           ,@MiddleName
           ,@LastName
           ,@Email
           )
END
GO
