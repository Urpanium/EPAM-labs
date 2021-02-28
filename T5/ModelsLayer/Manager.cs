namespace T5.ModelsLayer
{
    public class Manager
    {
        public int Id { get; set; }
        public string LastName { get; set; }


        public Manager()
        {
        }

        public Manager(string lastName)
        {
            LastName = lastName;
        }

        public override string ToString()
        {
            return LastName;
        }
    }
}