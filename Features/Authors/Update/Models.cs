using Microsoft.AspNetCore.Mvc;

namespace Authors.Update;

public class Request
{
    [FromRoute] public long Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? MainCategory { get; set; }
    public DateTimeOffset? DateOfBirth { get; set; }
}

public class Response
{
    public string Message => "Author updated!";
    public long AuthorId { get; set; }
}

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("{PropertyName} is required.");

        When(p => p.FirstName != null, () =>
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .Length(3, 50).WithMessage("{PropertyName} must be between 3 and 50 characters.");
        });

        When(p => p.LastName != null, () =>
        {
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .Length(3, 50).WithMessage("{PropertyName} must be between 3 and 50 characters.");
        });

        When(p => p.MainCategory != null, () =>
        {
            RuleFor(x => x.MainCategory)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .Length(3, 50).WithMessage("{PropertyName} must be between 3 and 50 characters.");
        });

        When(p => p.DateOfBirth != null, () =>
        {
            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .Must(date => date < DateTimeOffset.Now).WithMessage("{PropertyName} must be pass date.");
        });
    }
}