USE [RAM]
GO
/****** Object:  StoredProcedure [crd].[Generate_Etlloadid_CustomRecordType]    Script Date: 10/24/2017 8:48:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		William Chen
-- Create date: 2017-10-24
-- Description:	For Kristen's Communitry Resource Database Dashboard project
-- =============================================
ALTER PROCEDURE [crd].[Generate_Etlloadid_CustomRecordType]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	TRUNCATE TABLE CRD.Etlloadid_CustomRecordType;

	INSERT INTO CRD.Etlloadid_CustomRecordType (ETLLoadID, [CustomRecordType],[EnteredOn],[UpdatedOn])
			select p.ETLLoadID, ltrim(rtrim(f.CustomRecordType)) as CustomRecordType , p.EnteredOn, p.UpdatedOn
			from etlload as p outer apply   [crd].[F_CustomRecordType](p.etlloadid) as f 
			order by p.etlloadid
END
