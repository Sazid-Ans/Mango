﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango.Services.RewardsApi
{
    public interface IAzureServiceBusConsumer
    {
            Task Start();
            Task Stop();   
    }
}
