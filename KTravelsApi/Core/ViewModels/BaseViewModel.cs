namespace KTravelsApi.Core.ViewModels;

public record BaseViewModel
{
    public Guid Id { get; set; }
    
    public DateTime CreatedAt { get; set;}
    
    public DateTime? LastUpdated { get; set; }
}