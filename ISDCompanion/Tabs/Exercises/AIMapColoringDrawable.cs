using Microsoft.Maui.Graphics;
using Font = Microsoft.Maui.Graphics.Font;

namespace StudyCompanion;

public sealed class AIMapColoringDrawable : IDrawable
{
    private readonly IReadOnlyDictionary<string, string> _assignment;
    private readonly IReadOnlySet<string> _conflictedRegions;
    private readonly string? _currentRegion;

    private static readonly Dictionary<string, RectF> Regions = new()
    {
        ["WA"] = new RectF(12, 70, 90, 120),
        ["NT"] = new RectF(106, 32, 88, 82),
        ["SA"] = new RectF(106, 118, 88, 92),
        ["Q"] = new RectF(198, 38, 92, 88),
        ["NSW"] = new RectF(198, 130, 92, 72),
        ["V"] = new RectF(198, 206, 92, 46),
        ["T"] = new RectF(222, 262, 46, 28)
    };

    public AIMapColoringDrawable(IReadOnlyDictionary<string, string> assignment, IReadOnlySet<string> conflictedRegions, string? currentRegion)
    {
        _assignment = assignment;
        _conflictedRegions = conflictedRegions;
        _currentRegion = currentRegion;
    }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        const float baseWidth = 302f;
        const float baseHeight = 304f;
        var scale = Math.Min(dirtyRect.Width / baseWidth, dirtyRect.Height / baseHeight);
        var offsetX = (dirtyRect.Width - (baseWidth * scale)) / 2f;
        var offsetY = (dirtyRect.Height - (baseHeight * scale)) / 2f;

        foreach (var (region, rect) in Regions)
        {
            var drawRect = new RectF(offsetX + (rect.X * scale), offsetY + (rect.Y * scale), rect.Width * scale, rect.Height * scale);
            var fill = GetFillColor(_assignment.TryGetValue(region, out var color) ? color : null);
            var stroke = region == _currentRegion ? Colors.DarkGreen : _conflictedRegions.Contains(region) ? Colors.Crimson : Colors.Gray;
            var strokeSize = region == _currentRegion ? 4f : _conflictedRegions.Contains(region) ? 3f : 2f;

            canvas.FillColor = fill;
            if (region == "T")
            {
                canvas.FillEllipse(drawRect);
                canvas.StrokeColor = stroke;
                canvas.StrokeSize = strokeSize;
                canvas.DrawEllipse(drawRect);
            }
            else
            {
                canvas.FillRoundedRectangle(drawRect, 10f * scale);
                canvas.StrokeColor = stroke;
                canvas.StrokeSize = strokeSize;
                canvas.DrawRoundedRectangle(drawRect, 10f * scale);
            }

            canvas.Font = Font.DefaultBold;
            canvas.FontSize = 16f * scale;
            canvas.FontColor = Colors.Black;
            canvas.DrawString(region, drawRect, HorizontalAlignment.Center, VerticalAlignment.Center);
        }
    }

    private static Color GetFillColor(string? color)
    {
        return color switch
        {
            "red" => Color.FromArgb("#F28B82"),
            "green" => Color.FromArgb("#A5D6A7"),
            "blue" => Color.FromArgb("#90CAF9"),
            _ => Color.FromArgb("#ECEFF1")
        };
    }
}