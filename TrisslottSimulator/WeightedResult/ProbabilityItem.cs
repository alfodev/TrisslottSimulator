namespace JOS.WeightedResult
{
    public class ProbabilityItem<T>
    {
        public ProbabilityItem(int probability, T value) : this(probability, value, string.Empty)
        {

        }

        public ProbabilityItem(int probability, T value, string description)
        {
            Probability = probability;
            Value = value;
            Description = description;
        }

        public string Description { get; }
        public int Probability { get; }
        public T Value { get; }
    }
}
