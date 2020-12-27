namespace T2
{
    public abstract class SentenceItem
    {
        public virtual string Value { get; set; }
        public virtual int Length { get; }

        /*public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            return Value.Equals(obj.ToString());
        }*/

        protected bool Equals(SentenceItem other)
        {
            return Value.Equals(other.Value);
        }

        /*public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }*/
    }
}