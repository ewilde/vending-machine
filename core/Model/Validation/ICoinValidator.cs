namespace VendingMachine.Core
{
    public interface ICoinValidator
    {
        void Validate(Coin coin);
    }
}