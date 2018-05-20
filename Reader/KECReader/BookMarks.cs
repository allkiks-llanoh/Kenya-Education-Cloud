using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace KECReader
{
    

    public class BookMark
    {
        public string bookName { get; set; }
        public string chapterName { get; set; }
        public float lastReadPart { get; set; }
        public string fileName { get; set; }
    }

    public class BookmarkSetter
    {
        readonly ApplicationDataContainer localSettings;

        const string bookMark = "BOOKMARK";
        const string lastRead = "LASTREAD";


        public List<BookMark> myBookMarks { get; set; }
        public BookMark lastReadPart { get; set; }

        public BookmarkSetter()
        {
            localSettings = ApplicationData.Current.LocalSettings;
        }

        public List<BookMark> readBookMarks()
        {
            return localSettings.Values[bookMark] as List<BookMark>;
        }

        public void writeBookMark(BookMark bkm)
        {
            localSettings.Values[bookMark] = bkm;
        }

        public void writeLastRead()
        {
            localSettings.Values[lastRead] = lastReadPart;
        }


        public void readLastRead()
        {
            lastReadPart =  localSettings.Values[lastRead] as BookMark;            
        }
    }
}
