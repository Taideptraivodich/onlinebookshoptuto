using System.ComponentModel.DataAnnotations;

namespace OnlineBookShopTuto.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên sách")]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [StringLength(100)]
        public string Author { get; set; } = string.Empty;

        [Range(0, 100000000, ErrorMessage = "Giá không hợp lê")]
        public decimal Price {  get; set; }
        public int Stock {  get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }

        //Khóa ngoại: sách thuộc danh mục nào
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
