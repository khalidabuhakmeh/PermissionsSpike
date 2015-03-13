using System;
using System.Security.Claims;

namespace PermissionsSpike
{
    class Program
    {
        static void Main(string[] args)
        {
            // Setup a User / Principal Factory
            Permissions.User = GetIdentity;

            // Add any number of policies
            Permissions
                .Policies
                .Add<ViewUsersPolicy>()
                .Add<EditUsersPolicy>();

            // check if the user has permission, via policies
            var result = Permissions.Check("View", "Users");

            Console.WriteLine(result);

            var yes = Permissions.Check("Edit", "Users", new User {Username = "khalidabuhakmeh"});
            Console.WriteLine(yes);

            var no = Permissions.Check("Edit", "Users", new User { Username = "John Doe" });
            Console.WriteLine(no);

            Console.ReadLine();
        }

        private static ClaimsPrincipal GetIdentity()
        {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, "khalidabuhakmeh"),
            }, "Cookie", ClaimTypes.Name, ClaimTypes.Role);

            // if you want roles, just add as many as you want here (for loop maybe?)
            identity.AddClaim(new Claim(ClaimTypes.Role, "guest"));
            identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));

            return new ClaimsPrincipal(identity);
        }
    }

    public class User
    {
        public string Username { get; set; }
    }
}
