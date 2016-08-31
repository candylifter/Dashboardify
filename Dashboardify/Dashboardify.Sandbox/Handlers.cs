using Dashboardify.Handlers;

namespace Dashboardify.Sandbox
{
    public class Handlers
    {
        public void Do()
        {
            var updateItemHandler = new UpdateItemHandler();

            updateItemHandler.Handle();
        }
    }
}
