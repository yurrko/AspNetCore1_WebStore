using WebStore.Models.Cart;

namespace WebStore.infrastructure.Interfaces
{
    public interface ICartService
    {
        void DecrementFromCart( int id );

        void RemoveFromCart( int id );

        void RemoveAll();

        void AddToCart( int id );

        CartViewModel TransformCart();
    }
}
