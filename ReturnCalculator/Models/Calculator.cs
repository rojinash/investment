using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReturnCalculator.Models
{
    public class Calculator
    {
        
        [Required]
        [Display(Name = "Initial Amount")]
        public double InitialAmount { get; set; }

        [Required]
        [Display(Name = "Target Amount")]
        public double TargetAmount { get; set; }

        [Required]
        [Display(Name = "Years to reach target")]
        public double ContributionYears { get; set; }

        [Required]
        [Display(Name = "Annual Contribution")]
        public double AnnualContribution { get; set; }

        [Required]
        [Range(0,100)]
        [Display(Name = "Inflation")]
        public double Inflation { get; set; }

        [Display(Name = "Rate of Return")]
        public double RateOfReturn { get; set; }

        public Dictionary<int, double> timeAmountDict = new Dictionary<int, double>();
        public Calculator()
        {

        }

        public void CalculateReturn()
        {
            double annualizedRor = (Math.Pow(this.TargetAmount / this.InitialAmount, (1 / this.ContributionYears)) - 1);
            double rorAfterInflation = (1 + annualizedRor) / (1+(this.Inflation/100));
            Dictionary<int, double> allRateOfReturn = new Dictionary<int, double>();
            double tot = 0;
            for(int year = 0; year < this.ContributionYears; year++)
            {
                tot += this.AnnualContribution / Math.Pow(1 + annualizedRor, year);

                //double prevValue = year == 0 ? this.InitialAmount : this.timeAmountDict[year - 1];

                //double interestEarned = prevValue * rorAfterInflation;
                //double totalAmount = prevValue + interestEarned + this.AnnualContribution;
                //this.timeAmountDict.Add(year, totalAmount);
                
            }
            this.RateOfReturn = rorAfterInflation;
        }

    }
}
