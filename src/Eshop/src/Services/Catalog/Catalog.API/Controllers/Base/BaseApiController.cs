using Core.Models.Base;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers.Base
{
    /// <summary>
    /// Base Api Controller, contains all the shared logic among controllers
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    public abstract class BaseApiController<TService> : ControllerBase
    {
        /// <summary>
        /// Service being used by the controller
        /// </summary>
        protected readonly TService _service;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service"></param>
        protected BaseApiController(TService service)
        {
            _service = service;
        }

        /// <summary>
        /// Executes a service action
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceAction"></param>
        /// <returns></returns>
        protected ResponseDto<T> Execute<T>(Func<T> serviceAction)
        {
            try
            {
                var response = ResponseDto<T>.CreateSuccessResponse(serviceAction.Invoke());
                //return Ok(response);
                return response;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                //return BadRequest(ResponseDto<T>.CreateBadRequest(ex));
                return ResponseDto<T>.CreateBadRequest(ex); //EntidadMalaLoca --> ApiDatabaseException
            }
            catch (Exception ex)
            {
                return ResponseDto<T>.CreateErrorResponse(ex);
                //TODO: replace catch catch por filtros de control de excepciones
            }
        }

        /// <summary>
        /// Executes a service action async
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceAction"></param>
        /// <returns></returns>
        protected async Task<ResponseDto<T>> ExecuteAsync<T>(Func<Task<T>> serviceAction)
        {
            try
            {
                var response = ResponseDto<T>.CreateSuccessResponse(await serviceAction.Invoke());
                //return Ok(response);
                return response;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                //return BadRequest(ResponseDto<T>.CreateBadRequest(ex));
                return ResponseDto<T>.CreateBadRequest(ex);
            }
            catch (Exception ex)
            {
                return ResponseDto<T>.CreateErrorResponse(ex);
                //TODO: replace catch catch por filtros de control de excepciones
            }
        }
    }
}