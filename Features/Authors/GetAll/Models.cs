using Microsoft.AspNetCore.Mvc;

namespace Authors.GetAll;

public class Request
{
    [FromQuery] public string? MainCategory { get; set; }
}

public class Response
{
    public long Id { get; set; }
    public string FullName { get; set; } = null!;
    public string MainCategory { get; set; } = null!;
}