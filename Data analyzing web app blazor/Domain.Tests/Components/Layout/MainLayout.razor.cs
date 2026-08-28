using MudBlazor;

namespace Domain.Tests.Components.Layout
{
    public partial class MainLayout
    {
        private bool _isDarkMode = true;
        private static MudTheme _theme = new()
        {
            PaletteLight = new PaletteLight()
            {
                AppbarBackground = Colors.Gray.Lighten4,
                Primary = Colors.Gray.Darken3,
                Secondary = Colors.Gray.Darken4,
                Tertiary = Colors.Gray.Darken4
            },
            PaletteDark = new PaletteDark()
            {
                Primary = Colors.Gray.Darken3,
                Secondary = Colors.Gray.Darken4,
                Tertiary = Colors.Gray.Lighten2
            }
        };
    }
}
