using Microsoft.EntityFrameworkCore;

namespace Authors.Update;

public class Endpoint : EndpointWithMapping<Request, Response, Author>
{
    private readonly CourseLibraryContext _context;

    public Endpoint(CourseLibraryContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Verbs(Http.PUT);
        Routes("/authors/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        await ValidationRequestNames(req);
        ThrowIfAnyErrors();
        var entityFromDatabase = await _context.Authors.FirstOrDefaultAsync(a => a.Id == req.Id, ct);
        if (entityFromDatabase is null)
            AddError(r => r.Id, "Invalid Author Id.");
        ThrowIfAnyErrors();

        if (req.FirstName is not null) entityFromDatabase!.FirstName = req.FirstName;
        if (req.LastName is not null) entityFromDatabase!.LastName = req.LastName;
        if (req.MainCategory is not null) entityFromDatabase!.MainCategory = req.MainCategory;
        if (req.DateOfBirth is not null) entityFromDatabase!.DateOfBirth = (DateTimeOffset) req.DateOfBirth;
        _context.Authors.Update(entityFromDatabase!);

        if (req.FirstName is not null || req.LastName is not null)
        {
            var nameIsTaken = await NameIsTaken(entityFromDatabase!.FirstName, entityFromDatabase!.LastName);
            if (nameIsTaken)
                AddError($"Name \"{entityFromDatabase.FirstName} {entityFromDatabase!.LastName}\" already in use.");
            ThrowIfAnyErrors();
        }

        await _context.SaveChangesAsync(ct);
        await SendAsync(new Response
        {
            AuthorId = entityFromDatabase!.Id
        }, cancellation: ct);
    }

    private Task<bool> NameIsTaken(string firstName, string lastName)
    {
        return _context.Authors.AnyAsync(a =>
            a.FirstName.ToLower() == firstName.ToLower() && a.LastName.ToLower() == lastName.ToLower());
    }

    private async Task ValidationRequestNames(Request req)
    {
        if (req.FirstName is not null && req.LastName is not null)
        {
            var nameIsTaken = await NameIsTaken(req.FirstName, req.LastName);
            if (nameIsTaken)
                AddError($"Name \"{req.FirstName} {req.LastName}\" already in use.");
        }
    }

    private async Task Update(Author entity)
    {
        _context.Update(entity);
        await _context.SaveChangesAsync();
    }
}