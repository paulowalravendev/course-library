using Microsoft.EntityFrameworkCore;

namespace Authors.GetAll;

public class Endpoint : EndpointWithMapping<Request, List<Response>, Author>
{
    private readonly CourseLibraryContext _context;

    public Endpoint(CourseLibraryContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("/authors");
        AllowAnonymous();
    }

    public override async Task<List<Response>> ExecuteAsync(Request req, CancellationToken ct)
    {
        var query = _context.Authors.Select(a => new Response
        {
            Id = a.Id,
            FullName = a.FirstName + " " + a.LastName,
            MainCategory = a.MainCategory
        });

        if (string.IsNullOrEmpty(req.MainCategory) == false)
            query = query.Where(a => a.MainCategory.ToLower() == req.MainCategory.ToLower());

        return await query.OrderBy(a => a.FullName).ToListAsync(ct);
    }
}