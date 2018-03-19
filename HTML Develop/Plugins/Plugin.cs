using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HTML_Develop;
namespace Plugins
{
    
    public interface Plugin
    {
        void run();
        String PluginName();
        
    }
}
