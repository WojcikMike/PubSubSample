namespace MyPublisher
{
    using System;
    using System.Threading.Tasks;
    using ServiceControl.Plugin.CustomChecks;

    public class APeriodicCheck : CustomCheck
    {
        public APeriodicCheck()
        : base("Publisher-Periodic", "SomeCategory", TimeSpan.FromSeconds(5))
        {

        }

        public override Task<CheckResult> PerformCheck()
        {
            if (DateTime.Now.Minute % 2 == 0)
            {
                return CheckResult.Pass;
            }
            else
            {
                return CheckResult.Failed("Some service is not available.");
            }
        }
    }
}