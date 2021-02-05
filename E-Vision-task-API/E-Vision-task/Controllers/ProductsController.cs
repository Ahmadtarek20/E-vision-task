using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using E_Vision_task.Data;
using E_Vision_task.Domain;
using E_Vision_task.Dto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace E_Vision_task.Controllers
{
    [Route(V)]
    public class ProductsController : ControllerBase
    {
        private const string V = "api/product";
        private readonly IProducts _reboproducts;
        private readonly IMapper _mapper;

        private readonly IHostingEnvironment hostingEnvironment;
        public ProductsController(IProducts reboproducts, IHostingEnvironment hostingEnvironment, IMapper mapper)
        {
            _reboproducts = reboproducts;
            this.hostingEnvironment = hostingEnvironment;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _reboproducts.GetProducts();
            return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _reboproducts.GetProduct(id);
            return Ok(_mapper.Map<ProductDto>(product));

        }
        [HttpPost]
        public IActionResult Post([FromForm] ProductCreation product)
        {
            
            var uniqfilename = "defult.png";
            if (product.Photo != null)
            {
                uniqfilename = UplodFile(product.Photo);
            }
            var productToAdd = new Product()
            {
                Name = product.Name,
                LastUpdated = DateTime.UtcNow,
                Price = product.Price,
                PhotoPath = uniqfilename,
            };
            var prop = _reboproducts.Add(productToAdd);
            return Ok(prop);
        }
        private string UplodFile(IFormFile product)
        {
            string uniqfilename = null;
            if (product != null)
            {
                var uplodsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                if (!System.IO.Directory.Exists(uplodsFolder))
                {
                    Directory.CreateDirectory(uplodsFolder);
                }
                uniqfilename = Guid.NewGuid().ToString() + "-" + product.FileName;
                var filepath = Path.Combine(uplodsFolder, uniqfilename);
                product.CopyTo(new FileStream(filepath, FileMode.Create));
            }
            return uniqfilename;
        }

        [HttpPut(nameof(Edit))]
        [Consumes("multipart/form-data")]
        
        public async Task<IActionResult> Edit([FromForm] ProductEditeDto product)
        {
            var exist = await _reboproducts.GetProduct(product.Id);

            if (exist == null)
                return BadRequest();

            var productEntity = _mapper.Map<Product>(product);

            if (product.Photo != null)
            {

                productEntity.PhotoPath =  UplodFile(product.Photo);

            }

            var result = await _reboproducts.UpdateAsync(productEntity);
            return Ok(_mapper.Map<ProductDto>(result));

        }

        [HttpGet]
        [Route("ExportProducts")]
        public async Task<IActionResult> ExportProducts()
        {

            using (ExcelPackage package = new ExcelPackage())
            {

                var products = await _reboproducts.GetProducts();
                var dto = _mapper.Map<IEnumerable<ProductDto>>(products).ToList();

                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Product");
                int totalRows = dto.Count();

                worksheet.Cells[1, 1].Value = "Product ID";
                worksheet.Cells[1, 2].Value = "Product Name";
                worksheet.Cells[1, 3].Value = "Product Price";
                worksheet.Cells[1, 4].Value = "Product LastUpdate";
                worksheet.Cells[1, 5].Value = "Product Photo";
                int i = 0;
                for (int row = 2; row <= totalRows + 1; row++)
                {
                    worksheet.Cells[row, 1].Value = dto[i].Id;
                    worksheet.Cells[row, 2].Value = dto[i].Name;
                    worksheet.Cells[row, 3].Value = dto[i].Price;
                    worksheet.Cells[row, 4].Value = dto[i].LastUpdated;
                    worksheet.Cells[row, 5].Value = dto[i].Photo;
                    i++;
                }

                package.Save();

                var stream = new MemoryStream(package.GetAsByteArray());
                var content = stream.ToArray();

                return File(
                    content,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    "products.xlsx");

            }


        }

     

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (id == 0)
                return BadRequest();
            _reboproducts.Delete(id);
            return Ok();
        }

        
    }
}