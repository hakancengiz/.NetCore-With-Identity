namespace Core.Utilities.Results
{
    public interface IDataResult<out TEntity>:IResult
    {
        TEntity Data { get; }
    }
}
