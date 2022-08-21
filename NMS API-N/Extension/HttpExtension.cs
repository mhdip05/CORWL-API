using NMS_API_N.Pagination;
using System.Text.Json;

namespace NMS_API_N.Extension
{
    public static class HttpExtension
    {
        public static void AddPaginationHeader(this HttpResponse response, int currentPage, int itemsPerPage, int totalItems, int totalPages)
        {
            var paginationHeader = new PaginationHeader(currentPage, itemsPerPage, totalItems, totalPages);
            response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationHeader));

            // To make pagination header available
            response.Headers.Add("Access-Control-Pagination-Headers", "Pagination");
        }
    }
}
