using System;
using System.Collections.Generic;

#nullable disable

namespace TestDatabaseFirst.Models
{
    public partial class Class
    {
        public Class()
        {
            Students = new HashSet<Student>();
        }

        public int Classid { get; set; }
        public string Name { get; set; }
        public string Majors { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
