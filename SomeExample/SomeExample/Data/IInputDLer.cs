using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeExample.Data
{
    public interface IInputDLer
    {
        Task<string> DownloadPostingsAsync();
    }
}
