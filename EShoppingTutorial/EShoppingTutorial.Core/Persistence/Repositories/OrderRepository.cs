using System;
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

        public EShoppingTutorialDbContext SadraDbContext
        {
            get { return Context as EShoppingTutorialDbContext; }
        }

        public override void Add(Order entity)
        {
            entity.TrackingNumber = Guid.NewGuid();

            base.Add(entity);
        }
    }
}
