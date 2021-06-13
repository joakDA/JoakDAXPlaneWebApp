using XPlaneUDPExchange.Helpers;

namespace XPlaneUDPExchange.Model.Data
{
    public abstract class XPlaneData
    {
        protected Enum_DataGroup DataGroup;

        /// <summary>
        /// Get the type of data stored (DATA REF type).
        /// </summary>
        /// <returns></returns>
        public virtual Enum_DataGroup GetGroup()
        {
            return DataGroup;
        }
    }
}
