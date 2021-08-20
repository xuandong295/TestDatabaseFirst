using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

#nullable disable

namespace TestDatabaseFirst.Models
{
    public partial class Student
    {
        public Student()
        {
            Scores = new HashSet<Score>();
        }

        public int Studentid { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
        public int Class { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Class ClassNavigation { get; set; }

        public virtual ICollection<Score> Scores { get; set; }
    }
}
