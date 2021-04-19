using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModControl
{
    class Mod
    {
        private readonly string FileName;
        private readonly string ModName;
        private readonly string ModAuthor;

        public string GetFileName()
        {
            return this.FileName;
        }

        public Mod(string FileName)
        {
            this.FileName = FileName;
            //Open Zip, find actual mod name and author
        }
        
    }
}
