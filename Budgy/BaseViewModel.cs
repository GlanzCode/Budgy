using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Budgy;

public abstract class BaseViewModel : ObservableObject
{
    protected ILogger _logger;
    protected BaseViewModel(ILogger logger)
    {
        _logger = logger;
    }
}
