namespace SU.Backend.Models.Insurances;

/// <summary>
///     This class is only used as data transfer object for the InsuredPerson and not represented in the database.
/// </summary>
public class InsuredPerson
{
    public string InsuredPersonName { get; set; }
    public string InsuredPersonPersonalNumber { get; set; }
}