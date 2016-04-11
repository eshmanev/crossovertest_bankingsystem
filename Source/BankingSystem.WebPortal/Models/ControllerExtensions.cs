using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace BankingSystem.WebPortal.Models
{
    /// <summary>
    ///     Contains a set of extension methods for the <see cref="Controller" /> class.
    /// </summary>
    public static class ControllerExtensions
    {
        /// <summary>
        ///     Creates a new JSON error result.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>JsonResult</returns>
        public static JsonResult JsonError(this Controller controller, string message = "An error has occurred")
        {
            if (controller.Response != null)
                controller.Response.StatusCode = (int) HttpStatusCode.BadRequest;

            var model = new ErrorViewModel {Message = message, Details = new Dictionary<string, string>()};
            foreach (var item in controller.ModelState)
                foreach (var error in item.Value.Errors)
                    model.Details.Add(item.Key, error.ErrorMessage);

            return new JsonResult
            {
                Data = model,
                ContentType = null,
                ContentEncoding = null,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        /// <summary>
        ///     Creates a new JSON success result.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>JsonResult</returns>
        public static JsonResult JsonSuccess(this Controller controller, string message = "The operation has successfully completed")
        {
            if (controller.Response != null)
                controller.Response.StatusCode = (int) HttpStatusCode.OK;
            return new JsonResult
            {
                Data = new SuccessViewModel {Message = message},
                ContentType = null,
                ContentEncoding = null,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}