using AutoMapper;
using LantanaComfyAPI.Dto;
using LantanaComfyAPI.Interfaces;
using LantanaComfyAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;


namespace LantanaComfyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : Controller
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public ContactController(IContactRepository contactRepository, IMapper mapper)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Contact>))]
        public IActionResult GetTables()
        {
            var tables = _contactRepository.GetContacts();

            if(!ModelState.IsValid) 
                return BadRequest(ModelState);
            return Ok(tables);
        }

        [HttpGet("tableId")]
        [ProducesResponseType(200, Type = typeof(Contact))]
        [ProducesResponseType(400)]
        public IActionResult GetContact(int contactId)
        {
            if (!_contactRepository.ContactExists(contactId))
                return NotFound();
            var contact = _contactRepository.GetContact(contactId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(contact);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateContact([FromBody] ContactDto? contactCreate)
        {
            if (contactCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var contacts = _contactRepository.GetContacts()
                .FirstOrDefault(c => c.Email.Trim().ToUpper() == contactCreate.Email.TrimEnd().ToUpper());
            if (contacts != null)
            {
                ModelState.AddModelError("","Enquiry already exists");
                return StatusCode(422, ModelState);
            }

            var contactMap = _mapper.Map<Contact>(contactCreate);
            if (!_contactRepository.CreateContact(contactMap))
            {
               ModelState.AddModelError("","Something went wrong while trying to create a contact");
               return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{contactId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteContact(int contactId)
        {
            if (!_contactRepository.ContactExists(contactId))
                return NotFound();

            var contactToDelete = _contactRepository.GetContact(contactId);
            if (!_contactRepository.DeleteContact(contactToDelete))
            {
                ModelState.AddModelError("","Something went wrong while trying to delete the contact.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
        
    }
}
