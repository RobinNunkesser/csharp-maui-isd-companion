using System.Globalization;
using Italbytz.AI.Search.Demos.Romania;
using Italbytz.Graph;
using Italbytz.Graph.Abstractions;
using StudyCompanion.Resources.Strings;

namespace StudyCompanion;

public class AIRomaniaSearchViewModel : StepwiseExerciseViewModel
{
    private enum RomaniaDisplayMode
    {
        Combined,
        PathOnly,
        FrontierOnly
    }

    private readonly RomaniaSearchSimulator _simulator = new();
    private readonly IUndirectedGraph<string, ITaggedEdge<string, double>> _graph;
    private IReadOnlyList<RomaniaSearchStep> _steps = [];
    private readonly string[] _algorithms = ["A*", "Uniform Cost", "Breadth First"];
    private readonly string[] _displayModes = [AppResources.CombinedDisplayMode, AppResources.PathOnlyDisplayMode, AppResources.FrontierOnlyDisplayMode];
    private readonly string[] _startCities = RomaniaMap.Cities.Where(city => city != RomaniaMap.Bucharest).ToArray();

    private int _selectedAlgorithmIndex;
    private int _selectedDisplayModeIndex;
    private int _selectedStartCityIndex;
    private View? _exerciseContent;
    private string _stepText = string.Empty;
    private string _currentNodeText = string.Empty;
    private string _pathText = string.Empty;
    private string _frontierText = string.Empty;
    private string _exploredText = string.Empty;
    private string _successorsText = string.Empty;

    public AIRomaniaSearchViewModel()
    {
        _graph = new UndirectedGraph<string, ITaggedEdge<string, double>>()
        {
            Edges = RomaniaMap.Roads.Select(road => new TaggedEdge<string, double>(road.From, road.To, road.Cost)).ToArray()
        };

        _selectedStartCityIndex = Array.IndexOf(_startCities, RomaniaMap.Arad);
        RenderState();
    }

    public IReadOnlyList<string> Algorithms => _algorithms;

    public IReadOnlyList<string> DisplayModes => _displayModes;

    public IReadOnlyList<string> StartCities => _startCities;

    public int SelectedAlgorithmIndex
    {
        get => _selectedAlgorithmIndex;
        set
        {
            if (_selectedAlgorithmIndex == value)
            {
                return;
            }

            _selectedAlgorithmIndex = value;
            OnPropertyChanged();
            newExercise();
        }
    }

    public int SelectedStartCityIndex
    {
        get => _selectedStartCityIndex;
        set
        {
            if (_selectedStartCityIndex == value)
            {
                return;
            }

            _selectedStartCityIndex = value;
            OnPropertyChanged();
            newExercise();
        }
    }

    public int SelectedDisplayModeIndex
    {
        get => _selectedDisplayModeIndex;
        set
        {
            if (_selectedDisplayModeIndex == value)
            {
                return;
            }

            _selectedDisplayModeIndex = value;
            OnPropertyChanged();
            RenderState();
        }
    }

    public View? Exercise_Content
    {
        get => _exerciseContent;
        set
        {
            _exerciseContent = value;
            OnPropertyChanged();
        }
    }

    public string StepText
    {
        get => _stepText;
        set
        {
            _stepText = value;
            OnPropertyChanged();
        }
    }

    public string CurrentNodeText
    {
        get => _currentNodeText;
        set
        {
            _currentNodeText = value;
            OnPropertyChanged();
        }
    }

    public string PathText
    {
        get => _pathText;
        set
        {
            _pathText = value;
            OnPropertyChanged();
        }
    }

    public string FrontierText
    {
        get => _frontierText;
        set
        {
            _frontierText = value;
            OnPropertyChanged();
        }
    }

    public string ExploredText
    {
        get => _exploredText;
        set
        {
            _exploredText = value;
            OnPropertyChanged();
        }
    }

    public string SuccessorsText
    {
        get => _successorsText;
        set
        {
            _successorsText = value;
            OnPropertyChanged();
        }
    }

    public override void Initialize()
    {
        newExercise();
    }

    protected override void newExercise()
    {
        CurrentSolutionStep = 0;
        _steps = _simulator.Simulate(CurrentStartCity, CurrentAlgorithm);
        RenderState();
    }

    protected override void previousStep()
    {
        if (CurrentSolutionStep == 0)
        {
            return;
        }

        CurrentSolutionStep--;
        RenderState();
    }

    protected override void nextStep()
    {
        if (CurrentSolutionStep >= _steps.Count)
        {
            return;
        }

        CurrentSolutionStep++;
        RenderState();
    }

    protected override void showCompleteSolution()
    {
        CurrentSolutionStep = _steps.Count;
        RenderState();
    }

    protected override void showInfo()
    {
    }

    private RomaniaSearchAlgorithm CurrentAlgorithm => SelectedAlgorithmIndex switch
    {
        0 => RomaniaSearchAlgorithm.AStarSearch,
        1 => RomaniaSearchAlgorithm.UniformCostSearch,
        _ => RomaniaSearchAlgorithm.BreadthFirstSearch
    };

    private string CurrentStartCity => _startCities[_selectedStartCityIndex];

    private RomaniaDisplayMode CurrentDisplayMode => SelectedDisplayModeIndex switch
    {
        1 => RomaniaDisplayMode.PathOnly,
        2 => RomaniaDisplayMode.FrontierOnly,
        _ => RomaniaDisplayMode.Combined
    };

    private void RenderState()
    {
        if (_steps.Count == 0 || CurrentSolutionStep == 0)
        {
            var initialFrontier = new HashSet<string> { CurrentStartCity };
            Exercise_Content = CreateGraphView(
                markedEdges: new HashSet<string>(),
                successorEdges: new HashSet<string>(),
                directedMarkedEdges: new HashSet<string>(),
                directedSuccessorEdges: new HashSet<string>(),
                pathStates: new[] { CurrentStartCity },
                frontierStates: ShouldShowFrontier() ? initialFrontier : new HashSet<string>(),
                exploredStates: new HashSet<string>(),
                currentNode: null);
            StepText = $"{AppResources.Step} 0/{_steps.Count}";
            CurrentNodeText = AppResources.PressNextToStart;
            PathText = $"{AppResources.Path}: {CurrentStartCity}";
            FrontierText = $"{AppResources.Frontier}: {CurrentStartCity}";
            ExploredText = $"{AppResources.Explored}: -";
            SuccessorsText = $"{AppResources.Successors}: -";
            return;
        }

        var step = _steps[CurrentSolutionStep - 1];
        var pathStates = ShouldShowPath() ? step.PathStates : Array.Empty<string>();
        var frontierStates = ShouldShowFrontier() ? step.Frontier.Select(node => node.State).ToHashSet() : new HashSet<string>();
        var exploredStates = CurrentDisplayMode == RomaniaDisplayMode.Combined ? step.ExploredStates.ToHashSet() : new HashSet<string>();
        Exercise_Content = CreateGraphView(
            markedEdges: ShouldShowPath() ? BuildMarkedEdges(step.PathStates) : new HashSet<string>(),
            successorEdges: ShouldShowFrontier() ? BuildSuccessorEdges(step) : new HashSet<string>(),
            directedMarkedEdges: ShouldShowPath() ? BuildDirectedMarkedEdges(step.PathStates) : new HashSet<string>(),
            directedSuccessorEdges: ShouldShowFrontier() ? BuildDirectedSuccessorEdges(step) : new HashSet<string>(),
            pathStates: pathStates,
            frontierStates: frontierStates,
            exploredStates: exploredStates,
            currentNode: step.ExpandedNode.State);
        StepText = step.GoalReached
            ? $"{AppResources.Step} {CurrentSolutionStep}/{_steps.Count} - {AppResources.GoalReached}"
            : $"{AppResources.Step} {CurrentSolutionStep}/{_steps.Count}";
        CurrentNodeText = $"{AppResources.CurrentNode}: {step.ExpandedNode.State} [g={Format(step.ExpandedNode.PathCost)}, p={Format(step.ExpandedNode.Priority)}]";
        PathText = $"{AppResources.Path}: {string.Join(" -> ", step.PathStates)}";
        FrontierText = $"{AppResources.Frontier}: {FormatNodes(step.Frontier)}";
        ExploredText = $"{AppResources.Explored}: {string.Join(", ", step.ExploredStates)}";
        SuccessorsText = $"{AppResources.Successors}: {FormatNodes(step.Successors)}";
    }

    private bool ShouldShowPath()
    {
        return CurrentDisplayMode is RomaniaDisplayMode.Combined or RomaniaDisplayMode.PathOnly;
    }

    private bool ShouldShowFrontier()
    {
        return CurrentDisplayMode is RomaniaDisplayMode.Combined or RomaniaDisplayMode.FrontierOnly;
    }

    private View CreateGraphView(
        IReadOnlySet<string> markedEdges,
        IReadOnlySet<string> successorEdges,
        IReadOnlySet<string> directedMarkedEdges,
        IReadOnlySet<string> directedSuccessorEdges,
        IReadOnlyCollection<string> pathStates,
        IReadOnlySet<string> frontierStates,
        IReadOnlySet<string> exploredStates,
        string? currentNode)
    {
        return new GraphicsView
        {
            HeightRequest = 280,
            Drawable = new AIRomaniaGraphDrawable(
                _graph,
                markedEdges,
                successorEdges,
                directedMarkedEdges,
                directedSuccessorEdges,
                pathStates.ToHashSet(),
                frontierStates,
                exploredStates,
                currentNode)
        };
    }

    private static HashSet<string> BuildMarkedEdges(IReadOnlyList<string> pathStates)
    {
        return pathStates.Zip(pathStates.Skip(1), (from, to) => NormalizeEdge(from, to)).ToHashSet();
    }

    private static HashSet<string> BuildSuccessorEdges(RomaniaSearchStep step)
    {
        return step.Successors
            .Select(successor => NormalizeEdge(step.ExpandedNode.State, successor.State))
            .ToHashSet();
    }

    private static HashSet<string> BuildDirectedMarkedEdges(IReadOnlyList<string> pathStates)
    {
        return pathStates.Zip(pathStates.Skip(1), (from, to) => DirectedEdge(from, to)).ToHashSet();
    }

    private static HashSet<string> BuildDirectedSuccessorEdges(RomaniaSearchStep step)
    {
        return step.Successors
            .Select(successor => DirectedEdge(step.ExpandedNode.State, successor.State))
            .ToHashSet();
    }

    private static string NormalizeEdge(string from, string to)
    {
        return string.CompareOrdinal(from, to) <= 0 ? $"{from}|{to}" : $"{to}|{from}";
    }

    private static string DirectedEdge(string from, string to)
    {
        return $"{from}>{to}";
    }

    private static string FormatNodes(IReadOnlyList<RomaniaSearchTraceNode> nodes)
    {
        return nodes.Count == 0
            ? "-"
            : string.Join(" | ", nodes.Select(node => $"{node.State} [g={Format(node.PathCost)}, p={Format(node.Priority)}]"));
    }

    private static string Format(double value)
    {
        return value.ToString("0.##", CultureInfo.InvariantCulture);
    }
}