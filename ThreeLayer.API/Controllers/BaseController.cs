using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;
using ThreeLayer.API.Models;
using ThreeLayer.Business.Interfaces;
using ThreeLayer.Business.Notifications;

namespace ThreeLayer.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        private readonly INotifier _notifier;

        protected BaseController(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected bool OperateValidate()
        {
            return !_notifier.IsThereAnyNotification();
        }

        protected ActionResult CustomResponse(
            HttpStatusCode statusCode = HttpStatusCode.OK, 
            object result = null)
        {
            if (OperateValidate())
            {
                return new ObjectResult(result)
                {
                    StatusCode = Convert.ToInt32(statusCode),
                };
            }

            return BadRequest(new ResultWrapper<bool>(
                false,
                false,
                _notifier.GetNotificationsAsReadOnly().Select(s => s.Message).ToList(),
                "N/A"));
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotifierErroModelInvalidate(modelState);
            return CustomResponse();
        }

        protected void NotifierErroModelInvalidate(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotifyError(errorMsg);
            }
        }

        protected void NotifyError(string mensagem)
        {
            _notifier.Handle(new Notification(mensagem));
        }
    }
}
