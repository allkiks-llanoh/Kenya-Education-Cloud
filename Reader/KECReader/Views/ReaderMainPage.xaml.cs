using EpubSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace KECReader.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ReaderMainPage : Page
    {
        ICollection<EpubChapter> chapters;
        ICollection<EpubTextFile> html;

        BookmarkSetter bkMSt = new BookmarkSetter();

        EpubBook book;

        public ReaderMainPage()
        {
            this.InitializeComponent();
            bkMSt.readLastRead();

            Loaded += async (s, e) => {
                //Try to read an epub here
                //string epubName = "ms-appx:///Assets/gravitybook1.epub";

                string epubName = "ms-appx:///Assets/digital_fortress.epub";

                try
                {                    
                    book = EpubReader.Read(await readFile(epubName));
                    // Read metadata
                    string title = book.Title;
                    string[] authors = book.Authors.ToArray();
                    var cover = await imageFromByte(book.CoverImage);

                    txTitle.Text = title;
                    if(authors.Length > 0)
                    {
                        txTitle.Text += " By ";
                        for (int x = 0; x < authors.Length; x++)
                            txTitle.Text += authors[x];
                    }

                    imgCover.Source = cover;

                    // Get table of contents
                    chapters = book.TableOfContents;
                    html = book.Resources.Html;

                    lsChapters.ItemsSource = chapters;
                    flpPages.ItemsSource = html;
                    //flpPages.SelectedIndex = 2;

                    try
                    {
                        lsChapters.SelectedIndex = 3;
                        if(bkMSt.lastReadPart != null){

                            string fileName = bkMSt.lastReadPart.fileName;
                            var htmlBook = html.Where(h => h.FileName == fileName).FirstOrDefault();
                            //Get the id for that
                            if (htmlBook != null)
                            {
                                flpPages.SelectedItem = htmlBook;
                            }

                            try
                            {
                                string flName = bkMSt.lastReadPart.chapterName;
                                var chapter = chapters.Where(c => c.Title == flName).FirstOrDefault();
                                if (chapter != null)
                                    lsChapters.SelectedItem = chapter;
                            }
                            catch (Exception r)
                            { Debug.WriteLine(r.Message); }

                        }
                    }catch(Exception r) { Debug.WriteLine(r.Message); }
                }
                catch (Exception r)
                {
                    Debug.WriteLine(r.Message);
                    Debug.WriteLine(r.InnerException.Message);
                }
            };

            flpPages.SelectionChanged += (s, e) => {
                try
                {
                    string fileName = html.ElementAt(flpPages.SelectedIndex).FileName;
                    var chapter = chapters.Where(c => c.FileName == fileName).FirstOrDefault();                    
                    if (chapter != null)
                        lsChapters.SelectedItem = chapter;
                }
                catch(Exception r)
                { Debug.WriteLine(r.Message); }
            };

            lsChapters.SelectionChanged += (s, e) => {
                //get selected item
                
                string fileName = chapters.ElementAt(lsChapters.SelectedIndex).FileName;
                var htmlBook = html.Where(h => h.FileName == fileName).FirstOrDefault();
                //Get the id for that
                if (htmlBook != null)
                    flpPages.SelectedItem = htmlBook;                
            };

            Unloaded += (s, e) => {
                var lstRead = new BookMark() {
                    bookName = book.Title,
                    chapterName = ((EpubChapter)lsChapters.SelectedItem).Title,
                    fileName = ((EpubFile)flpPages.SelectedItem).FileName                    
                };

                bkMSt.lastReadPart = lstRead;
                bkMSt.writeLastRead();
            };
        }

        async public Task<byte[]> readFile(string fileName)
        {
            try
            {
                Uri appUri = new Uri(fileName);//File name should be prefixed with 'ms-appx:///Assets/*
                StorageFile epubFile = StorageFile.GetFileFromApplicationUriAsync(appUri).AsTask().ConfigureAwait(false).GetAwaiter().GetResult();
                var stream = await epubFile.OpenStreamForReadAsync();
                var bytes = new byte[(int)stream.Length];
                stream.Read(bytes, 0, (int)stream.Length);

                return bytes;
            }
            catch (Exception r)
            {
                Debug.WriteLine(r.Message);
                return null;
            }
        }

        public async static Task<BitmapImage> imageFromByte(Byte[] bytes)
        {
            BitmapImage image = new BitmapImage();
            using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
            {
                await stream.WriteAsync(bytes.AsBuffer());
                stream.Seek(0);
                await image.SetSourceAsync(stream);
            }
            return image;
        }
    }
}
