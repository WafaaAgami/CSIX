namespace CSIX.Data
{
    public class Activities : BaseModel
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeleteDate { get; set; }
        public string Description { get; set; }
        public virtual IList<Tasks> Tasks { get; set; }
    }
}
