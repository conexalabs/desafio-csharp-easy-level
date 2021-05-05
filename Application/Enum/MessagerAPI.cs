using System.ComponentModel;

namespace Application.Enum
{
    public enum MessagerAPI
    {
        [Description("Não foi possível acessar a API externa e encontrar esses dados no banco local. Por favor, tente mais tarte")]
        APIandBDNotDate = 1,
        [Description("AzamSharpBlogDevDatabase")]
        APIDisponivel = 2
    }
}