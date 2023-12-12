namespace Creations.Domain.Entities;
public class Customer  : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool PrivacyPolicyAccepted { get; set; }
}
