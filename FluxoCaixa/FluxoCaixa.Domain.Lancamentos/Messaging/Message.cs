namespace FluxoCaixa.Domain.Lancamentos.Messaging
{
    public class Message
    {
        public Message()
        {
            Content = string.Empty;
            CreatedAt = DateTime.Now;
            Id = 0;
        }
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
