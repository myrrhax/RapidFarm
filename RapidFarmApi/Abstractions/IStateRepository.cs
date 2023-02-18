using RapidFarmApi.Database.Entities;

namespace RapidFarmApi.Abstractions
{
    public interface IStateRepository
    {
        public Task AddState(State state);
        public Task<State?> GetStateById(Guid id);
        public Task DeleteState(Guid stateId);
        public Task<State?> GetLastState();
        public Task<List<State>>? GetDayStates(DateTime time);
        public Task<List<State>>? GetHourStates(DateTime time);
        public Task<List<State>>? GetMonthStates(DateTime time);
    }
}