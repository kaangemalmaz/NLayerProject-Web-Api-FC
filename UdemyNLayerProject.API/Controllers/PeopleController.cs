using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyNLayerProject.API.DTOs;
using UdemyNLayerProject.DataAccess;
using UdemyNLayerProject.Entity.Models;
using UdemyNLayerProject.Entity.Services;

namespace UdemyNLayerProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IService<Person> _service;

        public PeopleController(IMapper mapper, IService<Person> service)
        {
            _mapper = mapper;
            _service = service;
        }

        // GET: api/People
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPersons()
        {
            var people = await _service.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<PersonDto>>(people));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(int id)
        {
            var person = await _service.SingleOrDefaultAsync(i => i.Id == id);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PersonDto>(person));
        }

        // POST: api/People
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(PersonDto personDto)
        {
            var newperson = await _service.AddAsync(_mapper.Map<Person>(personDto));

            return CreatedAtAction("GetPerson", new { id = newperson.Id }, _mapper.Map<PersonDto>(newperson));
        }

      
    }
}
