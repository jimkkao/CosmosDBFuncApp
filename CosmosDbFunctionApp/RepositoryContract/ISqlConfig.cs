﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryContract
{
    public interface ISqlConfig
    {
        string EndPointUrl { get; }

        string PrimaryKey { get; }

        string DataBase { get; }

        string Container { get; }

        string PartitionKey { get; }
    }
}
