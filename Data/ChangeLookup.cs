using System.Threading.Tasks;

namespace CSIX.Data
{
    public class ChangeLookup : BaseModel
    {
        public virtual IList<TaskHistory> TaskHistory { get; set; }
    }
}
