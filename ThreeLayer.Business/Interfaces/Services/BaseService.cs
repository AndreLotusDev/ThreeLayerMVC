using FluentValidation;
using FluentValidation.Results;
using ThreeLayer.Business.Interfaces;
using ThreeLayer.Business.Models;
using ThreeLayer.Business.Notifications;

namespace ThreeLayer.Business.Interfaces.Services
{
    public abstract class BaseService
    {
        private readonly INotifier _notifier;

        protected BaseService(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected void Notifier(ValidationResult validationResults)
        {
            foreach (var error in validationResults.Errors)
            {
                Notifier(error.ErrorMessage);
            }
        }

        protected void Notifier(string mensagem)
        {
            _notifier.Handle(new Notification(mensagem));
        }

        protected bool ExecuteValidation<TV, TE>(TV validation, TE entity) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validation.Validate(entity);

            if (validator.IsValid) return true;

            Notifier(validator);

            return false;
        }
    }
}
