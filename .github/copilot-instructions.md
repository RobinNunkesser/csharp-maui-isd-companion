# Copilot Instructions for `csharp-maui-isd-companion`

## Code-Duplikation mit `csharp-maui-study-companion`

Dieses Repo teilt einen signifikanten Teil seines Codes mit
[`csharp-maui-study-companion`](https://github.com/RobinNunkesser/csharp-maui-study-companion).

Die Doppelung ist eine bewusste Architekturentscheidung (siehe `decisions.md` im
`agent-workbench`): Nach der Entkopplung vom eingebetteten Submodul werden beide Apps
unabhängig gepflegt.

### Betroffen sind insbesondere

- `ISDCompanion.Core/MealCollection.cs`
- `ISDCompanion.Core.Mock/` — alle Mock-Klassen
- `ISDCompanion/Common/` — `SectionViewModel`, `InternalBrowserPage`
- `ISDCompanion/Tabs/Mensa/` — `MealQuery`, `MensaViewModel`, `PriceConverter`, `MensaPage`
- `ISDCompanion/Tabs/Settings/` — Additives, Allergens, Settings
- `ISDCompanion/Tabs/Exercises/Quiz/` — `QuizPage`, `QuizViewModel`, `QuizStatisticsPage`

### Empfehlung bei Änderungen

Wenn du eine der oben genannten Dateien änderst, prüfe ob die entsprechende Datei in
`csharp-maui-study-companion` von derselben Verbesserung profitieren kann. Das gilt
insbesondere für:

- Bugfixes in gemeinsamer Logik
- Namespace-Umbenennungen (z. B. NuGet-Paketpfade)
- Vereinheitlichung von MVVM-Patterns oder Converter-Implementierungen
