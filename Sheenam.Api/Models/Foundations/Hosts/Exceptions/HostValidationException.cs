// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Hosts.Exceptions
{
    public class HostValidationException : Xeption
    {
        public HostValidationException(Xeption innerException)
            : base(message: "Host validation error occured, fix the errors and try again",
                  innerException)
        { }
    }
}
