using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_Highland;
using System.Data;
namespace DAO_Highland
{
    public class ProductDAO
    {
        public static bool DeleteProduct(ProductDTO sp)
        {
            string query = "Exec DeleteByProduct @id ";
            if (DataProvider.Instance.ExcuteNonQuery(query, new object[] { sp.ID }) == -1)
            {
                return false;
            }
            return true;
        }

        public static List<ProductDTO> GeProductByName(string name)
        {
            List<ProductDTO> listProduct = new List<ProductDTO>();
            string query = string.Format("SELECT * FROM PRODUCT WHERE dbo.FCHUYENCODAUTHANHKHONGDAU(NAMEPRODUCTS) LIKE N'%' + dbo.FCHUYENCODAUTHANHKHONGDAU(N'{0}') + '%' AND PRODUCT.IDTYPEDINK NOT IN (SELECT ID FROM TYPEPRODUCT WHERE STATUS = 0)", name);
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                ProductDTO Product = new ProductDTO(item);
                listProduct.Add(Product);
            }
            return listProduct;
        }

        public static List<ProductDTO> GetAllListProduct()
        {
            List<ProductDTO> listProduct = new List<ProductDTO>();
            string query = "SELECT * FROM PRODUCT";
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                ProductDTO Product = new ProductDTO(item);
                listProduct.Add(Product);
            }
            return listProduct;
        }

        public static int GetIDTypeProductByIDProduct(int id)
        {
            DataTable data = DataProvider.Instance.ExcuteQuery("SELECT D.IDTYPEDINK FROM PRODUCT AS D WHERE IDTYPEDINK = " + id);//chưa thanh toán
            if (data.Rows.Count > 0)
            {
                return Convert.ToInt32(data.Rows[0]["IDTypeDink"].ToString());
            }
            return -1;
        }

        public static List<ProductDTO> GetListProductByIDTypeProduct(int id, int status, string size)
        {
            var listProduct = new List<ProductDTO>();
            string query;
            if (id == 0)
                query = "SELECT * FROM PRODUCT WHERE STATUS = " + status + " AND PRODUCT.IDTYPEDINK NOT IN (SELECT ID FROM TYPEPRODUCT WHERE STATUS = 0)";
            else
            {
                if (status == -1)
                    query = string.Format("SELECT * FROM PRODUCT WHERE IDTYPEDINK = {0}", id);
                else
                    query = string.Format("SELECT * FROM PRODUCT WHERE IDTYPEDINK = {0} AND STATUS = {1}", id, status, size);

                if (!string.IsNullOrEmpty(query) && !string.IsNullOrEmpty(size))
                {
                    query = string.Format("{0} AND SIZE = '{1}'", query, size);
                }
            }
            DataTable data = DataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                ProductDTO Product = new ProductDTO(item);
                listProduct.Add(Product);
            }
            return listProduct;
        }

        public static bool InsertProduct(ProductDTO sp)
        {
            string query = "Exec InsertProduct @name , @pricebasic , @saleprice , @status , @idtype , @size ";
            if (DataProvider.Instance.ExcuteNonQuery(query, new object[] { sp.NameProducts, sp.PriceBasic, sp.SalePrice, sp.Status, sp.IDTypeProduct, sp.Size }) == 1)
            {
                return true;
            }
            return false;
        }

        public static bool UpdateProduct(ProductDTO sp)
        {
            string query = "Exec UpdateProduct @id , @name , @pricebasic , @saleprice , @status , @idtype , @size ";
            if (DataProvider.Instance.ExcuteNonQuery(query, new object[] { sp.ID, sp.NameProducts, sp.PriceBasic, sp.SalePrice, sp.Status, sp.IDTypeProduct, sp.Size }) == 1)
            {
                return true;
            }
            return false;
        }
    }
}
