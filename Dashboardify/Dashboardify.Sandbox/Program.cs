using System;
using Dashboardify.Models;
using Dashboardify.Repositories;
using System.Data;
using System.Dynamic;
using System.Runtime.InteropServices;

namespace Dashboardify.Sandbox
{
    class Program
    {
        private static Repositories _repositories;
        private static Handlers _handlers;


        static void Main(string[] args)
        {
            //_repositories = new Repositories();
            //_repositories.Do();

            _handlers = new Handlers();
            _handlers.Do();
        }
    }

}
