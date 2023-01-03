// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Guests.Exceptions
{
    public class UnauthorizedGuestException : Xeption
    {
        public UnauthorizedGuestException() 
            :base(message: "You must authenticate with a valid Bearer token to access this resource. Try again")
        { }
    }
}
