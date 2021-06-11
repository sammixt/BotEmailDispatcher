using EmailQueue.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailQueue
{
    public class Application : IApplication
    {
        IBusinessLogic businessLogic;
        public Application(IBusinessLogic _businessLogic)
        {
            businessLogic = _businessLogic;
        }

        public async Task Run()
        {
            try
            {
                Console.WriteLine("Email Dispatcher Running");
                string hostname = await businessLogic.GetResourceName();

                Console.WriteLine($"Resource {hostname}");
                while (true)
                {
                    try
                    {
                        await businessLogic.ProcessData(hostname);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    
                    await Task.Delay(6000);
                }
                
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }
    }
}
