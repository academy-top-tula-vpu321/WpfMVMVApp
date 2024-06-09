using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.IO;

namespace WpfMVMVApp
{
    public interface IFileService
    {
        List<Employee> Open(string fileName);
        void Save(string fileName, List<Employee> employees);
    }

    public class JsonFileService : IFileService
    {
        public List<Employee> Open(string fileName)
        {
            List<Employee> employees = new();
            
            DataContractJsonSerializer jsonSerializer 
                = new DataContractJsonSerializer(typeof(List<Employee>));
            
            using(FileStream stream = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                employees = jsonSerializer.ReadObject(stream) as List<Employee>;
            }

            return employees;
        }

        public void Save(string fileName, List<Employee> employees)
        {
            DataContractJsonSerializer jsonSerializer 
                = new DataContractJsonSerializer(typeof(List<Employee>));

            using(FileStream stream = new FileStream(fileName, FileMode.Create))
            {
                jsonSerializer.WriteObject(stream, employees);
            }
        }
    }
}
