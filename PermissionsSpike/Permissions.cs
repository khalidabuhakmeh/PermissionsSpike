using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace PermissionsSpike
{
    public static class Permissions
    {
        public static Func<ClaimsPrincipal> User { get; set; }
        public static Lazy<IList<Type>> PolicyTypes = new Lazy<IList<Type>>(() => new List<Type>());

        public static PolicyWrapper Policies
        {
            get { return new PolicyWrapper(PolicyTypes.Value); }
        }

        public class PolicyWrapper
        {
            private readonly ICollection<Type> _policyTypes;

            public PolicyWrapper(ICollection<Type> policyTypes)
            {
                _policyTypes = policyTypes;
            }

            public PolicyWrapper Add<T>()
                where T : Policy
            {
                _policyTypes.Add(typeof(T));
                return this;
            }
        }

        public static PolicyResult Check(string action, string resource)
        {
            var context = new PolicyContext
            {
                Actions = {action},
                Resources = {resource},
                User = User()
            };

            var policy = PolicyTypes
                .Value
                .Select(Activator.CreateInstance)
                .Cast<Policy>()
                .Select(x => { x.Context = context; return x; }  )
                .FirstOrDefault(x => x.IsMatch());

            // safe by default
            return policy == null ? new PolicyResult() : policy.CheckAccess();
        }

        public static PolicyResult Check<T>(string action, string resource, T target)
        {
            var context = new PolicyContext
            {
                Actions = { action },
                Resources = { resource },
                User = User()
            };

            var policy = PolicyTypes
                .Value
                .Select(Activator.CreateInstance)
                .Cast<Policy>()
                .Where(x => x != null)
                .Select(x => { x.Context = context; return x; })
                .FirstOrDefault(x => x.IsMatch());

            // safe by default
            if(policy == null) 
                return new PolicyResult();
            
            var targetPolicy = policy as Policy<T>;

            return targetPolicy != null ? targetPolicy.CheckAccess(target) : policy.CheckAccess();
        }
    }
}