using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMVMVApp
{
    public class Employee
    {
        static int globalId = 0;

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Age { get; set; }
        public Company? Company { get; set; }

        public Employee()
        {
            Id = ++globalId;
        }
    }

    public class Company
    {
        static int globalId = 0;

        public int Id { get; set; }
        public string Title { get; set; } = null!;

        public Company()
        {
            Id = ++globalId;
        }
    }
}
