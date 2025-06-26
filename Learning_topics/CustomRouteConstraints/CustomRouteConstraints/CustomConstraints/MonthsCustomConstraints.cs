namespace CustomRouteConstraints.CustomRouteConstraints;

public class MonthsCustomConstraints:IRouteConstraint{
    public bool Match(HttpContext? httpContext, IRouter? route, string routeKey,
                    RouteValueDictionary values, RouteDirection routeDirection)
    {
        if (!values.TryGetValue(routeKey, out var value) || value == null)
            return false;

        var stringValue = value.ToString();
        return stringValue == stringValue.ToUpperInvariant();
    }
}