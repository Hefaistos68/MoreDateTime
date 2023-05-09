namespace MoreDateTime
{
    /// <summary>
    /// The season.
    /// </summary>
    public enum Season
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        Undefined = 0,
        First = 21,            // For simplicity, we define a duplicate value so we can use this name in code 

#pragma warning disable CA1069 // Enums values should not be duplicated
        Spring = 21,
#pragma warning restore CA1069 // Enums values should not be duplicated
        Summer = 22,
        Autumn = 23,
        Winter = 24,
        SpringNorthernHemisphere = 25,
        SummerNorthernHemisphere = 26,
        AutumnNorthernHemisphere = 27,
        WinterNorthernHemisphere = 28,
        SpringSouthernHemisphere = 29,
        SummerSouthernHemisphere = 30,
        AutumnSouthernHemisphere = 31,
        WinterSouthernHemisphere = 32,
        Quarter1 = 33,
        Quarter2 = 34,
        Quarter3 = 35,
        Quarter4 = 36,
        Quadrimester1 = 37,
        Quadrimester2 = 38,
        Quadrimester3 = 39,
        Semestral1 = 40,
        Semestral2 = 41,

#pragma warning disable CA1069 // Enums values should not be duplicated
        Last = 41              // For simplicity, we define a duplicate value so we can use this name in code 
#pragma warning restore CA1069 // Enums values should not be duplicated
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}



