namespace PermissionsSpike
{
    public class PolicyResult
    {
        public PolicyResult()
        {
            Message = "Not Allowed";
        }

        public bool IsAllowed { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return string.Format("policy result : {0} | \"{1}\"", IsAllowed, Message);
        }
    }
}