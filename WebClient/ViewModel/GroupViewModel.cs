using Microsoft.AspNetCore.Mvc.Rendering;
using WebClient.Helpers;

namespace WebClient.Models;

public class GroupViewModel
{
    public List<SelectListItem>  selectList { get; set; }
    public List<ProductViewModel>? products { get; set; }
    public PaginationInfo? paginationInfo { get; set; }
}
