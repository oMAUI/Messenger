﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace HostMoraMsgr
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(ServiceMessenger.ServiceChat)))
            {
                host.Open();
                Console.WriteLine("OK!");
                Console.Read();
            }
            ServiceHost
        }
    }
}
