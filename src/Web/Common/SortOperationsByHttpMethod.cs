using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;

namespace Finisher.Web.Common;

public class SortOperationsByHttpMethod : IDocumentProcessor
{
    public void Process(DocumentProcessorContext context)
    {
        var sortedPaths = context.Document.Paths
            .OrderBy(path => GetHttpOrder(path.Value))
            .ToDictionary(p => p.Key, p => p.Value);

        context.Document.Paths.Clear();
        foreach (var item in sortedPaths)
        {
            context.Document.Paths[item.Key] = item.Value;
        }
    }

    private static int GetHttpOrder(NSwag.OpenApiPathItem item)
    {
        // Order: GET = 1, POST = 2, PUT = 3, DELETE = 4
        if (item.ActualPathItem.ContainsKey("get"))
        {
            return 1;
        }

        if (item.ActualPathItem.ContainsKey("post"))
        {
            return 2;
        }

        if (item.ActualPathItem.ContainsKey("put"))
        {
            return 3;
        }

        if (item.ActualPathItem.ContainsKey("delete"))
        {
            return 4;
        }

        return 99;
    }
}
