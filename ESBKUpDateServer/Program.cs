using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using ESBKUpDateInterface;
using ESBKUpDateLib;

namespace ESBKUpDateServer
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(ESBKUpDateService)))
            {
                host.Opened += delegate
                {
                    Console.WriteLine("ESBKUpDateService已经启动！");
                };

                host.Open();
                while (true)
                {
                    object input =  Console.ReadLine();
                    if (input.ToString().ToUpper() == "-Q")
                        break;
                }
                
            }
        }
    }
}
