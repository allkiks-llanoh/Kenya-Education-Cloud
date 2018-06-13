using KEC.ECommerce.Data.Models;
using KEC.ECommerce.Data.UnitOfWork.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KEC.ECommerce.Web.UI.Models
{
    public class OrderViewModel
    {
        private readonly IUnitOfWork _uow;
        private readonly Order _order;
        private readonly bool _includeItems;

        public OrderViewModel(IUnitOfWork uow, Order order, bool includeItems = false)
        {
            _uow = uow;
            _order = order;
            _includeItems = includeItems;
        }
        public long ItemsCount
        {
            get
            {
                return _uow.OrdersRepository
                          .GetShoppingLineItemsCount(_order.Id);
            }
        }

        public List<LineItemViewModel> Items
        {
            get
            {
                List<LineItemViewModel> itemsList = default(List<LineItemViewModel>);
                if (_includeItems)
                {
                    var items = _uow.OrdersRepository.GetLineItems(_order.Id);
                    itemsList = items.Any() ?
                        items.Select(p => new LineItemViewModel(_uow, p)).ToList() : new List<LineItemViewModel>();

                }
                return itemsList;
            }
        }
        public int Id
        {
            get
            {
                return _order.Id;
            }
        }
        public string TotalCost
        {
            get
            {
                return string.Format("KES {0:#.00}", _uow.OrdersRepository.GetOrderTotalCost(_order.Id));
            }
        }
        public string OrderNumber
        {
            get
            {
                return _order.OrderNumber;
            }
        }
        public string StepperClass(int step)
        {
            var strClass = default(string);
            if ((int)_order.Status >= step)
            {
                strClass = "completed step";
            }
            else
            {
                strClass = "active step";
            }
            return strClass;
        }
        public OrderStatus Status
        {
            get
            {
                return _order.Status;
            }
        }
    }
}
