using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace aes.Models.Datatables
{
    public class DatatablesGenerator : IDatatablesGenerator
    {
        public DtParams GetParams(HttpRequest request)
        {
            IFormCollection form = request.Form;

            if (int.TryParse(form["start"].FirstOrDefault(), out int start) &&
                int.TryParse(form["length"].FirstOrDefault(), out int length) &&
                int.TryParse(form["order[0][column]"].FirstOrDefault(), out int sortColumnIndex))
            {
                return new DtParams
                {
                    Start = start,
                    Length = length,
                    SearchValue = form["search[value]"].FirstOrDefault(),
                    SortColumnName = form[$"columns[{sortColumnIndex}][name]"].FirstOrDefault(),
                    SortDirection = form["order[0][dir]"].FirstOrDefault(),
                };
            }

            // Handle invalid input gracefully, e.g., log and return a default value.
            // You can also throw an exception if appropriate for your application.
            return null;
        }

        public JsonResult SortingPaging<T>(IEnumerable<T> data, DtParams Params, HttpRequest request, int totalRows, int totalRowsAfterFiltering)
        {
            IQueryable<T> queryableData = data.AsQueryable();

            IOrderedQueryable<T> sortedData = ApplySorting(queryableData, Params);
            IQueryable<T> pagedData = ApplyPaging(sortedData, Params);

            int draw = int.TryParse(request.Form["draw"].FirstOrDefault(), out int drawInt) ? drawInt : 0;

            // Consider if you can avoid materializing the entire list here
            List<T> dataList = pagedData.ToList();

            return new JsonResult(new
            {
                data = dataList,
                draw,
                recordsTotal = totalRows,
                recordsFiltered = totalRowsAfterFiltering
            });
        }


        private static IOrderedQueryable<T> ApplySorting<T>(IQueryable<T> data, DtParams Params)
        {
            if (!string.IsNullOrEmpty(Params.SortColumnName))
            {
                string sortExpression = $"{Params.SortColumnName} {Params.SortDirection}";
                return data.OrderBy(sortExpression);
            }

            // If no sorting is specified, return the data as is.
            return data.OrderBy(e => 0); // Order by a constant to avoid errors.
        }


        private static IQueryable<T> ApplyPaging<T>(IQueryable<T> data, DtParams Params)
        {
            return data.Skip(Params.Start).Take(Params.Length);
        }
    }

}