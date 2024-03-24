
using PdfSharpCore.Fonts;

namespace ConsoleApp4;

public class CustomFontResolver : IFontResolver
{
    private Dictionary<string, byte[]> fonts = new Dictionary<string, byte[]>();
    private string fontPath = "C:\\Users\\User\\RiderProjects\\ConsoleApp4\\ConsoleApp4\\Fonts";

    public CustomFontResolver()
    {
        // Load the Arial font from a file or embedded resource
        LoadFont("Arial", Path.Combine(fontPath, "arial.ttf"));
        LoadFont("Arial Bold", Path.Combine(fontPath, "arialbd.ttf"));
        LoadFont("Arial Italic", Path.Combine(fontPath, "ariali.ttf"));
        LoadFont("Arial Bold Italic", Path.Combine(fontPath, "arialbi.ttf"));
    }

    private void LoadFont(string faceName, string fontPath)
    {
        byte[] fontData = File.ReadAllBytes(fontPath);
        fonts[faceName] = fontData;
    }

    public FontResolverInfo? ResolveTypeface(string familyName, bool isBold, bool isItalic)
    {
        string faceName = GetFaceName(familyName, isBold, isItalic);
        if (fonts.ContainsKey(faceName))
            return new FontResolverInfo(faceName);
        return null;
    }

    public byte[]? GetFont(string faceName)
    {
        if (fonts.ContainsKey(faceName))
            return fonts[faceName];
        return null;
    }

    public string DefaultFontName => "Arial";

    private string GetFaceName(string familyName, bool isBold, bool isItalic)
    {
        string faceName = familyName;
        if (isBold && isItalic)
            faceName += " Bold Italic";
        else if (isBold)
            faceName += " Bold";
        else if (isItalic)
            faceName += " Italic";
        return faceName;
    }
}

//
// public class CustomFontResolver : IFontResolver
// {
//     private readonly string fontPath;
//
//     public CustomFontResolver(string fontPath)
//     {
//         this.fontPath = fontPath;
//     }
//
//     public FontResolverInfo? ResolveTypeface(string familyName, bool isBold, bool isItalic)
//     {
//         // Construct the font file path based on family name and style
//         string fontFileName = $"{familyName}{(isBold ? " Bold" : "")}{(isItalic ? " Italic" : "")}.ttf";
//         string fontFilePath = Path.Combine(fontPath, "arial.ttf");
//
//         // Check if the font file exists
//         if (File.Exists(fontFilePath))
//         {
//             return new FontResolverInfo("arial.ttf");
//         }
//         else
//         {
//             // Font file not found
//             return null;
//         }
//     }
//
//     public byte[] GetFont(string faceName)
//     {
//         // Load the font data from the file system
//         string fontFilePath = Path.Combine(fontPath, faceName);
//         if (File.Exists(fontFilePath))
//         {
//             return File.ReadAllBytes(fontFilePath);
//         }
//         else
//         {
//             throw new FileNotFoundException($"Font file '{fontFilePath}' not found.");
//         }
//     }
//
//     public string DefaultFontName => "arial";
// }

//
// public class CustomFontResolver : IFontResolver
// {
//     private string fontPath;
//
//     public CustomFontResolver(string fontPath)
//     {
//         this.fontPath = fontPath;
//     }
//
//     public FontResolverInfo? ResolveTypeface(string familyName, bool isBold, bool isItalic)
//     {
//         throw new NotImplementedException();
//     }
//
//     public byte[] GetFont(string faceName)
//     {
//         string fontFilePath = Path.Combine(fontPath, "arial.ttf");
//
//         if (File.Exists(fontFilePath))
//         {
//             return File.ReadAllBytes(fontFilePath);
//         }
//         else
//         {
//             throw new FileNotFoundException($"Font file '{fontFilePath}' not found.");
//         }
//     }
//
//     public string DefaultFontName => "Arial";
// }
public class FileFontResolver : IFontResolver // FontResolverBase
{
    public string DefaultFontName => throw new NotImplementedException();

    public byte[] GetFont(string faceName)
    {
        using (var ms = new MemoryStream())
        {
            using (var fs = File.Open(faceName, FileMode.Open))
            {
                fs.CopyTo(ms);
                ms.Position = 0;
                return ms.ToArray();
            }
        }
    }

    public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
    {
        if (familyName.Equals("Verdana", StringComparison.CurrentCultureIgnoreCase))
        {
            if (isBold && isItalic)
            {
                return new FontResolverInfo("Fonts/Verdana-BoldItalic.ttf");
            }
            else if (isBold)
            {
                return new FontResolverInfo("Fonts/Verdana-Bold.ttf");
            }
            else if (isItalic)
            {
                return new FontResolverInfo("Fonts/Verdana-Italic.ttf");
            }
            else
            {
                return new FontResolverInfo("Fonts/Verdana-Regular.ttf");
            }
        }

        return null;
    }
}