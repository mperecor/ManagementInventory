using FluentValidation;

namespace ManagementInventory.Application.Features.Inventory.Command.AddItem
{
    /// <summary>
    /// Class to validate object type <see cref="AddItemCommand"/>
    /// </summary>
    public class AddItemCommandValidator : AbstractValidator<AddItemCommand>
    {
        /// <summary>
        /// Constructor of the class where the validation conditions are indicated
        /// </summary>
        public AddItemCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El campo {Name} no puede estar vacío")
                .NotNull().WithMessage("El campo {Name} no puede ser NULL")
                .MaximumLength(100).WithMessage("El campo {Name} supera el tamaño máximo permitido de 100 caracteres");
        }
    }
}