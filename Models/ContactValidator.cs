using Core;
using FluentValidation;

namespace SampleApplication
{
    public class ContactValidator : ModelValidatorBase<Contact>
    {
        public ContactValidator()
        {
            RuleFor(item => item.Name).NotEmpty().WithMessage("Please provide a value for Name");
        }
    }
}