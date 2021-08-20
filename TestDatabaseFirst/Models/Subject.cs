using System;
using System.Collections.Generic;

#nullable disable

namespace TestDatabaseFirst.Models
{
    public partial class Subject
    {
        public Subject()
        {
            Scores = new HashSet<Score>();
        }

        public int Subjectid { get; set; }
        public string Name { get; set; }
        public int Credits { get; set; }

        public virtual ICollection<Score> Scores { get; set; }
    }
}
