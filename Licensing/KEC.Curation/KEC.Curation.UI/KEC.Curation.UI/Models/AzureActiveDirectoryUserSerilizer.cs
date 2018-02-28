using KEC.Curation.UI.Models;

namespace KEC.Curation.Web.Api.Serializers
{
   

    public class AzureActiveDirectoryUserSerilizer
    {
         private readonly ActiveDirectoryUser _adUser;
        public AzureActiveDirectoryUserSerilizer(ActiveDirectoryUser adUser)
        {

            _adUser = adUser;
        }
        //public string Id
        //{
        //    get
        //    {
        //        return _adUser.Id;
        //    }
        //}
        //public string UserPrincipalName
        //{
        //    get
        //    {
        //        return _adUser.UserPrincipalName;
        //    }
        //}
        //public string DisplayName
        //{
        //    get
        //    {
        //        return _adUser.DisplayName;
        //    }
        //}
        //public string GivenName
        //{
        //    get
        //    {
        //        return _adUser.GivenName;
        //    }
        //}


        //public string Mail
        //{
        //    get
        //    {
        //        return _adUser.Mail;
        //    }
        //}
        //public string MobilePhone
        //{
        //    get
        //    {
        //        return _adUser.MobilePhone;
        //    }
        //}
        //public string Surname
        //{
        //    get
        //    {
        //        return _adUser.Surname;
        //    }
        //}

    }
}
