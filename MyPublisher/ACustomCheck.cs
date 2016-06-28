namespace MyPublisher
{
    using System;
    using System.Threading.Tasks;
    using ServiceControl.Plugin.CustomChecks;

    public class ACustomCheck : CustomCheck
    {
        public ACustomCheck()
        : base("Publisher-StartUp", "SomeCategory")
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