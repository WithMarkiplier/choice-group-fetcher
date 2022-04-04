using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkiplierJson.models
{
    public class ChoiceGroup
    {

        public string watchCode { get; set; }
        public string videoTitle { get; set; }
        public string title { get; set; }
        public double showAt { get; set; }
        public string type { get; set; }
        public List<Choice> choices { get; set; }
        public Ending? ending { get; set; }
    }

    public class Choice
    {
        public string text { get; set; }
        public string nextChoiceGroupWatchCode { get; set; }
    }

    public class Ending
    {
        public string endingName { get; set; }
        public int endingNumber { get; set; }
        public string endingCode { get; set; }
    }
}
