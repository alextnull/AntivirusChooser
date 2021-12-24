using System;
using System.Xml.Serialization;

namespace AntivirusChooser.Models
{
    [Serializable]
    public class Node
    {
        [XmlAttribute]
        public int Id { get; set; }
        [XmlAttribute]
        public NodeType Type { get; set; }
        [XmlElement]
        public Answer Yes { get; set; }
        [XmlElement]
        public Answer No { get; set; }
        [XmlElement]
        public string Text { get; set; }

        public Node()
        {

        }
    }
}
