namespace BowlingApp.Service.Store.Models;

public class Frame
{
    public int FrameNumber { get; set; }
    public int? FirstDelivery { get; set; } // Pins knocked down in first delivery (null if not yet rolled)
    public int? SecondDelivery { get; set; } // Pins knocked down in second delivery (null if not yet rolled)
    public int? ThirdDelivery { get; set; } // Only for 10th frame if a spare or strike is rolled.
    public int Score { get; set; } // Total score for the frame
    public bool IsStrike { get; set; }
    public bool IsSpare { get; set; }
    public bool IsOpen { get; set; }
    public bool IsSplit { get; set; } // To flag if it's a split situation

    public Frame()
    {
        FirstDelivery = null;
        SecondDelivery = null;
        ThirdDelivery = null;
        Score = 0;
        IsStrike = false;
        IsSpare = false;
        IsOpen = true;
        IsSplit = false;
    }
}
