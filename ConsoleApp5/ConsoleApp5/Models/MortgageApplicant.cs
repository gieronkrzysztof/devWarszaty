using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp5.Models
{
    public class MortgageApplicant
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string SalaryType { get; set; }
        public decimal Salary { get; set; }
        public decimal Expenditures { get; set; }
        public decimal CreditAmount { get; set; }
        public int CreditMonths { get; set; }
    }
}
