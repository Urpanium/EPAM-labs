namespace T5.Stuff
{
    public class Client
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Client()
        {
            
        }
        
        public Client(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}