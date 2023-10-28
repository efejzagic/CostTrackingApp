using CsvHelper;
using Newtonsoft.Json;
using Storage.Domain.Entities;
using System.Globalization;
using System.Xml.Linq;

namespace Storage.UnitTest
{
    [TestClass]
    public class ArticleTest
    {

        static IEnumerable<object[]> ArticleCSV
        {
            get
            {
                return LoadDataCSV();
            }
        }

        public static IEnumerable<object[]> LoadDataCSV()
        {
            using (var reader = new StreamReader("Article.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var rows = csv.GetRecords<dynamic>();
                foreach (var row in rows)
                {
                    var values = ((IDictionary<String, Object>)row).Values;
                    var elements = values.Select(elem => elem.ToString()).ToList();
                    yield return new object[] { elements[0], elements[1],elements[2],
                    elements[3], elements[4], elements[5], elements[6] ,elements[7] ,elements[8]};
                }
            }
        }

        [TestMethod]
        [DynamicData("ArticleCSV")]
        
        public void TestMethod1(int id, string name, int quantity, double price, string description, int supplierId, bool
            orderRequired, bool inStock, bool retired)
        {
            Article article = new Article()
            {
                Id = id,
                Name = name,
                Quantity = quantity,
                Price = price,
                Description = description,
                SupplierId = supplierId,
                OrderRequired = orderRequired,
                InStock = inStock,
                retired = retired
            };
            Console.WriteLine(article.ToString());  
            var test = article;
            Assert.AreEqual(id, test.Id);
        }
    }
}