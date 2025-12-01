using Microsoft.Maui.Controls;

namespace HRMApp.View;

public partial class NewsDetailPage : ContentPage
{
    public NewsDetailPage(NewsItem news)
    {
        InitializeComponent();
        TitleLabel.Text = news.Title;
        ContentLabel.Text = news.Content;
        ThumbnailImage.Source = news.Thumbnail;
    }
}
