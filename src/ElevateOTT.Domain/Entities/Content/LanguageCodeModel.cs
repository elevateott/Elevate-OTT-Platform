namespace ElevateOTT.Domain.Entities.Content;

[Table("LanguageCodes")]
public class LanguageCodeModel : BaseEntity
{
    public string Code { get; set; } = string.Empty;
}
