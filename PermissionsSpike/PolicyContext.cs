using System.Collections.Generic;
using System.Security.Claims;

namespace PermissionsSpike
{
    public class PolicyContext
    {
        public PolicyContext()
        {
            Actions = new List<string>();
            Resources = new List<string>();
            User = new ClaimsPrincipal(new ClaimsIdentity());
        }

        public ClaimsPrincipal User { get; set; }
        public ICollection<string> Actions { get; set; }
        public ICollection<string> Resources { get; set; } 
    }
}