using System.Threading.Tasks;

namespace KEC.ECommerce.Web.UI.PDF
{
    /// <summary>
    /// Renders content based on razor templates
    /// </summary>
    public interface ITemplateService
    {
        /// <summary>
        /// Renders a teplate given the provided view model
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="filename"></param>
        /// <param name="viewModel"></param>
        /// <returns>Returns the rendered template content</returns>
        Task<string> RenderTemplateAsync<TViewModel>(string filename, TViewModel viewModel);
    }
}
