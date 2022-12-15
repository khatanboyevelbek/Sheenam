// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Guests.Exceptions
{
    public class GuestDependencyServiceException : Xeption
    {
        public GuestDependencyServiceException(Xeption innerExpection)
            : base(message: "Unexpected service error occured. Contact support",
                  innerExpection)
        { }
    }
}
