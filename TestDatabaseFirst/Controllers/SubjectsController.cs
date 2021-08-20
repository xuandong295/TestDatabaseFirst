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
    public class SubjectsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public SubjectsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Subjects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subject>>> GetSubjects()
        {
            return await _context.Subjects.ToListAsync();
        }

        // GET: api/Subjects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Subject>> GetSubject(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);

            if (subject == null)
            {
                return NotFound();
            }

            return subject;
        }
        // GET: api/Subjects/10
        [HttpGet("10")]
        // All subject have credits greater than 10
        public async Task<string> GetAllSubject10()
        {
            GetResult<Subject> result = new();
            try
            {
                List<Subject> subjects = new();
                var SubjectList = await _context.Subjects.ToListAsync();
                foreach (var item in SubjectList)
                {
                    if (item.Credits > 10)
                    {
                        subjects.Add(item);
                    }
                }
                if (subjects == null)
                {
                    result.Config(0, subjects, "No subject has more than 10 credits");
                    string convert = JsonConvert.SerializeObject(result);
                    return convert;
                }
                else
                {
                    result.Config(1, subjects, "Geted Successfully");
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
        // PUT: api/Subjects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubject(int id, Subject subject)
        {
            if (id != subject.Subjectid)
            {
                return BadRequest();
            }

            _context.Entry(subject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Subjects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Subject>> PostSubject(Subject subject)
        {
            _context.Subjects.Add(subject);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SubjectExists(subject.Subjectid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSubject", new { id = subject.Subjectid }, subject);
        }

        // DELETE: api/Subjects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }

            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubjectExists(int id)
        {
            return _context.Subjects.Any(e => e.Subjectid == id);
        }
    }
}
