namespace ContainerSurveyMaui.Services;

public static class PageTransitionExtensions
{
    public static async Task FadeIn(this VisualElement element, uint length = 250)
    {
        await element.FadeTo(1, length);
    }

    public static async Task FadeOut(this VisualElement element, uint length = 250)
    {
        await element.FadeTo(0, length);
    }
}
