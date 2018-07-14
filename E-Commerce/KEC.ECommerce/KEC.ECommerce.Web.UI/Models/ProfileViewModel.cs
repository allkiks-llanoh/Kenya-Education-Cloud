namespace KEC.ECommerce.Web.UI.Models
{
    public class ProfileViewModel
    {
        public ProfileViewModel(string code, string fullName, string emailAddress)
        {
            EmailAddress = emailAddress;
            FullName = fullName;
            IdenficationCode = code;
        }
        public string EmailAddress { get; private set; }
        public string FullName { get; private set; }
        public string IdenficationCode { get; private set; }
    }
}
