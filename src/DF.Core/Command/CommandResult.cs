using System;
using System.Runtime.Serialization;

namespace DF.Core.Command
{
    /// <summary>
    /// Represent Command result data structure
    /// It can be used as web service result
    /// </summary>
    /// <remarks>result for void command</remarks>
    [DataContract]
    public class CommandResult
    {
        [DataMember(Name = "status")]
        public ResultStatus Status { get; set; }

        [DataMember(Name = "message")]
        public string Message { get; set; }

        /// <summary>
        /// return a successful CommandResult 
        /// </summary>
        /// <returns>successful CommandResult</returns>
        public static CommandResult Okay()
        {
            return new CommandResult
            {
                Status = ResultStatus.Successful
            };
        }

        /// <summary>
        /// return a CommandResult with error status and error message
        /// </summary>
        /// <param name="message">error message</param>
        /// <returns>Error CommandResult</returns>
        public static CommandResult Error(string message)
        {
            return new CommandResult
            {
                Status = ResultStatus.Error,
                Message = message
            };
        }

        /// <summary>
        /// return a CommandResult from exception
        /// most inmportant exception included in error message
        /// </summary>
        /// <param name="ex">the exception</param>
        /// <returns>Error CommandResult</returns>
        public static CommandResult Error(Exception ex)
        {
            return new CommandResult
            {
                Status = ResultStatus.Error,
                Message = ex.GetBaseException().Message
            };
        }
    }

    /// <summary>
    /// provides a generic data weapper for Command result 
    /// </summary>
    /// <typeparam name="T">data type of return</typeparam>
    /// <remarks>result for not void result</remarks>
    public class CommandResult<T> : CommandResult
    {
        [DataMember(Name = "data")]
        public T Data { get; set; }

        /// <summary>
        /// returns a successful typed CommandResult
        /// </summary>
        /// <param name="data">return data</param>
        /// <returns>successful CommandResult</returns>
        public static CommandResult<T> Okay(T data)
        {
            return new CommandResult<T>
            {
                Status = ResultStatus.Successful,
                Data = data
            };
        }

        /// <summary>
        /// returns a typed CommandResult with an error
        /// </summary>
        /// <param name="message">error message</param>
        /// <returns>error CommandResult</returns>
        public new static CommandResult<T> Error(string message)
        {
            return new CommandResult<T>
            {
                Status = ResultStatus.Error,
                Message = message
            };
        }
    }

    /// <summary>
    /// result type status
    /// </summary>
    public enum ResultStatus
    {
        /// <summary>
        /// the command executed successfully 
        /// </summary>
        Successful = 1,

        /// <summary>
        /// the command executed and there is some information about it.
        /// </summary>
        Information = 2,

        /// <summary>
        /// the command executed and there is a warning.
        /// </summary>
        Warning = 3,

        /// <summary>
        /// the commad execution has been  failed with error. 
        /// </summary>
        Error = 4
    }
}
