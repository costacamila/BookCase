using System;
using System.Collections.Generic;

namespace BookCase.Domain.User
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        // Reference List Accounts 
        public List<Domain.User.User> Users { get; set; }
    }
}