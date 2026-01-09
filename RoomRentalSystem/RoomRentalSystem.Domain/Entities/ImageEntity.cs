using RoomRentalSystem.Domain.Exceptions;

namespace RoomRentalSystem.Domain.Entities;

public class ImageEntity : BaseEntity
{
    public string ImageData { get; private set; }
    public string Name { get; private set; }
    public string Type { get; private set; }

    private ImageEntity() { }

    public static ImageEntity Create(
        string imageData,
        string name,
        string type)
    {
        if (string.IsNullOrWhiteSpace(imageData))
        {
            throw new DomainException(nameof(imageData));
        }
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainException(nameof(name));
        }
        if (string.IsNullOrWhiteSpace(type))
        {
            throw new DomainException(nameof(type));
        }

        return new ImageEntity
        {
            ImageData = imageData,
            Name = name,
            Type = type
        };
    }
}