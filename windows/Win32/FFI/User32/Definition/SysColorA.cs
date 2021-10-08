namespace Win32.FFI.User32.Definition
{
    public enum SysColorA
    {
        // Dark shadow for three-dimensional display elements.
        COLOR_3DDKSHADOW = 21,
        // Face color for three-dimensional display elements and for dialog box backgrounds.
        COLOR_3DFACE = 15,
        // Highlight color for three-dimensional display elements (for edges facing the light source.)
        COLOR_3DHIGHLIGHT = 20,
        // Highlight color for three-dimensional display elements (for edges facing the light source.)
        COLOR_3DHILIGHT = 20,
        // Light color for three-dimensional display elements (for edges facing the light source.)
        COLOR_3DLIGHT = 22,
        // Shadow color for three-dimensional display elements (for edges facing away from the light source).
        COLOR_3DSHADOW = 16,
        // Active window border.
        COLOR_ACTIVEBORDER = 10,
        // Active window title bar. The associated foreground color is COLOR_CAPTIONTEXT. Specifies the left side color in the color gradient of an active window's title bar if the gradient effect is enabled.
        COLOR_ACTIVECAPTION = 2,

        // Background color of multiple document interface (MDI) applications.
        COLOR_APPWORKSPACE = 12,
        // Desktop.
        COLOR_BACKGROUND = 1,
        // Face color for three-dimensional display elements and for dialog box backgrounds. The associated foreground color is COLOR_BTNTEXT.
        COLOR_BTNFACE = 15,
        // Highlight color for three-dimensional display elements (for edges facing the light source.)
        COLOR_BTNHIGHLIGHT = 20,
        // Highlight color for three-dimensional display elements (for edges facing the light source.)
        COLOR_BTNHILIGHT = 20,
        // Shadow color for three-dimensional display elements (for edges facing away from the light source).
        COLOR_BTNSHADOW = 16,
        // Text on push buttons. The associated background color is COLOR_BTNFACE.
        COLOR_BTNTEXT = 18,
        // Text in caption, size box, and scroll bar arrow box. The associated background color is COLOR_ACTIVECAPTION.
        COLOR_CAPTIONTEXT = 9,
        // Desktop.
        COLOR_DESKTOP = 1,
        // Right side color in the color gradient of an active window's title bar. COLOR_ACTIVECAPTION specifies the left side color. Use SPI_GETGRADIENTCAPTIONS with the SystemParametersInfo function to determine whether the gradient effect is enabled.
        COLOR_GRADIENTACTIVECAPTION = 27,
        // Right side color in the color gradient of an inactive window's title bar. COLOR_INACTIVECAPTION specifies the left side color.
        COLOR_GRADIENTINACTIVECAPTION = 28,
        // Grayed (disabled) text. This color is set to 0 if the current display driver does not support a solid gray color.
        COLOR_GRAYTEXT = 17,
        // Item(s) selected in a control. The associated foreground color is COLOR_HIGHLIGHTTEXT.
        COLOR_HIGHLIGHT = 13,
        // Text of item(s) selected in a control. The associated background color is COLOR_HIGHLIGHT.
        COLOR_HIGHLIGHTTEXT = 14,
        // Color for a hyperlink or hot-tracked item. The associated background color is COLOR_WINDOW.
        COLOR_HOTLIGHT = 26,
        // Inactive window border.
        COLOR_INACTIVEBORDER = 11,
        // Inactive window caption. The associated foreground color is COLOR_INACTIVECAPTIONTEXT. Specifies the left side color in the color gradient of an inactive window's title bar if the gradient effect is enabled.
        COLOR_INACTIVECAPTION = 3,

        // Color of text in an inactive caption. The associated background color is COLOR_INACTIVECAPTION.
        COLOR_INACTIVECAPTIONTEXT = 19,
        // Background color for tooltip controls. The associated foreground color is COLOR_INFOTEXT.
        COLOR_INFOBK = 24,
        // Text color for tooltip controls. The associated background color is COLOR_INFOBK.
        COLOR_INFOTEXT = 23,
        // Menu background. The associated foreground color is COLOR_MENUTEXT.
        COLOR_MENU = 4,
        // The color used to highlight menu items when the menu appears as a flat menu (see SystemParametersInfo). The highlighted menu item is outlined with COLOR_HIGHLIGHT. Windows 2000:  This value is not supported.
        COLOR_MENUHILIGHT = 29,

        // The background color for the menu bar when menus appear as flat menus (see SystemParametersInfo). However, COLOR_MENU continues to specify the background color of the menu popup. Windows 2000:  This value is not supported.
        COLOR_MENUBAR = 30,

        // Text in menus. The associated background color is COLOR_MENU.
        COLOR_MENUTEXT = 7,
        // Scroll bar gray area.
        COLOR_SCROLLBAR = 0,
        // Window background. The associated foreground colors are COLOR_WINDOWTEXT and COLOR_HOTLITE.
        COLOR_WINDOW = 5,
        // Window frame.
        COLOR_WINDOWFRAME = 6,
        // Text in windows. The associated background color is COLOR_WINDOW.
        COLOR_WINDOWTEXT = 8,
    }
}