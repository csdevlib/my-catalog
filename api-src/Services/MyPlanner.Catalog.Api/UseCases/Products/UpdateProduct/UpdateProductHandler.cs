using MyPlanner.Catalog.Api.Models;

namespace MyPlanner.Catalog.Api.UseCases.Products.UpdateProduct
{
    public class UpdateProductCommand(string tenantId, string producId, string name, List<string> category, string description, string imageFile, int currencyValue, double commertialValue) : AbstractCommand
    {
        public string TenantId { get; } = tenantId;
        public string ProducId { get; } = producId;
        public string Name { get; } = name;
        public List<string> Category { get; } = category;
        public string Description { get; } = description;
        public string ImageFile { get; } = imageFile;
        public int CurrencyValue { get; } = currencyValue;
        public double CommertialValue { get; } = commertialValue;
    }

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.TenantId).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Category).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.ImageFile).NotEmpty();
            RuleFor(x => x.CommertialValue).GreaterThan(0);
        }
    }
    public class UpdateProductCommandHandler : AbstractCommandHandler<UpdateProductCommand, ResultSet>
    {
        private readonly IDocumentSession documentSession;

        public UpdateProductCommandHandler(IDocumentSession documentSession, ILogger<UpdateProductCommandHandler> logger) : base(logger)
        {
            this.documentSession = documentSession ?? throw new ArgumentNullException(nameof(documentSession));
        }

        public override async Task<ResultSet> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await documentSession.LoadAsync<Product>(request.Id, cancellationToken);

            if (product == null)
            {
                return ResultSet.Error($"Product {request.Id} was not found");
            }

            product.TenantId = request.TenantId;
            product.Name = request.Name;
            product.Category = request.Category;
            product.Description = request.Description;
            product.ImageFile = request.ImageFile;
            product.CurrencyValue = request.CurrencyValue;
            product.CommercialValue = request.CommertialValue;

            documentSession.Update(product);

            await documentSession.SaveChangesAsync(cancellationToken);

            return ResultSet.Success("Product updated sucessfully");
        }
    }
}
