using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundPacking
{
    class Folder : IComparable<Folder>
    {
        public int remaincap;
        public LinkedList<AudioFile> files;

        public Folder(int remaincap)
        {
            this.remaincap = remaincap;
            this.files = new LinkedList<AudioFile>();
        }
        public void addFile(AudioFile file)
        {
            files.AddLast(file);
            remaincap = remaincap - (int)file.Duration.TotalSeconds;
        }

        public int CompareTo(Folder other)
        {
            if (this.remaincap > other.remaincap)
                return 1;
            else if (this.remaincap == other.remaincap)
                return 0;
            else
                return -1;
        }
    }
}
