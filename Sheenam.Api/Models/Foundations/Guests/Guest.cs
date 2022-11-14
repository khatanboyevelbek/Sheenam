// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use comfort and pease
// ---------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sheenam.Api.Models.Foundations.Guests
{
    public class Guest
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        public DateTimeOffset DateOfBirth { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        [Column(TypeName = "ntext")]
        public string Address { get; set; }

        [Required]
        public GenderType Gender { get; set; }
    }
}
