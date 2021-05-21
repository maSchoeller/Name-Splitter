# Architektur 

## MVVM-Entwurfsmuster (View, ViewModels, ...)

Für die Trennung von Obefläche und Logik, wird das MVVM Pattern verwendet.
Als dritte Schicht wird eine Service Schicht eingeführt, sie enthält den Analyser und den Generator. 

### NameSplitterAnalyser

Der Analyser nimmt die Eingabe entgegen und separiert diese mit Hilfe den Informationen in der Datenbank.
Als Ergebnis liefert er ein Result Objekt zurück welches die Informationen über die Serparierung enthält.

### SalutationGenerator
Der Generator nimmt das Result des Analysers entgegen und generiert auf dessen Grundlage die passende Anrede.

## Oberfläche
Es können eigene Anreden, Titel und Bindewörter für den Namen ergänzt oder gelöscht werden.
Bindewörter sind Wörter die den Nachnamen einleiten, Wörter wie 'van' oder 'von'.

![oberflaeche](./Oberflaeche.png)

### Blau
Hier können die Eingaben getätigt werden. 

### Rot
Hier wird die Auswertung der Analyse visualiert und die Anredenvorschlag wird Angezeigt.

### Grün
Hier können die Informationen in der Datenbank angereichert werden, in der Tabview können die einzelnen Kategorien ausgewählt werden.

## Speicherung
Die Speicherung der Anreden, Titel und Bindewörter wurde über eine SqLite Datenbank gelöst diese liegt der Exe bei und heißt: ``namesplitter.db``.
Falls diese nicht vorhanden ist wird sie mit dem passenden Schema erstellt.
