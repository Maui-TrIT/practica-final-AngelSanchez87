using ShopApp.DataAccess;
using ShopApp.Views;

namespace ShopApp.Handlers
{
    public class ProductSearchHandler : SearchHandler
    {
        ShopDbContext dbContext;

        public ProductSearchHandler()
        {
            dbContext = new ShopDbContext();
        }

        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            if (string.IsNullOrWhiteSpace(newValue))
            {
                ItemsSource = null;
                return;
            }

            var resultados = dbContext.Products
                                .Where(p => p.Nombre.ToLowerInvariant()
                                    .Contains(newValue.ToLowerInvariant()))
                                .ToList();
            ItemsSource = resultados;
        }

        protected override async void OnItemSelected(object item)
        {
            await Shell.Current.GoToAsync($"{nameof(ProductDetailPage)}?id={((Product)item).Id}");
        }
    }
}
