namespace WebApplication1.Models
{
    public class OverTimeApplyModel
    {
        public OverTimeApplyDataModel[] OverTimeApplyData { get; set; }
    }

    public class OverTimeApplyDataModel
    {
        public string sn { get; set; }
        public string applyID { get; set; }
        public string applyDeptID { get; set; }
        public string employeeID { get; set; }
        public string employeeName { get; set; }
        public string departmentID { get; set; }
        public string startDateTime { get; set; }
        public string endDateTime { get; set; }
        public string supportDeptID_FK { get; set; }
        public string supportDeptName { get; set; }
        public string note { get; set; }
        public string payTypeKey { get; set; }
        public string payTypeValue { get; set; }
        public string mealOrderKey { get; set; }
        public string mealOrderValue { get; set; }
        public string nationalType { get; set; }
        public string departmentName { get; set; }
        public string currentSignLevelDeptID { get; set; }
        public string currentSignLevelDeptName { get; set; }
        public string signDocID { get; set; }
        public string formSeries { get; set; }
    }
}