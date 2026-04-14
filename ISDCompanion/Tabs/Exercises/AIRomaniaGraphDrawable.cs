using Italbytz.Graph;
using Italbytz.Graph.Abstractions;
using Italbytz.Graph.Maui;
using Microsoft.Maui.Graphics;
using Microsoft.Msagl.Core.Geometry.Curves;
using Microsoft.Msagl.Core.Layout;
using Microsoft.Msagl.Layout.Layered;
using Microsoft.Msagl.Miscellaneous;
using Font = Microsoft.Maui.Graphics.Font;
using Label = Microsoft.Msagl.Core.Layout.Label;
using Edge = Microsoft.Msagl.Core.Layout.Edge;
using Node = Microsoft.Msagl.Core.Layout.Node;

namespace StudyCompanion;

public sealed class AIRomaniaGraphDrawable : IDrawable
{
    private readonly GeometryGraph _graph;
    private readonly Func<Edge, double> _weight;
    private readonly HashSet<string> _markedEdges;
    private readonly HashSet<string> _successorEdges;
    private readonly HashSet<string> _pathNodes;
    private readonly HashSet<string> _frontierNodes;
    private readonly HashSet<string> _exploredNodes;
    private readonly string? _currentNode;
    private ICanvas? _canvas;
    private RectF _dirtyRect;
    private double _scale = 1.0;
    private bool _layoutPrepared;

    public AIRomaniaGraphDrawable(
        IUndirectedGraph<string, ITaggedEdge<string, double>> graph,
        IReadOnlySet<string> markedEdges,
        IReadOnlySet<string> successorEdges,
        IReadOnlySet<string> pathNodes,
        IReadOnlySet<string> frontierNodes,
        IReadOnlySet<string> exploredNodes,
        string? currentNode)
    {
        _graph = graph.ToGeometryGraph();
        _weight = edge => ((ITaggedEdge<string, double>)edge.UserData).Tag;
        _markedEdges = [.. markedEdges];
        _successorEdges = [.. successorEdges];
        _pathNodes = [.. pathNodes];
        _frontierNodes = [.. frontierNodes];
        _exploredNodes = [.. exploredNodes];
        _currentNode = currentNode;
    }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        _canvas = canvas;
        _dirtyRect = dirtyRect;

        if (!_layoutPrepared)
        {
            LayoutGraph();
            _layoutPrepared = true;
        }

        DrawGraph();
    }

    private void LayoutGraph()
    {
        var settings = new SugiyamaLayoutSettings
        {
            AspectRatio = _dirtyRect.Width / _dirtyRect.Height,
            MinimalHeight = 0.0,
            MinimalWidth = 0.0
        };

        LayoutHelpers.CalculateLayout(_graph, settings, null);
        _graph.UpdateBoundingBox();

        var scale = Math.Min(_dirtyRect.Height / _graph.BoundingBox.Height, _dirtyRect.Width / _graph.BoundingBox.Width);
        if (scale < 1.0)
        {
            _scale = scale;
            _graph.Transform(PlaneTransformation.ScaleAroundCenterTransformation(scale, new Microsoft.Msagl.Core.Geometry.Point(_graph.BoundingBox.Center.X, _graph.BoundingBox.Center.Y)));
            _graph.UpdateBoundingBox();
        }

        _graph.Translate(new Microsoft.Msagl.Core.Geometry.Point(-_graph.Left, -_graph.Bottom));
        _graph.Translate(new Microsoft.Msagl.Core.Geometry.Point((_dirtyRect.Width - _graph.BoundingBox.Width) / 2, (_dirtyRect.Height - _graph.BoundingBox.Height) / 2));
    }

    private void DrawGraph()
    {
        _canvas!.FontColor = Colors.Gray;
        _canvas.FontSize = (float)(16 * _scale);
        _canvas.Font = Font.Default;

        foreach (var edge in _graph.Edges)
        {
            var source = edge.Source.UserData as string ?? string.Empty;
            var target = edge.Target.UserData as string ?? string.Empty;
            var normalizedEdge = NormalizeEdge(source, target);
            DrawEdge(
                edge,
                _weight(edge),
                _markedEdges.Contains(normalizedEdge),
                _successorEdges.Contains(normalizedEdge));
        }

        foreach (var node in _graph.Nodes)
        {
            DrawNode(node);
        }
    }

    private void DrawNode(Node node)
    {
        var nodeName = node.UserData as string ?? string.Empty;
        var (strokeColor, fillColor, textColor, strokeSize) = GetNodeStyle(nodeName);

        _canvas!.FillColor = fillColor;
        _canvas.FillEllipse((float)node.BoundingBox.LeftBottom.X, (float)node.BoundingBox.LeftBottom.Y, (float)node.Width, (float)node.Height);
        _canvas.StrokeColor = strokeColor;
        _canvas.StrokeSize = strokeSize;
        _canvas.DrawEllipse((float)node.BoundingBox.LeftBottom.X, (float)node.BoundingBox.LeftBottom.Y, (float)node.Width, (float)node.Height);

        if (node.UserData is string text)
        {
            _canvas.FontColor = textColor;
            _canvas.DrawString(text, (float)node.BoundingBox.LeftBottom.X, (float)node.BoundingBox.LeftBottom.Y, (float)node.BoundingBox.Width, (float)node.BoundingBox.Height, HorizontalAlignment.Center, VerticalAlignment.Center);
            _canvas.FontColor = Colors.Gray;
        }
    }

    private (Color StrokeColor, Color FillColor, Color TextColor, float StrokeSize) GetNodeStyle(string nodeName)
    {
        if (nodeName == _currentNode)
        {
            return (Colors.DarkGreen, Colors.LightGreen, Colors.Black, 3f);
        }

        if (_frontierNodes.Contains(nodeName))
        {
            return (Colors.DarkGoldenrod, Colors.Gold, Colors.Black, 2.5f);
        }

        if (_pathNodes.Contains(nodeName))
        {
            return (Colors.DarkBlue, Colors.LightBlue, Colors.Black, 2.5f);
        }

        if (_exploredNodes.Contains(nodeName))
        {
            return (Colors.DimGray, Colors.LightGray, Colors.Black, 2f);
        }

        return (Colors.Gray, Colors.White, Colors.Gray, 1f);
    }

    private void DrawLabel(Label label, string text)
    {
        _canvas!.FontColor = Colors.Gray;
        _canvas.DrawString(text, (float)(label.BoundingBox.LeftBottom.X + 10.0), (float)label.BoundingBox.LeftBottom.Y, (float)label.BoundingBox.Width, (float)label.BoundingBox.Height, HorizontalAlignment.Left, VerticalAlignment.Center);
    }

    private void DrawEdge(Edge edge, double weight, bool mark, bool successor)
    {
        _canvas!.StrokeColor = mark ? Colors.Blue : successor ? Colors.DarkOrange : Colors.Grey;
        _canvas.StrokeSize = mark ? 4 : successor ? 3 : 2;

        if (edge.Curve is LineSegment line)
        {
            _canvas.DrawLine((float)line.Start.X, (float)line.Start.Y, (float)line.End.X, (float)line.End.Y);
        }
        else if (edge.Curve is Curve curve)
        {
            PathF path = new();
            path.MoveTo((float)edge.Curve.Start.X, (float)edge.Curve.Start.Y);
            foreach (var segment in curve.Segments)
            {
                if (segment is CubicBezierSegment bezier)
                {
                    path.CurveTo((float)bezier.B(1).X, (float)bezier.B(1).Y, (float)bezier.B(2).X, (float)bezier.B(2).Y, (float)bezier.B(3).X, (float)bezier.B(3).Y);
                }
                else if (segment is Ellipse ellipse)
                {
                    for (var i = ellipse.ParStart; i < ellipse.ParEnd; i += (ellipse.ParEnd - ellipse.ParStart) / 5.0)
                    {
                        var point = ellipse.Center + (Math.Cos(i) * ellipse.AxisA) + (Math.Sin(i) * ellipse.AxisB);
                        path.LineTo((float)point.X, (float)point.Y);
                    }
                }
                else if (segment is LineSegment segmentLine)
                {
                    path.LineTo((float)segmentLine.End.X, (float)segmentLine.End.Y);
                }
            }

            path.LineTo((float)edge.Curve.End.X, (float)edge.Curve.End.Y);
            _canvas.DrawPath(path);
        }

        DrawLabel(edge.Label, weight.ToString());
    }

    private static string NormalizeEdge(string from, string to)
    {
        return string.CompareOrdinal(from, to) <= 0 ? $"{from}|{to}" : $"{to}|{from}";
    }
}