Imports RollKraftIF.Infrastructure

Module Module1

    Sub Main()
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

        Console.WriteLine()
        Console.WriteLine("push any key to exit")
        Console.ReadKey()
    End Sub

End Module
