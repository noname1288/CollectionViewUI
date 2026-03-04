#if IOS
using UIKit;
#endif

using Microsoft.Maui.Platform;
using WeatherApp.Models;
using WeatherApp.ViewModels;

namespace WeatherApp.Views;

public partial class ListSomethingPage : ContentPage
{
    private bool _isUserNearBottom = true;
    private const int BottomThreshold = 2;

    private ListSomeThingPageViewModel _viewModel;
    
    public ListSomethingPage(ListSomeThingPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        _viewModel = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _viewModel.ExternalMessageReceived += OnExternalMessageReceived;
        _viewModel.ScrollToBottomRequested += ScrollToBottomRequested;
        
#if IOS
        KeyboardAutoManagerScroll.Disconnect();

        // UIKeyboard.Notifications.ObserveWillShow((sender, args) => {
        //     var keyboardHeight = args.FrameEnd.Height;
        //     MessagesList.TranslationY = keyboardHeight;
        // });
        //
        // UIKeyboard.Notifications.ObserveWillHide((sender, args) => {
        //     MessagesList.TranslationY = 0;
        // });

        UIKit.UIKeyboard.Notifications.ObserveWillShow((sender, args) =>
        {
            var keyboardHeight = args.FrameEnd.Height ;

            MainThread.BeginInvokeOnMainThread(() => { MainGrid.Margin = new Thickness(0, 0, 0, keyboardHeight); });
        });
        
        UIKit.UIKeyboard.Notifications.ObserveWillHide((sender, args) =>
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                MainGrid.Margin = new Thickness(0);
            });
        });

        UIKit.UIKeyboard.Notifications.ObserveDidHide((sender, args) =>
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                MainGrid.Margin = new Thickness(0);
            });
        });

#endif
        Console.WriteLine(DateTime.Now.Ticks);

    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _viewModel.ExternalMessageReceived -= OnExternalMessageReceived;
        _viewModel.ScrollToBottomRequested -= ScrollToBottomRequested;
        
#if IOS
        KeyboardAutoManagerScroll.Connect();
#endif
    }

    private void OnExternalMessageReceived(ChatMessageModel obj)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            if (_isUserNearBottom)
            {
                ScrollToBottom();
            }
            else
            {
                ShowNotification();
            }
        });
    }
    
    private void ScrollToBottomRequested()
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            ScrollToBottom();
            
            HideNotification();
        });
    }

    private void ScrollToBottom()
    {
        if (BindingContext is not ListSomeThingPageViewModel vm)
            return;

        if (vm.ChatMessages.Count == 0)
            return;

        MessagesList.ScrollTo(
            vm.ChatMessages.Count - 1,
            position: ScrollToPosition.End,
            animate: true);
    }

    private void HideNotification()
    {
        NewMessageButton.IsVisible = false;
    }
    
    private void ShowNotification()
    {
        NewMessageButton.IsVisible = true;
    }

    private void MessagesList_Scrolled(object sender, ItemsViewScrolledEventArgs e)
    {
        if (BindingContext is not ListSomeThingPageViewModel vm)
            return;

        var lastVisibleIndex = e.LastVisibleItemIndex;
        var totalCount = vm.ChatMessages.Count;
        
        //if lastestItem is visible
        if (totalCount == 0)
            return;

        // Nếu đang cách cuối <= 2 item → coi như ở cuối
        _isUserNearBottom = lastVisibleIndex >= totalCount - 1 - BottomThreshold;

        if (_isUserNearBottom)
        {
            HideNotification();
        }
    }

    private void OnNewClicked(object? sender, EventArgs e)
    {
        HideNotification();
        
        ScrollToBottom();
    }
    
    private void OnMessagesTapped(object sender, TappedEventArgs e)
    {
        HideKeyboard();
    }

    private void HideKeyboard()
    {
        if (MessageEntry.IsFocused)
        {
            MessageEntry.Unfocus();
        }
    }
}