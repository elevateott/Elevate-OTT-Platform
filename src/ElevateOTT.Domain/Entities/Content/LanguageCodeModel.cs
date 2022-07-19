namespace ElevateOTT.Domain.Entities.Content;

[Table("LanguageCodes")]
public class LanguageCodeModel : EntityBase
{
    public string Code { get; set; } = string.Empty;
}
