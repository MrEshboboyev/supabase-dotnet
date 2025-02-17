namespace SupabaseDemo.Api.Contracts;

public class CreateNewsletterRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int ReadTime { get; set; }
    public IFormFile CoverImage { get; set; }
}