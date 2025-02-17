namespace SupabaseDemo.Api.Contracts;

public class NewsletterResponse
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int ReadTime { get; set; }
    public string ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
}