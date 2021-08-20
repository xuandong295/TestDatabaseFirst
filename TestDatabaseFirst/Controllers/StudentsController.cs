using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using APIForStudent.Config;
using TestDatabaseFirst.Models;

namespace APIForStudent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public StudentsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Student
        [HttpGet]
        public async Task<string> GetStudents()
        {
            GetResult<Student> result = new();
            try
            {
                List<Student> students = new();
                students = await _context.Students.ToListAsync();     // if students == null ...
                result.Config(1, students, "Geted Successfully");
                string convert = JsonConvert.SerializeObject(result);
                return convert;
            }
            catch (Exception ex)
            {
                result.Config(0, null, ex.Message);
                string convert = JsonConvert.SerializeObject(result);
                return convert;
            }

        }
        // GET: api/Classes/5
        [HttpGet("{id}")]
        public async Task<string> GetStudent(int id)
        {
            GetResult<Student> result = new();
            try
            {
                List<Student> students = new();
                var student = await _context.Students.FindAsync(id);
                if (student == null)
                {
                    result.Config(0, null, "Not Found This StudentID");
                    string convert = JsonConvert.SerializeObject(result);
                    return convert;
                }
                else
                {
                    students.Add(student);
                    result.Config(1, students, "Geted Successfully");
                    string convert = JsonConvert.SerializeObject(result);
                    return convert;
                }
           
            }
            catch (Exception ex)
            {
                result.Config(0, null, ex.Message);
                string convert = JsonConvert.SerializeObject(result);
                return convert;
            }
        }
        [HttpGet("Retest1")]
        public async Task<ActionResult<List<Student>>> GetRetestStudent1()
        {
            List<Student> students = new();
            List<float> studentidHaveScoresLowerThan4 = new();
            var studentsList = await _context.Students.ToListAsync();
            var scoresList = await _context.Scores.ToListAsync();

            foreach (var score in scoresList)
            {
                if (score.Firstscore < 4)
                {
                    studentidHaveScoresLowerThan4.Add(score.Studentid);
                }
            }
            foreach (var student in studentsList)
            {
                foreach (var studentid in studentidHaveScoresLowerThan4)
                {
                    if (student.Studentid == studentid)
                    {
                        students.Add(student);
                    }
                }
            }
            return students;
        }
            // GET: api/Student/Retest
            [HttpGet("Retest")]
        public async Task<string> GetRetestStudent()
        {
            GetResult<Student> result = new();
            try
            {
                List<Student> students = new();
                List<float> studentidHaveScoresLowerThan4 = new();
                var studentsList = await _context.Students.ToListAsync();
                var scoresList = await _context.Scores.ToListAsync();

                foreach (var score in scoresList)
                {
                    if (score.Firstscore < 4)
                    {
                        studentidHaveScoresLowerThan4.Add(score.Studentid);
                    }
                }
                foreach (var student in studentsList)
                {
                    foreach (var studentid in studentidHaveScoresLowerThan4)
                    {
                        if (student.Studentid == studentid)
                        {
                            students.Add(student);
                        }
                    }
                }
                result.Config(0, students, "Geted Successfully");
                string convert = JsonConvert.SerializeObject(result, Formatting.Indented,
                                        new JsonSerializerSettings
                                             {
                                                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                             });
                return convert;
            }
            catch (Exception ex)
            {
                result.Config(0, null, ex.Message);
                string convert = JsonConvert.SerializeObject(result);
                return convert;

            }
        }
        // GET: api/Student/class/2017001
        [HttpGet("get-student-class/{classid}")]
        public async Task<string> GetStudentInClass(int classid)
        {
            GetResult<Student> result = new();
            try
            {
                List<Student> students = new();
                var studentsList = await _context.Students.ToListAsync();
                foreach (var student in studentsList)
                {
                    if (student.Class == classid)
                    {
                        students.Add(student);
                    }
                }
                if (students.Count == 0)
                {
                    result.Config(0, students, "Class Does Not Exist");
                    string convert = JsonConvert.SerializeObject(result);
                    return convert;
                }
                else
                {
                    result.Config(1, students, "Geted Successfully");
                    string convert = JsonConvert.SerializeObject(result);
                    return convert;
                }
            }
            catch (Exception ex)
            {
                result.Config(0, null, ex.Message);
                string convert = JsonConvert.SerializeObject(result);
                return convert;
            }
           
        }
        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<string> PutStudent(Student student)
        {
            PostPutDeleteResult result = new();
            try
            {
                _context.Entry(student).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                result.Config(1, "Update Successfully");
                string convert = JsonConvert.SerializeObject(result);
                return convert;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!StudentExists(student.Studentid))
                {
                    result.Config(0, "Not Found This StudentID");
                    string convert = JsonConvert.SerializeObject(result);
                    return convert;
                }
                else
                {
                    result.Config(0, ex.Message);
                    string convert = JsonConvert.SerializeObject(result);
                    return convert;
                }
            }
            catch(Exception ex)
            {
                result.Config(0, ex.Message);
                string convert = JsonConvert.SerializeObject(result);
                return convert;
            }
        }

        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<string> PostStudent(Student student)
        {
            PostPutDeleteResult result = new();
            try
            {
                _context.Students.Add(student);
                await _context.SaveChangesAsync();
                result.Config(1, "Added Successfully");
                string convert = JsonConvert.SerializeObject(result);
                return convert;
            }
            catch (DbUpdateException ex)
            {
                if (StudentExists(student.Studentid))
                {
                    result.Config(0, "StudentID Already Exist");
                    string convert = JsonConvert.SerializeObject(result);
                    return convert;
                }
                else
                {
                    result.Config(0, ex.Message);
                    string convert = JsonConvert.SerializeObject(result);
                    return convert;
                }
            }
            catch (Exception ex)
            {
                result.Config(0, ex.Message);
                string convert = JsonConvert.SerializeObject(result);
                return convert;
            }

        }

        // DELETE: api/Classes/5
        [HttpDelete("{id}")]
        public async Task<string> DeleteStudent(int id)
        {
            PostPutDeleteResult result = new();
            try
            {
                var student = await _context.Students.FindAsync(id);
                if (student == null)
                {
                    result.Config(0, "StudentID Does Not Exist");
                    string convert = JsonConvert.SerializeObject(result);
                    return convert;
                }
                else
                {
                    _context.Students.Remove(student);
                    await _context.SaveChangesAsync();
                    result.Config(1, "Deleted Successfully");
                    string convert = JsonConvert.SerializeObject(result);
                    return convert;
                }

            }
            catch (Exception ex)
            {
                result.Config(0, ex.Message);
                string convert = JsonConvert.SerializeObject(result);
                return convert;
            }
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Studentid == id);
        }

    }
}
