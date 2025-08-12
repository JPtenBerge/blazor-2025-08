using System.Reflection;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace DemoProject.Components;

public partial class Autocompleter<T> : ComponentBase
{
    public string Query { get; set; }
    [Parameter] public List<T> Data { get; set; } = null!;
    public List<T>? Suggestions { get; set; }
    public int? HighlightedSuggestionIndex { get; set; }

    [Parameter] public RenderFragment<T> ItemTemplate { get; set; }

    public void Autocomplete()
    {
        Suggestions = new();

        if (string.IsNullOrEmpty(Query))
        {
            return;
        }

        foreach (var item in Data)
        {
            // reflection - "dat duistere stuk code"

            // code die @ runtime introspectie op je code
            var props = item.GetType().GetProperties().Where(x => x.PropertyType == typeof(string));
            foreach (var prop in props)
            {
                var value = prop.GetValue(item, null) as string;
                if (value.Contains(Query, StringComparison.InvariantCultureIgnoreCase))
                {
                    Suggestions.Add(item);
                    break;
                }
            }
        }
    }

    public void HandleKeydown(KeyboardEventArgs args)
    {
        if (args.Key == "ArrowDown")
        {
            Next();
        }
    }

    public void Next()
    {
        if (HighlightedSuggestionIndex != null)
        {
            HighlightedSuggestionIndex = (HighlightedSuggestionIndex + 1) % Suggestions.Count;
            return;
        }

        HighlightedSuggestionIndex = 0;
    }
}