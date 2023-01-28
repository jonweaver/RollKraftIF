using System;
using System.ComponentModel;
using System.Linq;

namespace RollKraftIF.Infrastructure
{
    public class JobInfo
    {

        [DisplayName("job_number")]
        public string JobNumber { get; set; }

        [DisplayName("location")]
        public string Location { get; set; }
    }
}
