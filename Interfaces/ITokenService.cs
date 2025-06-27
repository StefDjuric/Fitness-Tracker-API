using FitnessTrackerAPI.Entities;

namespace FitnessTrackerAPI.Interfaces
{
    public interface ITokenService
    {
        public Task<string> CreateToken(User user);
    }
}
