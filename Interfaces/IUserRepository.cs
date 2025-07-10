using FitnessTrackerAPI.Entities;
using FitnessTrackerAPI.Models;

namespace FitnessTrackerAPI.Interfaces
{
    public interface IUserRepository
    {
        public Task<User?> GetUserByIdAsync(int id);
        public Task<User?> GetUserByUsernameAsync(string username);
        public Task<IEnumerable<User>> GetAllUsersAsync();
        public Task<bool> SaveAllAsync();
        void Update(User user);
        public Task<MemberDto?> GetMemberByIdAsync(int id);
        public Task<MemberDto?> GetMemberByUsernameAsync(string username);
        public Task<IEnumerable<MemberDto>> GetAllMembersAsync();
    }
}
