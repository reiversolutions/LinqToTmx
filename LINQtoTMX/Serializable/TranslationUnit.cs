#pragma warning disable 1591
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace LINQtoTMX.Serializable
{
    [Serializable]
    public class TranslationUnit
    {
        [XmlAttribute("tuid")]
        public string TranslationUnitId { get; set; }

        [XmlElement("tuv")]
        public List<TranslationUnitVariant> TranslationUnitVariants { get; set; }
    }
}
#pragma warning restore 1591