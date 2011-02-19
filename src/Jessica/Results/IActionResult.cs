using System.Web;

namespace Jessica.Results
{
    public interface IActionResult
    {
        void WriteToResponse(HttpContextBase context);
    }
}
