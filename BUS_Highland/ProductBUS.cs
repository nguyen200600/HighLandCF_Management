using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO_Highland;
using DTO_Highland;
namespace BUS_Highland
{
    public class ProductBUS
    {
        public static bool DeleteProduct(ProductDTO sp) => ProductDAO.DeleteProduct(sp);

        public static List<ProductDTO> GeProductByName(string name) => ProductDAO.GeProductByName(name);

        public static List<ProductDTO> GetAllListProduct() => ProductDAO.GetAllListProduct();

        public static int GetIDTypeProductByIDProduct(int id) => ProductDAO.GetIDTypeProductByIDProduct(id);

        public static List<ProductDTO> GetListProductByIDTypeProduct(int id, int status, string size = "") => ProductDAO.GetListProductByIDTypeProduct(id, status, size);

        public static bool InsertProduct(ProductDTO sp) => ProductDAO.InsertProduct(sp);

        public static bool UpdateProduct(ProductDTO sp) => ProductDAO.UpdateProduct(sp);
    }
}
