using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPFApp.Blog___News.BlogNewsData;

namespace WPFApp.Blog___News
{
    /// <summary>
    /// Interaction logic for CreateBlogNews.xaml
    /// </summary>
    /// 
    public partial class CreateBlogNews : Window
    {
        private readonly BlogNewsService _blogNewsService;
        private readonly IBlogNewsData _blogNewsData;

        public CreateBlogNews(BlogNewsService blogNewsService, IBlogNewsData blogNewsData)
        {
            InitializeComponent();
            _blogNewsService = blogNewsService;
            _blogNewsData = blogNewsData;

        }

        private void LoadBlogNews()
        {
            var blogNews = _blogNewsService.Get(_blogNewsData.blogNewsID);
            tbTitle.Text = blogNews.Title;
            rtbContent.Document.Blocks.Add(new Paragraph(new Run(blogNews.Content)));
            tbCategory.Text = blogNews.Category;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var title = tbTitle.Text;
            var content = GetRichTextBoxContents(rtbContent);
            var category = tbCategory.Text; 

            var blogNews = new BussinessObjects.BlogNews
            {
                Title = title,
                Content = content,
                PostDate = DateOnly.FromDateTime(DateTime.Now)
                Category = category
            };

            _blogNewsService.Update(blogNews);
        }

        private string GetRichTextBoxContents(RichTextBox rtb)
        {
            TextRange textRange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            return textRange.Text;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tbTitle.Text = "";
            rtbContent.Document.Blocks.Clear();
            tbCategory.Text = "";
        }
    }
}
