using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundPacking
{
    class AudioFile : IComparable<AudioFile>
    {
        //Audio File class from which the arrays will be created to hold 2 data members name & Duaration

        public string FileName;
        public TimeSpan Duration;

        public void SetFileName(string FileName)
        {
            this.FileName = FileName;
        }

        public string GetFileName()
        {
            return this.FileName;
        }

        public void SetDuration(TimeSpan Duration)
        {
            this.Duration = Duration;
        }

        public TimeSpan GetDuation()
        {
            return this.Duration;
        }

    
        int IComparable<AudioFile>.CompareTo(AudioFile other)
        {
            if (this.Duration.TotalSeconds > other.Duration.TotalSeconds)
                return 1;
            else if (this.Duration.TotalSeconds == other.Duration.TotalSeconds)
                return 0;
            else
                return -1;
        }
    }
}
