// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using System.Data;
using Sheenam.Api.Models.Foundations.Hosts.Exceptions;
using Host = Sheenam.Api.Models.Foundations.Hosts.Host;

namespace Sheenam.Api.Services.Foundations.Hosts
{
    public partial class HostService
    {
        public void ValidationHostOnAdd(Host host)
        {
            ValidationHostNotNull(host);

            Validate((Rule: isInvalid(host.Id), Parameter: nameof(host.Id)),
                (Rule: isInvalid(host.FirstName), Parameter: nameof(host.FirstName)),
                (Rule: isInvalid(host.LastName), Parameter: nameof(host.LastName)),
                (Rule: isInvalid(host.DateOfBirth), Parameter: nameof(host.DateOfBirth)),
                (Rule: isInvalid(host.Email), Parameter: nameof(host.Email)),
                (Rule: isInvalid(host.PhoneNumber), Parameter: nameof(host.PhoneNumber)));
        }

        private static void ValidationHostNotNull(Host host)
        {
            if (host is null)
            {
                throw new NullHostException();
            }
        }
        private static dynamic isInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
        };
        private static dynamic isInvalid(string text) => new
        {
            Condition = String.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };
        private static dynamic isInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidHostException = new InvalidHostException();

            foreach ((dynamic rule, string parameter) in validations) 
            {
                if(rule.Condition)
                {
                    invalidHostException.UpsertDataList(key: parameter, value: rule.Message);
                }
            }

            invalidHostException.ThrowIfContainsErrors();
        }
    }
}
