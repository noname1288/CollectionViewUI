using WeatherApp.Models;

namespace WeatherApp.Views.Templates;

public class MessageTemplateSelector : DataTemplateSelector
{
    public DataTemplate SenderDataTemplate { get; set; }
    public DataTemplate ReceiverDataTemplate { get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        var message = (ChatMessageModel)item;

        if (message.IsFromMe)
        {
            return SenderDataTemplate;
        }
        
        return ReceiverDataTemplate;
    }
}
