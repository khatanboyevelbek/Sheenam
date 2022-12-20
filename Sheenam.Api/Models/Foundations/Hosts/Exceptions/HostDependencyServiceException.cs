// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Hosts.Exceptions
{
    public class HostDependencyServiceException : Xeption
    {
        public HostDependencyServiceException(Xeption innerException) 
            : base(message: "Unexpected host service error occured. Contact support", 
                  innerException)
        { }
    }
}
