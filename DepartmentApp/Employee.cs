// Никита Жига

namespace DepartmentApp
{
    public class Employee
    {
        public int Id;
        public string Name;
        public int Age;
        public int Salary;
        public int DepartmentId;
        
        public override string ToString()
        {
            return $"{Id}\t{Name}\t{Age}\t{Salary}";
        }
    }
}