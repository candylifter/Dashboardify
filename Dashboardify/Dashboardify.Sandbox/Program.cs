using System;
using System.Threading.Tasks;


namespace Dashboardify.Sandbox
{
    class Program
    {

        static void Main(string[] args)
        {
            //var css = new CSS();

            //css.GetContentByCSS();
            
            var service = new Service();

            Task work = new Task(() =>
            {
                service.Do();
            });
            work.Start();

         
            Console.ReadKey();
        }
    }

}
