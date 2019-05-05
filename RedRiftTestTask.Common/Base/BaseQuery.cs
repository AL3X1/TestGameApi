using RedRiftTestTask.Common.Interfaces;

namespace RedRiftTestTask.Common.Base
{
    public abstract class BaseQuery
    {
        protected IRuntimeRepository RuntimeRepository { get; set; }

        public BaseQuery(IRuntimeRepository runtimeRepository)
        {
            RuntimeRepository = runtimeRepository;
        }
    }
}