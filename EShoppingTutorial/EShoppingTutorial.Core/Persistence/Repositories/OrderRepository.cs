using GenericRepositoryEntityFramework;

namespace EShoppingTutorial.Core.Persistence.Repositories;

public class OrderRepository(EShoppingTutorialDbContext context) : Repository<Order>(context), IOrderRepository
{
    public EShoppingTutorialDbContext EShoppingTutorialDbContext
    {
        get { return Context as EShoppingTutorialDbContext; }
    }

    public override void Add(Order entity)
    {
        // We can override repository virtual methods in order to customize repository behavior, Template Method Pattern
        // Code here

        base.Add(entity);
    }
}
