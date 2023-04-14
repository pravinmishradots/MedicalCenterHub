using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;
namespace ChildCareInfra.Helper
{
    public class SortHelper<T>
    {
        public static IQueryable<T> ApplySort(IQueryable<T> source, string? OrderBy)
        {
            if (!source.Any())
            {
                return source;
            }
            if (string.IsNullOrWhiteSpace(OrderBy))
            {
                return source;
            }

            var orderQueryBuilder = new StringBuilder();

            var orderParam = OrderBy.Trim().Split(',');
            var propertyInfo = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var param in orderParam)
            {
                if (string.IsNullOrWhiteSpace(param))
                {
                    continue;
                }
                var paramPropertyNam = param.Split(" ")[0];
                var objectProperty = propertyInfo.FirstOrDefault(x => x.Name.Equals(paramPropertyNam, StringComparison.InvariantCultureIgnoreCase));
                if (objectProperty == null)
                {
                    continue;
                }
                var sortingOrder = param.EndsWith(" desc") ? "descending" : "ascending";
                orderQueryBuilder.Append($"{objectProperty!.Name} {sortingOrder}, ");
                var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
                if (string.IsNullOrWhiteSpace(orderQuery))
                {
                    return source;
                }
                source = source.OrderBy(orderQuery);
            }
            return source;
        }

    }
}
