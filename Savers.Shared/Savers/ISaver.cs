namespace Savers.Shared.Savers;

public interface ISaver
{
    SaverType SaverType { get; }

    void Activate(string location);
    void Deactivate();
}