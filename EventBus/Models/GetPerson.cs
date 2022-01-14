namespace EventBus.Models
{
    public class GetPerson
    {
        public int UserId { get; set; }

        public GetPerson(int userId)
        {
            UserId = userId;
        }
    }
}
