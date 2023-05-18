using System.Net;
using System.Runtime.Serialization;

namespace Core.Models.Base
{
    /// <summary>
    /// Creates a response object
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DataContract]
    public class ResponseDto
    {
        /// <summary>
        /// Status Code for the petition
        /// </summary>
        [DataMember(Name = "status")]
        public int Status { get; set; } = default!;

        /// <summary>
        /// Error message if an error occurrs
        /// </summary>
        [DataMember(Name = "errorMessage")]
        public string ErrorMessage { get; set; } = default!;

    }

    /// <summary>
    /// Creates a generic response object
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseDto<T> : ResponseDto
    {
        /// <summary>
        /// Data requested by client
        /// </summary>
        [DataMember(Name = "data")]
        public T Data { get; set; } = default!;


        /// <summary>
        /// Creates a successful response
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ResponseDto<T> CreateSuccessResponse(T data)
        {
            return new ResponseDto<T>
            {
                Status = (int)HttpStatusCode.OK,
                Data = data
            };
        }

        /// <summary>
        /// Creates an error response
        /// </summary>
        /// <param name="ex">This is an awesome description</param>
        /// <returns>Returns magic</returns>
        public static ResponseDto<T> CreateErrorResponse(Exception ex)
        {
            return new ResponseDto<T>
            {
                Status = (int)HttpStatusCode.InternalServerError,
                ErrorMessage = ex.Message
            };
        }

        /// <summary>
        /// Creates a bad request response
        /// </summary>
        /// <param name="ex">This is an awesome description</param>
        /// <returns>Returns magic</returns>
        public static ResponseDto<T> CreateBadRequest(Exception ex)
        {
            return new ResponseDto<T>
            {
                Status = (int)HttpStatusCode.BadRequest,
                ErrorMessage = ex.Message
            };
        }
    }
}

