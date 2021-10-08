using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintballStore.DATA.EF/*.PaintballMetaData*/
{
    public class ProductMetaData
    {
        [Required(ErrorMessage ="*Required")]
        [Display(Name ="Product Type ID")]
        public int ProductTypeId { get; set; }
        
        [Display(Name = "Product Type ID")]
        public int ManufacturerId { get; set; }

        [Required(ErrorMessage ="The name field is required")]
        [Display(Name ="Product Name")]
        [StringLength(50,ErrorMessage ="The name cannot contain more than 50 characters")]
        public string ProductName { get; set; }

        [Display(Name ="Image")]
        [Required(ErrorMessage ="A photo of the product is required")]
        public string ProductImage { get; set; }

        [DisplayFormat(NullDisplayText = "[-N/A-]", DataFormatString = "{0:c}")]
        [Range(0, double.MaxValue, ErrorMessage = "Value must be a valid number. 0 or larger")]
        [Required(ErrorMessage ="The price field is required")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "*Required")]
        [Display(Name = "Description")]
        [StringLength(maximumLength: 100, ErrorMessage = "Description must be less than 100 characters")]
        public string Description { get; set; }


    }
    [MetadataType(typeof(ProductMetaData))]
    public partial class Product
    {

    }

    public class ProductTypeMetaData
    {
        [Display(Name ="Product Type")]
        [Required(ErrorMessage ="You must declare the product type")]
        [StringLength(50,ErrorMessage ="The name cannot contain more than 50 characters")]
       public string ProducTypeName { get; set; }
    }

    [MetadataType(typeof(ProductTypeMetaData))]
    public partial class ProductType
    {

    }

    public class ManufacturerMetaData
    {
        [Display(Name = "Manufacturer Name")]
        [Required(ErrorMessage = "The name field is required")]
        [StringLength(50, ErrorMessage = "The name cannot contain more than 50 characters")]
        public string ManufacturerName { get; set; }

    }
    
    [MetadataType(typeof(ManufacturerMetaData))]
    public partial class Manufacturer
    {

    }
    
}
