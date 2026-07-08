using GrupoMadero.Common.Results;
using Microsoft.AspNetCore.Mvc;

namespace InventoryOrderAPI.Extensions
{
    public static class ResultExtension
    {
        public static IActionResult Ok<TResult>(this Result<TResult> result) where TResult : class
        {
            return result.IsSuccess ?
                new OkObjectResult(result.Value) :
                result.BadRequest();
        }

        public static IActionResult OkOrNotFound<TResult>(this Result<TResult> result) where TResult : class
        {
            return result.IsSuccess ?
                new OkObjectResult(result.Value) :
                result.NotFound();
        }

        public static IActionResult Created<TResult>(this Result<TResult> result) where TResult : class
        {
            return result.IsSuccess
                ? new ObjectResult(result.Value) { StatusCode = StatusCodes.Status201Created }
                : result.BadRequest();
        }

        public static IActionResult NoContent(this Result result)
        {
            return result.IsSuccess ?
                new NoContentResult() :
                result.BadRequest();
        }

        public static IActionResult NoContentOrNotFound(this Result result)
        {
            return result.IsSuccess ?
                new NoContentResult() :
                result.NotFound();
        }

        public static IActionResult BadRequest<TResult>(this Result<TResult> result) where TResult : class
        {
            return new BadRequestObjectResult(new { result.Errors });
        }

        public static IActionResult BadRequest(this Result result)
        {
            return new BadRequestObjectResult(new { result.Errors });
        }

        public static IActionResult NotFound<TResult>(this Result<TResult> result) where TResult : class
        {
            return new NotFoundObjectResult(new { result.Errors });
        }

        public static IActionResult NotFound(this Result result)
        {
            return new NotFoundObjectResult(new { result.Errors });
        }
    }
}
