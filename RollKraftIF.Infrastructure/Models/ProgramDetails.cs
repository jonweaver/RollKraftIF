using System;
using System.Linq;

namespace RollKraftIF.Infrastructure.Models
{
    public class ProgramDetails
    {
        public override string ToString() => 
            $"{nameof(ProgramNumber)}: {ProgramNumber}\n{nameof(Bore)}: {Bore}\n{nameof(Material)}: {Material}";

        public string Bore { get; set; }

        public string Material { get; set; }

        public string ProgramNumber { get; set; }
    }
}
