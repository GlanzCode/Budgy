using System;
using System.Collections.Generic;
using System.Text;

namespace Budgy;

public interface ICalculationService
{
    decimal CalculateBudget(decimal expense, decimal income);

    double GetRatio(decimal expense, decimal income);
}
