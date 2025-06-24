namespace DefaultNamespace.Powers
{
    public interface ICardTransformer
    {
        TrickManager TrickManager { get; set; }
        
        void TransformCard(Card card);
    }
}