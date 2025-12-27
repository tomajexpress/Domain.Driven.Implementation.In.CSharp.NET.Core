using System.Collections.Generic;
namespace SharedKernel.Models;

public class QueryResult<T>
{
    public IEnumerable<T> Entities { get; protected set; }
    public int TotalCount { get; protected set; }

    public QueryResult(IEnumerable<T> entities, int totalCount)
    {
        TotalCount = totalCount;

        Entities = entities;
    }
}
