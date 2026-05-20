using System;
using System.Collections.Generic;
using System.Text;

namespace Budgy.Test;


public class CalculationServiceTests
{




    [Theory]
    [InlineData(2000, 5000, 3000)]
    [InlineData(5000, 2000, -3000)]

    public void BudgetCalculation_Should_Return_Correct_Result(decimal expense, decimal income, decimal expected)
    {
        var calculationService = new CalculationService();

        decimal result = calculationService.CalculateBudget(expense, income);

        Assert.Equal(expected, result);
    }
}
