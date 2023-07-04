using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CSIX.Data;

// Add profile data for application users by adding properties to the CSIXUser class
public class CSIXUser : IdentityUser
{
    public virtual IList<Tasks> Tasks { get; set; }
}

