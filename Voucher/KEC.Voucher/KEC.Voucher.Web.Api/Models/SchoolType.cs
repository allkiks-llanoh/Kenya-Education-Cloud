using KEC.Voucher.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace KEC.Voucher.Web.Api.Models
{
    public class SchoolType
    {
        private readonly DbSchoolType _dbSchoolType;

        public SchoolType(DbSchoolType dbSchoolType)
        {
            _dbSchoolType = dbSchoolType;
        }
        public int Id
        {
            get
            {
                return _dbSchoolType.Id;
            }
        }
        public string description
        {
            get
            {
                return _dbSchoolType.SchoolType;
            }
        }
        public ICollection<School> Schools
        {
            get
            {
                return _dbSchoolType.Schools.Select(p => new School(p)).ToList();
            }
        }
    }
}