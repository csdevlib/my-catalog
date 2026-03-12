using MyPlanner.Catalog.Api.Models;

namespace MyPlanner.Catalog.Api.UseCases.Products.GetProductById
{
    public record GetProductByIdResponse(Product product);

    public class GetProductIdByQueryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{tenantid}/{id}", async (string tenantId, string id, [AsParameters] ProductServices service) =>
            {
                var query = new GetProductByIdQuery(tenantId, id);

                var response = await service.Mediator.Send(query);

                return Results.Ok(response);
            })
                .WithTags(ENDPOINT.Tag)
                .WithName(ENDPOINT.GET.Name)
                .Produces<Product>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary(ENDPOINT.GET.Summary)
                .WithDescription(ENDPOINT.GET.Description);
        }
    }
}
