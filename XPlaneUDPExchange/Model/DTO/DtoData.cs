using System;
using System.Collections.Generic;
using System.Text;
using XPlaneUDPExchange.Helpers;

namespace XPlaneUDPExchange.Model.DTO
{
    public abstract class DtoData
    {
        protected Enum_DataGroup DataType;

        /// <summary>
        /// Get the name of the data type (DATA REF type).
        /// </summary>
        /// <returns></returns>
        public virtual Enum_DataGroup GetDataType()
        {
            return DataType;
        }
    }
}
