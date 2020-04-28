using SportStore.Application.Orders;
using SportStore.Domain;

namespace SportStore.UnitTests.UnitTests.Application.OrderTests
{
    internal static class AdressExtensions
    {
        internal static bool CompareToVm(this Adress adress, AdressVm vm)
        {
            string vmAdress = vm.Line1 + vm.Line2 + vm.Line3 + vm.Country + vm.State + vm.City + vm.Zip;
            return adress.ToString() == vmAdress;
        }
    }
}
