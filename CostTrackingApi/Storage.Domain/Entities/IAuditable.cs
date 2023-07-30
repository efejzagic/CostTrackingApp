namespace Storage.Domain.Entities
{
    public interface IAuditable
    {
        DateTime DateCreated { get; set; }
        DateTime? DateModified { get; set; }

    }
}
