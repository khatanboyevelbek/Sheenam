﻿// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Hosts.Exceptions
{
    public class NullHostException : Xeption
    {
        public NullHostException()
            : base(message: "Host is null")
        { }
    }
}
