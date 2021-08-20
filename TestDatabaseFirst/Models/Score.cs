using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

#nullable disable

namespace TestDatabaseFirst.Models
{
    public partial class Score
    {
        public int Subjectid { get; set; }
        public int Studentid { get; set; }
        public int Semester { get; set; }
        public float Firstscore { get; set; }
        public float? Secondscore { get; set; }

        public virtual Student Student { get; set; }
        [JsonIgnore]
        public virtual Subject Subject { get; set; }
    }
}
