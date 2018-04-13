using System;
using Sombra.Core.Enums;
using Sombra.Infrastructure.DAL;

namespace Sombra.UserService.DAL
{
    public class User : Entity
    {
        public Guid UserKey { get; set; }
        public UserType Type { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }

        public DateTime Created { get; set; }
    }
}