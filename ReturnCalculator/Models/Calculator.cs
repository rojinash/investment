using FormFactory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MathNet.Numerics;
using System.Linq;
using System.Threading.Tasks;

namespace ReturnCalculator.Models
{
    public class Calculator
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Initial Amount (in USD) is required")]
        [Display(Name = "Initial Amount (USD): ")]
        public double InitialAmount { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Target Amount (in USD) is required")]
        [Display(Name = "Target Amount (USD): ")]
        public double TargetAmount { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Contribution years (a positive integer) is required")]
        [Display(Name = "Contribution Years (Integer): ")]
        public int ContributionYears { get; set; }
        
        [Required(ErrorMessage = "Annual Contribution Amount (in USD) is required")]
        [Range(0, double.MaxValue)]
        [Display(Name = "Annual Contribution (USD): ")]
        public double AnnualContribution { get; set; }

        [Required(ErrorMessage = "Inflation (in percentage) is required")]
        [Range(0.01, 100)]
        [Display(Name = "Inflation (%): ")]
        public double Inflation { get; set; }

        public double RateOfReturn { get; set; }

        public double[] TimeAmountArr;
        public Calculator()
        {

        }

        public void CalculateReturn()
        {
            Polynomial[] amountAfterEachYear = new Polynomial[ContributionYears];
            amountAfterEachYear[0] = new Polynomial(new double[] { InitialAmount + AnnualContribution, InitialAmount });
            Polynomial yearlyInterestRate = new Polynomial(new double[] { 0, 1 });
            
            for(int i = 1; i < ContributionYears; i++)
            {
                Polynomial prevPoly = amountAfterEachYear[i - 1];
                Polynomial newInterestVal = Polynomial.Multiply(prevPoly, yearlyInterestRate);
                Polynomial prevPlusInterest = Polynomial.Add(prevPoly, newInterestVal);
                Polynomial finalVal = Polynomial.Add(prevPlusInterest, AnnualContribution);
                amountAfterEachYear[i] = finalVal;
            }

            Polynomial polynomialToSolve = Polynomial.Add(amountAfterEachYear[ContributionYears-1], (-1 * TargetAmount));
            System.Numerics.Complex[] roots = null;
            Task task = Task.Factory.StartNew(() =>
            {
                roots = polynomialToSolve.Roots();
            });
            task.Wait(TimeSpan.FromMilliseconds(6000));
            if (roots == null)
            {
                return;
            }

            double rateOfReturn = roots[ContributionYears - 1].Real;
            

            RateOfReturn = ((1 + rateOfReturn) / (1 + (Inflation/100))) - 1;
            BuildYearlyData(amountAfterEachYear);
            RateOfReturn = Math.Round(RateOfReturn * 100, 2, MidpointRounding.AwayFromZero);
        }
        public void BuildYearlyData(Polynomial[] yearlyValues)
        {
            List<double> yearlyDollarAmount = new List<double> { Math.Round(InitialAmount * (1 + Inflation), 2, MidpointRounding.AwayFromZero) };
            foreach(Polynomial polynomial in yearlyValues)
            {
                yearlyDollarAmount.Add(Math.Round(polynomial.Evaluate(RateOfReturn) * (1 + Inflation), 2, MidpointRounding.AwayFromZero));
            }

            TimeAmountArr = yearlyDollarAmount.ToArray();
           
        }


    }
}
