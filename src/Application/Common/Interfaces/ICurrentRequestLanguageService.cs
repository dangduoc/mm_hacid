using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Interfaces
{
    public interface ICurrentRequestLanguageService
    {
        public string Lang { get; }

        public bool IsDefault {get;}
    }
}
