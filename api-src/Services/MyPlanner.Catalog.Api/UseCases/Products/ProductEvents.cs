using BeyondNet.Ddd;

namespace MyPlanner.Catalog.Api.UseCases.Products
{
    public class ProductEvents
    {
        public record ProductCreatedEvent(
            string ProductId,
            string CompanyId,
            string Name,
            List<string> Category,
            string Description,
            string ImageFile,
            decimal Price) : DomainEvent;

        public record ProductUpdatedEvent(
            string ProductId,
            string CompanyId,
            string Name,
            List<string> Category,
            string Description,
            string ImageFile,
            decimal Price) : DomainEvent;

        public record ProductDeletedEvent(string ProductId) : DomainEvent;
    }
}

