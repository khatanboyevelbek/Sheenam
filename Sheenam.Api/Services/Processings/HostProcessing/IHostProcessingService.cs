// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Sheenam.Api.Models.Foundations.LoginModel;
using Host = Sheenam.Api.Models.Foundations.Hosts.Host;

namespace Sheenam.Api.Services.Processings.HostProcessing
{
    public interface IHostProcessingService
    {
        Host? RetrieveHostByCridentials(LoginModel loginModel);
    }
}
