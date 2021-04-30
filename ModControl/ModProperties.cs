using System.Xml.Linq;

namespace ModControl
{
    internal class ModProperties
    {
        public string author;
        public string title;
        public string version;
        public string icon;
        public string filename;
        public string desc;

        public ModProperties(string filename, string title, string author, string version, string icon, string desc)
        {
            this.title = title;
            this.author = author;
            this.version = version;
            this.icon = icon;
            this.filename = filename;
            this.desc = desc;
        }
    }
}