using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseProjectWebRazor.Services
{
    public class CurrentRequestLanguageService: ICurrentRequestLanguageService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentRequestLanguageService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string Lang => _httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.TwoLetterISOLanguageName.ToLower();

        public bool IsDefault => IsDefaultLanguage();
        private bool IsDefaultLanguage()
        {
            return string.Equals(Lang, "vi", StringComparison.OrdinalIgnoreCase);
        }
    }
}
