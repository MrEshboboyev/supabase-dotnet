using Microsoft.AspNetCore.Mvc;
using Supabase;
using SupabaseDemo.Api.Contracts;
using SupabaseDemo.Api.Models;

namespace SupabaseDemo.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class NewslettersController(Client client) : ControllerBase
{
    public async Task<IActionResult> CreateNewsletter(
        [FromForm] CreateNewsletterRequest request)
    {
        var newsletter = new Newsletter
        {
            Name = request.Name,
            Description = request.Description,
            ReadTime = request.ReadTime
        };

        var response = await client.From<Newsletter>().Insert(newsletter);

        var newNewsletter = response.Models.First(); 
    
        using var memoryStream = new MemoryStream();
        await request.CoverImage.CopyToAsync(memoryStream);
        var lastIndexOfDot = request.CoverImage.FileName.LastIndexOf('.');
        string extension = request.CoverImage.FileName.Substring(lastIndexOfDot + 1);
        
        await client.Storage.From("cover-images").Upload(
            memoryStream.ToArray(),
            $"newsletter-{newsletter.Id}.{extension}");
        
        return Ok(newNewsletter.Id);
    }
}