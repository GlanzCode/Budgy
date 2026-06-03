using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Budgy.Data;

public sealed class Category
{
    [Key]
    public required int Id { get; init; }
    public required string Name { get; set; }
    public string Color { get; set; }

    public ICollection<EntryTemplate> Entries { get; set; } = [];
}
