using FluentValidation;

namespace ManagementInventory.Application.Features.Inventory.Command.DeleteItemByName
{
    /// <summary>
    /// Class to validate object type <see cref="DeleteItemByNameCommand"/>
    /// </summary>
    public class DeleteItemByNameCommandValidator : AbstractValidator<DeleteItemByNameCommand>
    {
        /// <summary>
        /// Constructor of the class where the validation conditions are indicated
        /// </summary>
        public DeleteItemByNameCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El campo {Name} no puede estar vacío")
                .NotNull().WithMessage("El campo {Name} no puede ser NULL")
                .MaximumLength(100).WithMessage("El campo {Name} supera el tamaño máximo permitido de 100 caracteres");
        }
    }
}