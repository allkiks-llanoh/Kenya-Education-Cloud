using KEC.ECommerce.Data.Repositories;
using System;
namespace KEC.ECommerce.Data.UnitOfWork.Core
{
    public interface IUnitOfWork : IDisposable
    {
        PublicationsRepository PublicationsRepository { get; }
        PublishersRepository PublishersRepository { get; }
        LevelsRepository LevelsRepository { get; }
        AuthorsRepository AuthorsRepository { get; }
        SubjectsRepository SubjectsRepository { get; }
        CategoriesRepository CategoriesRepository { get; }
        ShoppingCartsRepository ShoppingCartsRepository { get; }
        ShoppingCartItemsRepository ShoppingCartItemsRepository { get; }
        OrdersRepository OrdersRepository { get; }
        LineItemsRepository LineItemsRepository { get; }
        LicencesRepository LicencesRepository { get;}
        PaymentsRepository PaymentsRepository { get; }
        PurchasedBookRepository PurchasedBookRepository { get; }
        int Complete();
    }
}
