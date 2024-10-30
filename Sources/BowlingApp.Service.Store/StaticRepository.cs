using BowlingApp.Service.Store.Models;

namespace BowlingApp.Service.Store;

public static class StaticRepository
{
    public readonly static Dictionary<int, Game> Games = new Dictionary<int, Game>();

    public static void CleareTheGameRepository()
    {
        Games.Clear();
    }
}
