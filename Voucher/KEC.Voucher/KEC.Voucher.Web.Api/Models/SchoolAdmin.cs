using KEC.Voucher.Data.Models;

namespace KEC.Voucher.Web.Api.Models
{
    public class SchoolAdmin
    {
        private readonly DbSchoolAdmin _dbSchoolAdmin;

        public SchoolAdmin(DbSchoolAdmin dbSchoolAdmin)
        {
            _dbSchoolAdmin = dbSchoolAdmin;
        }
        public int Id
        {
            get
            {
                return _dbSchoolAdmin.Id;
            }
        }
        public string FirstName
        {
            get
            {
                return _dbSchoolAdmin.FirstName;
            }
        }
        public string LastName
        {
            get
            {
                return _dbSchoolAdmin.LastName;
            }
        }
        public string PhoneNumber
        {
            get
            {
                return _dbSchoolAdmin.PhoneNumber;
            }
        }
        public string Email
        {
            get
            {
                return _dbSchoolAdmin.Email;
            }
        }
        public string guid
        {
            get
            {
                return _dbSchoolAdmin.guid;
            }
        }
    }
}