namespace Interfaces
{
    public interface IPoolableItem
    {
        public void OnGet();
        public void OnReturn();
    }
}
