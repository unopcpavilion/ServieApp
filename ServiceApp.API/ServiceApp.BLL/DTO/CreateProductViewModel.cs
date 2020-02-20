using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceApp.BLL.DTO
{
   public class CreateProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public bool Status { get; set; }
    }
}
