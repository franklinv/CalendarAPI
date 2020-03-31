using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoreWebAPIApp.Models;

namespace CoreWebAPIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly CalendarDBContext _context;

        public EventsController(CalendarDBContext context)
        {
            _context = context;
        }

        // GET: api/Events
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblEvent>>> GetTblEvent()
        {
            return await _context.TblEvent.ToListAsync();
        }

        // GET: api/Events/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblEvent>> GetTblEvent(int id)
        {
            var tblEvent = await _context.TblEvent.FindAsync(id);

            if (tblEvent == null)
            {
                return NotFound();
            }

            return tblEvent;
        }


        [HttpGet]
        [Route("Location/{strLocation}")]
        public async Task<IActionResult> GetTblEvent(string strLocation)
        {
            var EventRecord = await _context.TblEvent.Where(x => x.EventLocation == strLocation).ToListAsync();
           
            return Ok(EventRecord);
        }


        [HttpGet]
        [Route("Sort")]
        public async Task<IActionResult> Sort()
        {
            var EventRecord = await _context.TblEvent.OrderBy(s => s.EventTime).ToListAsync();

            if ((EventRecord == null) || (EventRecord.Count == 0))
            {
                return NotFound();
            }
           
            return Ok(EventRecord);
        }


        [HttpGet]
        [Route("Organizer/{strOrganizer}")]
        public async Task<IActionResult> GetEventsByOganizer(string strOrganizer)
        {
            var EventRecord = await _context.TblEvent.Where(x => x.EventOrganizer == strOrganizer).ToListAsync();

            if ((EventRecord==null) || (EventRecord.Count==0))
            {
                return NotFound();
            }

            return Ok(EventRecord);
        }



        // PUT: api/Events/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblEvent(int id, [FromBody]  TblEvent tblEvent)
        {
            if (id != tblEvent.Id)
            {
                return BadRequest();
            }

            _context.Entry(tblEvent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblEventExists(id))
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

        // POST: api/Events
 
        [HttpPost]
        public async Task<ActionResult<TblEvent>> PostTblEvent([FromBody] TblEvent tblEvent)
        {
            _context.TblEvent.Add(tblEvent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblEvent", new { id = tblEvent.Id }, tblEvent);
        }

        // DELETE: api/Events/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TblEvent>> DeleteTblEvent(int id)
        {
            var tblEvent = await _context.TblEvent.FindAsync(id);
            if (tblEvent == null)
            {
                return NotFound();
            }

            _context.TblEvent.Remove(tblEvent);
            await _context.SaveChangesAsync();

            return tblEvent;
        }

        private bool TblEventExists(int id)
        {
            return _context.TblEvent.Any(e => e.Id == id);
        }
    }
}
