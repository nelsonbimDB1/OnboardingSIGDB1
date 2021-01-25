using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OnboardingSIGDB1.Domain.Interfaces.Notification;
using System;
using System.Net;
using System.Text;

namespace OnboardingSIGDB1.API.Controllers
{
    public class BaseController : ControllerBase
    {
        private bool _disposed;
        protected readonly IDomainNotificationHandler _notification;
        protected readonly IMapper _mapper;

        #region Ctores

        protected BaseController(IDomainNotificationHandler notification, IMapper mapper)
        {
            _notification = notification;
            _mapper = mapper;
        }

        #endregion Ctores

        #region Dispose

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _notification?.Dispose();
                }

                _disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion Dispose

        #region Protected methods

        [NonAction]
        protected new IActionResult Response<T>(T content) =>
            _notification.HasNotifications ? BadRequest() : Ok(content);

        protected new IActionResult Response() =>
            _notification.HasNotifications ? BadRequest() : Ok();

        protected new IActionResult BadRequest()
        {
            var response = new ContentResult
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Content = JsonConvert.SerializeObject(
                        new
                        {
                            Notifications = _notification.GetNotifications()
                        }
                    )
            };

            return response;
        }

        protected IActionResult BadRequest<T>(T content)
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var response = new ContentResult
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Content = JsonConvert.SerializeObject(content, jsonSerializerSettings)
            };

            return response;
        }

        protected new IActionResult NotFound()
        {
            var stringBuilder = new StringBuilder();

            foreach (var erro in _notification.GetNotifications())
            {
                stringBuilder.Append($"{erro.Value} ");
            }

            var mensagem = stringBuilder.ToString().TrimEnd();

            var response = new ContentResult
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                Content = JsonConvert.SerializeObject(new { message = mensagem })
            };

            return response;
        }

        #endregion Protected methods

        #region Private methods

        private new IActionResult Ok()
        {
            var response = new ContentResult
            {
                StatusCode = (int)HttpStatusCode.OK
            };

            return response;
        }

        private IActionResult Ok<T>(T content)
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var serializeObject = content == null ? null : JsonConvert.SerializeObject(content, jsonSerializerSettings);
            var stringContent = string.IsNullOrEmpty(serializeObject) ? null : serializeObject;

            var response = new ContentResult
            {
                StatusCode = (int)HttpStatusCode.OK,
                Content = stringContent
            };

            return response;
        }

        #endregion Private methods
    }
}
