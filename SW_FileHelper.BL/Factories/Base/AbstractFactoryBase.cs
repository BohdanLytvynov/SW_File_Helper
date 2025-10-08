using SW_File_Helper.BL.Loggers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW_File_Helper.BL.Factories.Base
{
    public abstract class AbstractFactoryBase<TType, TCreationOptions> : IAbstractFactory<TType>
    {
        protected TCreationOptions CreationOptions { get; init; }
        protected ILogger Logger { get; init; }

        public AbstractFactoryBase(ILogger logger, TCreationOptions creationOptions)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            CreationOptions = creationOptions ?? throw new ArgumentNullException(nameof(creationOptions));
        }

        public abstract TType Create();
    }
}
