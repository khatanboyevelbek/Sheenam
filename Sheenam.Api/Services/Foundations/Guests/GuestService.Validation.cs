// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Sheenam.Api.Models.Foundations.Guests;
using Sheenam.Api.Models.Foundations.Guests.Exceptions;

namespace Sheenam.Api.Services.Foundations.Guests;

public partial class GuestService
{
    private void ValidationGuestNotNull(Guest guest)
    {
        if (guest is null)
        {
            throw new NullGuestException();
        }
    }
}
