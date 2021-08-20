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
    public class ClassesController : ControllerBase
    {
        private readonly MyDbContext _context;

        public ClassesController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Classes
        [HttpGet]
        public async Task<string> GetClasses()
        {
            GetResult<Class> result = new();
            try
            {
                List<Class> classes = new();
                classes = await _context.Classes.ToListAsync();
                result.Config(1, classes, "Geted Successfully");
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
        public async Task<string> GetClass(int id)
        {
            GetResult<Class> result = new();
            try
            {
                List<Class> classes = new();
                var Class = await _context.Classes.FindAsync(id);
                if (Class == null)
                {
                    result.Config(0, null, "Not Found This ClassID");
                    string convert = JsonConvert.SerializeObject(result);
                    return convert;
                }
                else
                {
                    classes.Add(Class);
                    result.Config(1, classes, "Geted Successfully");
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

        // PUT: api/Classes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<string> PutClass(Class @class)
        {
            PostPutDeleteResult result = new();
            try
            {
                _context.Entry(@class).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                result.Config(1, "Update Successfully");
                string convert = JsonConvert.SerializeObject(result);
                return convert;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!ClassExists(@class.Classid))
                {
                    result.Config(0, "Not Found This ClassID");
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

        // POST: api/Classes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<string> PostClass(Class @class)
        {
            PostPutDeleteResult result = new();
            try
            {
                _context.Classes.Add(@class);
                await _context.SaveChangesAsync();
                result.Config(1, "Added Successfully");
                string convert = JsonConvert.SerializeObject(result);
                return convert;
            }
            catch (DbUpdateException ex)
            {
                if (ClassExists(@class.Classid))
                {
                    result.Config(0, "ClassID Already Exist");
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
        public async Task<string> DeleteClass(int id)
        {
            PostPutDeleteResult result = new();
            try
            {
                var @class = await _context.Classes.FindAsync(id);
                if (@class == null)
                {
                    result.Config(0, "ClassID Does Not Exist");
                    string convert = JsonConvert.SerializeObject(result);
                    return convert;
                }
                else
                {
                    _context.Classes.Remove(@class);
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

        private bool ClassExists(int id)
        {
            return _context.Classes.Any(e => e.Classid == id);
        }
    }
}
