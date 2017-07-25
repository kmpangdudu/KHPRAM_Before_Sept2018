﻿-- ===========================================================================================================
-- Author:		William Chen
-- Create date: July 19, 2017
-- Description:	Log 
-- ===========================================================================================================
CREATE PROCEDURE [dbo].[Proc_apilog] 
@dbschema nvarchar(50) = N'dbo',
@csmethod nvarchar(50) = N'GET',
@format nvarchar(50),
@para nvarchar(50),
@lang nvarchar(4) = N'en',
@token nvarchar(50) = N'',
@cscontent nvarchar(50) = NULL ,
@csendpoint nvarchar(50) = NULL ,
@keywords nvarchar(4000) = NULL,
@csip nvarchar(50),
@csstatus nvarchar(50),
@cscode int,
@cshost nvarchar(50),
@csurl nvarchar(1000),
@csuseragent nvarchar(1000)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @aclid int = -1;
	DECLARE @aclName NVARCHAR(255);
	SELECT @aclid = [ACLID] FROM [dbo].AccessControlList WHERE [ACLToken]=@token;
	SELECT @aclName = [ACLName] FROM [dbo].AccessControlList WHERE [ACLToken]=@token;



INSERT INTO apilog
             (dbschema, csmethod, [format],[para],[lang], aclid, aclname, cscontent, csendpoint, keywords, [csip], [csstatus], [cscode], cshost, csurl, csuseragent)
VALUES        (@dbschema,@csmethod,@format,@para,@lang,@aclid,@aclname,@cscontent,@csendpoint,@keywords,@csip,@csstatus,@cscode,@cshost,@csurl, @csuseragent)


END