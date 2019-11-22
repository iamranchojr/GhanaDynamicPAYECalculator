
using Xunit;
using System;
using System.Collections.Generic;

namespace PAYECalc.Tests
{
    public class PAYECalculator_TestPAYECalculation
    {
        private readonly PAYECalculator _calculator;

        public PAYECalculator_TestPAYECalculation()
        {
            _calculator = new PAYECalculator();
            _calculator.AddTaxRate(new TaxRate(0, 288));
            _calculator.AddTaxRate(new TaxRate(5, 100));
            _calculator.AddTaxRate(new TaxRate(10, 140));
            _calculator.AddTaxRate(new TaxRate(17.5, 3000));
            _calculator.AddTaxRate(new TaxRate(25, 16472));
            _calculator.AddTaxRate(new TaxRate(30, 20000));
        }

        [Fact]
        public void IsPAYEZero_InputIs288_ReturnTrue()
        {
            var PAYE = _calculator.CalculatePAYE(283.50M);

            Assert.True(PAYE == 0, "PAYE should be 0");
        }

        [Fact]
        public void IsPAYE25Point83_InputIs600_ReturnFalse()
        {
            var PAYE = _calculator.CalculatePAYE(600M);

            Assert.False(PAYE == 25.87M, "PAYE should not be 25.87");
        }

        [Fact]
        public void IsPAYE125Point05_InputIs1134_ReturnTrue()
        {
            var PAYE = _calculator.CalculatePAYE(1134M);

            Assert.True(PAYE == 125.05M, "PAYE should be 125.05");
        }

        [Fact]
        public void IsPAYE224Point28_InputIs1701_ReturnTrue()
        {
            var PAYE = _calculator.CalculatePAYE(1701M);

            Assert.True(PAYE == 224.28M, "PAYE should be 224.28");
        }

        [Fact]
        public void IsPAYE1552_InputIs7560_ReturnTrue()
        {
            var PAYE = _calculator.CalculatePAYE(7560M);

            Assert.True(PAYE == 1552M, "PAYE should be 1552");
        }

        [Fact]
        public void IsPAYE3205Point78_InputIs14175_ReturnFalse()
        {
            var PAYE = _calculator.CalculatePAYE(7560M);

            Assert.False(PAYE == 3205.78M, "PAYE should not be 3205.78");
        }

        [Fact]
        public void IsExceptionThrownOnCreation_InputTaxRatesIsNull_ExceptionThrown()
        {
            List<TaxRate> testTaxRates = null;
            var potentialException = Record.Exception(() => new PAYECalculator(testTaxRates));
            Assert.Equal(PAYECalculator.NULL_TAX_RATES_EXCEPTION_MESSAGE, potentialException.Message);
        }

        [Fact]
        public void IsExceptionThrownOnCreation_InputTaxRatesIsEmpty_ExceptionThrown()
        {
            List<TaxRate> testTaxRates = new List<TaxRate>();
            var potentialException = Record.Exception(() => new PAYECalculator(testTaxRates));
            Assert.Equal(PAYECalculator.EMPTY_TAX_RATES_EXCEPTION_MESSAGE, potentialException.Message);
        }

        [Fact]
        public void IsNoExceptionOnCreation_InputTaxRatesIsNonEmpty_NoExceptionThrown()
        {
            List<TaxRate> testTaxRates = new List<TaxRate> { new TaxRate(0, 288) };
            var potentialException = Record.Exception(() => new PAYECalculator(testTaxRates));
            Assert.Null(potentialException);
        }

        [Fact]
        public void IsNoException_OnTaxRate_Creation_InputValuesCorrect_NoExceptionThrown()
        { 
            var potentialException = Record.Exception(() => new TaxRate(0, 288));
            Assert.Null(potentialException);
        }
        
        [Fact]
        public void IsException_OnTaxRate_Creation_InputRateNegative_ExceptionThrown()
        { 
            var potentialException = Record.Exception(() => new TaxRate(-0.01, 288));
            Assert.Equal(TaxRate.INVALID_TAXRATE_EXCEPTON_MESSAGE, potentialException.Message);
        }
        
        [Fact]
        public void IsException_OnTaxRate_Creation_InputChargeableIconeNegative_ExceptionThrown()
        { 
            var potentialException = Record.Exception(() => new TaxRate(1, -288));
            Assert.Equal(TaxRate.INVALID_CHARGEABLEINCOME_EXCEPTON_MESSAGE, potentialException.Message);
        }
        
        [Fact]
        public void IsException_OnTaxRate_Creation_InputRateGreaterThan100_ExceptionThrown()
        { 
            var potentialException = Record.Exception(() => new TaxRate(100.02, 288));
            Assert.Equal(TaxRate.INVALID_TAXRATE_EXCEPTON_MESSAGE, potentialException.Message);
        }
    }
}
