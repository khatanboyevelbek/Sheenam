// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Hosts.Exceptions
{
    public class FailedHostStorageException : Xeption
    {
        public FailedHostStorageException(Exception innerException)
            : base(message: "Failed host storage error", innerException)
        { }
    }
}
