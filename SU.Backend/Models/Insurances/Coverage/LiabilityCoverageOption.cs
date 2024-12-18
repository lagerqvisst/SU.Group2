﻿using System.ComponentModel.DataAnnotations;
using SU.Backend.Models.Enums.Insurance;

namespace SU.Backend.Models.Insurances.Coverage;

/// <summary>
///     This class represents the different coverage options for liability insurance. Reflects the business documentation.
/// </summary>
public class LiabilityCoverageOption
{
    [Key] public int LiabilityCoverageOptionId { get; set; }
    public Deductible Deductible { get; set; } // Självrisk
    public LiabilityCoverageOptionAmounts OptionAmount { get; set; } // 3, 5 or 10 miljoner
    public decimal MonthlyPremium { get; set; } // Monthly premium for the option
    public ICollection<LiabilityCoverage> LiabilityCoverages { get; set; } = new List<LiabilityCoverage>();
}