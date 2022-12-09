using TimeTrackingApp.Application.Repositories;
using TimeTrackingApp.Domain.Entities;
using TimeTrackingApp.Domain.Repositories;

namespace TimeTrackingApp.Domain.UnitOfWork;

public interface IUnitOfWork: IDisposable, IAsyncDisposable
{
    IGenericRepository<TimeTrack> TimeTrackRepository { get; }
    IGenericRepository<TrackingEvent> TrackingEventRepository { get; }
    IGenericRepository<TrackingEventType> TrackingEventTypeRepository { get; }
    void SaveChanges();   
    Task SaveChangesAsync();  
}