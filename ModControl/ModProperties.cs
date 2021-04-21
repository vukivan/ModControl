namespace ModControl
{
    internal class ModProperties
    {
        public readonly string author = null;
        public readonly string title = null;
        public readonly string version = null;
        public readonly string icon = null;
        public readonly string filename = null;

        public ModProperties(string filename, string title, string author, string version, string icon)
        {
            this.title = title;
            this.author = author;
            this.version = version;
            this.icon = icon;
            this.filename = filename;
        }
    }
}