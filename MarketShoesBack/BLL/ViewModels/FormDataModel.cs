using DLL.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
    public class FormDataModel<T>
    {
        public string Entity { get; set; }
        public List<IFormFile> Photos { get; set; }
        public T GetEntity()
        {
            return JsonConvert.DeserializeObject<T>(Entity);

        }
    }
}
