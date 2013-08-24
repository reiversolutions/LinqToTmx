#pragma warning disable 1591
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace LINQtoTMX.Serializable
{
    [Serializable]
    public class Body
    {
        [XmlElement("tu")]
        public List<TranslationUnit> TranslationUnits { get; set; }
    }
}
#pragma warning restore 1591