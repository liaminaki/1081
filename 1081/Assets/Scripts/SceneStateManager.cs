public static class SceneStateManager {
    private static string previousScene;

    public static string PreviousScene
    {
        get { return previousScene; }
        set { previousScene = value; }
    }
}