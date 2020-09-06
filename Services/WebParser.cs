using HtmlAgilityPack;
using Jobs.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobs.Services
{
    abstract public class WebParser<TResult,TNode> : IWebparser
    {
        protected HtmlWeb Website { get; set; } 
        protected HtmlDocument htmldocument { get; set; }

        
        public virtual Task<List<string>> GetPageData(string url)
        {
            throw new NotImplementedException();
        }

        protected abstract List<TResult> CreateObjectFromNode(List<TNode> nodes);
        

        protected abstract HtmlNode GetHtmlNode(HtmlNode s, string Class);
    }
}
