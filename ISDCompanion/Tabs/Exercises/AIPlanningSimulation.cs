using Italbytz.AI.Planning;

namespace StudyCompanion;

internal enum AIPlanningScenario
{
    GoHomeToSfo,
    ParcelToLab
}

internal sealed record AIPlanningStep(
    string AppliedAction,
    IReadOnlyList<string> CurrentState,
    IReadOnlyList<string> AvailableActions,
    IReadOnlyList<string> ExecutedPlan,
    IReadOnlyList<string> RemainingPlan,
    bool GoalReached);

internal sealed record AIPlanningSimulation(
    IReadOnlyList<string> InitialState,
    IReadOnlyList<string> Goal,
    IReadOnlyList<string> InitialAvailableActions,
    IReadOnlyList<string> PlannedActions,
    IReadOnlyList<AIPlanningStep> Steps);

internal sealed class AIPlanningSimulator
{
    public AIPlanningSimulation Simulate(AIPlanningScenario scenario)
    {
        var problem = scenario switch
        {
            AIPlanningScenario.ParcelToLab => CreateParcelToLabProblem(),
            _ => PlanningProblemFactory.GoHomeToSfoProblem()
        };
        var allActions = problem.GetPropositionalisedActions().ToList();
        var plan = new HierarchicalSearchAlgorithm().HierarchicalSearch(problem)?.ToList() ?? [];

        var currentState = problem.InitialState;
        var executedPlan = new List<string>();
        var remainingPlan = plan.Select(FormatActionName).ToList();
        var steps = new List<AIPlanningStep>();

        foreach (var action in plan)
        {
            executedPlan.Add(FormatActionName(action));
            if (remainingPlan.Count > 0)
            {
                remainingPlan.RemoveAt(0);
            }

            currentState = currentState.Result([action]);
            steps.Add(new AIPlanningStep(
                FormatActionDetails(action),
                FormatLiterals(currentState.Fluents),
                GetApplicableActions(currentState, allActions),
                executedPlan.ToList(),
                remainingPlan.ToList(),
                problem.Goal.All(goal => currentState.Fluents.Contains(goal))));
        }

        return new AIPlanningSimulation(
            FormatLiterals(problem.InitialState.Fluents),
            FormatLiterals(problem.Goal),
            GetApplicableActions(problem.InitialState, allActions),
            plan.Select(FormatActionName).ToList(),
            steps);
    }

    private static IReadOnlyList<string> GetApplicableActions(IState state, IEnumerable<IActionSchema> actions)
    {
        return actions
            .Where(state.IsApplicable)
            .Select(FormatActionDetails)
            .ToList();
    }

    private static IReadOnlyList<string> FormatLiterals(IEnumerable<Italbytz.AI.Planning.Fol.ILiteral> literals)
    {
        return literals.Select(literal => literal.ToString() ?? string.Empty).ToList();
    }

    private static string FormatActionName(IActionSchema action)
    {
        return action.Variables.Count == 0
            ? action.Name
            : $"{action.Name}({string.Join(", ", action.Variables)})";
    }

    private static string FormatActionDetails(IActionSchema action)
    {
        var precondition = string.Join(" & ", action.Precondition.Select(literal => literal.ToString()));
        var effect = string.Join(" & ", action.Effect.Select(literal => literal.ToString()));
        return $"{FormatActionName(action)}: {precondition} -> {effect}";
    }

    private static IPlanningProblem CreateParcelToLabProblem()
    {
        var initialState = new State("At(Robot,Depot) ^ PackageAt(Parcel,Depot) ^ HandsFree");
        var goal = Utils.Parse("PackageAt(Parcel,Lab)");
        var pickUp = new ActionSchema(
            "PickUp",
            null,
            "At(Robot,Depot) ^ PackageAt(Parcel,Depot) ^ HandsFree",
            "~PackageAt(Parcel,Depot) ^ Holding(Parcel) ^ ~HandsFree");
        var driveToLab = new ActionSchema(
            "DriveToLab",
            null,
            "At(Robot,Depot) ^ Holding(Parcel)",
            "~At(Robot,Depot) ^ At(Robot,Lab)");
        var dropOff = new ActionSchema(
            "DropOff",
            null,
            "At(Robot,Lab) ^ Holding(Parcel)",
            "PackageAt(Parcel,Lab) ^ HandsFree ^ ~Holding(Parcel)");

        return new PlanningProblem(initialState, goal, pickUp, driveToLab, dropOff);
    }
}