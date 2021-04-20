using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ModControl
{
    class Mod
    {
        private readonly string FileName;
        private readonly string ModTitle;
        private readonly string ModAuthor;
        private readonly string ModIcon;
        private readonly string ModVersion;

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

        public Mod(string ModStorageDirectory, string FileName)
        {
            this.FileName = FileName;
            XDocument modDescXml;
            //Open Zip, find actual mod name and author
            using ZipArchive archive = ZipFile.Open(ModStorageDirectory+"/"+ FileName, ZipArchiveMode.Read);
            ZipArchiveEntry entry = archive.GetEntry("modDesc.xml");
            if (entry != null )
            {
                //System.Diagnostics.Debug.WriteLine("Loading mod:" + FileName);
                using (StreamReader reader = new StreamReader(entry.Open()))
                modDescXml = XDocument.Load(reader);
                this.ModAuthor = modDescXml.Element("modDesc").Element("author").Value.Trim();
                XElement title = modDescXml.Element("modDesc").Element("title").Element("en");
                if (title != null)
                {
                    this.ModTitle = title.Value.Trim();
                }
                else
                {
                    this.ModTitle = modDescXml.Element("modDesc").Element("title").Value.Trim();
                }
                this.ModIcon = modDescXml.Element("modDesc").Element("iconFilename").Value.Trim();
                this.ModVersion = modDescXml.Element("modDesc").Element("version").Value.Trim();
            }
            else
            {
                // Report Error with Filename and reason "modDesc.xml" not found.
                MessageBox.Show(ModStorageDirectory + "/" +  FileName + ": modDesc.xml not Found", "Mod Storage Import Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new System.ArgumentNullException("modDesc.xml", "Cannot be null.");
            }
            
        }
        
    }
}
