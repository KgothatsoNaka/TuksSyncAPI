using TuksSyncAPI.Models;

namespace TuksSyncAPI.Repositories
{
    public interface IEventInfoRes
    {
        Task<IEnumerable<EventInfo>> GetEventInfo();
        Task<EventInfo?> GetEventInfoById(int id);
        Task<EventInfo> CreateEventInfo(EventInfo eventInfo);
        Task<EventInfo?> UpdateEventInfo(int id, EventInfo eventInfo);
        Task<bool> DeleteEventInfo(int id);
    }
}