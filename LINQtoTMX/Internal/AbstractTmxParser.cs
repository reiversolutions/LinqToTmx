using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LINQtoTMX.Internal
{
    internal abstract class AbstractTmxParser
    {
        abstract internal TmxFile ParseFile(FileInfo file);
    }
}
