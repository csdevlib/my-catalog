using MyPlanner.Catalog.Api.Models;


namespace MyPlanner.Catalog.Api.UseCases.Products.GetProductById
{
    public class GetProductByIdQuery(string tenantId, string productId) : AbstractQuery
    {
        public string TenantId { get; } = tenantId;
        public string ProductId { get; } = productId;
    }

    public class GetProductByIdQueryHandler : AbstractQueryHandler<GetProductByIdQuery, ResultSet>
    {
        private readonly IDocumentSession _documentSession;
        private readonly ILogger<GetProductByIdQueryHandler> logger;

        public GetProductByIdQueryHandler(IDocumentSession documentSession, ILogger<GetProductByIdQueryHandler> logger) : base(logger)
        {
            _documentSession = documentSession;
            this.logger = logger;
        }

        public async override Task<ResultSet> HandleQuery(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _documentSession.Query<Product>().FirstOrDefaultAsync(x => x.TenantId == request.TenantId && x.Id == request.ProductId, cancellationToken);

            if (response == null)
            {
                return ResultSet.Error($"Product with id {request.Id} not found");
            }

            return ResultSet.Success(response);
        }
    }
}
