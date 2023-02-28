using System;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;

namespace FluentLocalization
{
	public static class Extensions
	{
		public static IServiceCollection ConfigureLocalization(this IServiceCollection services)
		{
            services.AddLocalization(options => options.ResourcesPath = "Resources");
			services.Configure<RequestLocalizationOptions>(options =>
			{
				var supporterdCultures = new[]
				{
					new CultureInfo("en-US"),
					new CultureInfo("pt-BR")
				};

				options.DefaultRequestCulture = new RequestCulture(culture: "pt-BR", uiCulture: "pt-BR");
				options.SupportedCultures = supporterdCultures;
				options.SupportedUICultures = supporterdCultures;
			});
			return services;
		}
    }
}

