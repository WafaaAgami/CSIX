using CSIX.Data;

namespace CSIX.Model
{
    public class TaskModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public string StatusName
        {
            get
            {
                switch (Status)
                {
                    case 0:
                        return "New";
                    case 1:
                        return "In-Progress";
                    case 2:
                        return "Done";
                }
                return "Other";
            }
        }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? ActivityId { get; set; }
        public string? ActivityName { get; set; }
    }
}
