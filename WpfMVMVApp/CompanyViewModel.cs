using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfMVMVApp
{
    public class CompanyViewModel : INotifyPropertyChanged
    {
        Company company;

        public CompanyViewModel(Company company)
        {
            this.company = company;
        }

        public string Title
        {
            get => company.Title;
            set
            {
                company.Title = value;
                OnPropertyChanged();
            }
        }

        public int Id
        {
            get => company.Id;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            if(property is not null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
