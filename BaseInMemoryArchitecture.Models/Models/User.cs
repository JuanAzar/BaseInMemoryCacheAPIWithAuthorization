using BaseInMemoryArchitecture.Common.Models;

namespace BaseInMemoryArchitecture.Models.Models
{
    public class User : Entity
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public override int GetId()
        {
            return UserId;
        }

        public override void SetId(int userId)
        {
            UserId = userId;
        }
    }
}
