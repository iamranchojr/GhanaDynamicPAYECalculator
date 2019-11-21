using System;
using System.Collections.Generic;

namespace PAYECalc
{
    class Program
    {
        static void Main(string[] args)
        {
            // create tax rates using object initialization syntax
            // as the Rate and ChargeableIncome are exposed as public properties
            // Passing a rate that is not a valid percentage will throw an exception
            List<TaxRate> taxRates = new List<TaxRate>
            {
                new TaxRate
                {
                    Rate = 0,
                    ChargeableIncome = 288
                },

                new TaxRate
                {
                    Rate = 5,
                    ChargeableIncome = 100,
                },

                new TaxRate
                {
                    Rate = 17.5,
                    ChargeableIncome = 3000,
                },

                new TaxRate
                {
                    Rate = 10,
                    ChargeableIncome = 140,
                },
            };

            // create an instance of the PAYECalculator with the tax rates
            // NOTE: The constructor will throw an exception if tax rates is empty
            PAYECalculator calc = new PAYECalculator(taxRates);

            // The PAYECalculator provides you with a sort method to sort the
            // tax rates in ascending order incase your tax rates where not arrange so
            // it does this using the chargeable income
            calc.SortTaxRates();

            // The PAYECalculator contains an add method that allows you to add a new tax rate
            calc.AddTaxRate(new TaxRate(25, 16472));

            calc.AddTaxRate(new TaxRate(30, 20000));


            // you can also create a tax rate directly with the constructor
            var taxRate = new TaxRate(35, 30000);

            // you can update the values too
            taxRate.Rate = 55;
            taxRate.ChargeableIncome = 40000;

            // add the tax rate
            calc.AddTaxRate(taxRate);

            // It also contains a method to allow you to remove a tax rate
            calc.RemoveTaxRate(taxRate);

            // NB: The add and remove tax rate method calls the sort function
            // whenever an item is added or removed


            // finally, the PAYECalculator class contains a function that uses
            // the tax rates provided to calculate the PAYE, this function accepts
            // the taxable income, that is after all deductions have been done
            // including SSNIT
            var taxableIncome = 37800.00M;
            var PAYE = calc.CalculatePAYE(taxableIncome);
            Console.WriteLine($"PAYE of Taxable Income {taxableIncome} is {PAYE}");
        }
    }
}
