using System;
using System.Windows;

namespace DepartmentApp
{
    public partial class EditEmployeeWindow : Window
    {
        private Employee _employee;
        public delegate void ApplyEvent();

        public event ApplyEvent OnApply;
        
        public EditEmployeeWindow(Employee employee)
        {
            InitializeComponent();
            _employee = employee;
            txtId.Text = _employee.Id.ToString();
            txtName.Text = _employee.Name;
            txtAge.Text = _employee.Age.ToString();
            txtSalary.Text = _employee.Salary.ToString();
            txtDepartmentId.Text = _employee.DepartmentId.ToString();
        }

        private void Save()
        {
            var succeeded = true;
            succeeded &= int.TryParse(txtId.Text, out var id);
            succeeded &= int.TryParse(txtAge.Text, out var age);
            succeeded &= int.TryParse(txtSalary.Text, out var salary);
            succeeded &= int.TryParse(txtDepartmentId.Text, out var departmentId);
            if (!succeeded)
            {
                MessageBox.Show("Ошибка в вводе данных. Изменения не были сохранены");
                return;
            }

            _employee.Name = txtName.Text;
            _employee.Id = id;
            _employee.Age = age;
            _employee.Salary = salary;
            _employee.DepartmentId = departmentId;
        }

        private void OnCancel(object sender, RoutedEventArgs e) => Close();

        private void Apply(object sender, RoutedEventArgs e)
        {
            Save();
            OnApply?.Invoke();
            Close();
        }
    }
}