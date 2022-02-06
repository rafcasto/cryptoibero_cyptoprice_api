using System.Collections.Generic;

namespace cryptoibero_cyptoprice_api.Model
{
    public class ChartInfo
    {
        public List<double>  Data{get;set;}
        public List<string> Labels{get;set;}
        public string Title{get;set;}
    }
}