﻿using KEC.ECommerce.Data.UnitOfWork.Core;
using KEC.ECommerce.Web.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KEC.ECommerce.Web.UI.ViewComponents
{
    public class VerticalMenuViewComponent:ViewComponent
    {
        private readonly IUnitOfWork _uow;

        public VerticalMenuViewComponent(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public IViewComponentResult Invoke()
        {
            var categories = _uow.CategoriesRepository.GetAll();
            var categoryList = categories.Any() ? categories.Select(p => new CategoryViewModel(_uow,p)) : new List<CategoryViewModel>();
            return View(categoryList);
        }
    }
}
