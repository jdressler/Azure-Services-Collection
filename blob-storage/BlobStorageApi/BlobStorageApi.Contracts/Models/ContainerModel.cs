using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobStorageApi.Contracts.Models
{
    public class ContainerModel
    {

        public ContainerModel(string name, string lastModified)
        {
            Name = name;
            LastModified = lastModified;
        }

        public string Name { get; set; }
        public string LastModified { get; set; }

    }
}
