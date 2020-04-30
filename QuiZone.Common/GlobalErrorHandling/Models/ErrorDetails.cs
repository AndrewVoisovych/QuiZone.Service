using System;

namespace QuiZone.Common.GlobalErrorHandling.Models
{
    [Serializable]
    public sealed class ErrorDetails
    {
        public ErrorDetails() { }

        public ErrorDetails(string message)
        {
            Message = message;
        }

        public string Message { get; set; } = string.Empty;
    }
}
