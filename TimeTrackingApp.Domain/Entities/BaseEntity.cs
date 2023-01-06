namespace TimeTrackingApp.Domain.Entities;

public abstract class BaseEntity: IEquatable<BaseEntity>
{
    public Guid Id { get; set; }

    public bool Equals(BaseEntity? other)
    {
        if (other == null)
        {
            return false;
        }
        return Id == other.Id;
    }
}