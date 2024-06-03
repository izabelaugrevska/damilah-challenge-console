using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSISConsole.Data
{
    public class BookInfo
    {
        public string Title { get; set; }
        public List<string> Authors { get; set; }
        public List<string> Publishers { get; set; }
        public int FirstPublishYear { get; set; }
        public List<string> ISBNs { get; set; }
        public int NumberOfPages { get; set; }
        public string CoverUrl { get; set; }
    }
}