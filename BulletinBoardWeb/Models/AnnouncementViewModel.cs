using System.ComponentModel.DataAnnotations;

namespace BulletinBoardWeb.Models
{
    public class AnnouncementViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Заголовок обов'язковий")]
        [StringLength(100)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Опис обов'язковий")]
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        [Required]
        public bool Status { get; set; }
        [Required(ErrorMessage = "Оберіть категорію")]
        public string Category { get; set; }
        [Required(ErrorMessage = "Оберіть підкатегорію")]
        public string SubCategory { get; set; }
    }
}
