using System;
using FluentValidation;
using FluentLocalization.Domain.Commands;
using FluentLocalization.Domain.Models;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.ComponentModel;
using System.Reflection;

namespace FluentLocalization.Domain.Commands
{
	public class UpdateUserValidator : AbstractValidator<User>
	{
		private readonly IStringLocalizerFactory _localizerFactory;

		public UpdateUserValidator(IStringLocalizerFactory localizerFactory)
		{
			_localizerFactory = localizerFactory;

			RuleFor(x => x.Id)
			.NotEmpty()
			.WithName(GetResource("Id"));
			RuleFor(x => x.Name)
			.NotEmpty()
			.WithName(GetResource("Name"));
			RuleFor(x => x.Gender)
			.NotEmpty()
			.Matches("^[MF]?$")
			.WithName(GetResource("Gender"));
			RuleFor(x => x.Age)
			.NotEmpty()
			.GreaterThan(0)
			.WithName(GetResource("Age"));

			RuleFor(x => x.Name)
			.Must(NameIsUnique)
			.WithName(GetResource("Name"))
			.WithMessage(GetResource("PropertyExists"));
        }

		public string GetResource(string resourceName)
		{
			var localizer = _localizerFactory.Create("Resources", typeof(User).Assembly.GetName().Name);
			return localizer[resourceName];
		}

        public bool NameIsUnique(User user, string name)
		{
			if (string.IsNullOrEmpty(user.Name))
				return true;

			if (user.Name == "string")
				return false;

			return true;
		}
	}
}

