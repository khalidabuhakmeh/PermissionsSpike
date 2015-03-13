using System;
using System.Linq;

namespace PermissionsSpike
{
    public class ViewUsersPolicy : Policy
    {
        public override bool IsMatch()
        {
            return Context.Actions.Contains("View", StringComparer.InvariantCultureIgnoreCase) &&
                   Context.Resources.Contains("Users", StringComparer.InvariantCultureIgnoreCase);
        }

        public override PolicyResult CheckAccess()
        {
            var result = new PolicyResult
            {
                IsAllowed = Context.User.IsInRole("admin")
            };

            if (result.IsAllowed)
                result.Message = "You are an admin, awesome!";

            return result;
        }
    }
}