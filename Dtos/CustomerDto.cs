using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibApp.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public bool HasNewsletterSubscribed { get; set; }
        public MembershipTypeDto MembershipType { get; set; }
        [Display(Name = "Membership Type")]
        public byte? MembershipTypeId { get; set; }
        [Display(Name = "Date of Birth")]
        public DateTime? Birthdate { get; set; }
    }
}
