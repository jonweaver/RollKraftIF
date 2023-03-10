# Roll Kraft Stored Procedure Runner

## Configuration
Set connection string in App.config

```XML
<configuration>
	<connectionStrings>
		<add name="RKI_Prod_ConnectionString"
            connectionString="Data Source=DESKTOP-28CTE90\SQLEXPRESS;Initial Catalog=tempdb;Integrated Security=True" />
	</connectionStrings>
</configuration>
```
Or pass the connection string into the constructor

```C#
var repo = new RKIProdProgramInfoRepository("connection string");
```

## Example Usage: 

```VB.NET
Dim repo = New RKIProdProgramInfoRepository()

Dim jobInfo = New JobInfo With {
    .JobNumber = "136778-01",
    .Location = "EF1T2-.062"
}
Console.WriteLine($"Getting details for job# {jobInfo.JobNumber} location {jobInfo.Location}")
Console.WriteLine()

Console.WriteLine("We can get all of the program details in one shot:")
Dim details = repo.GetProgramDetails(jobInfo)
Console.WriteLine(details.ToString())
Console.WriteLine()

Console.WriteLine("Or we can get each piece individually:")
Dim program = repo.GetProgramName(jobInfo)
Console.WriteLine($"Program is {program}")
Dim rollBore = repo.GetRollBore(jobInfo)
Console.WriteLine($"Roll Bore is {rollBore}")
Dim rollMaterial = repo.GetRollMaterial(jobInfo)
Console.WriteLine($"Roll Material is {rollMaterial}")
```

## Testing
### Create Table

```SQL
USE [tempdb]
GO

/****** Object:  Table [dbo].[Jobs]    Script Date: 1/29/2023 1:28:17 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Jobs](
	[jobId] [int] IDENTITY(1,1) NOT NULL,
	[job_number] [varchar](100) NOT NULL,
	[location] [varchar](100) NOT NULL,
	[ProgramNumber] [varchar](100) NOT NULL,
	[Bore] [varchar](100) NOT NULL,
	[Material] [varchar](100) NOT NULL
) ON [PRIMARY]
GO
```
### Create Stored Procedures

```SQL
CREATE PROCEDURE [dbo].spGetTandP_Program
 @job_number nvarchar(100),
 @location nvarchar(100)

AS

BEGIN
    SELECT j.ProgramNumber
    FROM [tempdb].[dbo].[Jobs] j
	WHERE j.job_number = @job_number AND j.location = @location
END


CREATE PROCEDURE [dbo].spGetRollBore
 @job_number nvarchar(100),
 @location nvarchar(100)

AS

BEGIN
    SELECT j.Bore
    FROM [tempdb].[dbo].[Jobs] j
	WHERE j.job_number = @job_number AND j.location = @location
END

CREATE PROCEDURE [dbo].[spGetRollMaterial]
 @job_number nvarchar(100),
 @location nvarchar(100)

AS

BEGIN
    SELECT j.Material
    FROM [tempdb].[dbo].[Jobs] j
	WHERE j.job_number = @job_number AND j.location = @location
END

CREATE PROCEDURE [dbo].spGetProgramDetails
 @job_number nvarchar(100),
 @location nvarchar(100)

AS

BEGIN
    SELECT j.ProgramNumber,j.Bore,j.Material
    FROM [tempdb].[dbo].[Jobs] j
	WHERE j.job_number = @job_number AND j.location = @location
END
```
