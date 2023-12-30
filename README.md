A C# port of the Dear ImGui library

# Design

Instead of adopting the "DearImGUI" name for the lib, it is simply `UI`

So the code will look something like this:

```cs
if (UI.Button("Hello")) {
    Debug.Log("Clicked!");
}
```

# Porting

The Port is currently in progress. To speed things up we are using a Kotlin port of DearImGui since conversion is easier.

You can find the Kotlin Dear ImGui [here](https://github.com/kotlin-graphics/imgui/tree/master/core/src/main/kotlin/imgui)

# Known Issues
