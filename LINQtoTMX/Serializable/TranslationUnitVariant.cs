#pragma warning disable 1591
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace LINQtoTMX.Serializable
{
    [Serializable]
    public class TranslationUnitVariant
    {
        [XmlAttribute("xml:lang")]
        public string Language { get; set; }

        [XmlElement("seg")]
        public string Segment { get; set; }
    }
}
#pragma warning restore 1591