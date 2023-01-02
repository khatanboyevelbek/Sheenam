// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Hosts.Exceptions
{
    public class ForbiddenHostException : Xeption
    {
        public ForbiddenHostException() 
            : base(message: "You don't have permission to access this recourse")
        { }
    }
}
