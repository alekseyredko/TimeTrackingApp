using TimeTrackingApp.Core.Repositories;
using TimeTrackingApp.Domain.Entities;

namespace TimeTrackingApp.Application.UnitOfWork;

public interface IUnitOfWork: IDisposable, IAsyncDisposable
{
    IGenericRepository<TimeTrack> TimeTrackRepository { get; }
    ITrackingEventRepository TrackingEventRepository { get; }
    IGenericRepository<TrackingEventType> TrackingEventTypeRepository { get; }
    void SaveChanges();   
    Task SaveChangesAsync();  
}