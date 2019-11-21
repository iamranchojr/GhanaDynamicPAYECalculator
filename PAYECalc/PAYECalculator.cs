

/* MIT License

Copyright(c) 2019 Samuel Jr.Berkoh


Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

// usings
using System;
using System.Collections.Generic;
using System.Linq;

// namespace
namespace PAYECalc
{
    /// <summary>
    /// This class is used to Calculate the PAYE of an employee in Ghana
    /// by providing the typeparam="T" tax rates which include the rate in percentage,
    /// the band and the cummulative band to use for each
    /// </summary>
    public class PAYECalculator
    {

        // Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public PAYECalculator()
        {
            // initializeBands to empty list
            TaxRates = new List<TaxRate>();
        }

        /// <summary>
        /// Constructor to create a PAYECalc instance with tax rates
        /// The constructor calls a private Validate method which throws
        /// an exception of type System.Exception if tax rates is null or empty
        /// </summary>
        /// <param name="taxRates">List of tax rates</param>
        public PAYECalculator(List<TaxRate> taxRates)
        {
            // set tax rates, we assume they have been added in
            // order so no sorting will be performed
            TaxRates = taxRates;
            SortTaxRates();

            // Validate tax rates to make sure it's not null and that it contains items
            Validate();
        }


        // Properties

        /// <summary>
        /// Contains a list of the tax rates
        /// </summary>
        public List<TaxRate> TaxRates { get; private set; }


        // Methods

        /// <summary>
        /// Adds a new tax rate
        /// </summary>
        /// <param name="taxRate">Tax rate to add</param>
        public void AddTaxRate(TaxRate taxRate)
        {
            TaxRates.Add(taxRate);
            SortTaxRates();
        }

        /// <summary>
        /// Removes a tax rate
        /// </summary>
        /// <param name="taxRate">Tax rate to remove</param>
        public void RemoveTaxRate(TaxRate taxRate)
        {
            TaxRates.Remove(taxRate);
            SortTaxRates();
        }

        /// <summary>
        /// Sorts the tax rates in ascending order using the chargeable income
        /// we are assuming the tax will always be in ascending
        /// </summary>
        public void SortTaxRates()
        {
            if (TaxRates != null)
            {
                TaxRates.OrderBy(m => m.ChargeableIncome);
            }
        }

        /// <summary>
        /// Peforms validation to make sure Tax rates is not null and contains items.
        /// This function will throw an exception of type System.Exception if tax rates is null or empty
        /// </summary>
        private void Validate()
        {
            // tax rate should not be null
            if (TaxRates == null)
            {
                // throw exception if it is
                throw new Exception("Tax rates cannot be null");
            }

            // check to make sure at least it contains one item
            if (TaxRates.Count == 0)
            {
                // throw exception if it is
                throw new Exception("Tax rates cannot be empty. It should contain at least one item");
            }
        }

        /// <summary>
        /// Uses the tax rates provided to calculate the PAYE
        /// This method calls a private Validate method which throws
        /// an exception of type System.Exception if tax rates is null or empty
        /// </summary>
        /// <param name="taxableIncome"></param>
        /// <returns>Calculated PAYE</returns>
        public decimal CalculatePAYE(decimal taxableIncome)
        {
            // first validate, just to be on the safer side
            Validate();

            // variable to store calculated tax
            decimal PAYE = 0.00M;

            // loop through taxRates and calculate tax for each
            foreach (TaxRate taxRate in TaxRates)
            {
                // calculate tax and add it to PAYE
                PAYE += taxRate.CalculateTax(taxableIncome);

                // subtract the chargeable income from the taxable income
                taxableIncome -= taxRate.ChargeableIncome;

                // if taxable income is negative, it means we won't tax again
                // break out of loop
                if (taxableIncome <= 0) break;
            }

            // return calculated PAYE in 2 decimal places
            return Math.Round(PAYE, 2);
        }
    }
}
