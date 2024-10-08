using api.Models;

public class Product
{
    public int Id { get; set; } // Unique identifier for the product
    public string Name { get; set; } // Product name
    public string Description { get; set; } // Description of the product
    public decimal Price { get; set; } // Price of the product
    public bool IsFeatured { get; set; } = false; // Flag for featured products
    public DateTime CreatedAt { get; set; } // Date when the product was added
    public DateTime UpdatedAt { get; set; } // Date of the last update

    // Foreign Keys
    public int BrandId { get; set; } // Foreign key for the brand
    public int SubCategoryId { get; set; } // Foreign key for the category
    public int GenderId { get; set; } // Foreign key for the gender


    // Navigation properties
    public Brand Brand { get; set; } // Navigation property for Brand
    public SubCategory SubCategory { get; set; } // Navigation property for Category
    public Gender Gender { get; set; } // Navigation property for Gender
    public ICollection<ProductMaterial> ProductMaterials { get; set; } // Many-to-many for materials
    public ICollection<ProductVariant> Variants { get; set; }  // Variants of the product

}

