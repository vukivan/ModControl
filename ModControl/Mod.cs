﻿using System.Windows.Forms;

namespace ModControl
{
    internal class Mod
    {
        private readonly string fileName;
        private readonly string modTitle;
        private readonly string modAuthor;
        private readonly string modIcon;
        private readonly string modVersion;
        private volatile string Status;

        public string GetFileName()
        {
            return this.fileName;
        }

        public string GetModTitle ()
        {
            return this.modTitle;
        }

        public string GetModAuthor()
        {
            return this.modAuthor;
        }

        public string GetModVersion()
        {
            return this.modVersion;
        }

        public string GetModIcon()
        {
            return this.modIcon;
        }

        public ModStatus GetModStatus()
        {
            return Status switch
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
            return Status;
            
        }

        public void SetModStatus(ModStatus status)
        {
            Status = status switch
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
            this.fileName = Properties.filename;
            this.modTitle = Properties.title;
            this.modAuthor = Properties.author;
            this.modVersion = Properties.version;
            this.modIcon = Properties.icon;
            this.SetModStatus(ModStatus.Inactive);
        }
        
    }
}
