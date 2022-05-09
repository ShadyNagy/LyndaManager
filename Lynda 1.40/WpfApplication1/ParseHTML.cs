using HtmlAgilityPack;
using System.Text;
using System.Threading;
using System.Windows.Controls;

namespace WpfApplication1
{
    class ParseHTML
    {
        public ParseHTML() { }

        //public string doParsing(string html)
        //{
            //Thread t = new Thread(TParseMain);
            //t.ApartmentState = ApartmentState.STA;
            //t.Start((object)html);
            //t.Join();
            //return ReturnString;
        //}

       public HtmlNodeCollection getNodes(string htmlData, string nodeTag, string nodeID, string nodeClass)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(htmlData);

            if (nodeClass != "")
                return doc.DocumentNode.SelectNodes("//" + nodeTag + "[@class='" + nodeClass + "']");

            if (nodeID != "")
                return doc.DocumentNode.SelectNodes("//" + nodeTag + "[@id='" + nodeID + "']");

            return doc.DocumentNode.SelectNodes("//" + nodeTag);
        }

        public string GetAttributeFromClass(string htmlData, string NodeTag, string NodeId, string NodeClass, string NodeAttribute)
        {
            ParseHTML parseHTML = new ParseHTML();
            var node = parseHTML.getNode(htmlData, NodeTag, NodeId, NodeClass);

            return parseHTML.getAttribute(node, NodeAttribute).Value;
        }

        public HtmlAttribute getAttribute(HtmlNode node, string id)
        {
            return node.Attributes[id];
        }

        public HtmlNode getNode(string htmlData, string nodeTag, string nodeID, string nodeClass)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(htmlData);

            var nodes = getNodes(htmlData, nodeTag, nodeID, nodeClass);

            if (nodes != null)
            {
                foreach (var node in nodes)
                {
                    return node;
                }
            }

            return null;
        }

        public string getValueFromNode(HtmlNode node)
        {
            if (node != null)
            {
                HtmlAttribute att = getAttribute(node, "value");
                return att.Value;
            }

            return null;
        }

        public string GetSeasurfID(string html)
        {
            string seasurf = "";

            var node = getNode(html, "input", "seasurf", "");
            seasurf = getValueFromNode(node);          
        
            return seasurf;

        }
    }
}
