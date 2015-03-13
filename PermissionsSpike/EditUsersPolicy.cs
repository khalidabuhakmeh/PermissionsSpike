using System;
using System.Linq;

namespace PermissionsSpike
{
    public class EditUsersPolicy : Policy<User>
    {
        public override bool IsMatch()
        {
            return Context.Actions.Contains("Edit", StringComparer.InvariantCultureIgnoreCase) &&
                   Context.Resources.Contains("Users", StringComparer.InvariantCultureIgnoreCase);
        }

        public override PolicyResult CheckAccess(User target)
        {
            var result = new PolicyResult
            {
                IsAllowed =  target.Username == Context.User.Identity.Name
            };

            if (result.IsAllowed)
                result.Message = " this is your record awesome!";

            return result;
        }
    }
}