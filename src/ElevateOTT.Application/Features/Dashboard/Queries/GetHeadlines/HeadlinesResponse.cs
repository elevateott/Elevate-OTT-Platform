namespace ElevateOTT.Application.Features.Dashboard.Queries.GetHeadlines;

public class HeadlinesResponse
{
    #region Private Fields

    private readonly Random _rnd;

    #endregion Private Fields

    #region Public Constructors

    public HeadlinesResponse()
    {
        _rnd = new Random();
    }

    #endregion Public Constructors

    #region Public Properties

    public double TotalCost
    {
        get { return TotalCost = _rnd.Next(1, 1000000); }
        set { }
    }

    public double GraphiteOnRoof
    {
        get { return GraphiteOnRoof = _rnd.Next(1, 1000000); }
        set { }
    }

    public double GlobalSpread
    {
        get { return GlobalSpread = _rnd.Next(1, 1000000); }
        set { }
    }

    public double Roentgen
    {
        get { return Roentgen = _rnd.Next(1, 1000000); }
        set { }
    }

    #endregion Public Properties
}