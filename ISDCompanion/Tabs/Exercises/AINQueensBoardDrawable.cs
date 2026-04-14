using Microsoft.Maui.Graphics;
using Font = Microsoft.Maui.Graphics.Font;

namespace StudyCompanion;

public sealed class AINQueensBoardDrawable : IDrawable
{
    private readonly int _size;
    private readonly Dictionary<int, int> _queens;
    private readonly HashSet<int> _conflictedColumns;
    private readonly int? _currentColumn;

    public AINQueensBoardDrawable(int size, IReadOnlyDictionary<int, int> queens, IReadOnlySet<int> conflictedColumns, int? currentColumn)
    {
        _size = size;
        _queens = new Dictionary<int, int>(queens);
        _conflictedColumns = [.. conflictedColumns];
        _currentColumn = currentColumn;
    }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        var boardSize = Math.Min(dirtyRect.Width, dirtyRect.Height);
        var cellSize = boardSize / _size;
        var offsetX = (dirtyRect.Width - boardSize) / 2f;
        var offsetY = (dirtyRect.Height - boardSize) / 2f;

        for (var row = 0; row < _size; row++)
        {
            for (var column = 0; column < _size; column++)
            {
                canvas.FillColor = (row + column) % 2 == 0 ? Color.FromArgb("#F4F1E8") : Color.FromArgb("#8B6F47");
                canvas.FillRectangle(offsetX + (column * cellSize), offsetY + (row * cellSize), cellSize, cellSize);
            }
        }

        canvas.StrokeColor = Colors.Gray;
        canvas.StrokeSize = 1;
        canvas.DrawRectangle(offsetX, offsetY, boardSize, boardSize);

        for (var index = 1; index < _size; index++)
        {
            canvas.DrawLine(offsetX + (index * cellSize), offsetY, offsetX + (index * cellSize), offsetY + boardSize);
            canvas.DrawLine(offsetX, offsetY + (index * cellSize), offsetX + boardSize, offsetY + (index * cellSize));
        }

        canvas.Font = Font.DefaultBold;
        canvas.FontSize = cellSize * 0.55f;

        foreach (var (column, row) in _queens)
        {
            var color = _currentColumn == column
                ? Colors.DarkGreen
                : _conflictedColumns.Contains(column)
                    ? Colors.Crimson
                    : Colors.RoyalBlue;

            canvas.FontColor = color;
            canvas.DrawString(
                "Q",
                offsetX + (column * cellSize),
                offsetY + (row * cellSize),
                cellSize,
                cellSize,
                HorizontalAlignment.Center,
                VerticalAlignment.Center);
        }
    }
}