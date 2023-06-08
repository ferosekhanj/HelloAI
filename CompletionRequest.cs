using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloAI
{
    internal class CompletionRequest
    {
        public string model { get; set; } = "text-davinci-003";
        public double temperature { get; set; } = 0.7;
        public string prompt { get; set; }
        public int max_tokens { get; set; } = 100;
        public double top_p { get; set; } = 0.3;
        public double presence_penalty { get; set; } = 0;
        public double frequency_penalty { get; set; } = 0.5;
    }
}
