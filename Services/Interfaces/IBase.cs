namespace FagestProAdmin.Services.Interfaces
{
    public interface IBase<T>
    {
        public Task<bool> CreateAsync(object model);
        public Task<bool> UpdateAsync(object model);
        public Task<bool> RemoveAsync(object EntityId);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> GetByIdAsync(object EntityId);
    }
}
