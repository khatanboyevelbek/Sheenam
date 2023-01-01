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
            :base(message: "Guest request has not been completed. Try again")
        { }
    }
}
