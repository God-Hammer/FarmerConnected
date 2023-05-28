using System.ComponentModel.DataAnnotations;

namespace Data.Models.Requests.Put
{
    public class UpdateCustomerRequest
    {
        public string Name { get; set; }

        [Required, Phone]
        public string? Phone { get; set; }

        public string? Address { get; set; }


    }
}
