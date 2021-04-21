namespace ModControl
{
    internal class Mod
    {
        private readonly string FileName;
        private readonly string ModTitle;
        private readonly string ModAuthor;
        private readonly string ModIcon;
        private readonly string ModVersion;
        private string Status;

        public string GetFileName()
        {
            return this.FileName;
        }

        public string GetModTitle ()
        {
            return this.ModTitle;
        }

        public string GetModAuthor()
        {
            return this.ModAuthor;
        }

        public string GetModVersion()
        {
            return this.ModVersion;
        }

        public ModStatus GetModStatus()
        {
            return this.Status switch
            {
                "Inactive" => ModStatus.Inactive,
                "Active" => ModStatus.Active,
                "Backup" => ModStatus.Backup,
                "New" => ModStatus.New,
                "Update" => ModStatus.Update,
                _ => ModStatus.Unknown,
            };
        }

        public string GetModStatusString()
        {
            return this.Status;
            
        }

        public void SetModStatus(ModStatus status)
        {
            this.Status = status switch
            {
                ModStatus.Inactive => "Inactive",
                ModStatus.Active => "Active",
                ModStatus.Backup => "Backup",
                ModStatus.New => "New",
                ModStatus.Update => "Update",
                _ => "Unknown",
            };
        }

        public Mod(ModProperties Properties)
        {
            this.FileName = Properties.filename;
            this.ModTitle = Properties.title;
            this.ModAuthor = Properties.author;
            this.ModVersion = Properties.version;
            this.ModIcon = Properties.icon;
            this.SetModStatus(ModStatus.Inactive);
        }
        
    }
}
