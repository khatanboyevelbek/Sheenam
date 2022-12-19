// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Hosts.Exceptions
{
    public class AlreadyExistHostException : Xeption
    {
        public AlreadyExistHostException(Exception innerException)
            : base(message: "Host is already exist. Try again", 
                  innerException)
        { }
    }
}
