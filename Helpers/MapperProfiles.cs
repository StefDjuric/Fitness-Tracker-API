using AutoMapper;
using FitnessTrackerAPI.Entities;
using FitnessTrackerAPI.Models;

namespace FitnessTrackerAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, MemberDto>();
            CreateMap<RegisterDto, User>();
            CreateMap<Workout, WorkoutDto>().ReverseMap();
            CreateMap<Exercise, ExerciseDto>().ReverseMap();
            CreateMap<WeightliftingLog, WeightliftingLogDto>().ReverseMap();
            CreateMap<RunLog, RunLogDto>().ReverseMap();
            CreateMap<UserGoals, GoalsDto>().ReverseMap();
            CreateMap<WeeklyProgress, WeeklyProgressDto>().ReverseMap();
            CreateMap<WeightEntry, WeightEntryDto>().ReverseMap();
        }
    }
}
