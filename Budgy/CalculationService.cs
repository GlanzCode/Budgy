using System;
using System.Collections.Generic;
using System.Text;

namespace Budgy;

public sealed class CalculationService : ICalculationService
{
    public decimal CalculateBudget(decimal expense, decimal income)
    {
        return income - expense;
    }

    public double GetRatio(decimal monthlyExpense, decimal monthlyIncome)
    {
        if (monthlyIncome == 0)
            return 0.0;

        if (monthlyExpense > monthlyIncome)
            return 1.0;

        return (double)(monthlyExpense / monthlyIncome);

        
    }
}
