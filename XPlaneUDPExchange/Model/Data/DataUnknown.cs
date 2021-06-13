using XPlaneUDPExchange.Helpers;
namespace XPlaneUDPExchange.Model.Data
{
    public class DataUnknown : XPlaneData
    {
        #region PROPERTIES

        public float Value1 { get; set; }
        public float Value2 { get; set; }
        public float Value3 { get; set; }
        public float Value4 { get; set; }
        public float Value5 { get; set; }
        public float Value6 { get; set; }
        public float Value7 { get; set; }
        public float Value8 { get; set; }


        #endregion

        public DataUnknown()
        {
            this.DataGroup = Enum_DataGroup.Unknown;
            /*this.Value1 = values[0];
            this.Value2 = values[1];
            this.Value3 = values[2];
            this.Value4 = values[3];
            this.Value5 = values[4];
            this.Value6 = values[5];
            this.Value7 = values[6];
            this.Value8 = values[7];*/
        }
    }
}
