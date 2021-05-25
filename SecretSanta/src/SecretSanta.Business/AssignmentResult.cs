using System;

namespace SecretSanta.Business
{
    public class AssignmentResult
    {
        public bool IsSuccess => string.IsNullOrWhiteSpace(ErrorMessage);
        public string? ErrorMessage { get; }

        private AssignmentResult(string? errorMessage)
            => ErrorMessage = errorMessage;

        public static AssignmentResult Success() => new(null);

        public static AssignmentResult Error(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException($"'{nameof(message)}' cannot be null or whitespace.", nameof(message));
            }
            return new(message);
        }
    }
}