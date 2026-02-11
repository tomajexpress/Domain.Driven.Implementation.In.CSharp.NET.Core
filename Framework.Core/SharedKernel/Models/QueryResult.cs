namespace SharedKernel.Models;

public record QueryResult<T>
{
    public IEnumerable<T> Entities { get; protected set; }
    public int TotalCount { get; protected set; }

    public QueryResult(IEnumerable<T> entities, int totalCount)
    {
        TotalCount = totalCount;

        Entities = entities;
    }
}
