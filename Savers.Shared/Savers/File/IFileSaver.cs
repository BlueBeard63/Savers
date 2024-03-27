namespace Savers.Shared.Savers.File;

public interface IFileSaver<T> : ISaver
{
    T SaverItem { get; set; }
}