using System.ComponentModel.DataAnnotations;

namespace NTM_QLTS_De2.Models
{
    public class TaiSanFormVM
    {
        public int MaTs { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Hãy chọn loại tài sản")]
        public int MaLoai { get; set; }

        [Required(ErrorMessage = "Chưa nhập tên tài sản")]
        public string TenTs { get; set; }

        [Required(ErrorMessage = "Chưa nhập nguyên giá")]
        [Range(typeof(decimal), "1", "2000", ErrorMessage = "Giá phải từ 1 đến 2000")]
        public decimal? NguyenGia { get; set; }

        [RegularExpression(@"^.+\.(jpg|jpeg|png|webp)$", ErrorMessage = "Ảnh phải có đuôi jpg, jpeg, webp")]
        public string? Anh { get; set; }
    }
}
