namespace PlaywrightTests.Models;

public class BooksModel
{
    public int Id { get; set; }
    public string? BookId { get; set; }
    public string? BookName { get; set; }
    public string? BookAuthor { get; set; }
    public string? Department { get; set; }
    public bool Status { get; set; }
    public long CreatedAt { get; set; }
    public long UpdatedAt { get; set; }
}
