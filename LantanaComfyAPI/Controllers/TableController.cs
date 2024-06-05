using AutoMapper;
using LantanaComfyAPI.Dto;
using LantanaComfyAPI.Dto.OtherObjects;
using LantanaComfyAPI.Interfaces;
using LantanaComfyAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LantanaComfyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : Controller
    {
        private readonly ITableRepository _tableRepository;
        private readonly IMapper _mapper;

        public TableController(ITableRepository tableRepository, IMapper mapper)
        {
            _tableRepository = tableRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Table>))]
        [Authorize(Roles = StaticUserRoles.ADMIN)]

        public IActionResult GetTables()
        {
            var tables = _tableRepository.GetTables();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(tables);
        }

        [HttpGet("tableId/{tableId}")]
        [ProducesResponseType(200, Type = typeof(Table))]
        [ProducesResponseType(400)]
        [Authorize(Roles = StaticUserRoles.ADMIN)]

        public IActionResult GetTable(int tableId)
        {
            if (!_tableRepository.TableExists(tableId))
                return NotFound();
            var table = _tableRepository.GetTable(tableId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(table);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [Authorize(Roles = StaticUserRoles.USER)]

        public IActionResult CreateTable([FromBody] TableDto? tableCreate)
        {
            if (tableCreate == null)
                return BadRequest(ModelState);

            var tables = _tableRepository.GetTables()
                .FirstOrDefault(t => t.Email.Trim().ToUpper() == tableCreate.Email.TrimEnd().ToUpper());
            if (tables != null)
            {
                ModelState.AddModelError("","Table already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tableMap = _mapper.Map<Table>(tableCreate);
            if (!_tableRepository.CreateTable(tableMap))
            {
                ModelState.AddModelError("","Something went wrong while trying to save the table");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{tableId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize(Roles = StaticUserRoles.OWNER)]

        public IActionResult DeleteTable(int tableId)
        {
            if (!_tableRepository.TableExists(tableId))
                return NotFound();

            var tableToDelete = _tableRepository.GetTable(tableId);
            if (!_tableRepository.DeleteTable(tableToDelete))
            {
                ModelState.AddModelError("","Something went wrong while trying to delete the table.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
