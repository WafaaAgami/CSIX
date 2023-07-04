namespace CSIX.Data
{
    public class Tasks : BaseModel
    {
        public string Description { get; set; } = "";
        public int Status { get; set; }
        public DateTime Created_Date { get; set; }
        public string? UserId { get; set; }
        public CSIXUser? User { get; set; }
        public string? ActivityId { get; set; }
        public Activities? Activity { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DateDeleted { get; set; }
        public virtual IList<TaskHistory> TaskHistory { get; set; }
    }
}
