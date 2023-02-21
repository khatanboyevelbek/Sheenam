using System.ComponentModel.DataAnnotations.Schema;
using Host = Sheenam.Api.Models.Foundations.Hosts.Host;

namespace Sheenam.Api.Models.Foundations.Homes
{
    public class Home
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string AdditionalInfo { get; set; }
        public bool IsVacant { get; set; }
        public double Area { get; set; }
        public bool IsPetAllowed { get; set; }
        public decimal Price { get; set; }
        public bool IsShared { get; set; }
        public Guid HostId { get; set; }
        public Host Host { get; set; }
    }
}
