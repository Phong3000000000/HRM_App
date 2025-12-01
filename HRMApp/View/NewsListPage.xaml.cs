using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;

namespace HRMApp.View;

public partial class NewsListPage : ContentPage
{
    public ObservableCollection<NewsItem> NewsList { get; set; }

    public NewsListPage()
    {
        InitializeComponent();

        NewsList = new ObservableCollection<NewsItem>
        {
            new NewsItem
            {
                Title = "Chế độ nghỉ lễ 2022",
                Thumbnail = "https://cdn-icons-png.flaticon.com/512/747/747310.png",
                Content = "Theo Điều 112 Bộ luật Lao động 2019...",
                TimeAgo = "khoảng một phút trước"
            },
            new NewsItem
            {
                Title = "Sổ tay phòng chống Covid-19 tại cơ sở",
                Thumbnail = "https://cdn-icons-png.flaticon.com/512/1077/1077063.png",
                Content = "Các quy định về phòng dịch Covid-19...",
                TimeAgo = "9 tháng trước"
            }
        };

        BindingContext = this;
    }

    private async void OnNewsTapped(object sender, TappedEventArgs e)
    {
        if (sender is Frame frame && frame.BindingContext is NewsItem selected)
        {
            await Navigation.PushAsync(new NewsDetailPage(selected));
        }
    }

}

public class NewsItem
{
    public string Title { get; set; }
    public string Thumbnail { get; set; }
    public string Content { get; set; }
    public string TimeAgo { get; set; }
}
