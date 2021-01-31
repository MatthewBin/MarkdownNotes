using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkdownNodes.models
{
    public class MarkDownDir
    {
        public string DisplayName { get; set; }
        public string FullPath { get; set; }
        public List<MarkDownFile> MarkDownFiles { get; set; }
    }
}
