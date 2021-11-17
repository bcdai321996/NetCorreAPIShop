using System;
using System.Collections.Generic;
using System.Text;

namespace ShopNetCoreApi.Models.Enums
{
    public enum OrderStatus
    {
        InProgress,
        Confirmed,
        Shipping,
        Success,
        Canceled
    }
}
