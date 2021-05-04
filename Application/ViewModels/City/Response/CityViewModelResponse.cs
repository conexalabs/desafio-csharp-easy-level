namespace Application.ViewModels.City.Response
{
    public class CityViewModelResponse
    {
        public string Nome { get; set; }
        public string Temp { get; set; }
        public string lon { get; set; }
        public string lat { get; set; }
        public string Mensagem { get; set; } = "Valor buscado pelo API";
    }
}