using System.Collections.Generic;

namespace Profile.Analytic
{
    internal interface IAnalyticTools
    {
        void SendMessage(string alias, IDictionary<string, object> eventData = null);
    } 
}

