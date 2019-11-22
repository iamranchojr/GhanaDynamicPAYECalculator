
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

using System;

namespace PAYECalc
{
    /// <summary>
    /// This class models a tax rate
    /// </summary>
    public class TaxRate
    {
        //Constants
        public const string INVALID_CHARGEABLEINCOME_EXCEPTON_MESSAGE = "Invalid Chargeable Income";
        public const string INVALID_TAXRATE_EXCEPTON_MESSAGE = "Invalid value provided. Rate should be between 0 and 100";

        private double _rate;
        private decimal _chargeableIncome;

        public TaxRate() { }

        /// <summary>
        /// Constructor to directly create a Tax rate by passing in the rate and chargeable income
        /// </summary>
        /// <param name="rate">Tax rate in percentage</param>
        /// <param name="chargeableIncome">Tax rate chargeable income</param>
        public TaxRate(double rate, decimal chargeableIncome)
        {
            // This is not needed, an exception will be thrown from our properties
            // when invalid values are provided

            // reject if rate is < 0 
            // if (rate < 0) throw new ArgumentException(INVALID_TAXRATE_EXCEPTON_MESSAGE);

            // reject if chargeAbleIncome is <= 0 
            // if (chargeableIncome <= 0) throw new ArgumentException(INVALID_CHARGEABLEINCOME_EXCEPTON_MESSAGE);

            Rate = rate;
            ChargeableIncome = chargeableIncome;
        }

        /// <summary>
        /// Tax Rate in Percentage
        /// This property will throw a System.Exception when you try to set
        /// a value which is not a valid percentage
        /// </summary>
        public double Rate
        {
            get => _rate;

            set
            {
                // check to make sure value is within the 100 percentage limit
                if (value > 100 || value < 0)
                {
                    throw new ArgumentException(INVALID_TAXRATE_EXCEPTON_MESSAGE);
                }

                _rate = value;
            }
        }

        /// <summary>
        /// Chargeable Income
        /// </summary>
        public decimal ChargeableIncome
        {
            get => _chargeableIncome;
            set
            {
                // check to make sure value is within the 100 percentage limit
                if (value < 0)
                {
                    throw new ArgumentException(INVALID_CHARGEABLEINCOME_EXCEPTON_MESSAGE);
                }

                _chargeableIncome = value;
            }
        }

        /// <summary>
        /// Calculates tax rate for amount provided using the ChargeableIncome
        /// </summary>
        /// <param name="amount">amount to calculate tax on</param>
        /// <returns>Tax amount after calculation</returns>
        public decimal CalculateTax(decimal amount)
        {
            // subtract the Chargeable Income from the amount
            decimal result = amount - ChargeableIncome;

            // compare the result with the band
            if (result > ChargeableIncome)
            {
                // if it is greater than ChargeableIncome,
                // calculate the tax rate on the ChargeableIncome
                return ChargeableIncome * (decimal)(Rate / 100);
            }

            // else, perform the tax on the amount itself
            return amount * (decimal)(Rate / 100);
        }
    }
}
