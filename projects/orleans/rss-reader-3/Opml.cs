using System.Xml.Linq;

public class Opml
{
    public string? Title { get; set; }
    public DateTime? DateCreated { get; set; }
    public DateTime? DateModified { get; set; }
    public string? OwnerName { get; set; }
    public string? OwnerEmail { get; set; }
    public Uri? OwnerId { get; set; }
    public Uri? Docs { get; set; }
    public string? ExpansionState { get; set; }
    public int? VertScrollState { get; set; }
    public int? WindowTop { get; set; }
    public int? WindowLeft { get; set; }
    public int? WindowBottom { get; set; }
    public int? WindowRight { get; set; }
    public List<Outline> Outlines { get; private set; }

    public Opml()
    {
        Outlines = new List<Outline>();
    }

    public Opml(string xml):this()
    {
        LoadFromXML(xml);
    }

    public void LoadFromXML(string xml)
    {
        var elements = XElement.Parse(xml);
        var heads = elements.Element("head").Descendants();

        Func<string, string?> selectString = (filter) =>
            heads!.Where(x => x.Name == filter).Select(x => x.Value).FirstOrDefault();

        Func<string, int?> selectInt = (filter) =>
            heads!.Where(x => x.Name == filter).Select(x => Convert.ToInt32(x.Value)).FirstOrDefault();

        Func<string, DateTime?> selectDate = (filter) =>
            heads!.Where(x => x.Name == filter).Select(x => Convert.ToDateTime(x.Value)).FirstOrDefault();

        Func<string, Uri?> selectUri = (filter) =>
            heads!.Where(x => x.Name == filter).Select(x => new Uri(x.Value)).FirstOrDefault();

        Title = selectString("title");
        DateCreated = selectDate("dateCreated");
        DateModified = selectDate("dateModified");
        OwnerName = selectString("ownerName");
        OwnerEmail = selectString("ownerEmail");
        OwnerId = selectUri("ownerId");
        Docs = selectUri("docs");
        ExpansionState = selectString("expansionState");
        VertScrollState = selectInt("vertScrollState");
        WindowTop = selectInt("windowTop");
        WindowLeft = selectInt("windowLeft");
        WindowBottom = selectInt("windowBottom");
        WindowRight = selectInt("windowRight");

        var bodies = elements.Element("body").Elements();
        //todo: make it recursive
        foreach (var b in bodies)
        {
            var o = new Outline();
            Outlines.Add(o);
            TraverseBody(b, o);
        }
    }

    private void TraverseBody(XElement outline, Outline ot)
    {
        if (outline != null)
        {
            foreach (var att in outline.Attributes())
            {
                ot.Attributes[att.Name.ToString()] = att.Value;
            }
            
            foreach (var x in outline.Elements())
            {
                var o = new Outline();
                ot.Outlines.Add(o);
                TraverseBody(x, o);
            }
        }
    }

    public XElement ToXML()
    {
        var root = new XElement("opml",
            new XAttribute("version", "2.0"),
                new XElement("head",
                    new XElement("title", this.Title),
                    (this.DateCreated.HasValue) ? new XElement("dateCreated", this.DateCreated.Value.ToString("R")) : null,
                    (this.DateModified.HasValue) ? new XElement("dateModified", this.DateModified.Value.ToString("R")) : null,
                    (!string.IsNullOrWhiteSpace(this.OwnerName))? new XElement("ownerName", this.OwnerName) : null,
                    (!string.IsNullOrWhiteSpace(this.OwnerEmail)) ? new XElement("ownerEmail", this.OwnerEmail) : null
                    ));
                    
        var body = new XElement("body");
        foreach(var x in this.Outlines)
        {
            XElement newOutline = new XElement("outline");
            AddRecursiveChild(newOutline, x);
            body.Add(newOutline);
        }

        root.Add(body);

        return root;
    }

    private void AddRecursiveChild(XElement element, Outline o){
        
        element.Add(from y in o.Attributes
                    select new XAttribute(y.Key, y.Value));
        
        foreach(var oo in o.Outlines)
        {
            XElement newOutline = new XElement("outline");
        
            element.Add(newOutline);
            AddRecursiveChild(newOutline, oo);
        }
    }

}

public record Outline
{
    public Dictionary<string, string> Attributes { get; private set; } = new Dictionary<string, string>();

    public List<Outline> Outlines { get; private set; } = new List<Outline>();
}

public record RssSubscription
{
    public string? Title { get; set; }
    public string? OwnerName { get; set; }
    public string? OwnerEmail { get; set; }
    public DateTime? DateCreated { get; set; }
    public DateTime? DateModified { get; set; }
    public List<RssSubscriptionItem> Items { get; set; } = new List<RssSubscriptionItem>();

    /// <summary>
    /// List errors in parsing opml attributes
    /// </summary>
    public List<string> ParsingErrors { get; set; } = new List<string>();

    public RssSubscription(Opml opml)
    {
        Title = opml.Title;
        DateCreated = opml.DateCreated;
        DateModified = opml.DateModified;

        var line = 0;
        foreach (var x in opml.Outlines)
        {
            line++;
            var item = new RssSubscriptionItem();
            foreach (var y in x.Attributes)
            {
                try
                {
                    if (y.Key == "text")
                        item.Text = y.Value;
                    else if (y.Key == "description")
                        item.Description = y.Value;
                    else if (y.Key == "title")
                        item.Title = y.Value;
                    else if (y.Key == "name")
                        item.Name = y.Value;
                    else if (y.Key == "htmlUrl" && !string.IsNullOrWhiteSpace(y.Value))
                        item.HtmlUri = new Uri(y.Value);
                    else if (y.Key == "xmlUrl" && !string.IsNullOrWhiteSpace(y.Value))
                        item.XmlUri = new Uri(y.Value);
                }
                catch (Exception ex)
                {
                    ParsingErrors.Add("Error at line " + line + " in processing attribute " 
                        + y.Key + " with value " + y.Value + " " +  ex.Message);
                }
            }

            Items.Add(item);
        }
    }
}

public record RssSubscriptionItem
{
    public string? Text { get; set; }
    public string? Name { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public Uri? HtmlUri { get; set; }
    public Uri? XmlUri { get; set; }
}