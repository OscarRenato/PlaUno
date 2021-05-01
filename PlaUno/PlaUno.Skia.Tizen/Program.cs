using Tizen.Applications;
using Uno.UI.Runtime.Skia;

namespace PlaUno.Skia.Tizen
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new TizenHost(() => new PlaUno.App(), args);
            host.Run();
        }
    }
}
