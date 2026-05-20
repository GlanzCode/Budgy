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
}
