namespace SportData.Data.Models.Converters;

using HtmlAgilityPack;

using SportData.Data.Models.Cache;

public class ConvertOptions
{
    public HtmlDocument HtmlDocument { get; set; }

    //public IOrderedEnumerable<Document> Documents { get; set; }

    public GameCache Game { get; set; }

    public DisciplineCache Discipline { get; set; }

    public EventCache Event { get; set; }

    //public TableModel StandingTable { get; set; }

    //public IList<TableModel> Tables { get; set; }

    public List<TableModel> Tables { get; set; }

    public List<DocumentModel> Documents { get; set; }
}