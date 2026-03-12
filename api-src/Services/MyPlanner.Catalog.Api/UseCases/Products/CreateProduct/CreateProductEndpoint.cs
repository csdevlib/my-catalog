namespace MyPlanner.Catalog.Api.UseCases.Products.CreateProduct
{
    public record CreateProductRequest(string tenantId, string Name, List<string> Category, string Description, string ImageFile, int currencyValue, double commertialValue);
    public record class CreateProductResponse(string Id);
    public class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products", async (string tenantId, [AsParameters] ProductServices services, [FromBody] CreateProductRequest createProductRequest) =>
            {
                var command = createProductRequest.Adapt<CreateProductCommand>();
                command.TenantId = tenantId;
                command.Status = 1;

                var response = await services.Mediator.Send(command);

                return Results.Ok(response);

            })
                .WithTags(ENDPOINT.Tag)
                .WithName(ENDPOINT.CREATE.Name)
                .Produces<CreateProductResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary(ENDPOINT.CREATE.Summary)
                .WithDescription(ENDPOINT.CREATE.Description);
        }
    }
}
