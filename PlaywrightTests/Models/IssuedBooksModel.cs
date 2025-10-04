namespace PlaywrightTests.Models;

public class IssuedBooksModel
{
    public int StudentId { get; set; }
    public string? StudentName { get; set; }
    public int StudentRollNo { get; set; }
    public string? StudentDepartment { get; set; }
    public List<BooksIssuedModel>? BooksIssued { get; set; }
}

public class BooksIssuedModel
{
    public int BookId { get; set; }
    public string? BookName { get; set; }
    public string? BookUniqueId { get; set; }
    public string? BookDepartment { get; set; }
    public string? BookAuthor { get; set; }
}
