using HRMApp.Model.Notification;
using System.Diagnostics;

namespace HRMApp.View.Shared;

public partial class TopNotificationView : ContentView
{
    private SignalRNotification _notification;
    private TaskCompletionSource<bool> _userActionTcs;

    public TopNotificationView()
    {
        InitializeComponent();

        // Set important properties for proper layering
        ZIndex = 10000;
        VerticalOptions = LayoutOptions.Start;
        HorizontalOptions = LayoutOptions.Fill;
    }


    public async Task<bool> ShowNotificationAsync(SignalRNotification notification, int autoHideDelayMs = 5000)
    {
        _notification = notification;
        _userActionTcs = new TaskCompletionSource<bool>();

        // ✅ FIX: Use Content property instead of Body
        TitleLabel.Text = notification.Title ?? "Thông báo mới";
        BodyLabel.Text = notification.Content ?? notification.Body ?? ""; // Try Content first, then Body

        Debug.WriteLine($"🎯 TopNotificationView showing: '{notification.Title}' - '{notification.Content}'");

        // Show the notification with slide down animation
        await ShowWithAnimation();

        // Auto-hide after delay
        _ = Task.Run(async () =>
        {
            await Task.Delay(autoHideDelayMs);
            if (!_userActionTcs.Task.IsCompleted)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await HideWithAnimation();
                    _userActionTcs.TrySetResult(false);
                });
            }
        });

        return await _userActionTcs.Task;
    }

    private async Task ShowWithAnimation()
    {
        // Reset position and make visible
        TranslationY = -120;
        IsVisible = true;
        Opacity = 0;

        // Animate slide down and fade in
        var slideTask = this.TranslateTo(0, 0, 300, Easing.CubicOut);
        var fadeTask = this.FadeTo(1, 300);

        await Task.WhenAll(slideTask, fadeTask);
    }

    public async Task HideWithAnimation()
    {
        // Animate slide up and fade out
        var slideTask = this.TranslateTo(0, -120, 250, Easing.CubicIn);
        var fadeTask = this.FadeTo(0, 250);

        await Task.WhenAll(slideTask, fadeTask);

        IsVisible = false;
    }

    private async void OnNotificationTapped(object sender, EventArgs e)
    {
        await HideWithAnimation();
        _userActionTcs.TrySetResult(true);
    }

    // Xử lý gesture vuốt lên để tắt thông báo
    private async void OnSwipeUp(object sender, SwipedEventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("👆 Người dùng vuốt lên để tắt thông báo");
        await HideWithAnimation();
        _userActionTcs.TrySetResult(false);
    }

   
}