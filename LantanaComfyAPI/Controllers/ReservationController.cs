using AutoMapper;
using LantanaComfyAPI.Dto;
using LantanaComfyAPI.Interfaces;
using LantanaComfyAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LantanaComfyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : Controller
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;

        public ReservationController(IReservationRepository reservationRepository, IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Reservation>))]

        public IActionResult GetReservations()
        {
            var reservations = _reservationRepository.GetReservations();

            if(!ModelState.IsValid) 
                return BadRequest(ModelState);
            return Ok(reservations);
        }

        [HttpGet("reservationId")]
        [ProducesResponseType(200, Type = typeof(Reservation))]
        [ProducesResponseType(400)]
        public IActionResult GetReservation(int reservationId)
        {
            if (!_reservationRepository.ReversationExists(reservationId))
                return NotFound();
            var reservation = _reservationRepository.GetReservation(reservationId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(reservation);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReservation([FromBody] ReservationDto? reservationCreate)
        {
            if (reservationCreate == null) 
                return BadRequest(ModelState);

            var existingReservations = _reservationRepository.GetReservations()
                .FirstOrDefault(r => r.Email.Trim().ToUpper() == reservationCreate.Email.TrimEnd().ToUpper());
            if (existingReservations != null)
            {
                ModelState.AddModelError("","A reservation with this email already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            //Trying Mapping the ReservationDto to the Reservation Model
            var reservation = _mapper.Map<Reservation>(reservationCreate);
            if (!_reservationRepository.CreateReservation(reservation))
            {
                ModelState.AddModelError("","Something went wrong trying to save the reservation");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{reservationId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReservation(int reservationId)
        {
            if (!_reservationRepository.ReversationExists(reservationId))
                return NotFound();

            var reservationToDelete = _reservationRepository.GetReservation(reservationId);

            if (!_reservationRepository.DeleteReservation(reservationToDelete))
            {
                ModelState.AddModelError("","Something went wrong while trying to delete the reservation");
                return StatusCode(500, ModelState);
            }
            
            return NoContent();
        }

    }
}
