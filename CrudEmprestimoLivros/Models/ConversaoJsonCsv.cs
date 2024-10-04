
namespace CrudEmprestimoLivros.Models
{
    public class ConversaoJsonCsv
    {
        public string? Recebedor { get; set; } = " ";

        public static implicit operator ConversaoJsonCsv(string v)
        {
            throw new NotImplementedException();
        }
    }
}
