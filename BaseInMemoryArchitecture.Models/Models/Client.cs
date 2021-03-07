using BaseInMemoryArchitecture.Common.Models;

namespace BaseInMemoryArchitecture.Models.Models
{
    public class Client : Entity
    {
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string CreditCardType { get; set; }
        public string CreditCardNumber { get; set; }
        public string Debt { get; set; }

        public override int GetId()
        {
            return ClientId;
        }

        public override void SetId(int clientId)
        {
            ClientId = clientId;
        }
    }
}
