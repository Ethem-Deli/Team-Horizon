using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class ErrorModel : PageModel
{
    private readonly ILogger<ErrorModel> _logger;

    public ErrorModel(ILogger<ErrorModel> logger)
    {
        _logger = logger;
    }

    public string? RequestId { get; set; }
    public int? StatusCodeValue { get; set; }

    public void OnGet(int? code = null)
    {
        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        StatusCodeValue = code;

        _logger.LogError("Error page hit. StatusCode={StatusCode}, RequestId={RequestId}", StatusCodeValue, RequestId);
    }
}
