using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamenIIIParcial.Models;
using ExamenIIIParcial.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExamenIIIParcial.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmployesPage : ContentPage
    {
        EmployeesViewModel employeeViewModel;
        public EmployesPage(Pagos employee)
        {
            InitializeComponent();
            employeeViewModel = new EmployeesViewModel(this, employee);
            BindingContext = employeeViewModel;
            datePicker.SetBinding(DatePicker.DateProperty, "FechaSelected", BindingMode.TwoWay);
        }
    }
}