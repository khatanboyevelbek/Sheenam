// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Hosts.Exceptions
{
    public class FailedHostLoginException : Xeption
    {
        public FailedHostLoginException()
            :base(message: "Host is not found. Try again")
        { }
    }
}
