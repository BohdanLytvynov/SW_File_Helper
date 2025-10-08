using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW_File_Helper.ServiceWrappers
{
    public class ServiceWrapper
    {
        public IServiceProvider Services { get; }

        public ServiceWrapper(IServiceProvider serviceProvider)
        {
            if(serviceProvider == null)
                throw new ArgumentNullException(nameof(serviceProvider));
            Services = serviceProvider;
        }

        public T? GetRequiredService<T>()
        { 
            return Services.GetRequiredService<T>();
        }

        public T? GetService<T>()
        {
            return Services.GetService<T>();
        }
    }

    internal static class GlobalServiceWrapper
    { 
        public static IServiceProvider Services { get; set; }
    }
}
