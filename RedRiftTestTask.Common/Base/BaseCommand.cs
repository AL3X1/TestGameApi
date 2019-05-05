using Microsoft.EntityFrameworkCore;
using RedRiftTestTask.Common.Interfaces;

namespace RedRiftTestTask.Common.Base
{
    public abstract class BaseCommand
    {
        protected IRuntimeRepository RuntimeRepository { get; set; }

        public BaseCommand(IRuntimeRepository runtimeRepository)
        {
            RuntimeRepository = runtimeRepository;
        }
    }
}