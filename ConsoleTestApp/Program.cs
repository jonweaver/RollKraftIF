using RollKraftIF.Infrastructure;
using System;

namespace ConsoleTestApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var repo = new RKIProdProgramInfoRepository();

            var jobInfo = new JobInfo { JobNumber = "136778-01", Location = "EF1T2-.062" };

            Console.WriteLine($"Getting details for job# {jobInfo.JobNumber} location {jobInfo.Location}");

            var program = repo.GetProgramName(jobInfo);
            Console.WriteLine($"Program is {program}");

            var rollBore = repo.GetRollBore(jobInfo);
            Console.WriteLine($"Roll Bore is {rollBore}");

            var rollMaterial = repo.GetRollMaterial(jobInfo);
            Console.WriteLine($"Roll Material is {rollMaterial}");

            Console.WriteLine();
            Console.WriteLine("push any key to exit");

            Console.ReadKey();
        }
    }
}
