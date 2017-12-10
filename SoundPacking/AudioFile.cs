using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundPacking
{
    class AudioFile
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
    }
}
