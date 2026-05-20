using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Budgy;

public sealed class Entry
{
    [Key]
    public required int Id { get; set; }

    public string Title { get; set; }

    public string? Description { get; set; }

    public DateTime? PayDate { get; set; }

    public decimal Amount { get; set; }

    public bool IsExpense { get; set; }

    [NotMapped]
    public string ExpenseText => IsExpense ? "Expense" : "Income";
}
