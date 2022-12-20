// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using Sheenam.Api.Models.Foundations.Hosts.Exceptions;
using Sheenam.Api.Services.Foundations.Hosts;
using Host = Sheenam.Api.Models.Foundations.Hosts.Host;

namespace Sheenam.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class HostsController : RESTFulController
    {
        private readonly IHostService hostService;

        public HostsController(IHostService hostService)
        {
            this.hostService = hostService;
        }

        [HttpPost]
        public async ValueTask<ActionResult<Host>> PostHostAsync(Host host)
        {
            try
            {
                Host postedHost = await this.hostService.AddHostAsync(host);
                return Created(postedHost);
            }
            catch (HostValidationException hostValidationException)
            {
                return BadRequest(hostValidationException.InnerException);
            }
            catch (HostDependencyValidationException hostDependencyValidationException)
                when (hostDependencyValidationException.InnerException is AlreadyExistHostException)
            {
                return Conflict(hostDependencyValidationException.InnerException);
            }
            catch (HostDependencyValidationException hostDependencyValidationException)
            {
                return BadRequest(hostDependencyValidationException.InnerException);
            }
            catch (HostDependencyException hostDependencyException)
            {
                return InternalServerError(hostDependencyException.InnerException);
            }
            catch (HostDependencyServiceException hostDependencyServiceException)
            {
                return InternalServerError(hostDependencyServiceException.InnerException);
            }
        }
    }
}
