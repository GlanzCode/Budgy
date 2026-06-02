using Budgy.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Budgy.Feature.Statistics;

public sealed record EntryChartModel(Data.Category Category, decimal Amount, string Color);
