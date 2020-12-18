namespace T2
{
    
    public class Char
    {
        
        
        public Char()
        {
        }

        public Char(char value)
        {
            Value = value;
        }


        public char Value { get; set; }

        public override string ToString()
        {
            return Value + "";
        }
    }
}