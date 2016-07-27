using Topshelf;

namespace Dashboardify.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>                                 
            {
                x.Service<Service>(s =>                        
                {
                    s.ConstructUsing(name => new Service());   
                    s.WhenStarted(tc => tc.Start());             
                    s.WhenStopped(tc => tc.Stop());              
                });
                x.RunAsLocalSystem();                            

                x.SetDescription("Dashboardify Service");        
                x.SetDisplayName("Dashboardify.Service");                       
                x.SetServiceName("Dashboardify.Service");                       
            });
        }
    }
}