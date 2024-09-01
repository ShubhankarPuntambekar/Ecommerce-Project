using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Specifications
{
    public class ProductSpecParams
    {
        private List<string> _brand = [];
        private const int MaxPageSize = 50;

        public int PageIndex { get; set; } = 1;

        private int _pageSize = 6;

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value > MaxPageSize ? MaxPageSize : value; }
        }

        public List<string> Brand
        {
            get { return _brand; }
            set
            {
                _brand = value.SelectMany(x => x.Split(',',StringSplitOptions.RemoveEmptyEntries)).ToList();
            }
        }

        private List<string> _types = [];

        public List<string> Type
        {
            get { return _types; }
            set
            {
                _types = value.SelectMany(x => x.Split(',', StringSplitOptions.RemoveEmptyEntries)).ToList();
            }
        }

        public string? Sort { get; set; }

        private string? _search;

        public  string? Search
        {
            get { return _search ?? ""; }
            set { _search = value?.ToLower(); }
        }

    }
}
