
using AutumnBox.OpenFramework.Script;
using System;
public static int Main() {
    new AB().T();
    return 0;
}
public static ScriptInfo GetScriptInfo() {
    return new ScriptInfo();
}
public static void T() { }
class AB {
    public void T() {
        Console.WriteLine("Wowx");
    }
}