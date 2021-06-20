using System;
using System.Collections.Generic;

namespace JoakDAXPWebApp.Models.DataTable
{
    public class DataTableResponseModel<T>
    {
        /// <summary>
        /// Data to display on datable.
        /// </summary>
        public IEnumerable<T> Data { get; set; }
        /// <summary>
        /// Number of records displayed on each page.
        /// </summary>
        public int Draw { get; set; }
        /// <summary>
        /// Number of records filtered on query.
        /// </summary>
        public int RecordsFiltered { get; set; }
        /// <summary>
        /// Number of total records on database.
        /// </summary>
        public int RecordsTotal { get; set; }

        public DataTableResponseModel(IEnumerable<T> data, int draw, int recordsFiltered, int recordsTotal)
        {
            this.Data = data;
            this.Draw = draw;
            this.RecordsFiltered = recordsFiltered;
            this.RecordsTotal = recordsTotal;
        }
    }
}
