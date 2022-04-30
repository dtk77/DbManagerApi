using WebClient.Helpers;

namespace WebClient.Models;

public class GroupViewModel
{
    public List<ProductViewModel>? products { get; set; }
    public PaginationInfo? paginationInfo { get; set; }
}
