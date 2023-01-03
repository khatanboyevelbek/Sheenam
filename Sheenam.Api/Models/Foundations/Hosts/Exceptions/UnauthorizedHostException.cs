// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Hosts.Exceptions
{
    public class UnauthorizedHostException : Xeption
    {
        public UnauthorizedHostException() 
            :base(message: "You must authenticate with a valid Bearer token to access this resource. Try again")
        { }
    }
}
