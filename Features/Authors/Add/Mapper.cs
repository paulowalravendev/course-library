namespace Authors.Add;

public class Mapper : Mapper<Request, Response, Author>
{
    public override Author ToEntity(Request r)
        => new()
        {
            FirstName = r.FirstName,
            LastName = r.LastName,
            DateOfBirth = r.DateOfBirth,
            MainCategory = r.MainCategory
        };

    public override Response FromEntity(Author e) =>
        new()
        {
            AuthorId = e.Id
        };
}