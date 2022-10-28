namespace ManagementInventory.Application.Exceptions
{
    /// <summary>
    /// Class to get a response of an exception
    /// </summary>
    public class MessageResponseException
    {
        /// <summary>
        /// Errors number
        /// </summary>
        public int ErrorCount { get; set; }

        /// <summary>
        /// Error message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// List of errors
        /// </summary>
        public List<String> Errors { get; set; }
    }
}