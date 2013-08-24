using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LINQtoTMX.Internal
{
    internal abstract class AbstractLanguageRepository
    {
        abstract internal List<TmxFile> Get(DirectoryInfo languageRepositoryRoot, string languageCode);

        abstract internal List<TmxFile> Get(DirectoryInfo languageRepositoryRoot);

        abstract internal bool Add(TmxFile file);

        abstract internal bool Update(TmxFile file);

        abstract internal bool Delete(TmxFile file);
    }
}