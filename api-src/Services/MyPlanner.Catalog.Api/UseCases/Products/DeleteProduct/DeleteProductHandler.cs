using MyPlanner.Catalog.Api.Models;

namespace MyPlanner.Catalog.Api.UseCases.Products.DeleteProduct
{
    public class DeleteProductCommand(string tenantId, string productId) : AbstractCommand
    {
        public string TenantId { get; } = tenantId;
        public string ProductId { get; } = productId;
    }

    public class DeleteProductCommandHandler : AbstractCommandHandler<DeleteProductCommand, ResultSet>
    {
        private readonly IDocumentSession documentSession;
        private readonly ILogger<DeleteProductCommandHandler> logger;

        public DeleteProductCommandHandler(IDocumentSession documentSession, ILogger<DeleteProductCommandHandler> logger) : base(logger)
        {
            this.documentSession = documentSession ?? throw new ArgumentNullException(nameof(documentSession));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override async Task<ResultSet> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await documentSession.LoadAsync<Product>(request.Id, cancellationToken);

            if (product == null)
            {
                return ResultSet.Error($"Product with id {request.Id} not found", JsonSerializer.Serialize(product));
            }

            documentSession.Delete(product);

            await documentSession.SaveChangesAsync(cancellationToken);

            return ResultSet.Success();
        }
    }
}
