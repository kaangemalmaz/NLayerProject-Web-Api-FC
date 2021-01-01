using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;
using UdemyNLayerProject.Web.ApiService;
using UdemyNLayerProject.Web.DTOs;

namespace UdemyNLayerProject.Web.Filters
{
    public class NotFoundFilter : ActionFilterAttribute
    {
        private readonly CategoryApiService _categoryApiService;

        public NotFoundFilter(CategoryApiService categoryApiService)
        {
            _categoryApiService = categoryApiService;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            int id = (int)context.ActionArguments.Values.FirstOrDefault();

            var categories = await _categoryApiService.GetById(id);

            if (categories != null)
            {
                await next();
            }
            else
            {
                ErrorDto errorDto = new ErrorDto();
                errorDto.Errors.Add($"id'si {id} olan kategori veritabanında bulunamadı");
                context.Result = new RedirectToActionResult("Error", "Home",errorDto);
            }


        }
    }
}
