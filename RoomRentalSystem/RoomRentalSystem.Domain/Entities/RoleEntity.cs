using RoomRentalSystem.Domain.Exceptions;

namespace RoomRentalSystem.Domain.Entities;

public class RoleEntity : BaseEntity
{
    public string Name { get; init; }
    public ICollection<UserEntity> Users { get; init; }

    private RoleEntity() { }

    public static RoleEntity Create(Guid id, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainException(nameof(name));
        }

        return new RoleEntity
        {
            Id = id,
            Name = name
        };
    }
}