using ManagementInventory.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

/// <summary>
/// Class to manage http response exception
/// </summary>
public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
{
    /// <summary>
    /// Order
    /// </summary>
    public int Order => int.MaxValue - 10;

    /// <summary>
    /// Method on executing App
    /// </summary>
    /// <param name="context"></param>
    public void OnActionExecuting(ActionExecutingContext context) { }

    /// <summary>
    /// Method when app has been executed
    /// </summary>
    /// <param name="context"></param>
    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception is HttpResponseException httpResponseException)
        {
            context.Result = new ObjectResult(httpResponseException.Value)
            {
                StatusCode = (int)httpResponseException.StatusCode
            };

            context.ExceptionHandled = true;
        }
    }
}