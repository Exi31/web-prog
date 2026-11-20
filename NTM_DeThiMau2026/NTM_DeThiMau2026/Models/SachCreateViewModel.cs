using System.ComponentModel.DataAnnotations;

namespace NTM_DeThiMau2026.Models
{
    public class SachCreateViewModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn thể loại")]
        public int MaTl { get; set; }

        [Required(ErrorMessage = "Tên sách không được để trống")]
        [StringLength(100, ErrorMessage = "Tên sách tối đa 100 kí tự")]
        public string TenSach { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập giá sách")]
        [Range(typeof(decimal), "50", "1000", ErrorMessage = "Giá phải từ 50 đến 1000")]
        public decimal? Gia { get; set; }

        [RegularExpression(@"(?i)^.+\.(jpg|jpeg|png|webp)$", ErrorMessage = "Ảnh phải có đuôi .jpg, .jpeg, .png hoặc .webp")]
        public string? Anh { get; set; }
    }
}
