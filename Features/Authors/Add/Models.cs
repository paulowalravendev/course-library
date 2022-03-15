namespace Authors.Add;

public class Request
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string MainCategory { get; set; } = null!;
    public DateTimeOffset DateOfBirth { get; set; }
}

public class Response
{
    public string Message => "Author created!";
    public long AuthorId { get; set; }
}

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .Length(3, 50).WithMessage("{PropertyName} must be between 3 and 50 characters.");
        
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .Length(3, 50).WithMessage("{PropertyName} must be between 3 and 50 characters.");
        
        RuleFor(x => x.MainCategory)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .Length(3, 50).WithMessage("{PropertyName} must be between 3 and 50 characters.");

        RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .Must(date => date < DateTimeOffset.Now).WithMessage("{PropertyName} must be pass date.");
    }
}