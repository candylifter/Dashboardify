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


        //TODO: Move only to response who use it to send this property to UI
        public string SessionId { get; set; }
    }
}
