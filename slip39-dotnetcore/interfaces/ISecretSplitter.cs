using System.Collections.ObjectModel;
namespace superenrico.slip39_dotnetcore.interfaces;


public interface ISecretSplitter
{
    ReadOnlyCollection<byte[]> SplitSecret(int threshold, int totalShares, string secret);
}