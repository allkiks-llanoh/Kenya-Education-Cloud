using KEC.Voucher.Data.Models;

namespace KEC.Voucher.Web.Api.Models
{
    public class Batch
    {
        private readonly DbBatch _dbbatch;
        private readonly string _createVouchersUrl;
        private readonly string _getVouchersUrl;
        public Batch(DbBatch dbBatch)
        {
            _dbbatch = dbBatch;
        }
        public int Id
        {
            get
            {
                return _dbbatch.Id;
            }
        }
        public string CountyCode
        {
            get
            {
                return _dbbatch.County.CountyCode;
            }
        }
        public string CountyName
        {
            get
            {
                return _dbbatch.County.CountyName;
            }
        }
        public string BatchNumber
        {
            get
            {
                return _dbbatch.BatchNumber;
            }
        }

        public int Year
        {
            get
            {
                return _dbbatch.Year;
            }
        }
        public string SchoolType
        {
            get
            {
                return _dbbatch.SchoolType.SchoolType;
            }
        }
    }

}