using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartupRegistration.Startup.Exeptions
{
    public class ModelStateExeption : Exception
    {
        public ModelStateExeption(ModelStateDictionary modelStateDictionary)
        {
            ModelState = modelStateDictionary;
        }
        public ModelStateDictionary ModelState { get; set; }
    }
}
