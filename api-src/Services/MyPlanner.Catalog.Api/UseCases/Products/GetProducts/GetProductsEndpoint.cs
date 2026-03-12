using MyPlanner.Catalog.Api.Models;

namespace MyPlanner.Catalog.Api.UseCases.Products.GetProducts
{
    public record GetProductsRequest(int? PageNumber, int? PageSize, string CompanyId);
    public record GetProductResponse(IEnumerable<Product> Products);
    public class GetProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{tenantid}", async (string tenantid, [FromBody] GetProductsRequest getProductsRequest, [AsParameters] ProductServices services) =>
            {
                var query = new GetProductsQuery(getProductsRequest.PageNumber, getProductsRequest.PageSize, tenantid);

                var result = await services.Mediator.Send(query);

                return Results.Ok(result);
            })
                .WithTags(ENDPOINT.Tag)
                .WithName(ENDPOINT.LIST.Name)
                .Produces<IEnumerable<Product>>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary(ENDPOINT.LIST.Summary)
                .WithDescription(ENDPOINT.LIST.Description);
        }
    }
}
