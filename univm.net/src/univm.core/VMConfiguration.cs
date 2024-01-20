namespace univm.core
{
    public sealed class VMConfiguration
    {
        public bool UseDispatcher;
        public int DispatcherLimit;
        public IDispatcherFactory DispatcherFactory;
        public readonly static VMConfiguration NoDispatcherConfiguration = new VMConfiguration(new DefaultDispatcherFactory()) { UseDispatcher = false };
        public VMConfiguration(IDispatcherFactory dispatcherFactory)
        {
            DispatcherFactory = dispatcherFactory;
        }
    }
    public class DefaultDispatcherFactory : IDispatcherFactory
    {
        public IDispatcher CreateDispatcher()
        {
            return new DefaultDispatcher();
        }
    }
}