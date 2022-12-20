// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Hosts.Exceptions
{
    public class HostDependencyException : Xeption
    {
        public HostDependencyException(Xeption innerException)
            : base(message: "Host dependency error occured. Contact support",
                  innerException)
        { }
    }
}
