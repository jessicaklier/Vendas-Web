using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public double Basesalary { get; set; }
        public Department Department { get; set; }
        public int DepartmentId {get; set;}
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();// fiz a associação para muitos como esta no diagrama

        public Seller()
        {// fiz o contrutor vazio, pois vou criar o construtor com argumentos.
        }
        
        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            Basesalary = baseSalary;
            Department = department;
        }
        // adicionando vendas na minha lista de vendas.
        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }

        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }
    }
}

