using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfMVMVApp
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        Employee selectedEmployee;
        public ObservableCollection<Employee> Employees { get; set; }
        public ObservableCollection<Company> Companies { get; set; }

        IFileService fileService;
        IDialogService dialogService;

        public Employee SelectedEmployee
        {
            get => selectedEmployee;
            set
            {
                selectedEmployee = value;
                OnPropertyChanged();
            }
        }

        public ApplicationViewModel(IFileService fileService, IDialogService dialogService)
        {
            this.fileService = fileService;
            this.dialogService = dialogService;

            Companies = new()
            {
                new() { Title = "Yandex" },
                new() { Title = "Ozon" }
            };
            

            Employees = new()
            {
                new(){ Name = "Bobby", Age = 28, Company = Companies[0] },
                new(){ Name = "Sammy", Age = 19, Company = Companies[1] },
                new(){ Name = "Jimmy", Age = 32, Company = Companies[0] },
                new(){ Name = "Mikky", Age = 26, Company = Companies[1] },
            };
        }

        AppCommand addEmployeeCommand;
        public AppCommand AddEmployeeCommand
        {
            get
            {
                return addEmployeeCommand ??
                    (
                        addEmployeeCommand = new AppCommand
                        (
                            obj =>
                            {
                                Employee employee = new();
                                employee.Company = new();
                                
                                Employees.Insert(0, employee);
                                SelectedEmployee = employee;
                            }
                        )
                    );
            }
        }

        AppCommand removeEmployeeCommand;
        public AppCommand RemoveEmployeeCommand
        {
            get
            {
                return removeEmployeeCommand ??
                    (
                        removeEmployeeCommand = new AppCommand
                        (
                            obj =>
                            {
                                if(obj is Employee employee)
                                {
                                    Employees.Remove(employee);
                                }
                            },
                            obj => Employees.Count > 0
                        )
                    );
            }
        }

        AppCommand copyEmployeeCommand;
        public AppCommand CopyEmployeeCommand
        {
            get
            {
                return copyEmployeeCommand ??
                    (
                        copyEmployeeCommand = new AppCommand
                        (
                            obj =>
                            {
                                if(obj is Employee employee)
                                {
                                    Employee employeeCopy = new() 
                                    { 
                                        Name = employee.Name,
                                        Age = employee.Age,
                                        Company = employee.Company
                                    };
                                    Employees.Insert(Employees.IndexOf(employee), employeeCopy);
                                }
                            },
                            obj => Employees.Count > 0
                        )
                    );
            }
        }

        AppCommand openCommand;
        public AppCommand OpenCommand
        {
            get
            {
                return openCommand ?? 
                    (openCommand = new(
                        obj =>
                        {
                            try
                            {
                                if(dialogService.OpenFileDialog())
                                {
                                    var employees = fileService.Open(dialogService.FilePath);
                                    Employees.Clear();
                                    foreach ( Employee employee in employees )
                                    {
                                        Employees.Add(employee);
                                    }
                                    dialogService.ShowMessage("Employees is loading!");
                                }
                            }
                            catch(Exception ex)
                            {
                                dialogService.ShowMessage(ex.Message);
                            }
                        }
                    )
                );
            }
        }

        AppCommand saveCommand;
        public AppCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                    (
                        saveCommand = new(
                            obj =>
                            {
                                try
                                {
                                    if (dialogService.SaveFileDialog() == true)
                                    {
                                        fileService.Save(dialogService.FilePath, Employees.ToList());
                                        dialogService.ShowMessage("Employees save to file");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    dialogService.ShowMessage(ex.Message);
                                }
                            }
                            )
                    );
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            if (property is not null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
