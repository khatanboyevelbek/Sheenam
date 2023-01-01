// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using Sheenam.Api.Models.Foundations;
using Sheenam.Api.Models.Foundations.Guests;
using Sheenam.Api.Models.Foundations.Guests.Exceptions;

namespace Sheenam.Api.Services.Foundations.Guests;

public partial class GuestService
{
    private void ValidateGuestId(Guid id)
    {
        Validate((Rule: IsInvalid(id), Parameter: nameof(Guest.Id)));
    }
    private void ValidateGuestOnAdd(Guest guest)
    {
        ValidationGuestNotNull(guest);

        Validate(
          (Rule: IsInvalid(guest.Id), Parameter: nameof(guest.Id)),
          (Rule: IsInvalid(guest.FirstName), Parameter: nameof(guest.FirstName)),
          (Rule: IsInvalid(guest.LastName), Parameter: nameof(guest.LastName)),
          (Rule: IsInvalid(guest.DateOfBirth), Parameter: nameof(guest.DateOfBirth)),
          (Rule: IsInvalid(guest.Email), Parameter: nameof(guest.Email)),
          (Rule: IsInvalid(guest.Password), Parameter: nameof(guest.Password)),
          (Rule: IsInvalid(guest.Address), Parameter: nameof(guest.Address)),
          (Rule: IsInvalid(guest.Gender), Parameter: nameof(guest.Gender)));
    }
    private void ValidationGuestNotNull(Guest guest)
    {
        if (guest is null)
        {
            throw new NullGuestException();
        }
    }
    private static dynamic IsInvalid(Guid id) => new
    {
        Condition = id == Guid.Empty,
        Message = "Id is required"
    };

    private static dynamic IsInvalid(string text) => new
    {
        Condition = string.IsNullOrWhiteSpace(text),
        Message = "Text is invalid"
    };

    private static dynamic IsInvalid(GenderType gender) => new
    {
        Condition = Enum.IsDefined(gender) is false,
        Message = "Value is invalid"
    };

    private static dynamic IsInvalid(DateTimeOffset dateOfBirth) => new
    {
        Condition = dateOfBirth == default,
        Message = "Date is invalid"
    };

    private static void Validate(params (dynamic Rule, string Parameter)[] validations)
    {
        var invalidGuestException = new InvalidGuestException();

        foreach ((dynamic rule, string parameter) in validations)
        {
            if (rule.Condition)
            {
                invalidGuestException.UpsertDataList(parameter, rule.Message);
            }
        }

        invalidGuestException.ThrowIfContainsErrors();
    }
}
