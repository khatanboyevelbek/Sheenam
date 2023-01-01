// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Guests.Exceptions
{
    public class NotFoundGuestException : Xeption
    {
        public NotFoundGuestException()
            : base(message: "Guest is not found. Try again")
        { }
    }
}
