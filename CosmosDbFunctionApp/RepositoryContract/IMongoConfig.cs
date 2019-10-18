using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryContract
{
    public interface IMongoConfig
    {
        string ConnectionStr { get;  }

        string Database { get; }

        string Collection { get; }
    }
}
