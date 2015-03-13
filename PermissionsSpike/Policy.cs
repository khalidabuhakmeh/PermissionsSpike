namespace PermissionsSpike
{
    public abstract class Policy
    {
        public PolicyContext Context { get; set; }

        public abstract bool IsMatch();
        public abstract PolicyResult CheckAccess();
    }

    public abstract class Policy<T> : Policy
    {
        public override PolicyResult CheckAccess()
        {
            return CheckAccess(default(T));
        }

        public abstract PolicyResult CheckAccess(T target);
    }
}