using System;

namespace API.RequestModels.Accounts
{
    public class UpdateAccount
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
    }
}
