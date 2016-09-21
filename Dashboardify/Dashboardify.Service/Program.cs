using Topshelf;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace Dashboardify.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = log4net.LogManager.GetLogger("Dashboardify.Service");

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
                x.UseLog4Net();                      
            });

        }
    }
}