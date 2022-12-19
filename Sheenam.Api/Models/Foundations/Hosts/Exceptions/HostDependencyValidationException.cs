// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Hosts.Exceptions
{
    public class HostDependencyValidationException : Xeption
    {
        public HostDependencyValidationException(Xeption innerException)
            : base(message: "Host dependency error occured, fix the errors and try again", 
                  innerException)
        { }
    }
}
