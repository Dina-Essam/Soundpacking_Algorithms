using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundPacking
{
    class Folder
    {
        public int remaincap;
        public List<AudioFile> files;

        public Folder(int remaincap)
        {
            this.remaincap = remaincap;
            this.files = new List<AudioFile>();
        }
        public void addFile(AudioFile file)
        {
            files.Add(file);
            remaincap = remaincap - (int)file.Duration.TotalSeconds;
        }
    }
}
