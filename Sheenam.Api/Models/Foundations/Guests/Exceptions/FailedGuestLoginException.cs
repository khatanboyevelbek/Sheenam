﻿// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Guests.Exceptions
{
    public class FailedGuestLoginException : Xeption
    {
        public FailedGuestLoginException()
            : base(message: "Email or password is invalid. Try again")
        { }
    }
}
