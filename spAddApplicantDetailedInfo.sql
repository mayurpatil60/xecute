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
DROP PROCEDURE [dbo].[spAddApplicantDetailedInfo]
GO
/****** Object:  StoredProcedure [dbo].[spAddApplicantBasicDetails]    Script Date: 10/09/2017 13:48:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[spAddApplicantDetailedInfo]
(@PassportNo nvarchar(50)=null,
@CurrentAddress nvarchar(500)=null
,@BloodGroup nvarchar(5)=null
,@Gender varchar(20)=null
,@EmergencyContactNo nvarchar(20)=null
,@DOB DATE=null
)
as

INSERT INTO [dbo].[UserDetailedInfo]
           ([CurrentAddress]
           ,[PassportNumber]
           ,[BloodGroup]
		   ,[Gender]
		   ,[EmergencyContactNumber]
		   ,[DOB])
     VALUES
           (@CurrentAddress
           ,@PassportNo
           ,@BloodGroup
		   ,@Gender
		   ,@EmergencyContactNo
		   ,@DOB)