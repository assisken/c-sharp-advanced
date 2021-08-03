using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DepartmentApp
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<Department> _departments;
        private ObservableCollection<Employee> _employees;
        private EditEmployeeWindow _editEmployeeWindow;

        public MainWindow()
        {
            InitializeComponent();
            FillList();
        }

        private void UpdateLbEmployee(int selectedId) =>
            lbEmployee.ItemsSource = _employees.Select(employee => employee)
                .Where(employee => employee.DepartmentId == selectedId);

        private void FillList()
        {
            _departments = new ObservableCollection<Department>
            {
                new Department {Id = 1, Name = "Fishing Co."},
                new Department {Id = 2, Name = "Buz Inc."}
            };
            _employees = new ObservableCollection<Employee>
            {
                new Employee {Id = 1, Name = "Boris", Age = 12, Salary = 200000, DepartmentId = 1},
                new Employee {Id = 2, Name = "Maria", Age = 36, Salary = 10000, DepartmentId = 1},
                new Employee {Id = 3, Name = "Ann", Age = 24, Salary = 13000, DepartmentId = 1},
                new Employee {Id = 4, Name = "Andrey", Age = 48, Salary = 1000000, DepartmentId = 2}
            };
            cbDepartment.ItemsSource = _departments;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (cbDepartment.SelectedValue == null) return;

            var selected = (int) cbDepartment.SelectedValue;
            _employees.Add(
                new Employee {Id = 0, Name = "New Employee", Age = 0, Salary = 0, DepartmentId = selected}
            );
            UpdateLbEmployee(selected);
        }

        private void cbDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lbEmployee.UnselectAll();
            var comboBox = (ComboBox) sender;
            var selected = (int) comboBox.SelectedValue;
            AddButton.IsEnabled = true;
            UpdateLbEmployee(selected);
        }

        private void LbEmployee_OnMouseDoubleClick(object sender, RoutedEventArgs e)
        {
            var comboBox = (ListBox) sender;
            var selected = (Employee) comboBox.SelectedItem;
            _editEmployeeWindow = new EditEmployeeWindow(selected) {Owner = this};
            _editEmployeeWindow.OnApply += lbEmployee.Items.Refresh;
            _editEmployeeWindow.Show();
        }
    }
}