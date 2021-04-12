using FormFactory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReturnCalculator.Models
{
    public class Calculator
    {
        [Required(AllowEmptyStrings = false ,ErrorMessage = "Initial Amount (in USD) is required")]
        [Display(Name = "Initial Amount")]
        public double InitialAmount { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Target Amount (in USD) is required")]
        [Display(Name = "Target Amount")]
        public double TargetAmount { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Contribution years (a positive integer) is required")]
        [Display(Name = "Contribution Years")]
        public double ContributionYears { get; set; }
        
        [Required(AllowEmptyStrings = true)]
        [Display(Name = "Annual Contribution")]
        public double AnnualContribution { get; set; }

        [Required(AllowEmptyStrings = true)]
        [Display(Name = "Inflation")]
        public double Inflation { get; set; }

        public double RateOfReturn { get; set; }

        public Dictionary<int, double> timeAmountDict = new Dictionary<int, double>();
        public double[] timeAmountArr;
        public string JavascriptToRun { get; set; }
        public Calculator()
        {

        }

        public void CalculateReturn()
        {
            double annualizedRor = (Math.Pow(this.TargetAmount / this.InitialAmount, (1 / this.ContributionYears)) - 1);
            //double rorAfterInflation = (1 + annualizedRor) / (1+(this.Inflation/100));
            this.timeAmountArr = new double[(int)this.ContributionYears];
            for(int year = 0; year < this.ContributionYears; year++)
            {
                double prevValue = year == 0 ? this.InitialAmount : this.timeAmountDict[year - 1];

                double interestEarned = prevValue * annualizedRor;
                double totalAmount = Math.Round(prevValue + interestEarned + this.AnnualContribution, 2, MidpointRounding.AwayFromZero);
                this.timeAmountDict.Add(year, totalAmount);
                this.timeAmountArr[year] = totalAmount;
            }
            this.RateOfReturn = Math.Round(annualizedRor * 100, 2, MidpointRounding.AwayFromZero);
           
        }


    }
}
