namespace KTravelsApi.Core.Models;

public class BaseModel
{
    public Guid Id { get; set; }
    
    public DateTime CreatedAt { get; set;}
    
    public DateTime? LastUpdated { get; set; }
    
    public bool IsDeleted { get; set; }
    
    public BaseModel()
    {
        CreatedAt = DateTime.UtcNow;
        IsDeleted = false;
    }
}