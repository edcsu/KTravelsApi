namespace KTravelsApi.Core.ViewModels;

/// <summary>
/// The details of pagination of the requests
/// </summary>
public class SearchPagination
{
    private const int MaxPageSize = 100;

    /// <summary>
    /// Page number
    /// </summary>
    public int Page { get; set; } = 1;

    private int _itemsPerPage = 50;

    /// <summary>
    /// Number of requests returned per page
    /// </summary>
    public int ItemsPerPage
    {
        get => _itemsPerPage;

        set => _itemsPerPage = value > MaxPageSize ? MaxPageSize : value;
    }

    /// <summary>
    /// Total number of requests
    /// </summary>
    public int TotalItems { get; set; }

}