namespace KTravelsApi.Core.ViewModels;

public class PaginatedResponse<T>
{
    public PaginatedResponse(SearchPagination pagination, T items)
    {
        Pagination = pagination;
        this.Items = items;
    }

    public SearchPagination Pagination { get; set; }
 
    public T Items { get; set; }
}