using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseProjectWebRazor.Models
{
    public class CustomCultureRouteRouteModelConvention : IPageRouteModelConvention
    {
        public void Apply(PageRouteModel model)
        {
            List<SelectorModel> selectorModels = new List<SelectorModel>();
            foreach (var selector in model.Selectors.ToList())
            {
                var template = selector.AttributeRouteModel.Template;
                selectorModels.Add(new SelectorModel()
                {
                    AttributeRouteModel = new AttributeRouteModel
                    {
                        Template = "/{culture}" + "/" + template
                    }
                });
            }
            foreach (var m in selectorModels)
            {
                model.Selectors.Add(m);
            }
        }
    }
}
