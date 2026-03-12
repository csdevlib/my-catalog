namespace MyPlanner.Catalog.Api.UseCases.Products.UpdateProduct
{
    public record UpdateProductRequest(string Name, List<string> Category, string Description, string ImageFile, int currencyValue, double commertialValue);

    public record UpdateProductResponse(bool IsSuccess);

    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products/{tenantid}/products/{id}", async (string tenantId, string id, [AsParameters] ProductServices services, [FromBody] UpdateProductRequest request) =>
            {
                var command = new UpdateProductCommand(tenantId, id, request.Name, request.Category, request.Description, request.ImageFile, request.currencyValue, request.commertialValue);

                var response = await services.Mediator.Send(command);

                return response.IsSuccess ? Results.Ok(response) : Results.NotFound(response);
            })
                .WithTags(ENDPOINT.Tag)
                .WithName(ENDPOINT.UPDATE.Name)
                .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
                .Produces<UpdateProductResponse>(StatusCodes.Status404NotFound)
                .ProducesValidationProblem()
                .WithSummary(ENDPOINT.UPDATE.Summary)
                .WithDescription(ENDPOINT.UPDATE.Description);
        }
    }
}
