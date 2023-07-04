namespace CSIX.Data
{
    public class TaskHistory : BaseModel
    {
        public string TaskId { get; set; }
        public Tasks Task { get; set; }
        public DateTime ChangeDate { get; set; }

        public string ChangeLookupId { get; set; }
        public ChangeLookup ChangeLookup { get; set; }

        public string ChangeText { get; set; }
    }
}
