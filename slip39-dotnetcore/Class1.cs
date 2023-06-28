using superenrico.slip39_dotnetcore.model;
using System.Collections.ObjectModel;
namespace superenrico.slip39_dotnetcore;
public class Class1
{
    public ReadOnlyCollection<byte[]> hello  => (new superenrico.slip39_dotnetcore.model.SecretSlitter().SplitSecret(2,3, "0123456789abcdef0123456789abcdef"));
}
