using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WeatherApp.Models;

namespace WeatherApp.ViewModels;

public partial class ListSomeThingPageViewModel : ObservableObject
{
    [ObservableProperty] private string textEntry;
    
    public ObservableCollection<ChatMessageModel> ChatMessages { get;  }    
        = new ObservableCollection<ChatMessageModel>();
    
    public event Action<ChatMessageModel> ExternalMessageReceived;
    public event Action ScrollToBottomRequested;

    public ListSomeThingPageViewModel()
    {
        GenerateFakeMessages();
    }

    [RelayCommand]
    public void AddMessage()
    {
        if (string.IsNullOrEmpty(TextEntry))
            return;
        
        ChatMessages.Add(new ChatMessageModel
        {
            Text = TextEntry,
            IsFromMe = true,
        });
        
        //reset state
        TextEntry = string.Empty;
        
        //scroll To end 
        ScrollToBottomRequested.Invoke();
    }

    [RelayCommand]
    public void AddPartnerMessage()
    {
        var newMessage = new ChatMessageModel
        {
            Text = "Hello sender",
            IsFromMe = false,
        };
        
        ChatMessages.Add(newMessage);
        
        ExternalMessageReceived?.Invoke(newMessage);
    }
    
    private void GenerateFakeMessages()
    {
        ChatMessages.Add(new ChatMessageModel { Text = "Hello 👋", IsFromMe = false });
        ChatMessages.Add(new ChatMessageModel { Text = "Hi, how are you?", IsFromMe = true });
        ChatMessages.Add(new ChatMessageModel { Text = "I'm good, thanks!", IsFromMe = false });
        ChatMessages.Add(new ChatMessageModel { Text = "Are we meeting today?", IsFromMe = false });
        ChatMessages.Add(new ChatMessageModel { Text = "Yes, at 3PM.", IsFromMe = true });
        ChatMessages.Add(new ChatMessageModel { Text = "Great 👍", IsFromMe = false });
        ChatMessages.Add(new ChatMessageModel { Text = "Don't forget the documents.", IsFromMe = false });
        ChatMessages.Add(new ChatMessageModel { Text = "Already prepared.", IsFromMe = true });
        ChatMessages.Add(new ChatMessageModel { Text = "Nice!", IsFromMe = false });
        ChatMessages.Add(new ChatMessageModel { Text = "See you soon.", IsFromMe = true });

        ChatMessages.Add(new ChatMessageModel { Text = "On my way 🚗", IsFromMe = false });
        ChatMessages.Add(new ChatMessageModel { Text = "Traffic is heavy 😅", IsFromMe = false });
        ChatMessages.Add(new ChatMessageModel { Text = "No worries.", IsFromMe = true });
        ChatMessages.Add(new ChatMessageModel { Text = "Take your time.", IsFromMe = true });
        ChatMessages.Add(new ChatMessageModel { Text = "Almost there.", IsFromMe = false });
        ChatMessages.Add(new ChatMessageModel { Text = "Okay.", IsFromMe = true });
        ChatMessages.Add(new ChatMessageModel { Text = "Just arrived.", IsFromMe = false });
        ChatMessages.Add(new ChatMessageModel { Text = "Coming down.", IsFromMe = true });
        ChatMessages.Add(new ChatMessageModel { Text = "See you 👋", IsFromMe = false });
        ChatMessages.Add(new ChatMessageModel { Text = "See you!", IsFromMe = true });
    }
}