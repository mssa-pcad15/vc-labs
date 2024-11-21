
public class Rootobject
{
    public string status { get; set; }
    public string message { get; set; }
    public string messageTR { get; set; }
    public int systemTime { get; set; }
    public string endpoint { get; set; }
    public int rowCount { get; set; }
    public int creditUsed { get; set; }
    public Data data { get; set; }
}

public class Data
{
    public int drawNumber { get; set; }
    public string drawDate { get; set; }
    public string drawTime { get; set; }
    public string drawDateTime { get; set; }
    public int drawTimestamp { get; set; }
    public string jackpot { get; set; }
    public string cashPayout { get; set; }
    public string megaPlierPrizePool { get; set; }
    public string regularPrizePool { get; set; }
    public string totalPrizePool { get; set; }
    public int totalMegaPlierWinners { get; set; }
    public int totalRegularWinners { get; set; }
    public int overallWinners { get; set; }
    public int[] winningNumbers { get; set; }
    public int[] additionalNumbers { get; set; }
    public Gamedetails gameDetails { get; set; }
}

public class Gamedetails
{
    public int id { get; set; }
    public string gameName { get; set; }
    public string logo { get; set; }
    public string drawTimezone { get; set; }
    public string drawTime { get; set; }
    public string stopSaleTime { get; set; }
    public int claimDeadline { get; set; }
    public bool allowZero { get; set; }
    public int minBall { get; set; }
    public int maxBall { get; set; }
    public int drawnNumbers { get; set; }
    public int selectableBalls { get; set; }
    public int minimumSelectableBalls { get; set; }
    public int uniqueMainNumbers { get; set; }
    public int uniqueExtraNumbers { get; set; }
    public bool allowDuplicates { get; set; }
    public Additionalnumber[] additionalNumbers { get; set; }
    public Bonusnumber[] bonusNumbers { get; set; }
    public Prizemultiplier[] prizeMultipliers { get; set; }
    public object mainDrawName { get; set; }
    public State state { get; set; }
    public object[] statesWithMultiDraws { get; set; }
    public Drawdays drawDays { get; set; }
}

public class State
{
    public string state { get; set; }
    public string stateCode { get; set; }
    public string slug { get; set; }
    public string taxRate { get; set; }
    public int minimumLegalAge { get; set; }
}

public class Drawdays
{
    public bool Sunday { get; set; }
    public bool Monday { get; set; }
    public bool Tuesday { get; set; }
    public bool Wednesday { get; set; }
    public bool Thursday { get; set; }
    public bool Friday { get; set; }
    public bool Saturday { get; set; }
}

public class Additionalnumber
{
    public bool playerPicked { get; set; }
    public string name { get; set; }
    public int minBall { get; set; }
    public int maxBall { get; set; }
    public bool allowZero { get; set; }
    public bool isString { get; set; }
    public bool isMultiplier { get; set; }
    public int[] allowedValues { get; set; }
}

public class Bonusnumber
{
    public string name { get; set; }
    public string abbreviation { get; set; }
    public bool isMultiplier { get; set; }
    public object variant { get; set; }
}

public class Prizemultiplier
{
    public string name { get; set; }
    public string abbreviation { get; set; }
    public bool isMultiplier { get; set; }
    public object variant { get; set; }
}
