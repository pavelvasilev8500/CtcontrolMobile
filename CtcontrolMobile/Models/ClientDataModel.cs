namespace CtcontrolAPIService.Services
{
    public class ClientDataModel
    {
        public string Id { get; set; }
        public int DateNumber { get; set; }
        public string DateMonth { get; set; }
        public int DateYear { get; set; }
        public string Time { get; set; }
        public string Day { get; set; }
        public int WorktimeDay { get; set; }
        public int WorktimeHour { get; set; }
        public int WorktimeMinute { get; set;}
        public int WorktimeSecond { get; set;} 
        public int? Batary { get; set; }
        public int CpuTemperature { get; set; }
        public int? GpuTemperature { get; set; }
    }
}