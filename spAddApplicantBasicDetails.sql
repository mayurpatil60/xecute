/*    ==Scripting Parameters==

    Source Server Version : SQL Server 2008 R2 (10.50.4000)
    Source Database Engine Edition : Microsoft SQL Server Express Edition
    Source Database Engine Type : Standalone SQL Server

    Target Server Version : SQL Server 2017
    Target Database Engine Edition : Microsoft SQL Server Standard Edition
    Target Database Engine Type : Standalone SQL Server
*/

USE [XPConnect]
GO
DROP PROCEDURE [dbo].[spAddApplicantBasicDetails]
GO
/****** Object:  StoredProcedure [dbo].[spAddApplicantBasicDetails]    Script Date: 10/09/2017 13:48:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[spAddApplicantBasicDetails]
(@Email varchar(300)=null,
@FirstName nvarchar(300)=null
,@MiddleName nvarchar(300)= null
,@LastName nvarchar(300)=null
,@PhoneNo nvarchar(300)=null)
as

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