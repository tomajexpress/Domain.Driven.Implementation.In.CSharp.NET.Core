using GenericRepositoryEntityFramework;
using EShoppingTutorial.Core.Domain.Entities;
using EShoppingTutorial.Core.Domain.Repositories;


namespace EShoppingTutorial.Core.Persistence.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(EShoppingTutorialDbContext context) : base(context)
        {
            
        }

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
}
