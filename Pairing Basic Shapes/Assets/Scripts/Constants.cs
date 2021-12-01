
public static class Constants
{ 
    public enum State
    {
        NORMAL = 0,
        PREVIOUS = 1,
        PRESENTATION = 2
    }
    public static int rewards = 0;
    public static class Level0
    {
        public static State gameState = State.NORMAL;
        public static int currentShape = 0;
        public static int nextLevel;
        public static bool firstPresentation = true;
        public static bool firstTimeLevel = true;
        public static bool canSkip = false;
    }

    public static class Level1
    {
        public static State gameState = State.NORMAL;
        public static int currentSet = 0;
        public static int nextLevel;
        public static bool firstPresentation = true;
        public static bool firstTimeLevel = true;
        public static bool canSkip = false;
        public static int progress = 0;
        public static bool canLoadSavedScene = false;
        public static bool isGameCompleted = false;
    }
    public static class Level2
    {
        public static State gameState = State.NORMAL;
        public static int currentSet = 0;
        public static int nextLevel;
        public static bool firstPresentation = true;
        public static bool firstTimeLevel = true;
        public static bool canSkip = false;
        public static int progress = 0;
        public static bool canLoadSavedScene = false;
    }

    public static class Level3
    {
        public static State gameState = State.NORMAL;
        public static int currentSet = 0;
        public static int nextLevel;
        public static bool firstPresentation = true;
        public static bool firstTimeLevel = true;
        public static bool canSkip = false;
        public static int progress = 0;
        public static bool canLoadSavedScene = false;
    }

    public static class Level4
    {
        public static State gameState = State.NORMAL;
        public static int currentSet = 0;
        public static int nextLevel;
        public static bool firstPresentation = true;
        public static bool firstTimeLevel = true;
        public static bool canSkip = false;
        public static int progress = 0;
        public static bool canLoadSavedScene = false;
    }

}
