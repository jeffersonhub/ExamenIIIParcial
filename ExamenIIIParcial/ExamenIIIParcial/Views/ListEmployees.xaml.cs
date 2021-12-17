using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamenIIIParcial.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExamenIIIParcial.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListEmployees : ContentPage
    {
        ListEmployeesViewModel EmployeesListViewModel;
        public ListEmployees()
        {
            InitializeComponent();
            EmployeesListViewModel = new ListEmployeesViewModel(this);
            BindingContext = EmployeesListViewModel;
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            EmployeesListViewModel.Load();
        }
    }
}