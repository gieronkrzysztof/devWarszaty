using ConsoleApp5.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using static ScoringCalculatorLib.ScoringCalculator;

namespace ConsoleApp5
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = new List<MortgageApplicant>();

            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString("https://raw.githubusercontent.com/gieronkrzysztof/devWarszaty/master/mortgage.json");
                result = JsonConvert.DeserializeObject<List<MortgageApplicant>>(json);
            }

            var scoringCalculator = new ScoringCalculatorLib.ScoringCalculator();

            var i = 0;
            foreach (var applicant in result)
            {
                if(HasProperScoring(scoringCalculator, applicant))
                {
                    i++;
                    Console.WriteLine($"Accepted applicant: {applicant.Id}, {applicant.Name}, {applicant.Surname}");
                }
            }

            Console.WriteLine($"Accepted applicants count: {i}");
        }

        private static bool HasProperScoring(ScoringCalculatorLib.ScoringCalculator scoringCalculator, MortgageApplicant applicant)
        {
            var scoring = scoringCalculator.GetScoring(new ScoringClient
            {
                Salary = Math.Round(applicant.Salary, 2),
                Expenditures = applicant.Expenditures,
                CreditAmount = applicant.CreditAmount,
                CreditMonths = applicant.CreditMonths,
                SalaryType = GetSalaryType(applicant.SalaryType)
            });

            if (scoring == 'A' || scoring == 'B')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static ContractType GetSalaryType(string salaryType)
        {
            switch (salaryType)
            {
                case "FullTimeEmploymentContract":
                    return ContractType.FullTimeEmploymentContract;
                case "Freelancer":
                    return ContractType.Freelancer;
                case "B2BContract":
                    return ContractType.B2BContract;
                case "PartTimeEmploymentContract":
                    return ContractType.PartTimeEmploymentContract;
            }

            return ContractType.PartTimeEmploymentContract;
        }
    }
}
