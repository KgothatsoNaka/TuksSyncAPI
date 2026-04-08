using Microsoft.EntityFrameworkCore;
using TuksSyncAPI.Models;
using TuksSyncAPI.Data;

namespace TuksSyncAPI.Repositories
{
    public class EventInfoRes : IEventInfoRes
    {
        private readonly ApiDbContext _context;

        public EventInfoRes(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<EventInfo> CreateEventInfo(EventInfo eventInfo)
        {
            _context.EventInfos.Add(eventInfo);
            await _context.SaveChangesAsync();
            return eventInfo;
        }

        public async Task<bool> DeleteEventInfo(int id)
        {
            var eventInfo = await _context.EventInfos.FindAsync(id);
            if (eventInfo == null)
            {
                return false;
            }

            _context.EventInfos.Remove(eventInfo);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<EventInfo>> GetEventInfo()
        {
            return await _context.EventInfos.ToListAsync();
        }

        public async Task<EventInfo?> GetEventInfoById(int id)
        {
            return await _context.EventInfos.FindAsync(id);
        }

        public async Task<EventInfo?> UpdateEventInfo(int id, EventInfo eventInfo)
        {
            var existingEvent = await _context.EventInfos.FindAsync(id);
            if (existingEvent == null)
            {
                return null;
            }

            existingEvent.Title = eventInfo.Title;
            existingEvent.Location = eventInfo.Location;
            existingEvent.TicketPrice = eventInfo.TicketPrice;

            await _context.SaveChangesAsync();
            return existingEvent;
        }
    }
}