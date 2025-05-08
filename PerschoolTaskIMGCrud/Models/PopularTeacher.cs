using PerschoolTaskIMGCrud.DAL;
using System.ComponentModel.DataAnnotations.Schema;

namespace PerschoolTaskIMGCrud.Models
{
    public class PopularTeacher
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Designation { get; set; }
        public string? ImgUrl { get; set; }
        [NotMapped] //altindaki database getmesin seye yaziriq bunu biz. Migrationa dusmur yeni bu 
        public IFormFile? File { get; set; }

    }
}
