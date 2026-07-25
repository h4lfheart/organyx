using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Organyx.Development;

/// <summary>
/// Prepends <paramref name="routePrefix"/> to every controller route in <paramref name="assembly"/>.
/// Controllers without a class-level route get the prefix as their route template so action routes combine under it.
/// </summary>
public sealed class AssemblyRoutePrefixConvention(Assembly assembly, string routePrefix) : IApplicationModelConvention
{
    public void Apply(ApplicationModel application)
    {
        var prefix = new AttributeRouteModel(new RouteAttribute(routePrefix));

        foreach (var controller in application.Controllers)
        {
            if (controller.ControllerType.Assembly != assembly)
                continue;

            foreach (var selector in controller.Selectors)
            {
                selector.AttributeRouteModel = selector.AttributeRouteModel is null
                    ? prefix
                    : AttributeRouteModel.CombineAttributeRouteModel(prefix, selector.AttributeRouteModel);
            }
        }
    }
}
