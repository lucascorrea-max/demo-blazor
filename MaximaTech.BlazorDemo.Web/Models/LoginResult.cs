using System.Text.Json.Serialization;

public class LoginResult
{
    public bool Success { get; set; }
    public LoginData? Data { get; set; }
}

public class LoginData
{
    [JsonPropertyName("access_token")]
    public required string AccessToken { get; set; }
    public required Usuario Usuario { get; set; }
    public required Cliente Cliente { get; set; }
    public required IEnumerable<RotinaVersao> RotinaVersao { get; set; }

}

public class Usuario
{
    public int Codigo { get; set; }
    public required string Nome { get; set; }
    public required string Email { get; set; }

    // TODO: 🔐 Omitir estes campos e sugerir alteração para que sejam
    // removidos ou parametrizados na resposta da API de autenticação
    public string? Login { get; set; }
    public string? Senha { get; set; }
}

public class Cliente
{
    public int Codigo { get; set; }
    public int Codigomaxima { get; set; }
    public required string Nome { get; set; }
}

public class RotinaVersao
{
    public int CodigoVersao { get; set; }
    public int CodigoRotina { get; set; }
    public string? NomeRotina { get; set; }
    public string? LinkAcesso { get; set; }
    public string? LinkAcessoPDV { get; set; }
    public string? LinkDownload { get; set; }
    public string? Versao { get; set; }
    public bool Ativo { get; set; }
    public string? PackageName { get; set; }
    public int CodigoAmbiente { get; set; }
    public string? DescricaoAmbiente { get; set; }
}
