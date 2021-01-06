namespace T3.Enums
{
    public enum ClientDesire
    {
        Sleep = 0, // does not make calls, rejects incoming calls
        Awake, // does not make calls, accepts incoming calls
        Active, // does make calls, accepts incoming calls
        Talking // talking, new calls are not possible
    }
}