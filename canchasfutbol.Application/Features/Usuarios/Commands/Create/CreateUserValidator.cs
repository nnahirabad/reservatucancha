using FluentValidation;


namespace canchasfutbol.Application.Features.Usuarios.Commands.Create
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator() {

            RuleFor(u => u.Email)
                  .NotEmpty().WithMessage("Email no puede estar vacio")
                  .EmailAddress().WithMessage("El email debe tener un formato valido");

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("La contrasena no puede estar vacia")
                .MinimumLength(6).WithMessage("Debe contener al menos 6 caracteres");

            RuleFor(u => u.Username)
                .NotEmpty().WithMessage("Debes ingresar un username");

            RuleFor(u => u.Rol)
                .Must(rol => rol == "Admin" || rol == "Usuario")
                .WithMessage("El rol ingresado no es valido"); 


        
        
        
        
        }
    }
}
