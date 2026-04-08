using Microsoft.AspNetCore.Mvc;
using TuksSyncAPI.Repositories;
using TuksSyncAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace TuksSyncAPI.Controllers
{
    // This controller handles CRUD operations for EventInfo entities

    [ApiController]
    [Route("api/[controller]")]
    public class EventInfoCont : ControllerBase
    {
        
        private readonly IEventInfoRes _context;

        public EventInfoCont(IEventInfoRes context)
        {
            _context = context;
        }

        // Get all EventInfos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventInfo>>> GetEventInfos()
        {
            var events = await _context.GetEventInfo();
            return Ok(events);
        }

        // Get a specific EventInfo by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<EventInfo>> GetEventInfoById(int id)
        {
            var eventInfo = await _context.GetEventInfoById(id);

            if (eventInfo == null)
            {
                return NotFound();
            }

            return eventInfo;
        }


        // Create a new EventInfo
        [HttpPost]
        public async Task<ActionResult<EventInfo>> CreateEventInfo(EventInfo eventInfo)
        {            
            var createdEventInfo = await _context.CreateEventInfo(eventInfo);
            return CreatedAtAction(nameof(GetEventInfoById), new { id = createdEventInfo.Id }, createdEventInfo);
        }


        // Update an existing EventInfo
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEventInfo(int id, EventInfo eventInfo)
        {
            if (id != eventInfo.Id)
            {
                return BadRequest();
            }

            var updatedEventInfo = await _context.UpdateEventInfo(id, eventInfo);

            try
            {
                await _context.UpdateEventInfo(id, eventInfo);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventInfoExists(id))
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

        private bool EventInfoExists(int id)
        {
            return _context.GetEventInfoById(id) != null;
        }






        // Delete an EventInfo by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventInfo(int id)
        {
            var eventInfo = await _context.GetEventInfoById(id);

            if (eventInfo == null)
            {
                return NotFound();
            }

            await _context.DeleteEventInfo(id);
            return NoContent();
        }

    }
}