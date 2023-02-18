using Microsoft.EntityFrameworkCore;
using RapidFarmApi.Abstractions;
using RapidFarmApi.Database.Entities;

namespace RapidFarmApi.Database.Repository
{
    public class StateRepository : IStateRepository
    {
        private ApplicationDbContext _db;
        private ILogger<StateRepository> _logger;
        public StateRepository(ApplicationDbContext db, ILogger<StateRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task AddState(State state)
        {
            await _db.StateList.AddAsync(state);
        }

        public async Task DeleteState(Guid stateId)
        {
            State? state = await GetStateById(stateId);
            if (state != null)
                _db.StateList.Remove(state);
        }

        public Task<List<State>>? GetDayStates(DateTime time)
        {
            return _db.StateList.Where(
                e => e.AddTime.Month == time.Month &&
                e.AddTime.Year == time.Year &&
                e.AddTime.Day == time.Month)
                .ToListAsync();
        }

        public Task<List<State>>? GetHourStates(DateTime time)
        {
            return _db.StateList.Where(
                e => e.AddTime.Month == time.Month &&
                e.AddTime.Year == time.Year &&
                e.AddTime.Day == time.Day &&
                e.AddTime.Hour == time.Hour
            )
            .ToListAsync();
        }

        public Task<List<State>>? GetMonthStates(DateTime time)
        {
            return _db.StateList.Where(
                e => e.AddTime.Month == time.Month &&
                e.AddTime.Year == time.Year
            )
            .ToListAsync();
        }

        public Task<State>? GetLastState()
        {
            return _db.StateList.LastAsync();
        }

        public Task<State?> GetStateById(Guid id)
        {
            return _db.StateList.FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}