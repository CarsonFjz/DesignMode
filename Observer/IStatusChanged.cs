using System;
using System.Collections.Generic;
using System.Text;

namespace Observer
{
    public interface IStatusChanged
    {
        int Status { get; }
        void OnStatusChanged(int newStatus);
    }
}
