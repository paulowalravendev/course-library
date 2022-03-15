using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Authors.Add;

public class Endpoint : Endpoint<Request, Response, Mapper>
{
    private readonly CourseLibraryContext _context;

    public Endpoint(CourseLibraryContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("/authors");
        AllowAnonymous();
    }

    public override async Task HandleAsync([FromBody] Request req, CancellationToken ct)
    {
        var entity = Map.ToEntity(req);

        var nameIsTaken = await NameIsTaken(entity.FirstName, entity.LastName);
        if (nameIsTaken)
            AddError("Name already in use");
        ThrowIfAnyErrors();

        await Create(entity);

        await SendAsync(Map.FromEntity(entity), cancellation: ct);
    }

    private Task<bool> NameIsTaken(string firstName, string lastName)
        => _context.Authors.AnyAsync(a =>
            a.FirstName.ToLower() == firstName.ToLower() && a.LastName.ToLower() == lastName.ToLower());

    private async Task Create(Author entity)
    {
        await _context.Authors.AddAsync(entity);
        await _context.SaveChangesAsync();
    }
}