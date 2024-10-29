namespace BowlingApp.Service.Store.Models;

public class FrameScore
{
    public int FrameNumber { get; set; }
    public int FrameTotal { get; set; }
    public bool IsOpen { get; set; }
    public bool IsStrike { get; set; }
    public bool IsSpare { get; set; }
}
