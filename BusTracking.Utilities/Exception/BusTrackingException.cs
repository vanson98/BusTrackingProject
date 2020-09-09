using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.Utilities
{
    public class BusTrackingException : Exception
    {
        public BusTrackingException()
        {

        }
        public BusTrackingException(string message) : base(message)
        {

        }

        public BusTrackingException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
