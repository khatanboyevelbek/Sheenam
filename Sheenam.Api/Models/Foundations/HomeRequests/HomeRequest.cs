using Sheenam.Api.Models.Foundations.Guests;
using Sheenam.Api.Models.Foundations.Homes;

namespace Sheenam.Api.Models.Foundations.HomeRequests
{
    public class HomeRequest
    {
        public Guid Id { get; set; }
        public Guid GuestId { get; set; }
        public Guest Guest { get; set; }
        public Guid HomeId { get; set; }
        public Home Home { get; set; }
        public string Message { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }

    }
}
