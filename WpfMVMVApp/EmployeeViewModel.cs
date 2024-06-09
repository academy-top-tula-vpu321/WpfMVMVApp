using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfMVMVApp
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        Employee employee;

        public EmployeeViewModel(Employee employee)
        {
            this.employee = employee;
        }

        public string Name
        {
            get => employee.Name;
            set
            {
                employee.Name = value;
                OnPropertyChanged();
            }
        }

        public int Age
        {
            get => employee.Age;
            set
            {
                employee.Age = value;
                OnPropertyChanged();
            }
        }

        public Company Company
        {
            get => employee.Company;
            set
            {
                employee.Company = value;
                OnPropertyChanged();
            }
        }
        public int Id
        {
            get => employee.Id;
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            if (property is not null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
