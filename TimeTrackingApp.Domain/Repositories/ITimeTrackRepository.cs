using TimeTrackingApp.Application.Repositories;
using TimeTrackingApp.Domain.Entities;

namespace TimeTrackingApp.Domain.Repositories;

public interface ITimeTrackRepository : IGenericRepository<TimeTrack>
{
}