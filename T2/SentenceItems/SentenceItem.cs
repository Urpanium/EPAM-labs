namespace T2
{
    public abstract class SentenceItem
    {
        public virtual string Value { get; set; }
        public virtual int Length { get; }

        protected bool Equals(SentenceItem other)
        {
            return Value.Equals(other.Value);
        }
    }
}