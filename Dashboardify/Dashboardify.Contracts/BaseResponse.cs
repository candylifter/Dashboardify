using System.Collections.Generic;

namespace Dashboardify.Contracts
{
    public class BaseResponse
    {
        public bool HasErrors
        {
            get { return Errors.Count > 0; }
        }

        public IList<ErrorStatus> Errors { get; set; }

        public string SessionId { get; set; }
    }
}
