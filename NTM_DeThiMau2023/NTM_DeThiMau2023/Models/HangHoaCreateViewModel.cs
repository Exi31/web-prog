using System.ComponentModel.DataAnnotations;

namespace NTM_DeThiMau2023.Models
{
    public class HangHoaCreateViewModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn loại hàng")]
        public int MaLoai { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên hàng")]
        [StringLength(50, ErrorMessage = "Tên hàng tối đa 50 kí tự")]
        public string TenHang { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập giá")]
        [Range(typeof(decimal), "100", "5000", ErrorMessage = "Giá phải từ 100 đến 5000")]
        public decimal? Gia { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên ảnh")]
        [RegularExpression(@"^.+\.(jpg|png|gif|tiff)$", ErrorMessage = "Ảnh phải có đuôi .jpg, .png, .gif, .tiff")]
        public string Anh { get; set; } = "";
    }
}
