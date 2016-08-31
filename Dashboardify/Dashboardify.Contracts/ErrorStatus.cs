namespace Dashboardify.Contracts
{
    public class ErrorStatus
    {
        public string Code { get; set; }

        public ErrorStatus(string code)
        {
            Code = code;
        }
    }
}
