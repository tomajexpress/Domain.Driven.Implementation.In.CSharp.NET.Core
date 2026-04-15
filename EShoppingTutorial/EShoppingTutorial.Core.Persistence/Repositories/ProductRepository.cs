using GenericRepository.EntityFramework;

namespace EShoppingTutorial.Core.Persistence.Repositories;

internal class ProductRepository(EShoppingTutorialDbContext context) : GenericRepository<Product>(context), IProductRepository
{
    public EShoppingTutorialDbContext? EShoppingTutorialDbContext
    {
        get { return Context as EShoppingTutorialDbContext; }
    }
}
