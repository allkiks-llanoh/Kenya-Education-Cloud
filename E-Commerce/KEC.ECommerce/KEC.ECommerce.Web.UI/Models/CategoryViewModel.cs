using KEC.ECommerce.Data.Models;
using KEC.ECommerce.Data.UnitOfWork.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KEC.ECommerce.Web.UI.Models
{
    public class CategoryViewModel
    {
        private readonly Category _category;

        public CategoryViewModel(Category category)
        {
            _category = category;
        }
        public string Name
        {
            get
            {
                return _category.Name;
            }
        }
        public int Id
        {
            get
            {
                return _category.Id;
            }
        }
    }
}
