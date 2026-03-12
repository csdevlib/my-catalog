using MyPlanner.Catalog.Api.Models;


namespace MyPlanner.Catalog.Api.UseCases.Products.GetProducts;

public class GetProductsQuery(int? PageNumber, int? PageSize, string TenantId) : AbstractQuery
{
    public int? PageNumber { get; } = PageNumber;
    public int? PageSize { get; } = PageSize;
    public string TenantId { get; } = TenantId;
}

public class GetProductsQueryHandler : AbstractQueryHandler<GetProductsQuery, ResultSet>
{
    private readonly IDocumentSession documentSession;
    private readonly ILogger<GetProductsQueryHandler> logger;

    public GetProductsQueryHandler(IDocumentSession documentSession, ILogger<GetProductsQueryHandler> logger) : base(logger)
    {
        this.documentSession = documentSession ?? throw new ArgumentNullException(nameof(documentSession));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public override async Task<ResultSet> HandleQuery(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var response = await documentSession.Query<Product>().Where(p => p.TenantId == query.TenantId).ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 10, cancellationToken);

        return ResultSet.Success(response);
    }
}