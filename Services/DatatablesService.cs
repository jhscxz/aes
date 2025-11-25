using aes.Models.Datatables;
using aes.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aes.Services
{
    public class DatatablesService<TEntity> : IDatatablesService<TEntity> where TEntity : class
    {
        public JsonResult GetData(HttpRequest request, IEnumerable<TEntity> list,
            IDatatablesGenerator datatablesGenerator, Func<IEnumerable<TEntity>, DtParams, IEnumerable<TEntity>> dtData)
        {
            DtParams dTParams = datatablesGenerator.GetParams(request);

            IEnumerable<TEntity> dataList = list;

            if (!string.IsNullOrEmpty(dTParams.SearchValue))
            {
                dataList = dtData(dataList, dTParams);
            }

            List<TEntity> dataListForPaging = dataList.ToList();
            int filteredRows = dataListForPaging.Count;

            int totalRows = string.IsNullOrEmpty(dTParams.SearchValue) ? list.Count() : filteredRows;

            return datatablesGenerator.SortingPaging(dataListForPaging, dTParams, request, totalRows, filteredRows);
        }

    }


}